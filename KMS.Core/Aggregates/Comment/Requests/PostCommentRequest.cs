namespace KMS.Core.Aggregates.Comment.Requests;
public class PostCommentRequest
{
    public string? Title { get; set; }
    public string Text { get; set; } = null!;
    public Guid AuthorId { get; set; }
    public Guid? ParentId { get; set; }
}
