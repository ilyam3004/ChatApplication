namespace Contracts.Requests;

public record SendMessageRequest(
    Guid ChatId,
    Guid UserId,
    string Message,
    bool? ExcludeCurrentUser);