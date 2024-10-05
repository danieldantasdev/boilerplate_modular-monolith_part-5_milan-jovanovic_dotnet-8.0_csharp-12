using Application.Abstractions.Data;
using Hangfire;
using Hangfire.PostgreSql;
using Infrastructure.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Modules.Training.Infrastructure.Database;
using Modules.Users.Infrastructure.Database;
using Npgsql;
using Testcontainers.PostgreSql;
using Testcontainers.Redis;

namespace Api.FunctionalTests.Abstractions;

public class FunctionalTestWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder()
        .WithImage("postgres:latest")
        .WithDatabase("runtrackr")
        .WithUsername("postgres")
        .WithPassword("postgres")
        .Build();

    private readonly RedisContainer _redisContainer = new RedisBuilder()
        .WithImage("redis:7.0")
        .Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.AddHangfire(config =>
                config.UsePostgreSqlStorage(
                    options => options.UseNpgsqlConnection(_dbContainer.GetConnectionString())));

            services.RemoveAll(typeof(IDbConnectionFactory));
            services.AddSingleton<IDbConnectionFactory>(_ =>
                new DbConnectionFactory(new NpgsqlDataSourceBuilder(_dbContainer.GetConnectionString()).Build()));

            services.RemoveAll(typeof(DbContextOptions<UsersDbContext>));
            services.AddDbContext<UsersDbContext>(options =>
                options
                    .UseNpgsql(_dbContainer.GetConnectionString())
                    .UseSnakeCaseNamingConvention());

            services.RemoveAll(typeof(DbContextOptions<TrainingDbContext>));
            services.AddDbContext<TrainingDbContext>(options =>
                options
                    .UseNpgsql(_dbContainer.GetConnectionString())
                    .UseSnakeCaseNamingConvention());

            services.RemoveAll(typeof(RedisCacheOptions));
            services.AddStackExchangeRedisCache(redisCacheOptions =>
                redisCacheOptions.Configuration = _redisContainer.GetConnectionString());
        });
    }

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
        await _redisContainer.StartAsync();
    }

    public new async Task DisposeAsync()
    {
        await _dbContainer.StopAsync();
        await _redisContainer.StopAsync();
    }
}
