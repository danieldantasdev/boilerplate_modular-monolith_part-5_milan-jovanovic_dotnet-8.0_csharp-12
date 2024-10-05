using Modules.Training.Domain.Workouts;

namespace Modules.Training.Application.Workouts.GetById;

public sealed record ExerciseResponse
{
    public Guid ExerciseId { get; init; }

    public ExerciseType ExerciseType { get; init; }

    public TargetType TargetType { get; init; }

    public decimal? Distance { get; init; }

    public TimeSpan? Duration { get; init; }
}
