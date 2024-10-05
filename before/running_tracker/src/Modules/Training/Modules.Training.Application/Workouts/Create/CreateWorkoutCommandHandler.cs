using Application.Abstractions.Messaging;
using Modules.Training.Application.Abstractions.Data;
using Modules.Training.Domain.Users;
using Modules.Training.Domain.Workouts;
using Modules.Users.Api;
using SharedKernel;

namespace Modules.Training.Application.Workouts.Create;

internal sealed class CreateWorkoutCommandHandler(
    IUsersApi usersApi,
    IWorkoutRepository workoutRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateWorkoutCommand, Guid>
{
    public async Task<Result<Guid>> Handle(
        CreateWorkoutCommand request,
        CancellationToken cancellationToken)
    {
        UserResponse? user = await usersApi.GetAsync(request.UserId, cancellationToken);
        if (user is null)
        {
            return Result.Failure<Guid>(UserErrors.NotFound(request.UserId));
        }

        var workout = new Workout(Guid.NewGuid(), user.Id, request.Name);

        workoutRepository.Insert(workout);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return workout.Id;
    }
}
