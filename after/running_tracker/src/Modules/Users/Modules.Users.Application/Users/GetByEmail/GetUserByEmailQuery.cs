using Application.Abstractions.Messaging;

namespace Modules.Users.Application.Users.GetByEmail;

public sealed record GetUserByEmailQuery(string Email) : IQuery<UserResponse>;
