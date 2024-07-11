using Application.Common.Constants;
using Application.Common.Result;
using Application.Models;
using Data.Repositories;
using MediatR;

namespace Application.Features.Chats.Queries.SearchChat;

public class SearchChatQueryHandler 
    : IRequestHandler<SearchChatsQuery, Result<List<ChatResult>>>
{
    private readonly IChatRepository _chatRepository;

    public SearchChatQueryHandler(IChatRepository chatRepository)
    {
        _chatRepository = chatRepository;
    }

    public async Task<Result<List<ChatResult>>> Handle(SearchChatsQuery query, 
        CancellationToken cancellationToken)
    {
        var chats = await _chatRepository.SearchChats(query.SearchQuery);

        if (chats.Count == 0)
            return Errors.Chat.ChatsBySearchQueryNotFound;
            
        return chats.Select(c => new ChatResult(c)).ToList();
    }
}