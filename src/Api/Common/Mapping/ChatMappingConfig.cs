using Application.Features.Chats.Commands.JoinChat;
using Application.Models;
using Contracts.Requests;
using Contracts.Responses;
using Mapster;

namespace Api.Common.Mapping;

public class ChatMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<(JoinChatRequest, string), JoinChatCommand>()
            .Map(dest => dest.UserId, src => src.Item1.UserId)
            .Map(dest => dest.ChatId, src => src.Item1.ChatId)
            .Map(dest => dest.ConnectionId, src => src.Item2);
        
        config.NewConfig<ChatResult, ChatResponse>()
            .Map(dest => dest.ChatId, src => src.Chat.ChatId)
            .Map(dest => dest.ChatOwnerId, src => src.Chat.ChatOwnerId)
            .Map(dest => dest.ChatName, src => src.Chat.ChatName);
    }
}