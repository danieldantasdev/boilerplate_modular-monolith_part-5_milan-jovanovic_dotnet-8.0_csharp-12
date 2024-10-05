using Infrastructure.Outbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Modules.Training.Application.Abstractions.Data;
using Modules.Training.Domain.Activities;
using Modules.Training.Domain.Workouts;
using Modules.Training.Infrastructure.Database;
using Modules.Training.Infrastructure.Repositories;
using SharedKernel;

namespace Modules.Training.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddTrainingInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.TryAddSingleton<InsertOutboxMessagesInterceptor>();

        string? connectionString = configuration.GetConnectionString("Database");
        Ensure.NotNullOrEmpty(connectionString);

        services.AddDbContext<TrainingDbContext>(
            (sp, options) => options
                .UseNpgsql(connectionString)
                .UseSnakeCaseNamingConvention()
                .AddInterceptors(sp.GetRequiredService<InsertOutboxMessagesInterceptor>()));

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<TrainingDbContext>());
        
        services.AddScoped<IWorkoutRepository, WorkoutRepository>();
        services.AddScoped<IExerciseRepository, ExerciseRepository>();
        services.AddScoped<IActivityRepository, ActivityRepository>();

        return services;
    }
}
