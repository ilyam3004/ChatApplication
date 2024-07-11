namespace Contracts.Requests;

public record CreateChatRequest(
    string ChatName,
    Guid UserId);