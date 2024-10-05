using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Modules.Users.Application.Abstractions.Data;
using Modules.Users.Domain.Followers;
using Modules.Users.Domain.Users;
using SharedKernel;

namespace Modules.Users.Application.Followers.StartFollowing;

internal sealed class StartFollowingCommandHandler(
    IUserRepository userRepository,
    IFollowerService followerService,
    IFollowerRepository followerRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<StartFollowingCommand>
{
    public async Task<Result> Handle(
        StartFollowingCommand command,
        CancellationToken cancellationToken)
    {
        User? user = await userRepository.GetByIdAsync(command.UserId, cancellationToken);
        if (user is null)
        {
            return Result.Failure(UserErrors.NotFound(command.UserId));
        }

        User? followed = await userRepository.GetByIdAsync(command.FollowedId, cancellationToken);
        if (followed is null)
        {
            return Result.Failure(UserErrors.NotFound(command.FollowedId));
        }

        Result<Follower> result = await followerService.StartFollowingAsync(
            user,
            followed,
            cancellationToken);

        if (result.IsFailure)
        {
            return result;
        }

        followerRepository.Insert(result.Value);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
