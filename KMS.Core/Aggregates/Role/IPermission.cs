namespace KMS.Core.Aggregates.Role;

public interface IPermission
{
    string Id { get; }
    bool HasPermission(string permissionId);
}
