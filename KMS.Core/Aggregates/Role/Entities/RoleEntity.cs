using Utils.Library.Interfaces;

namespace KMS.Core.Aggregates.Role.Entities;

public class RoleEntity : BaseEntity, IAggregateRoot
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }

    public bool IsDefault { get; set; }

    public List<RolePermissionEntity> Permissions { get; private set; } = new();

    public void Update(RoleEntity request)
    {
        Name = request.Name;
        Description = request.Description;
    }

    public void SetPermissions(IEnumerable<string> permissions)
    {
        Permissions = permissions.Select(x => new RolePermissionEntity { PermissionId = x, RoleId = Id }).ToList();
    }

}
