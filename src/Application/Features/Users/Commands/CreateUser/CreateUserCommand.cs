using Application.Common.Result;
using Application.Models.Users;
using MediatR;

namespace Application.Features.Users.Commands.CreateUser;

public record CreateUserCommand(string Username)
    : IRequest<Result<UserResult>>;