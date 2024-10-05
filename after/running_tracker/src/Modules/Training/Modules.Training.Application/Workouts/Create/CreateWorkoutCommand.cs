using Application.Abstractions.Messaging;

namespace Modules.Training.Application.Workouts.Create;

public sealed record CreateWorkoutCommand(Guid UserId, string Name) : ICommand<Guid>;
