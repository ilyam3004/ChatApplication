using Application.Common.Result;
using Application.Models;
using MediatR;

namespace Application.Features.Users.Commands.CreateUser;

public record CreateUserCommand(string Username)
    : IRequest<Result<UserResult>>;