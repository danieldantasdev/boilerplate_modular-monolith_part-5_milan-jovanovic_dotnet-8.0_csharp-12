using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Modules.Training.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddTrainingApplication(this IServiceCollection services)
    {
        services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly, includeInternalTypes: true);

        return services;
    }
}
