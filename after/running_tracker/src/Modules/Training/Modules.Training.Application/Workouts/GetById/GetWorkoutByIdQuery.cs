using Application.Abstractions.Messaging;

namespace Modules.Training.Application.Workouts.GetById;

public sealed record GetWorkoutByIdQuery(Guid WorkoutId) : IQuery<WorkoutResponse>;
