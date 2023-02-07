using KMS.Core.Aggregates.Comment.Entities;
using KMS.Core.Aggregates.Comment.Requests;

namespace KMS.Core.Interfaces.Comment;
public interface ICommentService
{
    Task<ICollection<CommentEntity>> GetComments(Guid? parentId, CancellationToken cancellationToken = default);
    Task<CommentEntity> PostComment(PostCommentRequest request, CancellationToken cancellationToken = default);
    Task DeleteUserComments(Guid userId, CancellationToken cancellationToken = default);
}
