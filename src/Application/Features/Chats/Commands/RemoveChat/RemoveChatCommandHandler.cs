using Application.Common.Constants;
using Application.Common.Result;
using Application.Models;
using Data.Repositories;
using MediatR;

namespace Application.Features.Chats.Commands.RemoveChat;

public class RemoveChatCommandHandler
    : IRequestHandler<RemoveChatCommand, Result<List<UserResult>>>
{
    private readonly IChatRepository _chatRepository;

    public RemoveChatCommandHandler(IChatRepository chatRepository)
    {
        _chatRepository = chatRepository;
    }

    public async Task<Result<List<UserResult>>> Handle(RemoveChatCommand command,
        CancellationToken cancellationToken)
    {
        var chat = await _chatRepository.GetChatWithUsers(command.ChatId);

        if (chat is null)
            return Errors.Chat.ChatNotFound;

        if (chat.ChatOwnerId != command.UserId)
            return Errors.Chat.Unauthorized;

        await _chatRepository.Remove(chat);

        return chat.Users.Select(u => new UserResult(u)).ToList();
    }
}