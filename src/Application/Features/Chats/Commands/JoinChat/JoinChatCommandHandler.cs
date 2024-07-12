using Application.Common.Constants;
using Application.Common.Result;
using Application.Models;
using Data.Repositories;
using Data.Entities;
using MediatR;

namespace Application.Features.Chats.Commands.JoinChat;
public class JoinChatCommandHandler 
    : IRequestHandler<JoinChatCommand, Result<MessageResult>>
{
    private readonly IMessageRepository _messageRepository;
    private readonly IChatRepository _chatRepository;
    private readonly IRepository<User> _userRepository;

    public JoinChatCommandHandler(IChatRepository chatRepository,
        IRepository<User> userRepository, 
        IMessageRepository messageRepository)
    {
        _chatRepository = chatRepository;
        _userRepository = userRepository;
        _messageRepository = messageRepository;
    }

    public async Task<Result<MessageResult>> Handle(JoinChatCommand command, 
        CancellationToken cancellationToken)
    {
        var chat = await _chatRepository.GetByIdAsync(command.ChatId);

        if (chat is null)
            return Errors.Chat.ChatNotFound;

        var user = await _userRepository.GetByIdAsync(command.UserId);

        if (user is null)
            return Errors.User.UserNotFound;

        user.ChatId = chat.ChatId;
        user.ConnectionId = command.ConnectionId;
        
        await _userRepository.Update(user);

        var message = await AddMessageAboutUserJoining(user);

        if (message is null)
            return Errors.Message.MessageNotFound;

        return new MessageResult(message);
    }
    
    
    private async Task<Message?> AddMessageAboutUserJoining(User user)
    {
        var message = new Message
        {
            MessageId = Guid.NewGuid(),
            ChatId = (Guid)user.ChatId!,
            UserId = user.UserId,
            Date = DateTime.UtcNow,
            Text = Common.Constants.Messages.Chat.UserJoinedTheChat(user.Username),
        };

        await _messageRepository.AddAsync(message);
        
        var messageWithUser = await _messageRepository.GetMessageWithUser(message.MessageId);

        return messageWithUser;
    }
}