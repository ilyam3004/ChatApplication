using Application.Common.Result;
using Application.Models.Chats;
using MediatR;

namespace Application.Features.Chats.Queries.GetAllChats;

public record GetAllChatsQuery() : IRequest<Result<List<ChatResult>>>;