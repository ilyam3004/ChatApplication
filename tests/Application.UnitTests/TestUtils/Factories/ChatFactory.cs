using Application.UnitTests.TestUtils.TestConstants;
using Data.Entities;

namespace Application.UnitTests.TestUtils.Factories;

public static class ChatFactory
{
    public static Chat CreateChat(Guid? chatId = null,
        Guid? chatOwnerId = null,
        string? chatName = null)
        => new()
        {
            ChatId = chatId ?? Constants.Chat.ChatId,
            ChatOwnerId = chatOwnerId ?? Constants.User.UserId,
            ChatName = chatName ?? Constants.Chat.ChatName
        };
}