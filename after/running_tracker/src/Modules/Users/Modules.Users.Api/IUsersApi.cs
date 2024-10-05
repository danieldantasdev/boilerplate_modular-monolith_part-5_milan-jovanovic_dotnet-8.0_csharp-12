namespace Modules.Users.Api;

public interface IUsersApi
{
    Task<UserResponse?> GetAsync(Guid id, CancellationToken cancellationToken = default);
}
