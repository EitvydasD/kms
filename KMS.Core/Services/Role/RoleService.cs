using KMS.Core.Aggregates.Role.Entities;
using KMS.Core.Aggregates.Role.Specs;
using KMS.Core.Exceptions;
using KMS.Core.Interfaces.Role;
using Utils.Library.Interfaces;

namespace KMS.Core.Services.Role;

public class RoleService : IRoleService
{
    public RoleService(IRepository<RoleEntity> roleRepo)
    {
        RoleRepo = roleRepo ?? throw new ArgumentNullException(nameof(roleRepo));
    }

    private IRepository<RoleEntity> RoleRepo { get; }
    
    public async Task<ICollection<RoleEntity>> GetRoles(CancellationToken cancellationToken = default)
    {
        return await RoleRepo.ListAsync(cancellationToken);
    }

    public async Task<RoleEntity> GetRole(Guid roleId, CancellationToken cancellationToken = default)
    {
        var role = await RoleRepo.FirstOrDefaultAsync(new GetRoleSpec(roleId), cancellationToken);

        if (role is null)
        {
            throw new RoleNotFoundException(roleId);
        }

        return role;
    }

    public async Task<RoleEntity> CreateRole(RoleEntity role, CancellationToken cancellationToken = default)
    {
        return await RoleRepo.AddAsync(role, cancellationToken);
    }

    public async Task<RoleEntity> UpdateRole(Guid roleId, RoleEntity role, CancellationToken cancellationToken = default)
    {
        var roleEntity = await GetRole(roleId, cancellationToken);
        
        roleEntity.Update(role);
        
        await RoleRepo.SaveChangesAsync(cancellationToken);
        return roleEntity;
    }

    public async Task DeleteRole(Guid roleId, CancellationToken cancellationToken = default)
    {
        await RoleRepo.DeleteAsync(await GetRole(roleId, cancellationToken), cancellationToken);
    }
}
