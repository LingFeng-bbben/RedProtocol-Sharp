# RedProtocol-Sharp
A C# lib for [RedProtocol](https://github.com/chrononeko/QQNTRedProtocol) (Chronocat)

# Quick Sample 
```C#
    using RedProtocol_Sharp;

    static RedProtocal red;
    static async Task Main(string[] args)
    {
        red = new RedProtocal(“127.0.0.1:16530”, "token here");
        await red.WsConnect();
        red.OnMessageRecv += Red_OnMessageRecv;
        while (true) ;
    }
    private static void Red_OnMessageRecv(object? sender, MsgList[] e)
    {
        foreach(var message in e)
        {
            foreach (var ele in message.Elements)
            {
                if (ele.TextElement != null)
                {
                    Console.WriteLine(message.PeerName + " :" + message.SendNickName + " :" + ele.TextElement.Content);
                }
            }
        }
    }
```
