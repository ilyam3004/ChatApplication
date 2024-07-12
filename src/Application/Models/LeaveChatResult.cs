namespace Application.Models;

public record LeaveChatResult(
    Guid ChatId,
    Guid UserId,
    string Username);