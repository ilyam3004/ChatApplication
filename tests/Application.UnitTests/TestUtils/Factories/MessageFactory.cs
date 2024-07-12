using Application.UnitTests.TestUtils.TestConstants;
using Data.Entities;

namespace Application.UnitTests.TestUtils.Factories;

public static class MessageFactory
{
    public static Message CreateMessage(Guid? messageId = null,
        Guid? userId = null,
        Guid? chatId = null, 
        string? text = null,
        DateTime? dateTime = null)
        => new()
        {
            MessageId = messageId ?? Constants.Message.MessageId,
            UserId = userId ?? Constants.User.UserId,
            ChatId = chatId ?? Constants.Chat.ChatId,
            Text = text ?? Constants.Message.MessageText,
            Date = dateTime ?? Constants.Message.Date,
            User = UserFactory.CreateUser()
        };
}