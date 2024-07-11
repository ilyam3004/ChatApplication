using Application.Common.Result;
using Application.Models;
using MediatR;

namespace Application.Features.Chats.Commands.CreateChat;

public record CreateChatCommand(
    string ChatName,
    Guid UserId) : IRequest<Result<ChatResult>>;