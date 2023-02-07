using KMS.API.Models.User;

namespace KMS.API.Models.Comment;

public class CommentModel
{
    public Guid Id { get; init; }
    public string? Title { get; init; }
    public string Text { get; init; } = null!;
    public UserModel? Author { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
}
