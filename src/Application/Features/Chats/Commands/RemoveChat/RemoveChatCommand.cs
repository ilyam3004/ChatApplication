using Application.Common.Result;
using Mapster;
using MediatR;

namespace Application.Features.Chats.Commands.RemoveChat;

public record RemoveChatCommand(
    Guid ChatId,
    Guid UserId) : IRequest<Result<Deleted>>;