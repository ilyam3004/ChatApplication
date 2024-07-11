using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace Api.Hubs;

public class ChatHub : Hub
{
    /*private readonly ISender _sender;
    private readonly IMapper _mapper;

    public ChatHub(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    public async Task JoinRoom(JoinRoomRequest request)
    {
        var command = _mapper.Map<JoinRoomCommand>((request, Context.ConnectionId));

        ErrorOr<UserResponse> result = await _mediator.Send(command);

        await result.Match(
            async onValue => await SendDataToRoomAboutAddingUser(onValue),
            async onError =>
                await Clients
                    .Client(Context.ConnectionId)
                    .SendAsync("ReceiveError", GenerateProblem(result.Errors))
        );
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var command = new LeaveRoomCommand(Context.ConnectionId);

        ErrorOr<UserResponse> result = await _mediator.Send(command);

        await result.Match(
            async onValue =>
                await SendDataToRoomAboutUserLeaving(onValue),
            async onError =>
                await SendRemovingErrorToClientIfErrorTypeIsNotFound(onError[0])
        );
    }

    private async Task SendDataToRoomAboutAddingUser(UserResponse response)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, response.RoomId);
        await SendUserData(response);
        await SendUserList(response.RoomId);
        await SendMessageToRoom(
            new SendMessageRequest(
                response.UserId,
                response.Username,
                response.RoomId,
                $"User {response.Username} has joined the room",
                false
            )
        );
        await SendAllRoomMessages(response.RoomId);
    }

    private async Task SendDataToRoomAboutUserLeaving(UserResponse response)
    {
        await SendUserList(response.RoomId);
        await SendMessageToRoom(
            new SendMessageRequest(
                response.UserId,
                response.Username,
                response.RoomId,
                $"User {response.Username} has left the room",
                false
            )
        );
    }

    public async Task SendUserMessage(string message)
    {
        var query = new GetUserByConnectionIdQuery(Context.ConnectionId);
        ErrorOr<UserResponse> result = await _mediator.Send(query);

        await result.Match(
            async onValue =>
                await SendMessageToRoom(
                    new SendMessageRequest(
                        onValue.UserId,
                        onValue.Username,
                        onValue.RoomId,
                        message,
                        true
                    )
                ),
            async onError =>
                await Clients
                    .Client(Context.ConnectionId)
                    .SendAsync("ReceiveError", GenerateProblem(onError))
        );
    }

    public async Task SendAllRoomMessages(string roomId)
    {
        var query = new GetRoomMessagesQuery(roomId);
        List<MessageResponse> result = await _mediator.Send(query);

        await Clients
            .Client(Context.ConnectionId)
            .SendAsync("ReceiveRoomMessages", result);
    }

    public async Task SendImageToRoom(SendImageRequest request)
    {
        var command = _mapper.Map<SaveImageCommand>(request);
        ErrorOr<MessageResponse> result = await _mediator.Send(command);

        await result.Match(
            async onValue =>
                await Clients
                    .Group(onValue.RoomId)
                    .SendAsync("ReceiveMessage", onValue),
            async onError =>
                await Clients
                    .Client(Context.ConnectionId)
                    .SendAsync("ReceiveError", GenerateProblem(onError))
        );
    }

    private async Task SendMessageToRoom(SendMessageRequest request)
    {
        var command = _mapper.Map<SaveMessageCommand>(request);
        ErrorOr<MessageResponse> result = await _mediator.Send(command);

        await result.Match(
            async onValue =>
                await Clients.Group(onValue.RoomId).SendAsync("ReceiveMessage", onValue),
            async onError =>
                await Clients
                    .Client(Context.ConnectionId)
                    .SendAsync("ReceiveError", GenerateProblem(onError))
        );
    }

    private async Task SendRemovingErrorToClientIfErrorTypeIsNotFound(Error error)
    {
        if (error.Type != ErrorType.Unexpected)
        {
            await Clients
                .Client(Context.ConnectionId)
                .SendAsync("ReceiveError", error);
        }
    }

    private async Task SendUserList(string roomId)
    {
        var query = new GetUserListQuery(roomId);

        List<UserResponse> result = await _mediator.Send(query);

        await Clients
            .Group(roomId)
            .SendAsync("ReceiveUserList", result);
    }

    private async Task SendUserData(UserResponse response)
    {
        await Clients
            .Client(Context.ConnectionId)
            .SendAsync("ReceiveUserData", response);
    }
}*/
}