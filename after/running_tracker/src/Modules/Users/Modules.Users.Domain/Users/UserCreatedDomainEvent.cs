using SharedKernel;

namespace Modules.Users.Domain.Users;

public sealed record UserCreatedDomainEvent(Guid UserId) : IDomainEvent;
