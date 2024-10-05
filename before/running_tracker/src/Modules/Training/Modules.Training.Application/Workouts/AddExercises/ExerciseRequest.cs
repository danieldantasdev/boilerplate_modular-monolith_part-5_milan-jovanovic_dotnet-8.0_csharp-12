using Modules.Training.Domain.Workouts;

namespace Modules.Training.Application.Workouts.AddExercises;

public sealed record ExerciseRequest(
    ExerciseType ExerciseType,
    TargetType TargetType,
    decimal? DistanceInMeters,
    int? DurationInSeconds);
