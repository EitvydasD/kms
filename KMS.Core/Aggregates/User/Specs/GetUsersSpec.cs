using Ardalis.Specification;
using KMS.Core.Aggregates.User.Entities;
using KMS.Core.Aggregates.User.Requests;

namespace KMS.Core.Aggregates.User.Specs;

public class GetUsersSpec : Specification<UserEntity>
{
    public GetUsersSpec(GetUserRequest request)
    {
        if (request.IsDriver == true)
        {
            Query
                .Where(x => x.Roles.Any(x => x.Role.Name == "Driver"));
        }

        Query
            .Include(x => x.Roles)
                .ThenInclude(x => x.Role)
            .OrderBy(x => x.FirstName)
                .ThenBy(x => x.LastName);
    }
}
