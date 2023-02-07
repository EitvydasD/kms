using Ardalis.Specification;
using KMS.Core.Aggregates.Role.Entities;
using KMS.Core.Aggregates.Role.Filters;

namespace KMS.Core.Aggregates.Role.Specs;

public class GetRolesSpec : Specification<RoleEntity>
{
    public GetRolesSpec(GetRoleFilter? filter = null)
    {
        if (filter is not null)
        {
            if (filter.IsDefault.HasValue)
            {
                Query.Where(x => x.IsDefault == filter.IsDefault);
            }
        }
    }
}
