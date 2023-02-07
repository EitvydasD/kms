using KMS.Core.Aggregates.User.Entities;

namespace KMS.Core.Aggregates.Auth;

public class Authentication
{
    public Authentication(UserEntity user)
    {
        User = user;
    }

    public UserEntity User { get; }
}
