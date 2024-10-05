using SharedKernel;

namespace Modules.Users.Domain.Followers;

public sealed record FollowerCreatedDomainEvent(Guid UserId, Guid FollowedId) : IDomainEvent;
