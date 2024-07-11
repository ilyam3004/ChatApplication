namespace Contracts.Requests;

public record JoinChatRequest(
    string UserId, 
    string ChatId);