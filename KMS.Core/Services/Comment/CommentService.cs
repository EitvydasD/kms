using KMS.Core.Aggregates.Comment.Entities;
using KMS.Core.Aggregates.Comment.Requests;
using KMS.Core.Aggregates.Comment.Specs;
using KMS.Core.Interfaces.Comment;
using Utils.Library.Interfaces;

namespace KMS.Core.Services.Comment;
public class CommentService : ICommentService
{
    public CommentService(IRepository<CommentEntity> commentRepo)
    {
        CommentRepo = commentRepo ?? throw new ArgumentNullException(nameof(commentRepo));
    }

    private IRepository<CommentEntity> CommentRepo { get; }

    public async Task<ICollection<CommentEntity>> GetComments(Guid? parentId, CancellationToken cancellationToken = default)
    {
        var spec = new GetCommentsSpec(parentId);
        return await CommentRepo.ListAsync(spec, cancellationToken);
    }

    public async Task<CommentEntity> PostComment(PostCommentRequest request, CancellationToken cancellationToken = default)
    {
        var entity = CommentEntity.Create(request);

        return await CommentRepo.AddAsync(entity, cancellationToken);
    }

    public async Task DeleteUserComments(Guid userId, CancellationToken cancellationToken = default)
    {
        var comments = await CommentRepo.ListAsync(new GetUserCommentsSpec(userId), cancellationToken);
        await CommentRepo.DeleteRangeAsync(comments, cancellationToken);
    }
}