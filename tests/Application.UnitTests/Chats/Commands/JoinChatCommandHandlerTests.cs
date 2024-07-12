using Application.Common.Constants;
using Application.Features.Chats.Commands.JoinChat;
using Application.Features.Chats.Commands.TestUtils;
using Application.UnitTests.TestUtils.Extensions;
using Application.UnitTests.TestUtils.Factories;
using Data.Repositories;
using Data.Entities;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace Application.UnitTests.Users.Commands;

public class JoinChatCommandHandlerTests
{
    private readonly JoinChatCommandHandler _sut;
    private readonly IChatRepository _chatRepositoryMock;
    private readonly IRepository<User> _userRepositoryMock;
    private readonly IMessageRepository _messageRepository;

    public JoinChatCommandHandlerTests()
    {
        _userRepositoryMock = Substitute.For<IRepository<User>>();
        _chatRepositoryMock = Substitute.For<IChatRepository>();
        _messageRepository = Substitute.For<IMessageRepository>();

        _sut = new JoinChatCommandHandler(
            _chatRepositoryMock,
            _userRepositoryMock,
            _messageRepository);
    }

    [Fact]
    public async Task Handler_WhenUserAndChatExists_ShouldAssignChatIdAndConnectionIdToUserCreateTheMessageAboutUserJoiningAndReturnMessageResultWithUserRelation()
    {
        var command = JoinChatCommandUtils.CreateJoinChatCommand();

        _chatRepositoryMock.GetByIdAsync(command.ChatId)
            .Returns(ChatFactory.CreateChat());

        _userRepositoryMock.GetByIdAsync(command.UserId)
            .Returns(UserFactory.CreateUser());

        _messageRepository.GetMessageWithUser(Arg.Any<Guid>())
            .Returns(MessageFactory.CreateMessage());

        var result = await _sut.Handle(command, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        result.Value.ValidateRetrievedMessage();
        await _userRepositoryMock.Received().Update(Arg.Any<User>());
        await _messageRepository.Received(1).AddAsync(Arg.Any<Message>());
    }
    
    [Fact]
    public async Task Handler_WhenUserAndChatExistsButMessageAboutJoiningNotCreated_ShouldReturnMessageNotFoundError()
    {
        var command = JoinChatCommandUtils.CreateJoinChatCommand();

        _chatRepositoryMock.GetByIdAsync(command.ChatId)
            .Returns(ChatFactory.CreateChat());

        _userRepositoryMock.GetByIdAsync(command.UserId)
            .Returns(UserFactory.CreateUser());

        _messageRepository.GetMessageWithUser(Arg.Any<Guid>())
            .ReturnsNull();

        var result = await _sut.Handle(command, CancellationToken.None);

        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().ContainEquivalentOf(Errors.Message.MessageNotFound);
        await _userRepositoryMock.Received(1).Update(Arg.Any<User>());
        await _messageRepository.Received(1).AddAsync(Arg.Any<Message>());
    }
    
    
    [Fact]
    public async Task Handler_WhenChatExistsButUserNotExists_ShouldReturnUserNotFound()
    {
        var command = JoinChatCommandUtils.CreateJoinChatCommand();

        _chatRepositoryMock.GetByIdAsync(command.ChatId)
            .Returns(ChatFactory.CreateChat());

        _userRepositoryMock.GetByIdAsync(command.UserId)
            .ReturnsNull();

        var result = await _sut.Handle(command, CancellationToken.None);

        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().ContainEquivalentOf(Errors.User.UserNotFound);
        await _userRepositoryMock.Received(0).Update(Arg.Any<User>());
        await _messageRepository.Received(0).AddAsync(Arg.Any<Message>());
    }
    
    
    [Fact]
    public async Task Handler_WhenChatNotExists_ShouldReturnChatNotFound()
    {
        var command = JoinChatCommandUtils.CreateJoinChatCommand();

        _chatRepositoryMock.GetByIdAsync(command.ChatId)
            .ReturnsNull();

        var result = await _sut.Handle(command, CancellationToken.None);

        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().ContainEquivalentOf(Errors.Chat.ChatNotFound);
        await _userRepositoryMock.Received(0).Update(Arg.Any<User>());
        await _messageRepository.Received(0).AddAsync(Arg.Any<Message>());
    }
}