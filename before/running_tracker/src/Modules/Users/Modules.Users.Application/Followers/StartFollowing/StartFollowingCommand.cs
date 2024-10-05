using Application.Abstractions.Messaging;

namespace Modules.Users.Application.Followers.StartFollowing;

public sealed record StartFollowingCommand(Guid UserId, Guid FollowedId) : ICommand;
