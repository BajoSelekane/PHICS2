using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Infrastructure.Authentication;

internal static class ClaimsPrincipalExtensions
{
    public static Guid GetUserId(this ClaimsPrincipal? principal)
    {
        if (principal is null)
            throw new ApplicationException("User principal is null");

        var userIdClaim =
            principal.FindFirst(ClaimTypes.NameIdentifier)?.Value
            ?? principal.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

        if (string.IsNullOrWhiteSpace(userIdClaim))
            throw new ApplicationException("User id claim is missing");

        if (!Guid.TryParse(userIdClaim, out var userId))
            throw new ApplicationException("User id is not a valid GUID");

        return userId;
    }


    //public static Guid GetUserId(this ClaimsPrincipal? principal)
    //{
    //    string? userId = principal?.FindFirstValue(JwtRegisteredClaimNames.Sub);

    //    return Guid.TryParse(userId, out Guid parsedUserId) ?
    //        parsedUserId :
    //        throw new ApplicationException("User id is unavailable");
    //}
}
