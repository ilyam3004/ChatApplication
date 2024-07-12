using Application.Common.Result;
using MediatR;

namespace Application.Features.Chats.Commands.LeaveChat;

public record LeaveChatCommand(
    Guid UserId) : IRequest<Deleted>;