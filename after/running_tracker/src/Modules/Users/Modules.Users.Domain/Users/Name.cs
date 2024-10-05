using SharedKernel;

namespace Modules.Users.Domain.Users;

public sealed record Name
{
    public Name(string? value)
    {
        Ensure.NotNullOrEmpty(value);

        Value = value;
    }

    public string Value { get; }
}
