using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Users.Domain.Followers;
using Modules.Users.Domain.Users;

namespace Modules.Users.Infrastructure.Database.Configurations;

internal sealed class FollowerConfiguration : IEntityTypeConfiguration<Follower>
{
    public void Configure(EntityTypeBuilder<Follower> builder)
    {
        builder.HasKey(f => new { f.UserId, f.FollowedId });

        builder.HasIndex(f => new { f.FollowedId, f.UserId });

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(f => f.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(f => f.FollowedId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Ignore(f => f.Id);
    }
}
