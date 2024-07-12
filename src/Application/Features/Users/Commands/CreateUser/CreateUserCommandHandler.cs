using Application.Common.Result;
using Application.Models;
using Data.Repositories;
using Data.Entities;
using MediatR;

namespace Application.Features.Users.Commands.CreateUser;

public class CreateUserCommandHandler 
    : IRequestHandler<CreateUserCommand, Result<UserResult>>
{
    private readonly IRepository<User> _userRepository;

    public CreateUserCommandHandler(IRepository<User> userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<UserResult>> Handle(CreateUserCommand command, 
        CancellationToken cancellationToken)
    {
        var user = new User
        {
            UserId = Guid.NewGuid(),
            Username = command.Username,
            ChatId = null
        };

        await _userRepository.AddAsync(user);

        return new UserResult(user);
    }
}