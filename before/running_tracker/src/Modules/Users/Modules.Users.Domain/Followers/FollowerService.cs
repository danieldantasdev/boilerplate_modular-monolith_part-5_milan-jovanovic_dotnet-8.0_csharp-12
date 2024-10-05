using Modules.Users.Domain.Users;
using SharedKernel;

namespace Modules.Users.Domain.Followers;

public sealed class FollowerService(
    IFollowerRepository followerRepository,
    IDateTimeProvider dateTimeProvider) : IFollowerService
{
    public async Task<Result<Follower>> StartFollowingAsync(
        User user,
        User followed,
        CancellationToken cancellationToken)
    {
        if (user.Id == followed.Id)
        {
            return Result.Failure<Follower>(FollowerErrors.SameUser);
        }

        if (!followed.HasPublicProfile)
        {
            return Result.Failure<Follower>(FollowerErrors.NonPublicProfile);
        }

        if (await followerRepository.IsAlreadyFollowingAsync(
                user.Id,
                followed.Id,
                cancellationToken))
        {
            return Result.Failure<Follower>(FollowerErrors.AlreadyFollowing);
        }

        var follower = Follower.Create(user.Id, followed.Id, dateTimeProvider.UtcNow);

        return follower;
    }
}
