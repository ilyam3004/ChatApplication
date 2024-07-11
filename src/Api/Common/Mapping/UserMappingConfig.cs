using Application.Features.Users.Commands.CreateUser;
using Application.Models.Users;
using Contracts.Requests;
using Contracts.Responses;
using Mapster;

namespace Api.Common.Mapping;

public class UserMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<UserResult, UserResponse>()
            .Map(dest => dest.Username, src => src.User.Username)
            .Map(dest => dest.UserId, src => src.User.UserId);
    }
}