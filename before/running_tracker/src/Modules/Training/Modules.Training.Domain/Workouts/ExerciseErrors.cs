using SharedKernel;

namespace Modules.Training.Domain.Workouts;

public static class ExerciseErrors
{
    public static readonly Error MissingDuration = Error.NotFound(
        "Exercises.MissingDuration",
        "The exercise duration is missing");

    public static readonly Error MissingDistance = Error.NotFound(
        "Exercises.MissingDistance",
        "The exercise distance is missing");

    public static Error NotFound(Guid exerciseId) => Error.NotFound(
        "Exercises.NotFound",
        $"The exercise with the Id = '{exerciseId}' was not found");
}
