using Application.Models;
using Application.UnitTests.TestUtils.TestConstants;
using FluentAssertions;

namespace Application.UnitTests.TestUtils.Extensions;

public static class MessagesExtensions
{ 
    public static void ValidateRetrievedMessage(this MessageResult result)
    {
        result.Message.UserId.Should().Be(Constants.User.UserId);
        result.Message.MessageId.Should().Be(Constants.Message.MessageId);
        result.Message.ChatId.Should().Be(Constants.Chat.ChatId);
        result.Message.Text.Should().Be(Constants.Message.MessageText);
        
        result.Message.User.Should().NotBeNull();
        result.Message.Chat.Should().BeNull();
    }
}