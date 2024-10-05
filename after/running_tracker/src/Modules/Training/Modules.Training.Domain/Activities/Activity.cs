using Modules.Training.Domain.Workouts;
using SharedKernel;

namespace Modules.Training.Domain.Activities;

public sealed class Activity : Entity
{
    private Activity(Guid id, Guid userId, Guid workoutId, Distance distance, TimeSpan duration)
        : base(id)
    {
        UserId = userId;
        WorkoutId = workoutId;
        Distance = distance;
        Duration = duration;
    }

    private Activity()
    {
    }

    public Guid UserId { get; private set; }

    public Guid WorkoutId { get; private set; }

    public Distance Distance { get; private set; }

    public TimeSpan Duration { get; private set; }

    public static Activity Create(Guid userId, Guid workoutId, Distance distance, TimeSpan duration)
    {
        var activity = new Activity(
            Guid.NewGuid(),
            userId,
            workoutId,
            distance,
            duration);

        return activity;
    }
}
