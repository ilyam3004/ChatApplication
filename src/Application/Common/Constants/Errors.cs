namespace Application.Common.Constants;

public static class Errors
{
    public static class Chat
    {
         public static Error Unauthorized => Error.Unauthorized("Chat.Unauthorized",
             description: "You have no permission to delete this chat because you are not an owner.");
         
         public static Error ChatNotFound => Error.NotFound("Chat.ChatNotFound",
             description: "Chat not found.");
         
         public static Error ChatsBySearchQueryNotFound => Error.NotFound("Chats.ChatsBySearchQueryNotFound", 
             description: "Chats by specified search query not found.");
    }
    
    public static class User
    {
         public static Error UserNotFound => Error.NotFound("User.UserNotFound",
             description: "User not found.");
    }
}