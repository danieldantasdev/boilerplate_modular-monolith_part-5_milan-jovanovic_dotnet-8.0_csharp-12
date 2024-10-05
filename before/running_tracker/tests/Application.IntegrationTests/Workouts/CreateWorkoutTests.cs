using Application.IntegrationTests.Abstractions;
using FluentAssertions;
using Modules.Training.Application.Workouts.Create;
using Modules.Training.Domain.Workouts;
using Modules.Users.Application.Users.Create;
using SharedKernel;

namespace Application.IntegrationTests.Workouts;

public class CreateWorkoutTests : BaseIntegrationTest
{
    public CreateWorkoutTests(IntegrationTestWebAppFactory factory)
        : base(factory)
    {
    }

    [Fact]
    public async Task Handle_Should_CreateWorkout_WhenUserExists()
    {
        // Arrange
        var createUser = new CreateUserCommand(Faker.Internet.Email(), Faker.Internet.UserName(), true);
        Guid userId = (await Sender.Send(createUser)).Value;

        var command = new CreateWorkoutCommand(userId, "Test workout");

        // Act
        Result<Guid> result = await Sender.Send(command);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_Should_AddWorkoutToDatabase_WhenCommandIsValid()
    {
        // Arrange
        var createUser = new CreateUserCommand(Faker.Internet.Email(), Faker.Internet.UserName(), true);
        Guid userId = (await Sender.Send(createUser)).Value;

        var command = new CreateWorkoutCommand(userId, "Test workout");

        // Act
        Result<Guid> result = await Sender.Send(command);

        // Assert
        Workout? workout = await TrainingDbContext.Workouts.FindAsync(result.Value);

        workout.Should().NotBeNull();
    }
}
