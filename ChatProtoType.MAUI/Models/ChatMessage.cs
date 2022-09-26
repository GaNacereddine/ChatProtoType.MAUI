namespace ChatProtoType.MAUI.Models
{
    public class ChatMessage
    {
        public ChatUser Sender { get; set; }
        public string Message { get; set; }
        public string Time { get; set; }
        public string ReplyText { get; set; }
        public ChatUser ReplyUser { get; set; }
        public bool IsLocal { get; set; }
    }
}