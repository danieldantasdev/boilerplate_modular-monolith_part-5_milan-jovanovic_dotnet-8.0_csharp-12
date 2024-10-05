using Application.Abstractions.Messaging;

namespace Modules.Users.Application.Users.Create;

public sealed record CreateUserCommand(string Email, string Name, bool HasPublicProfile)
    : ICommand<Guid>;
