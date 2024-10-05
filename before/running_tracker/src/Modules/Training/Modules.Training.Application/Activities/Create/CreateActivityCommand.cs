using Application.Abstractions.Messaging;

namespace Modules.Training.Application.Activities.Create;

public sealed record CreateActivityCommand(
    Guid UserId,
    Guid WorkoutId,
    decimal DistanceInMeters,
    int DurationInSeconds) : ICommand<Guid>;
