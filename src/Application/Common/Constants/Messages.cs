namespace Application.Common.Constants;

public static class Messages
{
    public static class Chat
    {
        public static string ChatDeleted(Guid chatId) 
            => $"Chat {chatId} deleted succesfully";
        
        public static string DisconnectedFromChat(Guid chatId) 
            => $"You have been disconnected from the chat {chatId} because the root deleted it";
                
        public const string LeavedChat = "You succesfully leaved the chat";

        public static string UserJoinedTheChat(string username)
            => $"{username} joined the chat.";
        
        public static string UserLeavedTheChat(string username)
            => $"{username} leaved the chat.";
    }
}