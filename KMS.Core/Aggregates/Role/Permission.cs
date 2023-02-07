namespace KMS.Core.Aggregates.Role;

public class Permission : IPermission
{
    public Permission(string id)
    {
        Id = id;
    }

    public string Id { get; }
    public bool HasPermission(string permissionId) => Id.Equals(permissionId, StringComparison.OrdinalIgnoreCase);
}
