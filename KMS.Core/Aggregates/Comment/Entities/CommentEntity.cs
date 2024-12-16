using Ardalis.Specification;
using KMS.Core.Aggregates.Comment.Requests;
using KMS.Core.Aggregates.User.Entities;
using Utils.Library.Interfaces;

namespace KMS.Core.Aggregates.Comment.Entities;
public class CommentEntity : BaseEntity, IAggregateRoot
{
    public string? Title { get; set; }

    public string Text { get; set; } = null!;

    public Guid AuthorId { get; set; }
    
    public UserEntity? Author { get; set; }

    public Guid? ParentId { get; set; }

    public CommentEntity? Parent { get; set; }

    public static CommentEntity Create(PostCommentRequest request) => new()
    {
        Title = request.Title,
        Text = request.Text,
        ParentId = request.ParentId,
        AuthorId = request.AuthorId
    };
}