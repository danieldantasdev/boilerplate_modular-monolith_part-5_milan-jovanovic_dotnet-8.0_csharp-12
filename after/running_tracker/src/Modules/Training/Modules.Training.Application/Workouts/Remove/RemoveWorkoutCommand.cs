using Application.Abstractions.Messaging;

namespace Modules.Training.Application.Workouts.Remove;

public sealed record RemoveWorkoutCommand(Guid WorkoutId) : ICommand;
