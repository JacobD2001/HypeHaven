using System.Security.Claims;

namespace HypeHaven.Helpers
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetUserId(this ClaimsPrincipal user)
        {
            if(user.Identity.IsAuthenticated)
            return user.FindFirst(ClaimTypes.NameIdentifier).Value;

            return null;
        }
    }
}
