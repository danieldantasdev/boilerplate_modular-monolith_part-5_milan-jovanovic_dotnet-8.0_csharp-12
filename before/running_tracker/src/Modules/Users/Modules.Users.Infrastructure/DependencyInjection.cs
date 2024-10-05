using Infrastructure.Outbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Modules.Users.Api;
using Modules.Users.Application.Abstractions.Data;
using Modules.Users.Domain.Followers;
using Modules.Users.Domain.Users;
using Modules.Users.Infrastructure.Api;
using Modules.Users.Infrastructure.Database;
using Modules.Users.Infrastructure.Repositories;
using SharedKernel;

namespace Modules.Users.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddUsersInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.TryAddSingleton<InsertOutboxMessagesInterceptor>();

        string? connectionString = configuration.GetConnectionString("Database");
        Ensure.NotNullOrEmpty(connectionString);

        services.AddDbContext<UsersDbContext>(
            (sp, options) => options
                .UseNpgsql(connectionString)
                .UseSnakeCaseNamingConvention()
                .AddInterceptors(sp.GetRequiredService<InsertOutboxMessagesInterceptor>()));

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<UsersDbContext>());

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IFollowerRepository, FollowerRepository>();

        services.AddScoped<IUsersApi, UsersApi>();

        return services;
    }
}
