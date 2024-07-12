using Application.Common.Result;
using Application.Models;
using Data.Entities;
using Data.Repositories;
using MediatR;

namespace Application.Features.Chats.Queries.GetAllChats;

public class GetAllChatsQueryHandler 
    : IRequestHandler<GetAllChatsQuery, Result<List<ChatResult>>>
{
    private readonly IRepository<Chat> _repository;

    public GetAllChatsQueryHandler(IRepository<Chat> repository)
    {
        _repository = repository;
    }

    public async Task<Result<List<ChatResult>>> Handle(
        GetAllChatsQuery request, CancellationToken cancellationToken)
    {
        var chats = await _repository.GetAll();

        return chats.Select(ch => new ChatResult(ch))
            .ToList();
    }
}