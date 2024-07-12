using Application.UnitTests.TestUtils.TestConstants;
using Application.Features.Chats.Commands.JoinChat;

namespace Application.Features.Chats.Commands.TestUtils;

public static class JoinChatCommandUtils
{
    public static JoinChatCommand CreateJoinChatCommand()
        => new(Constants.User.UserId,
            Constants.Chat.ChatId,
            Constants.User.ConnectionId);
}