using System.Collections.Generic;
using System.Linq;
using Schma.E3ProjectManager.Core.Domain;

namespace Schma.E3ProjectManager.Presentation.Web.ViewModels
{
    public class TopBarViewModel
    {
        public string Username { get; set; }
        public IEnumerable<RoleEnum> Roles { get; set; }
        public string ProfilePicture { get; set; }
        public bool IsAdmin => Roles.Any(r => r >= RoleEnum.Admin);
        public bool IsSuperAdmin => Roles.Any(r => r >= RoleEnum.SuperAdmin);
    }
}
