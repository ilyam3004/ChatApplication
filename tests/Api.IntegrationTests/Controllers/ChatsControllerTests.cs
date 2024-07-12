using Api.IntegrationTests.Utils.Factories;
using Api.IntegrationTests.Controllers.Utils;
using Api.IntegrationTests.Utils.Constants;
using System.Net.Http.Json;
using Contracts.Responses;
using FluentAssertions;
using Xunit;

namespace Api.IntegrationTests.Controllers;

public class ChatsControllerTests
{
    private readonly HttpClient _client;

    public ChatsControllerTests()
    {
        var application = new SimpleChatWebApplicationFactory();
        _client = application.CreateClient();
    }
    
    [Fact]
    public async Task GetAll_WhenNoChatsInDb_ShouldReturnEmpty()
    {
        // Act
        var response = await _client.GetAsync("chats");
        
        // Assert
        response.EnsureSuccessStatusCode();

        var chatsResponse = await response.Content.ReadFromJsonAsync<List<ChatResponse>>();
        chatsResponse.Should().BeEmpty();
    }
    
    [Fact]
    public async Task CreateChat_ShouldAddChatToDataBaseAndReturnChatResponse()
    {
        //Arrange
        var request = CreateChatRequestUtils.CreateChatRequest();
        
        // Act
        var response = await _client.PostAsJsonAsync("chats", request);
        
        // Assert
        response.EnsureSuccessStatusCode();

        var chatsResponse = await response.Content.ReadFromJsonAsync<ChatResponse>();
        chatsResponse.ChatName.Should().Be(Constants.Chat.ChatName);
        chatsResponse.ChatOwnerId.Should().Be(Constants.User.UserId);
    }
}