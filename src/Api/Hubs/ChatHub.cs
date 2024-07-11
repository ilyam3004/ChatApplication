using Application.Features.Chats.Commands.JoinChat;
using Microsoft.AspNetCore.SignalR;
using Api.Common.Helpers;
using Application.Features.Messages.Commands.SaveMessage;
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
    
    public async Task TestMe(string someRandomText)
    {
        await Clients.All.SendAsync(
            $"{this.Context.User.Identity.Name} : {someRandomText}",
            CancellationToken.None);
    }    

    public async Task JoinChat(string UserId, string ChatId)
    {
        var command = new JoinChatCommand(
            Guid.Parse(UserId),
            Guid.Parse(ChatId));

        var result = await _sender.Send(command);

        await result.Match(
            async value =>
            {
                var response = _mapper.Map<UserResponse>(value);
                await JoinUserToChatAndNotifyAboutAddingUser(ChatId, response);
            },
            async onError =>
                await Clients
                    .Client(Context.ConnectionId)
                    .SendAsync("ReceiveError",
                        ErrorHelper.GenerateProblem(result.Errors))
        );
    }

    public async Task SendMessage(SaveMessageRequest request)
    {
        var command = _mapper.Map<SaveMessageCommand>(request);
        var result = await _sender.Send(command);

        await result.Match(
            async value =>
            {
                var chatId = value.Message.ChatId.ToString();
                var response = _mapper.Map<MessageResponse>(value);
                await Clients.Group(chatId).SendAsync("ReceiveMessage", value);
            },
            async onError =>
                await Clients
                    .Client(Context.ConnectionId)
                    .SendAsync("ReceiveError", ErrorHelper.GenerateProblem(onError)));
    }

    private async Task JoinUserToChatAndNotifyAboutAddingUser(string chatId,
        UserResponse userResponse)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, chatId);

        var request = new SaveMessageRequest(
            Guid.Parse(chatId),
            userResponse.UserId,
            $"User {userResponse.Username} has joined the chat.");

        await SendMessage(request);
    }
}