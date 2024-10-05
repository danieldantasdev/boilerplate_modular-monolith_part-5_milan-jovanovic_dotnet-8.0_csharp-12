using Application.Abstractions.Events;
using MediatR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Events;

internal sealed class IntegrationEventProcessorJob(
    InMemoryMessageQueue queue,
    IPublisher publisher,
    ILogger<IntegrationEventProcessorJob> logger)
    : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await foreach (IIntegrationEvent integrationEvent in queue.Reader.ReadAllAsync(stoppingToken))
        {
            try
            {
                await publisher.Publish(integrationEvent, stoppingToken);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Something went wrong! {IntegrationEventId}", integrationEvent.Id);
            }
        }
    }
}
