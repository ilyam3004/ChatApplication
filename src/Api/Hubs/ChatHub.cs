using Application.Features.Messages.Queries.GetChatMessages;
using Application.Features.Messages.Commands.SaveMessage;
using Application.Features.Chats.Commands.LeaveChat;
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
        var command = _mapper.Map<JoinChatCommand>((request, Context.ConnectionId));

        var result = await _sender.Send(command);

        await result.Match(
            async value => await HandleUserJoiningAsync(value),
            async error => await SendErrorsToUser(error));
    }

    public async Task LeaveChat(LeaveChatRequest request)
    {
        var command = _mapper.Map<LeaveChatCommand>(request);

        var result = await _sender.Send(command);

        await result.Match(
            async value => await HandleUserLeavingAsync(value),
            async error => await SendErrorsToUser(error));
    }

    public async Task SendMessageToChat(SendMessageRequest request)
    {
        var command = _mapper.Map<SaveMessageCommand>(request);
        var result = await _sender.Send(command);

        await result.Match(
            async value => await HandleMessageSendingAsync(
                value,
                request.ExcludeCurrentUser),
            async error => await SendErrorsToUser(error));
    }

    private async Task HandleUserJoiningAsync(MessageResult result)
    {
        await JoinUserToChat(result.Message.ChatId);
        await HandleMessageSendingAsync(result,
            excludeCurrentUser: true);
        await SendAllChatMessageToNewUser(result.Message.ChatId);
    }

    private async Task HandleUserLeavingAsync(MessageResult result)
    {
        await HandleMessageSendingAsync(result,
            excludeCurrentUser: true);

        await RemoveUserFromChat(result.Message.ChatId);
    }

    private async Task HandleMessageSendingAsync(MessageResult value,
        bool? excludeCurrentUser = null)
    {
        var chatId = value.Message.ChatId.ToString();
        var response = _mapper.Map<MessageResponse>(value);

        if (excludeCurrentUser is true)
            await Clients.GroupExcept(chatId, Context.ConnectionId).SendAsync(
                Constants.Hub.ReceiveMessageMethodName,
                response);
        else
            await Clients.Group(chatId).SendAsync(
                Constants.Hub.ReceiveMessageMethodName,
                response);
    }

    private async Task JoinUserToChat(Guid chatId)
        => await Groups.AddToGroupAsync(
            Context.ConnectionId,
            chatId.ToString(),
            CancellationToken.None);

    private async Task RemoveUserFromChat(Guid chatId)
    {
        await Groups.RemoveFromGroupAsync(
            Context.ConnectionId,
            chatId.ToString(),
            CancellationToken.None);

        await Clients.Client(Context.ConnectionId)
            .SendAsync(Messages.Chat.LeavedChat);
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