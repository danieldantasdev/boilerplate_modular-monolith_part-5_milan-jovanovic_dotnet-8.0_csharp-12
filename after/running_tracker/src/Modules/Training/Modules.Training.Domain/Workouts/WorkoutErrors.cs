using SharedKernel;

namespace Modules.Training.Domain.Workouts;

public static class WorkoutErrors
{
    public static Error NotFound(Guid workoutId) => Error.NotFound(
        "Workouts.NotFound",
        $"The workout with the Id = '{workoutId}' was not found");
}
