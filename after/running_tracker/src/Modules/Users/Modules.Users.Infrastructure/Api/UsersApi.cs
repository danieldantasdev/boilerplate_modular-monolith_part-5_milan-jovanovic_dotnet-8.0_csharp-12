using Microsoft.EntityFrameworkCore;
using Modules.Users.Api;
using Modules.Users.Infrastructure.Database;

namespace Modules.Users.Infrastructure.Api;

public class UsersApi(UsersDbContext context) : IUsersApi
{
    public Task<UserResponse?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return context.Users
            .Where(u => u.Id == id)
            .Select(u => new UserResponse(u.Id, u.Name.Value))
            .FirstOrDefaultAsync(cancellationToken);
    }
}
