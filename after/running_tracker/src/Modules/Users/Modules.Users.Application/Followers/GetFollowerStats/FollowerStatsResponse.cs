namespace Modules.Users.Application.Followers.GetFollowerStats;

public sealed class FollowerStatsResponse
{
    public Guid UserId { get; init; }

    public int FollowerCount { get; init; }

    public int FollowingCount { get; init; }
}
