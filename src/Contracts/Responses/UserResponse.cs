namespace Contracts.Responses;

public record UserResponse(
    Guid UserId,
    string Username);