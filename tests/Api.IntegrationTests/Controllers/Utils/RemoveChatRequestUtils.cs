using Api.IntegrationTests.Utils.Constants;
using Contracts.Requests;

namespace Api.IntegrationTests.Controllers.Utils;

public static class RemoveChatRequestUtils
{
    public static RemoveChatRequest CreateRemoveChatRequest()
        => new(Constants.Chat.ChatId,
            Constants.User.UserId);
}