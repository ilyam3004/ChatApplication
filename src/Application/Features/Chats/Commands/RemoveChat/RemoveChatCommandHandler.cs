using Application.Common.Constants;
using Application.Common.Result;
using Data.Repositories;
using Data.Entities;
using MediatR;

namespace Application.Features.Chats.Commands.RemoveChat;

public class RemoveChatCommandHandler
    : IRequestHandler<RemoveChatCommand, Result<Deleted>>
{
    private readonly IRepository<Chat> _chatRepository;

    public RemoveChatCommandHandler(IRepository<Chat> chatRepository)
    {
        _chatRepository = chatRepository;
    }

    public async Task<Result<Deleted>> Handle(RemoveChatCommand command,
        CancellationToken cancellationToken)
    {
        var chat = await _chatRepository.GetByIdAsync(command.ChatId);

        if (chat is null)
            return Errors.Chat.ChatNotFound;

        if (chat.ChatOwnerId != chat.ChatOwnerId)
            return Errors.Chat.Unauthorized;

        _chatRepository.Remove(chat);

        return new Deleted(Messages.Chat.ChatDeleted);
    }
}