using FluentValidation;

namespace Modules.Training.Application.Workouts.RemoveExercise;

internal sealed class RemoveExerciseCommandValidator : AbstractValidator<RemoveExerciseCommand>
{
    public RemoveExerciseCommandValidator()
    {
        RuleFor(c => c.WorkoutId).NotEmpty().WithErrorCode(WorkoutErrorCodes.RemoveExercise.MissingWorkoutId);
        RuleFor(c => c.ExerciseId).NotEmpty().WithErrorCode(WorkoutErrorCodes.RemoveExercise.MissingExerciseId);
    }
}
