using Hangfire;

namespace Web.Api.Extensions;

public static class BackgroundJobExtensions
{
    public static IApplicationBuilder UseBackgroundJobs(this WebApplication app)
    {
        IRecurringJobManager recurringJobManager = app.Services.GetRequiredService<IRecurringJobManager>();

        recurringJobManager.AddOrUpdate<Modules.Users.Infrastructure.Outbox.IProcessOutboxMessagesJob>(
            "users-outbox-processor",
            job => job.ProcessAsync(),
            app.Configuration["BackgroundJobs:Outbox:Schedule"]);

        recurringJobManager.AddOrUpdate<Modules.Training.Infrastructure.Outbox.IProcessOutboxMessagesJob>(
            "training-outbox-processor",
            job => job.ProcessAsync(),
            app.Configuration["BackgroundJobs:Outbox:Schedule"]);

        return app;
    }
}
