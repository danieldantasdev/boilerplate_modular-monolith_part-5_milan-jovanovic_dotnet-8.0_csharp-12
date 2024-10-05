using FluentValidation;

namespace Modules.Training.Application.Workouts.Create;

internal sealed class CreateWorkoutCommandValidator : AbstractValidator<CreateWorkoutCommand>
{
    public CreateWorkoutCommandValidator()
    {
        RuleFor(c => c.UserId)
            .NotEmpty().WithErrorCode(WorkoutErrorCodes.CreateWorkout.MissingUserId);

        RuleFor(c => c.Name)
            .NotEmpty().WithErrorCode(WorkoutErrorCodes.CreateWorkout.MissingName)
            .MaximumLength(100).WithErrorCode(WorkoutErrorCodes.CreateWorkout.NameTooLong);
    }
}
