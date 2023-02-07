using Ardalis.Specification;
using KMS.Core.Aggregates.Comment.Entities;

namespace KMS.Core.Aggregates.Comment.Specs;
public class GetUserCommentsSpec : Specification<CommentEntity>
{
	public GetUserCommentsSpec(Guid userId)
	{
        Query
            .Where(x => x.AuthorId == userId);
    }
}
