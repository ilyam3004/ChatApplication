using Application.Models.Chats;
using Contracts.Responses;
using Mapster;

namespace Api.Common.Mapping;

public class ChatMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ChatResult, ChatResponse>()
            .Map(dest => dest.ChatId, src => src.Chat.ChatId)
            .Map(dest => dest.ChatOwnerId, src => src.Chat.ChatOwnerId)
            .Map(dest => dest.ChatName, src => src.Chat.ChatName);
    }
}