using Application.Common.Result;
using Application.Models;
using MediatR;

namespace Application.Features.Chats.Queries.GetChatUsers;

public record GetChatUsersQuery(
    Guid ChatId) : IRequest<Result<List<UserResult>>>;
