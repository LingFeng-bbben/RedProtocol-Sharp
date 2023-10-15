using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedProtocol_Sharp
{
    public partial class UserProfile
    {
        public string? Uid { get; set; }
        public string? Qid { get; set; }
        public string Uin { get; set; }
        public string Nick { get; set; }
        public string? Remark { get; set; }
        public string LongNick { get; set; }
        public Uri? AvatarUrl { get; set; }
        public long BirthdayYear { get; set; }
        public long BirthdayMonth { get; set; }
        public long BirthdayDay { get; set; }
        public long Sex { get; set; }
        public long? TopTime { get; set; }
        public bool? IsBlock { get; set; }
        public bool? IsMsgDisturb { get; set; }
        public bool? IsSpecialCareOpen { get; set; }
        public bool? IsSpecialCareZone { get; set; }
        public string? RingId { get; set; }
        public long? Status { get; set; }
        public long? CategoryId { get; set; }
        public bool? OnlyChat { get; set; }
        public bool? QzoneNotWatch { get; set; }
        public bool? QzoneNotWatched { get; set; }
    }

    public partial class GroupProfile
    {
        public string GroupCode { get; set; }
        public long MaxMember { get; set; }
        public long MemberCount { get; set; }
        public string GroupName { get; set; }
        public long GroupStatus { get; set; }
        public long MemberRole { get; set; }
        public bool? IsTop { get; set; }
        public long? ToppedTimestamp { get; set; }
        public long? PrivilegeFlag { get; set; }
        public bool? IsConf { get; set; }
        public bool? HasModifyConfGroupFace { get; set; }
        public bool? HasModifyConfGroupName { get; set; }
        public string? RemarkName { get; set; }
        public Uri? AvatarUrl { get; set; }
        public bool? HasMemo { get; set; }
        public long? GroupShutupExpireTime { get; set; }
        public long? PersonShutupExpireTime { get; set; }
        public long? DiscussToGroupUin { get; set; }
        public long? DiscussToGroupMaxMsgSeq { get; set; }
        public long? DiscussToGroupTime { get; set; }
    }

    public enum ChatType
    {
        PRIVATE = 1,
        GROUP = 2,
    }

    public partial class Peer
    {
        public ChatType ChatType { get; set; }
        public string? PeerUid { get; set; }
        public string? PeerUin { get; set; }
        public int? GuildId = null; // 一直为 Null

        public Peer(ChatType chatType, string? peerUid)
        {
            ChatType = chatType;
            PeerUid = peerUid;
            PeerUin = peerUid;
        }
    }

    public class MessageHistorySetting
    {
        public Peer Peer { get; set; }
        public int Count { get; set; }
        public string? OffsetMsgId { get; set; }
        public MessageHistorySetting(ChatType chatType, int count, string? peerUid, long? offsetMsgId = 0)
        {
            Peer = new Peer(chatType, peerUid);
            Count = count;
            OffsetMsgId = offsetMsgId.ToString();
        }
    }

    public partial class MessageReturn
    {
        public long Result { get; set; }
        public string ErrMsg { get; set; }
        public MsgList[]? MsgList { get; set; }
    }

    public partial class MsgList
    {
        public string MsgId { get; set; }
        public string MsgRandom { get; set; }
        public long MsgSeq { get; set; }
        public long CntSeq { get; set; }
        public long ChatType { get; set; }
        public long MsgType { get; set; }
        public long SubMsgType { get; set; }
        public long SendType { get; set; }
        public string PeerUid { get; set; }
        public string ChannelId { get; set; }
        public string GuildId { get; set; }
        public long GuildCode { get; set; }
        public long FromUid { get; set; }
        public long FromAppid { get; set; }
        public long MsgTime { get; set; }
        public string MsgMeta { get; set; }
        public long SendStatus { get; set; }
        public string SendMemberName { get; set; }
        public string SendNickName { get; set; }
        public string GuildName { get; set; }
        public string ChannelName { get; set; }
        public MsgListElement[] Elements { get; set; }
        public Record[] Records { get; set; }
        public object[] EmojiLikesList { get; set; }
        public long CommentCnt { get; set; }
        public long DirectMsgFlag { get; set; }
        public object[] DirectMsgMembers { get; set; }
        public string PeerName { get; set; }
        public bool Editable { get; set; }
        public string AvatarMeta { get; set; }
        public long RoleId { get; set; }
        public long TimeStamp { get; set; }
        public bool IsImportMsg { get; set; }
        public long AtType { get; set; }
        public long RoleType { get; set; }
        public RoleInfo FromChannelRoleInfo { get; set; }
        public RoleInfo FromGuildRoleInfo { get; set; }
        public RoleInfo LevelRoleInfo { get; set; }
        public long RecallTime { get; set; }
        public bool IsOnlineMsg { get; set; }
        public string GeneralFlags { get; set; }
        public long ClientSeq { get; set; }
        public string SenderUin { get; set; }
        public string PeerUin { get; set; }
    }

    public enum ElementType
    {
        TextElement = 1,
        PicElement = 2,
        PttElement = 4,
        FaceElement = 6,
        ReplyElement = 7,
        GrayTipElement = 8,
    }

    public partial class MsgListElement
    {
        public ElementType ElementType { get; set; }
        public string ElementId { get; set; }
        public string ExtBufForUi { get; set; }
        public PicElement? PicElement { get; set; }
        public ReplyElement? ReplyElement { get; set; }
        public TextElement? TextElement { get; set; }
        public GrayTipElement? GrayTipElement { get; set; }
        public FaceElement? FaceElement { get; set; }
        public PttElement? PttElement { get; set; }
    }

    public partial class FaceElement
    {
        public long FaceIndex { get; set; }
        public long FaceType { get; set; }
        public string FaceText { get; set; }
    }

    //撤回之后的消息会变成这个
    public partial class GrayTipElement
    {
        public long SubElementType { get; set; }
        public RevokeElement RevokeElement { get; set; }
    }

    public partial class RevokeElement
    {
        public long OperatorTinyId { get; set; }
        public long OperatorRole { get; set; }
        public string OperatorNick { get; set; }
        public string OperatorMemRemark { get; set; }
        public bool IsSelfOperate { get; set; }
        public string OperatorUin { get; set; }
        public string OrigMsgSenderUin { get; set; }
    }

    public partial class PicElement
    {
        public long PicSubType { get; set; }
        public string FileName { get; set; }
        public long FileSize { get; set; }
        public long PicWidth { get; set; }
        public long PicHeight { get; set; }
        public bool Original { get; set; }
        public string Md5HexStr { get; set; }
        public string SourcePath { get; set; }
        public long TransferStatus { get; set; }
        public long Progress { get; set; }
        public long PicType { get; set; }
        public long InvalidState { get; set; }
        public string FileUuid { get; set; }
        public string FileSubId { get; set; }
        public long ThumbFileSize { get; set; }
        public string Summary { get; set; }
    }

    public partial class PttElement
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string Md5HexStr { get; set; }
        public long FileSize { get; set; }
        public long Duration { get; set; }
        public long FormatType { get; set; }
        public long VoiceType { get; set; }
        public long VoiceChangeType { get; set; }
        public bool CanConvert2Text { get; set; }
        public long FileId { get; set; }
        public string FileUuid { get; set; }
        public string Text { get; set; }
        public long TranslateStatus { get; set; }
        public long TransferStatus { get; set; }
        public long Progress { get; set; }
        public long PlayState { get; set; }
        public long[] WaveAmplitudes { get; set; }
        public long InvalidState { get; set; }
        public long FileSubId { get; set; }
    }

    public partial class ReplyElement
    {
        public long ReplayMsgId { get; set; }
        public long ReplayMsgSeq { get; set; }
        public long ReplayMsgRootSeq { get; set; }
        public long ReplayMsgRootMsgId { get; set; }
        public long ReplayMsgRootCommentCnt { get; set; }
        public string SourceMsgIdInRecords { get; set; }
        public string SourceMsgText { get; set; }
        public SourceMsgTextElem[] SourceMsgTextElems { get; set; }
        public long SenderUid { get; set; }
        public long ReplyMsgClientSeq { get; set; }
        public long ReplyMsgTime { get; set; }
        public long ReplyMsgRevokeType { get; set; }
        public bool SourceMsgIsIncPic { get; set; }
        public bool SourceMsgExpired { get; set; }
        public string SenderUin { get; set; }
        public long SenderUinStr { get; set; }
    }

    public partial class SourceMsgTextElem
    {
        public long ReplyAbsElemType { get; set; }
        public string TextElemContent { get; set; }
    }

    public partial class TextElement
    {
        public string Content { get; set; }
        public long AtType { get; set; }
        public long AtUid { get; set; }
        public long AtTinyId { get; set; }
        public string AtNtUid { get; set; }
        public long SubElementType { get; set; }
        public long AtChannelId { get; set; }
        public string AtNtUin { get; set; }
        public string AtUin { get; set; }
    }

    public partial class RoleInfo
    {
        public long RoleId { get; set; }
        public string Name { get; set; }
        public long Color { get; set; }
    }

    public partial class Record
    {
        public string MsgId { get; set; }
        public string MsgRandom { get; set; }
        public long MsgSeq { get; set; }
        public long CntSeq { get; set; }
        public long ChatType { get; set; }
        public long MsgType { get; set; }
        public long SubMsgType { get; set; }
        public long SendType { get; set; }
        public long PeerUid { get; set; }
        public string ChannelId { get; set; }
        public string GuildId { get; set; }
        public long GuildCode { get; set; }
        public long FromUid { get; set; }
        public long FromAppid { get; set; }
        public long MsgTime { get; set; }
        public string MsgMeta { get; set; }
        public long SendStatus { get; set; }
        public string SendMemberName { get; set; }
        public string SendNickName { get; set; }
        public string GuildName { get; set; }
        public string ChannelName { get; set; }
        public RecordElement[] Elements { get; set; }
        public object[] Records { get; set; }
        public object[] EmojiLikesList { get; set; }
        public long CommentCnt { get; set; }
        public long DirectMsgFlag { get; set; }
        public object[] DirectMsgMembers { get; set; }
        public string PeerName { get; set; }
        public bool Editable { get; set; }
        public string AvatarMeta { get; set; }
        public long RoleId { get; set; }
        public long TimeStamp { get; set; }
        public bool IsImportMsg { get; set; }
        public long AtType { get; set; }
        public long RoleType { get; set; }
        public RoleInfo FromChannelRoleInfo { get; set; }
        public RoleInfo FromGuildRoleInfo { get; set; }
        public RoleInfo LevelRoleInfo { get; set; }
        public long RecallTime { get; set; }
        public bool IsOnlineMsg { get; set; }
        public string GeneralFlags { get; set; }
        public long ClientSeq { get; set; }
        public long SenderUin { get; set; }
        public long PeerUin { get; set; }
    }

    public partial class RecordElement
    {
        public long ElementType { get; set; }
        public string ElementId { get; set; }
        public string ExtBufForUi { get; set; }
        public TextElement TextElement { get; set; }
    }


    public partial class OpenWSConfig
    {
        public string type { get; set; }
        public Payload payload { get; set; }
        public OpenWSConfig(string _type, string _token)
        {
            type = _type;
            payload = new Payload() { token = _token };
        }
    }

    public partial class Payload
    {
        public string token { get; set; }
    }

    public partial class WSMessage
    {
        public string? type { get; set; }
        public object? payload { get; set; }
    }

    public partial class ServerInfo
    {
        public string Version { get; set; }
        public string Name { get; set; }
        public AuthData AuthData { get; set; }
    }

    public partial class AuthData
    {
        public string Account { get; set; }
        public string MainAccount { get; set; }
        public string Uin { get; set; }
        public string Uid { get; set; }
        public string NickName { get; set; }
        public long Gender { get; set; }
        public long Age { get; set; }
        public Uri FaceUrl { get; set; }
        public string A2 { get; set; }
        public string D2 { get; set; }
        public string D2Key { get; set; }
    }

    public partial class MessageMessage
    {
        public string type { get; set; }
        public MessagePayload payload { get; set; }
        public MessageMessage(Peer _peer, List<MsgListElement> _msgListElements)
        {
            type = "message::send";
            payload = new MessagePayload() { peer = _peer, elements = _msgListElements };
        }
    }

    public partial class MessagePayload
    {
        public Peer peer { get; set; }
        public List<MsgListElement> elements { get; set; } = new List<MsgListElement> { };
    }

    public partial class ImageUpResponse
    {
        public string Md5 { get; set; }
        //public ImageInfo ImageInfo { get; set; }
        public long FileSize { get; set; }
        public string FilePath { get; set; }
        public string NtFilePath { get; set; }
    }
}


