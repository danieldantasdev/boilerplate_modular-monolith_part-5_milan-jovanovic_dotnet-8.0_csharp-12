using FluentValidation;

namespace Modules.Training.Application.Workouts.AddExercises;

internal sealed class AddExercisesCommandValidator : AbstractValidator<AddExercisesCommand>
{
    public AddExercisesCommandValidator()
    {
        RuleFor(c => c.Exercises)
            .NotEmpty().WithErrorCode(WorkoutErrorCodes.AddExercises.MissingExercises);

        RuleForEach(c => c.Exercises)
            .ChildRules(e =>
            {
                e.RuleFor(r => r.ExerciseType)
                    .IsInEnum().WithErrorCode(WorkoutErrorCodes.AddExercises.InvalidExerciseType);

                e.RuleFor(r => r.TargetType)
                    .IsInEnum().WithErrorCode(WorkoutErrorCodes.AddExercises.InvalidTargetType);
            });
    }
}
