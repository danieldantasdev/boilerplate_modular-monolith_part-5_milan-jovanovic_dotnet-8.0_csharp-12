using FluentValidation;

namespace Modules.Training.Application.Workouts.Remove;

internal sealed class RemoveWorkoutCommandValidator : AbstractValidator<RemoveWorkoutCommand>
{
    public RemoveWorkoutCommandValidator()
    {
        RuleFor(c => c.WorkoutId)
            .NotEmpty().WithErrorCode(WorkoutErrorCodes.RemoveWorkout.MissingWorkoutId);
    }
}
