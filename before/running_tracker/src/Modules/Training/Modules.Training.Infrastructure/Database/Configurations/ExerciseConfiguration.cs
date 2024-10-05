using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Training.Domain.Workouts;

namespace Modules.Training.Infrastructure.Database.Configurations;

internal sealed class ExerciseConfiguration : IEntityTypeConfiguration<Exercise>
{
    public void Configure(EntityTypeBuilder<Exercise> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Distance)
            .HasConversion(
                distance => distance != null ? distance.Meters : default(decimal?),
                meters => meters.HasValue ? new Distance(meters.Value) : default);
    }
}
