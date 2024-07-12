using Application.Common.Result;
using Application.Models;
using MediatR;

namespace Application.Features.Chats.Commands.JoinChat;

public record JoinChatCommand(
    Guid UserId, 
    Guid ChatId,
    string ConnectionId) : IRequest<Result<MessageResult>>;