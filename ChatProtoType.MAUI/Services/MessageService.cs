using ChatProtoType.MAUI.Models;

namespace ChatProtoType.MAUI.Services
{
    public class MessageService
    {
        static MessageService _instance;

        public static MessageService Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new MessageService();

                return _instance;
            }
        }

        public readonly ChatUser user0 = new()
        {
            Name = "Tester Root",
            Image = "p1.jpg",
            Color = Color.FromArgb("#FFE0EC")
        };
        public readonly ChatUser user1 = new()
        {
            Name = "Alaya Cordova",
            Image = "p2.jpg",
            Color = Color.FromArgb("#FFE0EC")
        };
        public readonly ChatUser user2 = new()
        {
            Name = "Cecily Trujillo",
            Image = "p3.jpg",
            Color = Color.FromArgb("#BFE9F2")
        };
        public readonly ChatUser user3 = new()
        {
            Name = "Eathan Sheridan",
            Image = "p4.jpg",
            Color = Color.FromArgb("#FFD6C4")
        };
        public readonly ChatUser user4 = new()
        {
            Name = "Komal Orr",
            Image = "p5.jpg",
            Color = Color.FromArgb("#C3C1E6")
        };
        public readonly ChatUser user5 = new()
        {
            Name = "Sariba Abood",
            Image = "p6.jpg",
            Color = Color.FromArgb("#FFE0EC")
        };
        public readonly ChatUser user6 = new()
        {
            Name = "Justin O'Moore",
            Image = "p7.jpg",
            Color = Color.FromArgb("#FFE5A6")
        };
        public readonly ChatUser user7 = new()
        {
            Name = "Alissia Shah",
            Image = "p8.jpg",
            Color = Color.FromArgb("#FFE0EC")
        };
        public readonly ChatUser user8 = new()
        {
            Name = "Antoni Whitney",
            Image = "p9.jpg",
            Color = Color.FromArgb("#FFE0EC")
        };
        public readonly ChatUser user9 = new()
        {
            Name = "Jaime Zuniga",
            Image = "p10.jpg",
            Color = Color.FromArgb("#C3C1E6")
        };
        public readonly ChatUser user10 = new()
        {
            Name = "Barbara Cherry",
            Image = "p11.jpg",
            Color = Color.FromArgb("#FF95A2")
        };

        public List<ChatUser> GetUsers()
        {
            return new List<ChatUser>
            {
                user0, user1, user2, user3, user4, user5, user6, user7, user8, user9, user10
            };
        }

        public List<ChatMessage> GetMessages(ChatUser sender)
        {
            return new List<ChatMessage> {
                new ChatMessage
                {
                    Sender = user0,
                    Time = "18:42",
                    Message = "Hey there! What\'s up? Is everything ok?",
                    IsLocal = true,
                },
                new ChatMessage
                {
                    Sender = sender,
                    Time = "18:42",
                    Message = "Can I call you back later?, I\'m in a meeting.",
                    ReplyText = "It was bad the first try!!!!",
                    ReplyUser = user0
                },
                new ChatMessage
                {
                    Sender = sender,
                    Time = "18:39",
                    Message = "Yeah. Do you have any good song to recommend?",
                },
                new ChatMessage
                {
                    Sender = sender,
                    Time = "18:39",
                    Message = "Hi! I went shopping today and found a nice t-shirt.",
                },
                new ChatMessage
                {
                    Sender = user0,
                    Time = "18:36",
                    Message = "I passed you on the ride to work today, see you later.",
                    IsLocal = true,
                },
                new ChatMessage
                {
                    Sender= sender,
                    Time = "18:35",
                    Message= "Hey there! What\'s up?",
                },
            };
        }
    }
}