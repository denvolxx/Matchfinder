using System.Security.Claims;

namespace Matchfinder.Extensions
{
    public static class ClaimsPrincipleExtensions
    {
        public static string GetUsername(this ClaimsPrincipal user)
        {
            string? username = user.FindFirstValue(ClaimTypes.NameIdentifier);
            if (username == null)
            {
                throw new Exception("Cannot get username from token");
            }
            return username;
        }
    }
}
