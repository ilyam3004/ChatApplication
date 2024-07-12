using Application.Common.Constants;
using Application.Common.Result;
using Application.Models;
using Data.Repositories;
using Data.Entities;
using MediatR;

namespace Application.Features.Chats.Commands.LeaveChat;

public class LeaveChatCommandHandler
    : IRequestHandler<LeaveChatCommand, Result<LeaveChatResult>>
{
    private readonly IRepository<User> _repository;

    public LeaveChatCommandHandler(IRepository<User> repository)
    {
        _repository = repository;
    }

    public async Task<Result<LeaveChatResult>> Handle(LeaveChatCommand command,
        CancellationToken cancellationToken)
    {
        var user = await _repository.GetByIdAsync(command.UserId);

        if (user is null)
            return Errors.User.UserNotFound;

        LeaveChatResult result = new(
            (Guid)user.ChatId!, 
            user.UserId,
            user.Username);
        
        user.ChatId = null;

        await _repository.Update(user);

        return result;
    }
}