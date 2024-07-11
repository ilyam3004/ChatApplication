using Application.Features.Chats.Commands.JoinChat;
using Microsoft.AspNetCore.SignalR;
using Api.Common.Helpers;
using Contracts.Requests;
using Contracts.Responses;
using MapsterMapper;
using MediatR;

namespace Api.Hubs;

public class ChatHub : Hub
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public ChatHub(IMapper mapper, ISender sender)
    {
        _mapper = mapper;
        _sender = sender;
    }
    
    public async Task JoinChat(JoinChatRequest request)
    {
        var command = _mapper.Map<JoinChatCommand>(request);

        var result = await _sender.Send(command);
        
        await result.Match(
            async value =>
            {
                var response = _mapper.Map<UserResponse>(value);
                await JoinUserToChatAndNotifyAboutAddingUser(request.ChatId, response);
            },
            async onError =>
                await Clients
                    .Client(Context.ConnectionId)
                    .SendAsync("ReceiveError", 
                        ErrorHelper.GenerateProblem(result.Errors))
        );
    }

    public async Task SendMessage(string userId, string chatId, string message)
    {
        await Clients.Group(chatId).SendAsync("ReceiveMessage", userId, message);
    }
    
    private async Task JoinUserToChatAndNotifyAboutAddingUser(string chatId, 
        UserResponse userResponse)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, chatId);
    }
}