using Bogus;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Modules.Training.Infrastructure.Database;
using Modules.Users.Infrastructure.Database;

namespace Application.IntegrationTests.Abstractions;

public abstract class BaseIntegrationTest : IClassFixture<IntegrationTestWebAppFactory>, IDisposable
{
    private readonly IServiceScope _scope;

    protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
    {
        _scope = factory.Services.CreateScope();
        Sender = _scope.ServiceProvider.GetRequiredService<ISender>();
        UsersDbContext = _scope.ServiceProvider.GetRequiredService<UsersDbContext>();
        TrainingDbContext = _scope.ServiceProvider.GetRequiredService<TrainingDbContext>();
        Faker = new Faker();
    }

    protected ISender Sender { get; }

    protected UsersDbContext UsersDbContext { get; }

    protected TrainingDbContext TrainingDbContext { get; }

    protected Faker Faker { get; }

    public void Dispose()
    {
        _scope.Dispose();
    }
}
