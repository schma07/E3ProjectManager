using System;
using System.Security.Claims;

namespace Schma.E3ProjectManager.Infrastructure.Shared.Extensions
{
    public static class PrincipalExtensions
    {
        public static string GetCulture(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            return principal.FindFirstValue("localization:culture");
        }

        public static string GetUICulture(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            return principal.FindFirstValue("localization:uiculture");
        }
    }
}
