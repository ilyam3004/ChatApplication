namespace Contracts.Responses;

public record ChatResponse(
    Guid ChatId,
    Guid ChatOwnerId,
    string ChatName);