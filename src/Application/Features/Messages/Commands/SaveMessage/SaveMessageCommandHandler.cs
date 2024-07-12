using Application.Common.Constants;
using Application.Common.Result;
using Application.Models;
using Data.Repositories;
using Data.Entities;
using MediatR;

namespace Application.Features.Messages.Commands.SaveMessage;

public class SaveMessageCommandHandler
    : IRequestHandler<SaveMessageCommand, Result<MessageResult>>
{
    private readonly IMessageRepository _messageRepository;

    public SaveMessageCommandHandler(IMessageRepository messageRepository)
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
            Text = command.Message,
        };

        await _messageRepository.AddAsync(message);

        var messageWithUser = await _messageRepository
            .GetMessageWithUser(message.MessageId);

        if (messageWithUser is null)
            return Errors.Message.MessageNotFound;
        
        return new MessageResult(messageWithUser);
    }
}