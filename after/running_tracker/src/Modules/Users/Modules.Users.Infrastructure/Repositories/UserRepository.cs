using Microsoft.EntityFrameworkCore;
using Modules.Users.Domain.Users;
using Modules.Users.Infrastructure.Database;

namespace Modules.Users.Infrastructure.Repositories;

internal sealed class UserRepository(UsersDbContext context) : IUserRepository
{
    public Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return context.Users.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }

    public async Task<bool> IsEmailUniqueAsync(Email email)
    {
        return !await context.Users.AnyAsync(u => u.Email == email);
    }

    public void Insert(User user)
    {
        context.Users.Add(user);
    }
}
