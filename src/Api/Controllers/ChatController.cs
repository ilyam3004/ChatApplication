using Application.Features.Chats.Commands.CreateChat;
using Application.Features.Chats.Commands.RemoveChat;
using Application.Features.Chats.Queries.GetAllChats;
using Application.Features.Chats.Queries.SearchChat;
using Microsoft.AspNetCore.SignalR;
using Application.Common.Constants;
using Microsoft.AspNetCore.Mvc;
using Contracts.Responses;
using Application.Models;
using Contracts.Requests;
using MapsterMapper;
using Api.Hubs;
using Application.Common.Result;
using MediatR;

namespace Api.Controllers;

[Route("chats")]
public class ChatController : ApiController
{
    private readonly IHubContext<ChatHub> _chatHub;
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public ChatController(ISender sender,
        IMapper mapper,
        IHubContext<ChatHub> chatHub)
    {
        _sender = sender;
        _mapper = mapper;
        _chatHub = chatHub;
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

        var removeResult = await _sender.Send(query);

        if (removeResult.IsSuccess)
            await DisconnectAllChatMembers(removeResult.Value, request.ChatId);
        
        return removeResult.Match(
            value => Ok(new Deleted(
                Messages.Chat.ChatDeleted(request.ChatId))), 
            Problem);
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

    private async Task DisconnectAllChatMembers(List<UserResult> users, Guid chatId)
    {
        foreach (var user in users)
        {
            string? connectionId = user.User.ConenctionId;

            if (connectionId is null) 
                continue;
            
            await _chatHub.Groups.RemoveFromGroupAsync(
                connectionId,
                chatId.ToString());

            await _chatHub.Clients.Client(connectionId)
                .SendAsync(Messages.Chat.DisconnectedFromChat(chatId));
        }
    }
}