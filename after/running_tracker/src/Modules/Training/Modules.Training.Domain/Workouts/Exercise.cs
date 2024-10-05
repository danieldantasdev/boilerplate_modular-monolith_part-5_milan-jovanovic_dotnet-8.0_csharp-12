using SharedKernel;

namespace Modules.Training.Domain.Workouts;

public sealed class Exercise : Entity
{
    private Exercise(
        Guid id,
        Guid workoutId,
        ExerciseType exerciseType,
        TargetType targetType,
        Distance? distance,
        TimeSpan? duration)
        : base(id)
    {
        WorkoutId = workoutId;
        ExerciseType = exerciseType;
        TargetType = targetType;
        Distance = distance;
        Duration = duration;
    }

    private Exercise()
    {
    }

    public Guid WorkoutId { get; private set; }

    public ExerciseType ExerciseType { get; private set; }

    public TargetType TargetType { get; private set; }

    public Distance? Distance { get; private set; }

    public TimeSpan? Duration { get; private set; }

    public static Result<Exercise> Create(
        Guid workoutId,
        ExerciseType exerciseType,
        TargetType targetType,
        Distance? distance,
        TimeSpan? duration)
    {
        if (targetType == TargetType.Distance && distance is null)
        {
            return Result.Failure<Exercise>(ExerciseErrors.MissingDistance);
        }

        if (targetType == TargetType.Time && duration is null)
        {
            return Result.Failure<Exercise>(ExerciseErrors.MissingDuration);
        }

        var exercise = new Exercise(
            Guid.NewGuid(),
            workoutId,
            exerciseType,
            targetType,
            distance,
            duration);

        return exercise;
    }
}
