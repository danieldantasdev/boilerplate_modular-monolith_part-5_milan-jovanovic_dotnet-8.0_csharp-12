using FluentValidation;

namespace Modules.Users.Application.Followers.StartFollowing;

internal sealed class StartFollowingCommandValidator : AbstractValidator<StartFollowingCommand>
{
    public StartFollowingCommandValidator()
    {
        RuleFor(c => c.UserId)
            .NotEmpty().WithErrorCode(FollowerErrorCodes.StartFollowing.MissingUserId);

        RuleFor(c => c.FollowedId)
            .NotEmpty().WithErrorCode(FollowerErrorCodes.StartFollowing.MissingFollowedId);
    }
}
