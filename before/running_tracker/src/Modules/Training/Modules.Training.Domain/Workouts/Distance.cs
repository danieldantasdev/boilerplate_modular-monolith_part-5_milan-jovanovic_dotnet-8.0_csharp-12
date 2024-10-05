using SharedKernel;

namespace Modules.Training.Domain.Workouts;

public sealed record Distance
{
    private const decimal OneKilometer = 1000.0m;

    public Distance(decimal meters)
    {
        Ensure.GreaterThanZero(meters);

        Meters = meters;
    }

    private Distance()
    {
    }

    public decimal Meters { get; init; }

    public decimal Kilometers => Meters / OneKilometer;
}
