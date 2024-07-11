using Application.Models;
using Contracts.Responses;
using Mapster;

namespace Api.Common.Mapping;

public class MessageMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<MessageResult, MessageResponse>()
            .Map(dest => dest.UserName, src => src.Message.User.Username)
            .Map(dest => dest.Message, src => src.Message.Text);
    }
}