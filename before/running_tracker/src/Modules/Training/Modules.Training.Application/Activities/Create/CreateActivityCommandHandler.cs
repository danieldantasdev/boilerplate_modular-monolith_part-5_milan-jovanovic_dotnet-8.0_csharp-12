using Application.Abstractions.Messaging;
using Modules.Training.Application.Abstractions.Data;
using Modules.Training.Domain.Activities;
using Modules.Training.Domain.Users;
using Modules.Training.Domain.Workouts;
using Modules.Users.Api;
using SharedKernel;

namespace Modules.Training.Application.Activities.Create;

internal sealed class CreateActivityCommandHandler(
    IUsersApi usersApi,
    IWorkoutRepository workoutRepository,
    IActivityRepository activityRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateActivityCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateActivityCommand request, CancellationToken cancellationToken)
    {
        UserResponse? user = await usersApi.GetAsync(request.UserId, cancellationToken);
        if (user is null)
        {
            return Result.Failure<Guid>(UserErrors.NotFound(request.UserId));
        }

        Workout? workout = await workoutRepository.GetByIdAsync(request.WorkoutId, cancellationToken);
        if (workout is null)
        {
            return Result.Failure<Guid>(WorkoutErrors.NotFound(request.WorkoutId));
        }

        var activity = Activity.Create(
            user.Id,
            workout.Id,
            new Distance(request.DistanceInMeters),
            TimeSpan.FromSeconds(request.DurationInSeconds));

        activityRepository.Insert(activity);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return activity.Id;
    }
}
