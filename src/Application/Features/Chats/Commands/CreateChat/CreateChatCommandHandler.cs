using Application.Common.Result;
using Application.Models.Chats;
using Data.Entities;
using Data.Repositories;
using MediatR;

namespace Application.Features.Chats.Commands.CreateChat;

public class CreateChatCommandHandler 
    : IRequestHandler<CreateChatCommand, Result<ChatResult>>
{
    private readonly IRepository<Chat> _chatRepository;

    public CreateChatCommandHandler(IRepository<Chat> chatRepository)
    {
        _chatRepository = chatRepository;
    }

    public async Task<Result<ChatResult>> Handle(CreateChatCommand command, 
        CancellationToken cancellationToken)
    {
        Chat chat = new()
        {
            ChatId = Guid.NewGuid(),
            ChatName = command.ChatName
        };

        await _chatRepository.AddAsync(chat);

        return new ChatResult(chat);
    }
}