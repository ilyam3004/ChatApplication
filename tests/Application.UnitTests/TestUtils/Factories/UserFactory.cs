using Application.UnitTests.TestUtils.TestConstants;
using Data.Entities;

namespace Application.UnitTests.TestUtils.Factories;

public static class UserFactory
{
    public static User CreateUser(Guid? userId = null,
        string? username = null,
        string? connectionId = null,
        Guid? chatId = null)
        => new()
        {
            UserId = userId ?? Constants.User.UserId,
            Username = username ?? Constants.User.Username,
            ConnectionId = connectionId,
            ChatId = chatId
        };
}