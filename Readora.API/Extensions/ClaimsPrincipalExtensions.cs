using System.Security.Claims;

namespace Readora.API.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static bool TryGetUserId(this ClaimsPrincipal user, out int userId)
    {
        var userIdString = user.FindFirstValue(ClaimTypes.NameIdentifier);

        return int.TryParse(userIdString, out userId);
    }
}
