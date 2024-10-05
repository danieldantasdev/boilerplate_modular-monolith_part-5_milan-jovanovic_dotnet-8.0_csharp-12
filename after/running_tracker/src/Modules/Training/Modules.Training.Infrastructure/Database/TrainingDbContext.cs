using System.Data;
using Infrastructure.Database.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Modules.Training.Application.Abstractions.Data;
using Modules.Training.Domain.Activities;
using Modules.Training.Domain.Workouts;

namespace Modules.Training.Infrastructure.Database;

public sealed class TrainingDbContext(DbContextOptions<TrainingDbContext> options)
    : DbContext(options), IUnitOfWork
{
    public DbSet<Workout> Workouts { get; set; }

    public DbSet<Exercise> Exercises { get; set; }

    public DbSet<Activity> Activities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schema.Training);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TrainingDbContext).Assembly);
        modelBuilder.ApplyConfiguration(new OutboxMessageConfiguration());
    }

    public async Task<IDbTransaction> BeginTransactionAsync()
    {
        return (await Database.BeginTransactionAsync()).GetDbTransaction();
    }
}
