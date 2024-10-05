using Application.Abstractions.Caching;
using Application.Abstractions.Data;
using Application.Abstractions.Events;
using Application.Abstractions.Notifications;
using Application.Abstractions.Storage;
using Azure.Storage.Blobs;
using Hangfire;
using Hangfire.PostgreSql;
using Infrastructure.Caching;
using Infrastructure.Database;
using Infrastructure.Events;
using Infrastructure.Notifications;
using Infrastructure.Storage;
using Infrastructure.Time;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using SharedKernel;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddTransient<INotificationService, NotificationService>();

        AddDatabase(services, configuration);
        AddCaching(services, configuration);
        AddHealthChecks(services, configuration);
        AddMessaging(services);
        AddStorage(services, configuration);
        AddBackgroundJobs(services, configuration);
        
        return services;
    }

    private static void AddDatabase(IServiceCollection services, IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString("Database");
        Ensure.NotNullOrEmpty(connectionString);

        services.AddSingleton<IDbConnectionFactory>(_ =>
            new DbConnectionFactory(new NpgsqlDataSourceBuilder(connectionString).Build()));
    }

    private static void AddCaching(IServiceCollection services, IConfiguration configuration)
    {
        string redisConnectionString = configuration.GetConnectionString("Cache")!;

        services.AddStackExchangeRedisCache(options =>
            options.Configuration = redisConnectionString);

        services.AddSingleton<ICacheService, CacheService>();
    }

    private static void AddHealthChecks(IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddHealthChecks()
            .AddNpgSql(configuration.GetConnectionString("Database")!)
            .AddRedis(configuration.GetConnectionString("Cache")!);
    }

    private static void AddMessaging(IServiceCollection services)
    {
        services.AddSingleton<InMemoryMessageQueue>();
        services.AddTransient<IEventBus, EventBus>();
        services.AddHostedService<IntegrationEventProcessorJob>();
    }

    private static void AddStorage(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IBlobService, BlobService>();
        services.AddSingleton(_ => new BlobServiceClient(configuration.GetConnectionString("BlobStorage")));
    }

    private static void AddBackgroundJobs(IServiceCollection services, IConfiguration configuration)
    {
        services.AddHangfire(config =>
            config.UsePostgreSqlStorage(
                options => options.UseNpgsqlConnection(configuration.GetConnectionString("Database")!)));

        services.AddHangfireServer(options => options.SchedulePollingInterval = TimeSpan.FromSeconds(1));
    }
}
