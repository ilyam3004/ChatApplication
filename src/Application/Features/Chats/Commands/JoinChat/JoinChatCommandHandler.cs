using Application.Common.Constants;
using Application.Common.Result;
using Application.Models;
using Data.Entities;
using Data.Repositories;
using MediatR;

namespace Application.Features.Chats.Commands.JoinChat;

public class JoinChatCommandHandler 
    : IRequestHandler<JoinChatCommand, Result<UserResult>>
{
    private readonly IRepository<Chat> _chatRepository;
    private readonly IRepository<User> _userRepository;

    public JoinChatCommandHandler(IRepository<Chat> chatRepository, 
        IRepository<User> userRepository)
    {
        _chatRepository = chatRepository;
        _userRepository = userRepository;
    }

    public async Task<Result<UserResult>> Handle(JoinChatCommand command, 
        CancellationToken cancellationToken)
    {
        var chat = await _chatRepository.GetByIdAsync(command.ChatId);

        if (chat is null)
            return Errors.Chat.ChatNotFound;

        var user = await _userRepository.GetByIdAsync(command.UserId);

        if (user is null)
            return Errors.User.UserNotFound;

        return new UserResult(user);
    }
}