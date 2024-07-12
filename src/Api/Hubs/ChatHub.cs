using Application.Features.Messages.Queries.GetChatMessages;
using Application.Features.Messages.Commands.SaveMessage;
using Application.Features.Chats.Commands.JoinChat;
using Microsoft.AspNetCore.SignalR;
using Application.Common.Constants;
using Contracts.Responses;
using Api.Common.Helpers;
using Application.Common;
using Application.Models;
using Contracts.Requests;
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
            async value => await HandleUserJoinAsync(value, command),
            async error => await SendErrorsToUser(error));
    }

    public async Task SendMessageToChat(SaveMessageRequest request,
        bool excludeCurrentUser = false)
    {
        var command = _mapper.Map<SaveMessageCommand>(request);
        var result = await _sender.Send(command);

        await result.Match(
            async value => await HandleMessageSendingAsync(value, excludeCurrentUser),
            async error => await SendErrorsToUser(error));
    }

    private async Task HandleUserJoinAsync(UserResult userResult, JoinChatCommand command)
    {
        var response = _mapper.Map<UserResponse>(userResult);
        var chatId = command.ChatId;

        await JoinUserToChat(chatId);
        await NotifyChatMembersAboutUserJoining(chatId, response);
        await SendAllChatMessageToNewUser(chatId);
    }

    private async Task HandleMessageSendingAsync(MessageResult value,
        bool excludeCurrentUser)
    {
        var chatId = value.Message.ChatId.ToString();
        var response = _mapper.Map<MessageResponse>(value);

        if (excludeCurrentUser)
            await Clients.GroupExcept(chatId, Context.ConnectionId).SendAsync(
                    Constants.Hub.ReceiveMessageMethodName, 
                    response);
        else
            await Clients.Group(chatId).SendAsync(
                Constants.Hub.ReceiveMessageMethodName, 
                response);
    }

    private async Task JoinUserToChat(Guid chatId)
        => await Groups.AddToGroupAsync(Context.ConnectionId, chatId.ToString());

    private async Task NotifyChatMembersAboutUserJoining(Guid chatId, UserResponse userResponse)
    {
        var request = new SaveMessageRequest(
            chatId,
            userResponse.UserId,
            Messages.Chat.UserHasJoinTheChat(userResponse.Username));

        await SendMessageToChat(request, excludeCurrentUser: true);
    }

    private async Task SendAllChatMessageToNewUser(Guid chatId)
    {
        var query = new GetChatMessagesQuery(chatId);

        var result = await _sender.Send(query);

        await result.Match(
            async value =>
            {
                var messages = _mapper.Map<List<MessageResponse>>(value);
                await Clients.Client(Context.ConnectionId)
                    .SendAsync(Constants.Hub.ReceiveMessageMethodName,
                        messages,
                        CancellationToken.None);
            },
            async error => await SendErrorsToUser(error));
    }

    private async Task SendErrorsToUser(List<Error> errors)
        => await Clients.Client(Context.ConnectionId)
            .SendAsync(
                Constants.Hub.ReceiveErrorMethodName,
                ErrorHelper.GenerateProblem(errors));
}