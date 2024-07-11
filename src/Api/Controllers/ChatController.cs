using Application.Features.Chats.Commands.CreateChat;
using Application.Features.Chats.Queries.GetAllChats;
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
            value => Ok(_mapper.Map<ChatResponse>(value)), 
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

    // [HttpDelete("{chatId}")]
    // public async Task<IActionResult> CreateChat(Guid chatId)
    // {
    //     var query = _mapper.Map<RemoveChatCommand>(request);
    //
    //     var result = await _sender.Send(query);
    //
    //     return result.Match(
    //         value => Ok(_mapper.Map<ChatResponse>(value)), 
    //         Problem);
    // }
    
    
    [HttpGet("{query}")]
    public async Task<IActionResult> SearchChats(string query)
    {
        return Ok();
    }
}