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
    private readonly IRepository<User> _userRepository;

    public SaveMessageCommandHandler(IMessageRepository messageRepository,
        IRepository<User> userRepository)
    {
        _messageRepository = messageRepository;
        _userRepository = userRepository;
    }

    public async Task<Result<MessageResult>> Handle(SaveMessageCommand command,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(command.UserId);

        if (user is null)
            return Errors.User.UserNotFound;

        if (user.ChatId is null)
            return Errors.User.UserNotJoinedChat;

        var message = new Message
        {
            MessageId = Guid.NewGuid(),
            ChatId = (Guid)user.ChatId,
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