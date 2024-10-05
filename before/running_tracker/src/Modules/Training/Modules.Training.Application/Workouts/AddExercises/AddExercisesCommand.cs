using Application.Abstractions.Messaging;

namespace Modules.Training.Application.Workouts.AddExercises;

public sealed record AddExercisesCommand(Guid WorkoutId, List<ExerciseRequest> Exercises)
    : ICommand;
