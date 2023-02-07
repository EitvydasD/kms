using KMS.Core.Aggregates.Auth;
using KMS.Core.Interfaces;
using System.Globalization;
using System.Security.Claims;

namespace KMS.Core.Services.Auth;

public static class AuthenticationClaimsFactory
{
    public static ICollection<Claim> FromAuthentication(Authentication authentication)
    {
        var claims = new List<Claim>
        {
            new Claim(nameof(ICallerAccessor.UserId), authentication.User.Id.ToString("D", CultureInfo.InvariantCulture))
        };
        claims.AddRange(authentication.User.GetPermissions().Select(permission => new Claim(PermissionsRequiredAttribute.PermissionType, permission)));
        return claims;
    }
}
