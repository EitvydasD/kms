using KMS.API.Models.Role;
using KMS.Core;
using KMS.Core.Aggregates.Role;
using KMS.Core.Interfaces.Role;
using Microsoft.AspNetCore.Mvc;

namespace KMS.API.Controllers;

[ApiController]
[PermissionsRequired(nameof(PermissionId.RoleView))]
[Route("api/role")]
public class RoleController : BaseController
{
    public RoleController(IRoleService roleService)
    {
        RoleService = roleService ?? throw new ArgumentNullException(nameof(roleService));
    }

    private IRoleService RoleService { get; }

    [HttpGet]
    public async Task<ICollection<RoleModel>> GetRoles(CancellationToken cancellationToken = default)
    {
        var response = await RoleService.GetRoles(cancellationToken);
        return Mapper.Map<ICollection<RoleModel>>(response);
    }
}
