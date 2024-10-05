using Application.Abstractions.Messaging;

namespace Modules.Training.Application.Workouts.RemoveExercise;

public sealed record RemoveExerciseCommand(Guid WorkoutId, Guid ExerciseId) : ICommand;
