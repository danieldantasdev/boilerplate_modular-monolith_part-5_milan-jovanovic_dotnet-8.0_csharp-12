using Modules.Training.Domain.Activities;
using Modules.Training.Infrastructure.Database;

namespace Modules.Training.Infrastructure.Repositories;

internal sealed class ActivityRepository(TrainingDbContext context) : IActivityRepository
{
    public void Insert(Activity activity)
    {
        context.Activities.Add(activity);
    }
}
