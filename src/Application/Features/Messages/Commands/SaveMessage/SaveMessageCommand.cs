using Application.Common.Result;
using Application.Models;
using MediatR;

namespace Application.Features.Messages.Commands.SaveMessage;

public record SaveMessageCommand(
    Guid ChatId, 
    Guid UserId,
    string Message) : IRequest<Result<MessageResult>>;