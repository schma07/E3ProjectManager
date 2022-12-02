using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Schma.E3ProjectManager.Common;
using Schma.E3ProjectManager.Core.Application;
using Schma.E3ProjectManager.Core.Domain;
using Schma.E3ProjectManager.Infrastructure.Models;

namespace Schma.E3ProjectManager.Presentation.Web.Services
{
    public class AuthenticatedUserService : IAuthenticatedUserService
    {
        public AuthenticatedUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserId = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? null;
            Username = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value ?? null;
            Name = httpContextAccessor.HttpContext?.User?.FindFirst(ApplicationUser.FullNameClaimType)?.Value ?? null;
            Culture = httpContextAccessor.HttpContext?.User?.FindFirst(ApplicationUser.CultureClaimType)?.Value ?? null;
            UiCulture = httpContextAccessor.HttpContext?.User?.FindFirst(ApplicationUser.UiCultureClaimType)?.Value ?? null;
            Theme = httpContextAccessor.HttpContext?.User?.FindFirst(ApplicationUser.ThemeClaimType)?.Value.ToEnum<ThemeEnum>() ?? default;
            var profilePictureClaim = httpContextAccessor.HttpContext?.User?.FindFirst(ApplicationUser.ProfilePictureClaimType)?.Value;
            if (profilePictureClaim != null)
            {
                ProfilePicture = profilePictureClaim;
            }

            var roles = httpContextAccessor.HttpContext?.User?.Claims.Where(c => c.Type == ClaimTypes.Role);
            if (roles != null)
                Roles = roles.Select(r => r.Value.ToEnum<RoleEnum>());
        }

        public string UserId { get; }

        public string Username { get; }
        public string Name { get; }
        public string ProfilePicture { get; }
        public string Culture { get; set; }
        public string UiCulture { get; set; }
        public IEnumerable<RoleEnum> Roles { get; }
        public ThemeEnum Theme { get; private set; }

        public bool HasRole(RoleEnum role)
        {
            return Roles.Contains(role);
        }
    }
}