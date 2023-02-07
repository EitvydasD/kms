using KMS.Core.Aggregates.Comment.Entities;

namespace KMS.Infrastructure.Data.Config.Comment;

public class CommentEntityConfiguration : BaseEntityConfiguration<CommentEntity>
{
    protected override void ConfigureEntity(EntityTypeBuilder<CommentEntity> builder)
    {
        builder.ToTable("Comments");

        builder.Property(x => x.Title).HasColumnType("nvarchar(64)");
        builder.Property(x => x.Text).IsRequired().HasColumnType("nvarchar(512)");
        
        builder.HasOne(x => x.Author)
            .WithMany()
            .HasForeignKey(x => x.AuthorId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Parent)
            .WithMany()
            .HasForeignKey(x => x.ParentId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
