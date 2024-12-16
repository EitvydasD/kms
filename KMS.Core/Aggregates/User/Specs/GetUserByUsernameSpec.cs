using Ardalis.Specification;

namespace KMS.Core.Aggregates.User.Specs;

public class GetUserByUsernameSpec : BaseGetUserSpec
{
    public GetUserByUsernameSpec(string username)
    {
        Query
            .Where(x => x.Username == username);
    }
}
