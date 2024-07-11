using Application.Common.Result;
using Application.Models.Users;
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
            ChatId = Guid.NewGuid(),
            ConnectionId = Guid.NewGuid().ToString(),
            Username = command.Username
        };

        _userRepository.AddAsync(user);

        return new UserResult(user);
    }
}