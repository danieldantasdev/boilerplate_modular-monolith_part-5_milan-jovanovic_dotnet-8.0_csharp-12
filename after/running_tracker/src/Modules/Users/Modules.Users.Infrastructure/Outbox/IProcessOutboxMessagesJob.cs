namespace Modules.Users.Infrastructure.Outbox;

public interface IProcessOutboxMessagesJob
{
    Task ProcessAsync();
}
