using KMS.Core.Aggregates.User.Entities;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace KMS.Infrastructure.Data.Config.User;

public class UserEntityConfiguration : BaseEntityConfiguration<UserEntity>
{
    protected override void ConfigureEntity(EntityTypeBuilder<UserEntity> builder)
    {
        builder.ToTable("Users");
        
        builder.Property(x => x.Username).IsRequired().HasColumnType("nvarchar(128)");
        builder.HasIndex(x => x.Username).IsUnique();
        
        
        builder.Property(x => x.FirstName).IsRequired().HasColumnType("nvarchar(32)");
        builder.Property(x => x.LastName).IsRequired().HasColumnType("nvarchar(32)");
        builder.Property(x => x.Email).IsRequired().HasColumnType("nvarchar(128)");
        builder.Property(x => x.Phone).IsRequired().HasColumnType("nvarchar(32)");
        builder.Property(x => x.BirthDate).HasColumnType("date").HasConversion(DateOnlyConverter);

        builder.Property(x => x.PasswordHash).IsRequired().HasColumnType("varbinary(20)");
        builder.Property(x => x.PasswordSalt).IsRequired().HasColumnType("varbinary(32)");
        builder.Property(x => x.PasswordChangedAt).IsRequired();

        builder.HasMany(x => x.Roles)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }

    public static ValueConverter DateOnlyConverter { get; } = new ValueConverter<DateOnly, DateTime>(
        v => v.ToDateTime(TimeOnly.MinValue),
        v => DateOnly.FromDateTime(v));
}
