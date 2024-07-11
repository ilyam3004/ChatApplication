namespace Contracts.Requests;

public record SaveMessageRequest(
    Guid ChatId,
    Guid UserId,
    string Message);