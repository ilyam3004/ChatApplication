using Application.Common.Constants;
using Application.Common.Result;
using Application.Models;
using Data.Repositories;
using MediatR;

namespace Application.Features.Chats.Queries.GetChatUsers;

public class GetChatUsersQueryHandler 
    : IRequestHandler<GetChatUsersQuery, Result<List<UserResult>>>
{
    private readonly IChatRepository _chatRepository;
    
    public GetChatUsersQueryHandler(IChatRepository chatRepository)
    {
        _chatRepository = chatRepository;
    }

    public async Task<Result<List<UserResult>>> Handle(
        GetChatUsersQuery query, 
        CancellationToken cancellationToken)
    {
        var chat = await _chatRepository.GetChatWithUsers(query.ChatId);

        if (chat is null)
            return Errors.Chat.ChatNotFound;

        return chat.Users.Select(u => new UserResult(u)).ToList();
    }
}