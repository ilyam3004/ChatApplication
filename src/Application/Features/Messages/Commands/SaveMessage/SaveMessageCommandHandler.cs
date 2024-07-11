using Application.Common.Result;
using Application.Models;
using Data.Entities;
using Data.Repositories;
using MediatR;

namespace Application.Features.Messages.Commands.SaveMessage;

public class SaveMessageCommandHandler
    : IRequestHandler<SaveMessageCommand, Result<MessageResult>>
{
    private readonly IRepository<Message> _messageRepository;

    public SaveMessageCommandHandler(IRepository<Message> messageRepository)
    {
        _messageRepository = messageRepository;
    }

    public async Task<Result<MessageResult>> Handle(SaveMessageCommand command,
        CancellationToken cancellationToken)
    {
        var message = new Message
        {
            MessageId = Guid.NewGuid(),
            ChatId = command.ChatId,
            UserId = command.UserId,
            Date = DateTime.UtcNow,
        };

        await _messageRepository.AddAsync(message);

        return new MessageResult(message);
    }
}