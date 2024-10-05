using Application.Abstractions.Events;

namespace Modules.Training.Application.Workouts.Remove;

public record WorkoutRemovedIntegrationEvent(Guid Id, Guid WorkoutId) : IntegrationEvent(Id);
