using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Modules.Training.Infrastructure.Database;
using Modules.Users.Infrastructure.Database;

namespace Web.Api.Extensions;

public static class MigrationExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        using UsersDbContext usersContext =
            scope.ServiceProvider.GetRequiredService<UsersDbContext>();

        usersContext.Database.Migrate();

        using TrainingDbContext trainingContext =
            scope.ServiceProvider.GetRequiredService<TrainingDbContext>();

        trainingContext.Database.Migrate();
    }
}
