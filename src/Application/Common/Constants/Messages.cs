namespace Application.Common.Constants;

public static class Messages
{
    public static class Chat
    {
        public const string ChatDeleted = "Chat deleted succesfully";

        public static string UserHasJoinTheChat(string username)
            => $"User {username} has joined the chat.";
    }
}