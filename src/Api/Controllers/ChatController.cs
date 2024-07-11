using Application.Features.Chats.Queries.GetAllChats;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("chats")]
public class ChatController : ApiController
{
    private readonly ISender _sender;

    public ChatController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllChats()
    {
        var query = new GetAllChatsQuery();

        var result = await _sender.Send(query);

        return result.Match(
            value => Ok(value), 
            Problem);
    }
    
    
    [HttpGet("{query}")]
    public async Task<IActionResult> SearchChats(string query)
    {
        return Ok();
    }
}