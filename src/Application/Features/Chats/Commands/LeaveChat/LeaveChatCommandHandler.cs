using Application.Common.Constants;
using Application.Common.Result;
using Application.Models;
using Data.Repositories;
using Data.Entities;
using MediatR;
 namespace Application.Features.Chats.Commands.LeaveChat;

public class LeaveChatCommandHandler
    : IRequestHandler<LeaveChatCommand, Result<MessageResult>>
{
    private readonly IMessageRepository _messageRepository;
    private readonly IRepository<User> _repository;

    public LeaveChatCommandHandler(IRepository<User> repository, 
        IMessageRepository messageRepository)
    {
        _repository = repository;
        _messageRepository = messageRepository;
    }

    public async Task<Result<MessageResult>> Handle(LeaveChatCommand command,
        CancellationToken cancellationToken)
    {
        var user = await _repository.GetByIdAsync(command.UserId);

        if (user is null)
            return Errors.User.UserNotFound;

        if (user.ChatId is null)
            return Errors.User.UserNotJoinedChat;

        var message = await AddMessageAboutUserLeaving(user);
        
        user.ChatId = null;

        await _repository.Update(user);

        return new MessageResult(message);
    }

    private async Task<Message> AddMessageAboutUserLeaving(User user)
    {
        var message = new Message
        {
            MessageId = Guid.NewGuid(),
            ChatId = (Guid)user.ChatId!,
            UserId = user.UserId,
            Date = DateTime.UtcNow,
            Text = Common.Constants.Messages.Chat.UserLeavedTheChat(user.Username),
        };

        await _messageRepository.AddAsync(message);

        return message;
    }
}