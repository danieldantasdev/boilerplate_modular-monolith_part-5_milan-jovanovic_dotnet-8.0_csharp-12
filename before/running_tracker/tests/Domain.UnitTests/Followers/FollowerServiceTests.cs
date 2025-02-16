﻿using FluentAssertions;
using Modules.Users.Domain.Followers;
using Modules.Users.Domain.Users;
using NSubstitute;
using SharedKernel;

namespace Domain.UnitTests.Followers;

public class FollowerServiceTests
{
    private readonly IFollowerRepository _followerRepositoryMock;
    private readonly FollowerService _followerService;
    private static readonly Email Email = Email.Create("test@test.com").Value;
    private static readonly Name Name = new("Full Name");
    private static readonly DateTime UtcNow = DateTime.UtcNow;

    public FollowerServiceTests()
    {
        _followerRepositoryMock = Substitute.For<IFollowerRepository>();

        IDateTimeProvider dateTimeProviderMock = Substitute.For<IDateTimeProvider>();
        dateTimeProviderMock.UtcNow.Returns(UtcNow);

        _followerService = new FollowerService(_followerRepositoryMock, dateTimeProviderMock);
    }

    [Fact]
    public async Task StartFollowingAsync_Should_ReturnError_WhenFollowingSameUser()
    {
        // Arrange
        var user = User.Create(Email, Name, hasPublicProfile: false);

        // Act
        Result<Follower> result = await _followerService.StartFollowingAsync(user, user, default);

        // Assert
        result.Error.Should().Be(FollowerErrors.SameUser);
    }

    [Fact]
    public async Task StartFollowingAsync_Should_ReturnError_WhenFollowingNonPublicProfile()
    {
        // Arrange
        var user = User.Create(Email, Name, hasPublicProfile: true);
        var followed = User.Create(Email, Name, hasPublicProfile: false);

        // Act
        Result<Follower> result = await _followerService.StartFollowingAsync(user, followed, default);

        // Assert
        result.Error.Should().Be(FollowerErrors.NonPublicProfile);
    }

    [Fact]
    public async Task StartFollowingAsync_Should_ReturnError_WhenAlreadyFollowing()
    {
        // Arrange
        var user = User.Create(Email, Name, hasPublicProfile: true);
        var followed = User.Create(Email, Name, hasPublicProfile: true);

        _followerRepositoryMock
            .IsAlreadyFollowingAsync(user.Id, followed.Id)
            .Returns(true);

        // Act
        Result<Follower> result = await _followerService.StartFollowingAsync(user, followed, default);

        // Assert
        result.Error.Should().Be(FollowerErrors.AlreadyFollowing);
    }

    [Fact]
    public async Task StartFollowingAsync_Should_ReturnSuccess_WhenFollowerCreated()
    {
        // Arrange
        var user = User.Create(Email, Name, hasPublicProfile: true);
        var followed = User.Create(Email, Name, hasPublicProfile: true);

        _followerRepositoryMock
            .IsAlreadyFollowingAsync(user.Id, followed.Id)
            .Returns(false);

        // Act
        Result<Follower> result = await _followerService.StartFollowingAsync(user, followed, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task StartFollowingAsync_Should_ReturnFollower_WhenFollowerCreated()
    {
        // Arrange
        var user = User.Create(Email, Name, hasPublicProfile: true);
        var followed = User.Create(Email, Name, hasPublicProfile: true);

        _followerRepositoryMock
            .IsAlreadyFollowingAsync(user.Id, followed.Id)
            .Returns(false);

        // Act
        Result<Follower> result = await _followerService.StartFollowingAsync(user, followed, default);

        // Assert
        Follower follower = result.Value;
        follower.UserId.Should().Be(user.Id);
        follower.FollowedId.Should().Be(followed.Id);
        follower.CreatedOnUtc.Should().Be(UtcNow);
    }
}
