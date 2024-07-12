using Api.IntegrationTests.Utils.Constants;
using Contracts.Requests;

namespace Api.IntegrationTests.Controllers.Utils;

public static class CreateChatRequestUtils
{
    public static CreateChatRequest CreateChatRequest()
        => new(Constants.Chat.ChatName,
            Constants.User.UserId);
}