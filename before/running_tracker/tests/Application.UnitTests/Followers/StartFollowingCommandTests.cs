using Application.Abstractions.Data;
using FluentAssertions;
using Modules.Users.Application.Abstractions.Data;
using Modules.Users.Application.Followers.StartFollowing;
using Modules.Users.Domain.Followers;
using Modules.Users.Domain.Users;
using NSubstitute;
using SharedKernel;

namespace Application.UnitTests.Followers;

public class StartFollowingCommandTests
{
    private static readonly User User = User.Create(
        Email.Create("test@test.com").Value,
        new Name("FullName"),
        hasPublicProfile: true);
    private static readonly StartFollowingCommand Command = new(Guid.NewGuid(), Guid.NewGuid());

    private readonly StartFollowingCommandHandler _handler;
    private readonly IUserRepository _userRepositoryMock;
    private readonly IFollowerService _followerServiceMock;
    private readonly IFollowerRepository _followerRepositoryMock;
    private readonly IUnitOfWork _unitOfWorkMock;

    public StartFollowingCommandTests()
    {
        _userRepositoryMock = Substitute.For<IUserRepository>();
        _followerServiceMock = Substitute.For<IFollowerService>();
        _followerRepositoryMock = Substitute.For<IFollowerRepository>();
        _unitOfWorkMock = Substitute.For<IUnitOfWork>();

        _handler = new StartFollowingCommandHandler(
            _userRepositoryMock,
            _followerServiceMock,
            _followerRepositoryMock,
            _unitOfWorkMock);
    }

    [Fact]
    public async Task Handle_Should_ReturnError_WhenUserNotFound()
    {
        // Arrange
        _userRepositoryMock.GetByIdAsync(Command.UserId)
            .Returns((User?)null);

        // Act
        Result result = await _handler.Handle(Command, default);

        // Assert
        result.Error.Should().Be(UserErrors.NotFound(Command.UserId));
    }

    [Fact]
    public async Task Handle_Should_ReturnError_WhenFollowedNotFound()
    {
        // Arrange
        _userRepositoryMock.GetByIdAsync(Command.UserId)
            .Returns(User);

        _userRepositoryMock.GetByIdAsync(Command.FollowedId)
            .Returns((User?)null);

        // Act
        Result result = await _handler.Handle(Command, default);

        // Assert
        result.Error.Should().Be(UserErrors.NotFound(Command.FollowedId));
    }

    [Fact]
    public async Task Handle_Should_ReturnError_WhenStartFollowingFails()
    {
        // Arrange
        _userRepositoryMock.GetByIdAsync(Command.UserId)
            .Returns(User);

        _userRepositoryMock.GetByIdAsync(Command.FollowedId)
            .Returns(User);

        _followerServiceMock.StartFollowingAsync(User, User, default)
            .Returns(Result.Failure<Follower>(FollowerErrors.SameUser));

        // Act
        Result result = await _handler.Handle(Command, default);

        // Assert
        result.Error.Should().Be(FollowerErrors.SameUser);
    }

    [Fact]
    public async Task Handle_Should_CallInsertOnRepository_WhenStartFollowingDoesNotFail()
    {
        // Arrange
        _userRepositoryMock.GetByIdAsync(Command.UserId)
            .Returns(User);

        _userRepositoryMock.GetByIdAsync(Command.FollowedId)
            .Returns(User);

        DateTime utcNow = DateTime.UtcNow;
        _followerServiceMock.StartFollowingAsync(User, User, default)
            .Returns(Follower.Create(Command.UserId, Command.FollowedId, utcNow));

        // Act
        await _handler.Handle(Command, default);

        // Assert
        _followerRepositoryMock.Received(1)
            .Insert(Arg.Is<Follower>(f => f.UserId == Command.UserId &&
                                          f.FollowedId == Command.FollowedId &&
                                          f.CreatedOnUtc == utcNow));
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccess_WhenStartFollowingDoesNotFail()
    {
        // Arrange
        _userRepositoryMock.GetByIdAsync(Command.UserId)
            .Returns(User);

        _userRepositoryMock.GetByIdAsync(Command.FollowedId)
            .Returns(User);

        _followerServiceMock.StartFollowingAsync(User, User, default)
            .Returns(Follower.Create(Command.UserId, Command.FollowedId, DateTime.UtcNow));

        // Act
        Result result = await _handler.Handle(Command, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_Should_CallUnitOfWork_WhenStartFollowingDoesNotFail()
    {
        // Arrange
        _userRepositoryMock.GetByIdAsync(Command.UserId)
            .Returns(User);

        _userRepositoryMock.GetByIdAsync(Command.FollowedId)
            .Returns(User);

        _followerServiceMock.StartFollowingAsync(User, User, default)
            .Returns(Follower.Create(Command.UserId, Command.FollowedId, DateTime.UtcNow));

        // Act
        await _handler.Handle(Command, default);

        // Assert
        await _unitOfWorkMock.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    }
}
