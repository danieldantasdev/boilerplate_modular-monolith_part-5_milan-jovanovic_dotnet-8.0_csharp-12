using Application.Abstractions.Events;
using Application.Abstractions.Messaging;
using Modules.Training.Application.Abstractions.Data;
using Modules.Training.Domain.Workouts;
using SharedKernel;

namespace Modules.Training.Application.Workouts.Remove;

internal sealed class RemoveWorkoutCommandHandler(
    IWorkoutRepository workoutRepository,
    IUnitOfWork unitOfWork,
    IEventBus eventBus)
    : ICommandHandler<RemoveWorkoutCommand>
{
    public async Task<Result> Handle(
        RemoveWorkoutCommand request,
        CancellationToken cancellationToken)
    {
        Workout? workout = await workoutRepository.GetByIdAsync(
            request.WorkoutId,
            cancellationToken);

        if (workout is null)
        {
            return Result.Failure(WorkoutErrors.NotFound(request.WorkoutId));
        }

        workoutRepository.Remove(workout);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        await eventBus.PublishAsync(new WorkoutRemovedIntegrationEvent(Guid.NewGuid(), workout.Id), cancellationToken);

        return Result.Success();
    }
}
