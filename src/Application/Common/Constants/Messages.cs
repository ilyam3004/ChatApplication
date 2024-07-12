namespace Application.Common.Constants;

public static class Messages
{
    public static class Chat
    {
        public const string ChatDeleted = "Chat deleted succesfully";
        public const string LeavedChat = "You succesfully leaved the chat";

        public static string UserJoinedTheChat(string username)
            => $"{username} joined the chat.";
        
        public static string UserLeavedTheChat(string username)
            => $"{username} leaved the chat.";
    }
}