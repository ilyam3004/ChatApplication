namespace Contracts.Requests;

public record RemoveChatRequest(
    Guid ChatId,
    Guid UserId);