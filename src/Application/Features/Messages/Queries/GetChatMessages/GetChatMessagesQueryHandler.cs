using Application.Common.Result;
using Application.Models;
using Data.Repositories;
using MediatR;

namespace Application.Features.Messages.Queries.GetChatMessages;

public class GetChatMessagesQueryHandler 
: IRequestHandler<GetChatMessagesQuery, Result<List<MessageResult>>>
{
    private readonly IMessageRepository _messageRepository;

    public GetChatMessagesQueryHandler(IMessageRepository messageRepository)
    {
        _messageRepository = messageRepository;
    }

    public async Task<Result<List<MessageResult>>> Handle(GetChatMessagesQuery query, 
        CancellationToken cancellationToken)
    {
        var chatMessages = await _messageRepository.GetChatMessages(query.ChatId);
        
        return chatMessages.Select(m => new MessageResult(m)).ToList();
    }
}