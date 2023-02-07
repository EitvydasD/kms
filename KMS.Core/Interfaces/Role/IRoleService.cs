using KMS.Core.Aggregates.Role.Entities;

namespace KMS.Core.Interfaces.Role;

public interface IRoleService
{
    Task<ICollection<RoleEntity>> GetRoles(CancellationToken cancellationToken = default);

    Task<RoleEntity> GetRole(Guid roleId, CancellationToken cancellationToken = default);

    Task<RoleEntity> CreateRole(RoleEntity request, CancellationToken cancellationToken = default);

    Task<RoleEntity> UpdateRole(Guid roleId, RoleEntity request, CancellationToken cancellationToken = default);

    Task DeleteRole(Guid roleId, CancellationToken cancellationToken = default);
}
