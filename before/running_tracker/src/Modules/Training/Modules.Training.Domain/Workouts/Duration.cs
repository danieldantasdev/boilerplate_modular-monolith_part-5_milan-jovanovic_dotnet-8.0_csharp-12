using SharedKernel;

namespace Modules.Training.Domain.Workouts;

public sealed record Duration
{
    public Duration(DateTime startUtc, DateTime endUtc)
    {
        Ensure.StartDatePrecedesEndDate(startUtc, endUtc);

        StartUtc = startUtc;
        EndUtc = endUtc;
    }

    public DateTime StartUtc { get; init; }

    public DateTime EndUtc { get; init; }

    public TimeSpan GetDuration() => EndUtc - StartUtc;
}
