using Ardalis.Specification;
using KMS.Core.Aggregates.Comment.Entities;

namespace KMS.Core.Aggregates.Comment.Specs;
public class GetCommentsSpec : Specification<CommentEntity>
{
	public GetCommentsSpec(Guid? parentId)
	{
        if (parentId is not null)
        {
            Query
                .Where(x => x.ParentId == parentId);
        }
        else
        {
            Query
                .Where(x => x.ParentId == null);
        }

        Query
            .Include(x => x.Author)
            .Include(x => x.Parent)
            .OrderBy(x => x.CreatedAt);
    }
}
