namespace Contracts.Requests;

public record SendMessageRequest(
    Guid UserId,
    string Message,
    bool? ExcludeCurrentUser);