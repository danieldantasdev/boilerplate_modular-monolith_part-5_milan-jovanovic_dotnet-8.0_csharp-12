using Application.Abstractions.Messaging;

namespace Modules.Users.Application.Followers.GetRecentFollowers;

public sealed record GetRecentFollowersQuery(Guid UserId) : IQuery<List<FollowerResponse>>;
