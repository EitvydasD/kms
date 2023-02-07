using Ardalis.Specification;

namespace KMS.Core.Aggregates.Users.Specs;

public class GetUserByUsernameSpec : BaseGetUserSpec
{
    public GetUserByUsernameSpec(string username)
    {
        Query
            .Where(x => x.Username == username);
    }
}
