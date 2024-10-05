using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Modules.Users.Domain.Followers;

namespace Modules.Users.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddUsersApplication(this IServiceCollection services)
    {
        services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly, includeInternalTypes: true);

        services.AddScoped<IFollowerService, FollowerService>();

        return services;
    }
}
