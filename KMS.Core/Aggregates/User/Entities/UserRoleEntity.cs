using KMS.Core.Aggregates.Role.Entities;
using Utils.Library.Interfaces;

namespace KMS.Core.Aggregates.User.Entities;

public class UserRoleEntity : IAggregateRoot
{
    public UserEntity? User { get; set; }
    public Guid UserId { get; set; }

    public RoleEntity? Role { get; set; }
    public Guid RoleId { get; set; }
}
