using Modules.Training.Domain.Workouts;
using Modules.Training.Infrastructure.Database;

namespace Modules.Training.Infrastructure.Repositories;

internal sealed class ExerciseRepository(TrainingDbContext context) : IExerciseRepository
{
    public void Insert(Exercise exercise)
    {
        context.Exercises.Add(exercise);
    }
}
