using Ardalis.Specification;
using KMS.Core.Aggregates.Role.Entities;

namespace KMS.Core.Aggregates.Role.Specs;

public class GetRoleSpec : Specification<RoleEntity>, ISingleResultSpecification
{
    public GetRoleSpec(Guid roleId)
    {
        Query
            .Include(x => x.Permissions)
            .Where(x => x.Id == roleId);
    }
}
