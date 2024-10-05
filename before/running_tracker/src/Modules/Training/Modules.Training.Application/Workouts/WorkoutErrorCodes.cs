namespace Modules.Training.Application.Workouts;

public static class WorkoutErrorCodes
{
    public static class CreateWorkout
    {
        public const string MissingUserId = nameof(MissingUserId);
        public const string MissingName = nameof(MissingName);
        public const string NameTooLong = nameof(NameTooLong);
    }

    public static class RemoveWorkout
    {
        public const string MissingWorkoutId = nameof(MissingWorkoutId);
    }

    public static class AddExercises
    {
        public const string MissingExercises = nameof(MissingExercises);
        public const string InvalidExerciseType = nameof(InvalidExerciseType);
        public const string InvalidTargetType = nameof(InvalidTargetType);
    }

    public static class RemoveExercise
    {
        public const string MissingWorkoutId = nameof(MissingWorkoutId);
        public const string MissingExerciseId = nameof(MissingExerciseId);
    }
}
