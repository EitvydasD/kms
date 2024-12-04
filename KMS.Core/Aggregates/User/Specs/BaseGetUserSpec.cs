using Ardalis.Specification;
using KMS.Core.Aggregates.User.Entities;

namespace KMS.Core.Aggregates.User.Specs;

public abstract class BaseGetUserSpec : Specification<UserEntity>, ISingleResultSpecification
{
    public BaseGetUserSpec()
    {
        Query
            .Include(x => x.Roles)
                .ThenInclude(x => x.Role)
                    .ThenInclude(x => x.Permissions);
    }
}
