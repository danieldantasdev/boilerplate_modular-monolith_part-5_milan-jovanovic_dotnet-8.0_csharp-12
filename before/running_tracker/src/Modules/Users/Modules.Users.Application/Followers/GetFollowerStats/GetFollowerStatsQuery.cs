using Application.Abstractions.Messaging;

namespace Modules.Users.Application.Followers.GetFollowerStats;

public sealed record GetFollowerStatsQuery(Guid UserId) : IQuery<FollowerStatsResponse>;
