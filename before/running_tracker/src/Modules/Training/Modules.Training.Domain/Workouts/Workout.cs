using SharedKernel;

namespace Modules.Training.Domain.Workouts;

public sealed class Workout : Entity
{
    private readonly List<Exercise> _exercises = new();

    public Workout(Guid id, Guid userId, string name)
        : base(id)
    {
        UserId = userId;
        Name = name;
    }

    private Workout()
    {
    }

    public Guid UserId { get; private set; }

    public string Name { get; private set; }

    public List<Exercise> Exercises => _exercises.ToList();

    public Result AddExercise(
        ExerciseType exerciseType,
        TargetType targetType,
        decimal? distanceInMeters,
        int? durationInSeconds)
    {
        Result<Exercise> result = Exercise.Create(
            Id,
            exerciseType,
            targetType,
            distanceInMeters.HasValue ? new Distance(distanceInMeters.Value) : null,
            durationInSeconds.HasValue ? TimeSpan.FromSeconds(durationInSeconds.Value) : null);

        if (result.IsFailure)
        {
            return result;
        }

        _exercises.Add(result.Value);

        return Result.Success();
    }

    public void RemoveExercise(Exercise exercise)
    {
        _exercises.Remove(exercise);
    }

    public void ClearExercises()
    {
        _exercises.Clear();
    }
}
