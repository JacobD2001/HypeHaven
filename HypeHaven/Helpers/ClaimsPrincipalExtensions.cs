using System.Security.Claims;

namespace HypeHaven.Helpers
{
    public static class ClaimsPrincipalExtensions
    {
        /// <summary>
        /// Gets the user ID from the claims principal.
        /// </summary>
        /// <param name="user">The claims principal.</param>
        /// <returns>The user ID.</returns>
        public static string GetUserId(this ClaimsPrincipal user)
        {
            if(user.Identity.IsAuthenticated)
            return user.FindFirst(ClaimTypes.NameIdentifier).Value;

            return null;
        }
    }
}
