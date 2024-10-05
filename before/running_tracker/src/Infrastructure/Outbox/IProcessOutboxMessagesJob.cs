namespace Infrastructure.Outbox;

public interface IProcessOutboxMessagesJob
{
    Task ProcessAsync();
}
