using KMS.Core.Aggregates.Trip;
using KMS.Core.Aggregates.Trip.Entities;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace KMS.Infrastructure.Data.Config.Trip;

public class TripEntityConfiguration : BaseEntityConfiguration<TripEntity>
{
    protected override void ConfigureEntity(EntityTypeBuilder<TripEntity> builder)
    {
        builder.ToTable("Trips");

        builder.Property(x => x.DepartedAt);
        builder.Property(x => x.ArrivedAt);

        builder.HasOne(x => x.Driver)
            .WithMany()
            .HasForeignKey(x => x.DriverId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(x => x.Status)
            .IsRequired()
            .HasConversion(x => x.Value, x => TripStatus.FromValue(x))
            .HasDefaultValue(TripStatus.Pending);

        builder.HasMany(x => x.Responsible)
            .WithOne(x => x.Trip)
            .OnDelete(DeleteBehavior.Cascade);
    }

    public static ValueConverter DateOnlyConverter { get; } = new ValueConverter<DateOnly, DateTime>(
        v => v.ToDateTime(TimeOnly.MinValue),
        v => DateOnly.FromDateTime(v));
}
