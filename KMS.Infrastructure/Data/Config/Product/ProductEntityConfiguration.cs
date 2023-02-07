using KMS.Core.Aggregates.Order.Entities;
using KMS.Core.Aggregates.Product.Entities;

namespace KMS.Infrastructure.Data.Config.Product;

public class ProductEntityConfiguration : BaseEntityConfiguration<ProductEntity>
{
    protected override void ConfigureEntity(EntityTypeBuilder<ProductEntity> builder)
    {
        builder.ToTable("Products");

        builder.Property(x => x.Title).IsRequired().HasColumnType("nvarchar(64)");
        builder.Property(x => x.Price).IsRequired().HasColumnType("decimal(18,2)");
        builder.Property(x => x.Quantity).IsRequired();
    }
}
