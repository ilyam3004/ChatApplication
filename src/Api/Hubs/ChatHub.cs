using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace Api.Hubs;

public class ChatHub : Hub
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public ChatHub(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }
}