using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace KMS.Core.Attributes;

public static class ClaimPricinpalProvider
{
    public static ClaimsPrincipal? GetPrincipal(HttpContext context)
    {
        if (context.User.Identity!.IsAuthenticated)
        {
            return context.User;
        }

        if (context.Request.Query.TryGetValue("token", out var token))
        {
            return GetPrincipalFromToken(context, token);
        }

        return null;
    }

    public static ClaimsPrincipal? GetPrincipalFromToken(HttpContext context, string token)
    {
        var validationParameters = context.RequestServices.GetRequiredService<TokenValidationParameters>();

        try
        {
            var handler = new JwtSecurityTokenHandler();
            return handler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
        }
        catch (Exception)
        {
            return null;
        }
    }
}
