using Application.Features.Users.Commands.CreateUser;
using Microsoft.AspNetCore.Mvc;
using Contracts.Responses;
using Contracts.Requests;
using MapsterMapper;
using MediatR;

namespace Api.Controllers;

[Route("users")]
public class UserController : ApiController
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;
    
    public UserController(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(CreateUserRequest request)
    {
        var command = _mapper.Map<CreateUserCommand>(request);

        var result = await _sender.Send(command);

        return result.Match(
            value => Ok(_mapper.Map<UserResponse>(value)),
            Problem);
    }
}