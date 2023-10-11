﻿using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace RedProtocol_Sharp
{
    public class RedProtocal
    {
        string apiRoot = "";
        string Url;
        HttpClient Client = new HttpClient();
        ClientWebSocket ws = new ClientWebSocket();
        public event EventHandler<MsgList[]> OnMessageRecv;
        string accessToken = "";

        public RedProtocal(string url, string token)
        {
            Url = url;
            accessToken = "Bearer " + token;
            apiRoot = "http://"+ Url + "/api/";
            Client.BaseAddress = new Uri(apiRoot);
        }
        public async Task WsConnect()
        {
            Console.WriteLine("Connecting to ws");
            var uri = new Uri("ws://"+ Url);
            await ws.ConnectAsync(uri, CancellationToken.None);
            while (ws.State != WebSocketState.Open);
            Thread messageloop = new Thread(MessageLoop);
            messageloop.Start();
        }
        private async void MessageLoop()
        {
            string open = JsonConvert.SerializeObject(new OpenWSConfig("meta::connect", "5a664a08562f69b301c7adade704921c4dcb32155b4be752c74bb58b757709ad"));
            var buf = Encoding.UTF8.GetBytes(open);
            await ws.SendAsync(buf, WebSocketMessageType.Text, true, CancellationToken.None);
            var buffer = new byte[1024 * 4];
            WebSocketReceiveResult result = await ws.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            List<byte> bs = new List<byte>();
            while (!result.CloseStatus.HasValue)
            {
                //文本消息
                if (result.MessageType == WebSocketMessageType.Text)
                {
                    bs.AddRange(buffer.Take(result.Count));

                    //消息是否已接收完全
                    if (result.EndOfMessage)
                    {
                        //发送过来的消息
                        string userMsg = Encoding.UTF8.GetString(bs.ToArray(), 0, bs.Count);
                        //Console.WriteLine(userMsg);
                        ProcessWSMessage(userMsg);
                        //OnMessageRecv?.Invoke(this, userMsg);
                        //清空消息容器
                        bs = new List<byte>();
                    }
                }
                //继续监听Socket信息
                result = await ws.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            }
            await ws.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
        }

        void ProcessWSMessage(string message)
        {
            var obj = JsonConvert.DeserializeObject<WSMessage>(message);
            if (obj == null) return;
            if (obj.type == "meta::connect")
            {
                var payload = JsonConvert.DeserializeObject<ServerInfo>(obj.payload.ToString());
                Console.WriteLine("Connected to " + payload.Name + ", Vision " + payload.Version);
                Console.WriteLine("我他妈是" + payload.AuthData.NickName + "啊，你不认识我了？老乡？");
            }
            if(obj.type == "message::recv")
            {
                var messages = JsonConvert.DeserializeObject<MsgList[]>(obj.payload.ToString());
                if (messages == null) return;
                OnMessageRecv?.Invoke(this,messages);
            }
        }

        private async Task<string> GetText(string method, string? context = null)
        {
            var req = new HttpRequestMessage(HttpMethod.Get, apiRoot + method);
            req.Headers.Add("Authorization", accessToken);
            if (context != null) req.Content = new StringContent(context);
            var resp = await Client.SendAsync(req);
            var text = await resp.Content.ReadAsStringAsync();
            return text;
        }

        private async Task<T> GetObject<T>(string method)
        {
            var txt = await GetText(method);
            var obj = JsonConvert.DeserializeObject<T>(txt);
            return obj;
        }

        private async Task<T> GetObject<T>(string method,object context)
        {
            var resover = new JsonSerializerSettings();
            resover.ContractResolver = new CamelCasePropertyNamesContractResolver();
            var outstr = JsonConvert.SerializeObject(context,resover);
            var txt = await GetText(method,outstr);

            var obj = JsonConvert.DeserializeObject<T>(txt);
            return obj;
        }

        public async Task<UserProfile> GetUserProfileAsync()
        {
            var obj = await GetObject<UserProfile>("getSelfProfile");
            return obj;
        }

        public async Task<UserProfile[]> GetFriendListAsync()
        {
            var obj = await GetObject<UserProfile[]>("/bot/friends");
            return obj;
        }

        public async Task<GroupProfile[]> GetGroupListAsync()
        {
            var obj = await GetObject<GroupProfile[]>("/bot/groups");
            return obj;
        }

        public async Task<MessageReturn> GetMessages(MessageHistorySetting historySetting)
        {
            var obj = await GetObject<MessageReturn>("/message/getHistory", historySetting);
            return obj;
        }
    }
}
