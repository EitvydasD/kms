using KMS.Core.Aggregates.Order;
using KMS.Core.Aggregates.Order.Entities;

namespace KMS.Infrastructure.Data.Config.Order;

public class OrderEntityConfiguration : BaseEntityConfiguration<OrderEntity>
{
    protected override void ConfigureEntity(EntityTypeBuilder<OrderEntity> builder)
    {
        builder.ToTable("Orders");

        builder.Property(x => x.OrderDate).IsRequired().HasColumnType("datetime").HasDefaultValueSql("SYSDATETIMEOFFSET()");
        builder.Property(x => x.Total).IsRequired().HasColumnType("decimal(18,2)");
        builder.Property(x => x.Status).IsRequired()
            .HasConversion(x => x.Value, x => OrderStatus.FromValue(x))
            .HasDefaultValue(OrderStatus.Pending);

        builder.HasMany(x => x.Products)
            .WithOne()
            .HasForeignKey(x => x.Id)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
