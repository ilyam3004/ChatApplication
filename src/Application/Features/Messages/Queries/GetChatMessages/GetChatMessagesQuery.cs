using Application.Common.Result;
using Application.Models;
using MediatR;

namespace Application.Features.Messages.Queries.GetChatMessages;

public record GetChatMessagesQuery(
    Guid ChatId) : IRequest<Result<List<MessageResult>>>;