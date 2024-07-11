using Application.Common.Result;
using Application.Models.Chats;
using MediatR;

namespace Application.Features.Chats.Commands.CreateChat;

public record CreateChatCommand(
    string ChatName) : IRequest<Result<ChatResult>>;