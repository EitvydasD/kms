using KMS.Core.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Utils.Library.Exceptions;

namespace KMS.Core;
public class PermissionsRequiredAttribute : TypeFilterAttribute
{
    public static string PermissionType => "Permissions";

    public PermissionsRequiredAttribute(params string[] permissions) : base(typeof(AnyPermissionRequiredFilter))
    {
        Arguments = new object[] { permissions };
    }
}

public class AnyPermissionRequiredFilter : IAsyncAuthorizationFilter
{
    public string[] Permissions { get; private set; }

    public AnyPermissionRequiredFilter(string[] permissions)
    {
        Permissions = permissions;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var principal = ClaimPricinpalProvider.GetPrincipal(context.HttpContext);
        if (principal == null)
        {
            return;
        }

        foreach (var permission in Permissions)
        {
            var authorized = principal.Claims.Any(x => x.Type == PermissionsRequiredAttribute.PermissionType && x.Value == permission.ToString());
            if (authorized)
            {
                return;
            }
        }
        throw new ForbiddenException("Access to this resource is denied.");
    }
}
