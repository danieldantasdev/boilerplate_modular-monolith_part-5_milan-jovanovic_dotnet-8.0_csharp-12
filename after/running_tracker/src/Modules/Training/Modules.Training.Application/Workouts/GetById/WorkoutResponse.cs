namespace Modules.Training.Application.Workouts.GetById;

public sealed record WorkoutResponse
{
    public Guid Id { get; init; }

    public Guid UserId { get; init; }

    public string Name { get; init; } = string.Empty;

    public List<ExerciseResponse> Exercises { get; init; } = new();
}
