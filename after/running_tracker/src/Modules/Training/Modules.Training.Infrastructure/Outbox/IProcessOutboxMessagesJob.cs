namespace Modules.Training.Infrastructure.Outbox;

public interface IProcessOutboxMessagesJob
{
    Task ProcessAsync();
}
