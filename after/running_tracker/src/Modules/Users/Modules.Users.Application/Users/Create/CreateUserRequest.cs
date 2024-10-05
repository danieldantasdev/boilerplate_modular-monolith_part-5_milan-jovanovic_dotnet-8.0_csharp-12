namespace Modules.Users.Application.Users.Create;

public sealed record CreateUserRequest(string Email, string Name, bool HasPublicProfile);
