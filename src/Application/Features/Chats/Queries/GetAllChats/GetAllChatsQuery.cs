using Application.Common.Result;
using Application.Models;
using MediatR;

namespace Application.Features.Chats.Queries.GetAllChats;

public record GetAllChatsQuery() : IRequest<Result<List<ChatResult>>>;