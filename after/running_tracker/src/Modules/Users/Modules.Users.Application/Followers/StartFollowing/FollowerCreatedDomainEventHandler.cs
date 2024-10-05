using Application.Abstractions.Notifications;
using MediatR;
using Modules.Users.Application.Users;
using Modules.Users.Application.Users.GetById;
using Modules.Users.Domain.Followers;
using SharedKernel;

namespace Modules.Users.Application.Followers.StartFollowing;

internal sealed class FollowerCreatedDomainEventHandler(
    ISender sender,
    INotificationService notificationService) : INotificationHandler<FollowerCreatedDomainEvent>
{
    public async Task Handle(
        FollowerCreatedDomainEvent notification,
        CancellationToken cancellationToken)
    {
        Result<UserResponse> result = await sender.Send(
            new GetUserByIdQuery(notification.UserId),
            cancellationToken);

        if (result.IsFailure)
        {
            throw new UserNotFoundException(notification.UserId);
        }

        await notificationService.SendAsync(
            notification.FollowedId,
            $"{result.Value.Name} started following you!",
            cancellationToken);
    }
}
