using Hangfire;
using Infrastructure.Outbox;

namespace Web.Api.Extensions;

public static class BackgroundJobExtensions
{
    public static IApplicationBuilder UseBackgroundJobs(this WebApplication app)
    {
        IRecurringJobManager recurringJobManager = app.Services.GetRequiredService<IRecurringJobManager>();

        recurringJobManager.AddOrUpdate<IProcessOutboxMessagesJob>(
            "outbox-processor",
            job => job.ProcessAsync(),
            app.Configuration["BackgroundJobs:Outbox:Schedule"]);

        return app;
    }
}
