using KMS.Core.Aggregates.Trip.Entities;

namespace KMS.Infrastructure.Data.Config.Trip;

public class UserTripEntityConfiguration : IEntityTypeConfiguration<UserTripEntity>
{
    public void Configure(EntityTypeBuilder<UserTripEntity> builder)
    {
        builder.ToTable("UserTrips");
        builder.HasKey(x => new { x.UserId, x.TripId });

        builder
            .HasOne(x => x.User)
            .WithMany()
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(x => x.Trip)
            .WithMany(x => x.Responsible)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
