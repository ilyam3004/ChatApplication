using Application.Common.Result;
using Application.Models;
using Mapster;
using MediatR;

namespace Application.Features.Chats.Commands.RemoveChat;

public record RemoveChatCommand(
    Guid ChatId,
    Guid UserId) : IRequest<Result<List<UserResult>>>;