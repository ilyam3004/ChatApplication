using Application.Features.Chats.Commands.CreateChat;
using Application.Features.Chats.Commands.RemoveChat;
using Application.Features.Chats.Queries.GetAllChats;
using Application.Features.Chats.Queries.SearchChat;
using Microsoft.AspNetCore.Mvc;
using Contracts.Responses;
using Contracts.Requests;
using MapsterMapper;
using MediatR;

namespace Api.Controllers;

[Route("chats")]
public class ChatController : ApiController
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public ChatController(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllChats()
    {
        var query = new GetAllChatsQuery();

        var result = await _sender.Send(query);

        return result.Match(
            value => Ok(_mapper.Map<List<ChatResponse>>(value)), 
            Problem);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateChat(CreateChatRequest request)
    {
        var query = _mapper.Map<CreateChatCommand>(request);

        var result = await _sender.Send(query);

        return result.Match(
            value => Ok(_mapper.Map<ChatResponse>(value)), 
            Problem);
    }

    [HttpDelete]
    public async Task<IActionResult> RemoveChat(RemoveChatRequest request)
    {
        var query = _mapper.Map<RemoveChatCommand>(request);
    
        var result = await _sender.Send(query);
    
        return result.Match(Ok, Problem);
    }
    
    [HttpGet("{searchQuery}")]
    public async Task<IActionResult> SearchChats(string searchQuery)
    {
        var query = new SearchChatsQuery(searchQuery);

        var result = await _sender.Send(query);

        return result.Match(
            value => Ok(_mapper.Map<List<ChatResponse>>(value)),
            Problem);
    }
}