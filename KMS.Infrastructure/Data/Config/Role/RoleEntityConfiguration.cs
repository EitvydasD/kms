using KMS.Core.Aggregates.Role.Entities;

namespace KMS.Infrastructure.Data.Config.Role;

public class RoleEntityConfiguration : BaseEntityConfiguration<RoleEntity>
{
    protected override void ConfigureEntity(EntityTypeBuilder<RoleEntity> builder)
    {
        builder.ToTable("Roles");

        builder.Property(x => x.Name).HasColumnType("nvarchar(100)").IsRequired();
        builder.Property(x => x.Description).HasColumnType("nvarchar(256)");
        builder.Property(x => x.IsDefault).IsRequired().HasDefaultValue(false);

        builder
            .HasMany(x => x.Permissions)
            .WithOne(x => x.Role)
            .HasForeignKey(x => x.RoleId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
