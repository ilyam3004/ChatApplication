namespace Contracts.Requests;

public record JoinChatRequest(
    Guid UserId, 
    string ChatId);