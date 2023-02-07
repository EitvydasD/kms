using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using KMS.Core;

namespace KMS.Infrastructure.Data.Config;

public abstract class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        ConfigureEntity(builder);

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");

        builder.Property(x => x.CreatedAt).IsRequired().HasDefaultValueSql("SYSDATETIMEOFFSET()");
        builder.Property(x => x.ModifiedAt).IsRequired().HasDefaultValueSql("SYSDATETIMEOFFSET()");

    }
    protected abstract void ConfigureEntity(EntityTypeBuilder<TEntity> builder);
}
