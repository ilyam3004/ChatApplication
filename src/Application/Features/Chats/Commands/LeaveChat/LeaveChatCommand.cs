using Application.Common.Result;
using Application.Models;
using MediatR;

namespace Application.Features.Chats.Commands.LeaveChat;

public record LeaveChatCommand(
    Guid UserId) : IRequest<Result<MessageResult>>;