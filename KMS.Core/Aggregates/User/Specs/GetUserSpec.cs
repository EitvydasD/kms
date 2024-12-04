using Ardalis.Specification;

namespace KMS.Core.Aggregates.User.Specs;

public class GetUserSpec : BaseGetUserSpec
{
    public GetUserSpec(Guid userId)
    {
        Query
            .Where(x => x.Id == userId);
    }
}
