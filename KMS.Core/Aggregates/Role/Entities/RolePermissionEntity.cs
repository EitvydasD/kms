using Utils.Library.Interfaces;

namespace KMS.Core.Aggregates.Role.Entities;

public class RolePermissionEntity : IAggregateRoot
{
    public RoleEntity? Role { get; set; }
    public Guid RoleId { get; set; }

    public string PermissionId { get; set; } = null!;
}
