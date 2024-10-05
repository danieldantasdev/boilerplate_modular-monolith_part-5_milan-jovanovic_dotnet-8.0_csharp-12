using Application.Abstractions.Messaging;
using Modules.Training.Application.Abstractions.Data;
using Modules.Training.Domain.Workouts;
using SharedKernel;

namespace Modules.Training.Application.Workouts.RemoveExercise;

internal sealed class RemoveExerciseCommandHandler(IWorkoutRepository workoutRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<RemoveExerciseCommand>
{
    public async Task<Result> Handle(RemoveExerciseCommand request, CancellationToken cancellationToken)
    {
        Workout? workout = await workoutRepository.GetByIdAsync(request.WorkoutId, cancellationToken);

        if (workout is null)
        {
            return Result.Failure(WorkoutErrors.NotFound(request.WorkoutId));
        }

        Exercise? exercise = workout.Exercises.Find(e => e.Id == request.ExerciseId);

        if (exercise is null)
        {
            return Result.Failure(ExerciseErrors.NotFound(request.ExerciseId));
        }

        workout.RemoveExercise(exercise);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
