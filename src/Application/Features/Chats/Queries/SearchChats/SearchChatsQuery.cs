using Application.Common.Result;
using Application.Models;
using MediatR;

namespace Application.Features.Chats.Queries.SearchChat;

public record SearchChatsQuery(
    string SearchQuery) : IRequest<Result<List<ChatResult>>>;