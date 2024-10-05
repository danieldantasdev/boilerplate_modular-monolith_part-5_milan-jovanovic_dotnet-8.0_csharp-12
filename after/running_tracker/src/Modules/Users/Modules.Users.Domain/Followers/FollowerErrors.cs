using SharedKernel;

namespace Modules.Users.Domain.Followers;

public static class FollowerErrors
{
    public static readonly Error SameUser = Error.Problem(
        "Followers.SameUser",
        "Can't follow yourself");

    public static readonly Error NonPublicProfile = Error.NotFound(
        "Followers.NonPublicProfile",
        "Can't follow non-public profiles");

    public static readonly Error AlreadyFollowing = Error.Conflict(
        "Followers.AlreadyFollowing",
        "Already following");
}
