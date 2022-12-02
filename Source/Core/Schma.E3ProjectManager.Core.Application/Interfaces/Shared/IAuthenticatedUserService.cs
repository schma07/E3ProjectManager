using System.Collections.Generic;
using Schma.E3ProjectManager.Common;
using Schma.E3ProjectManager.Core.Domain;

namespace Schma.E3ProjectManager.Core.Application
{
    public interface IAuthenticatedUserService
    {
        string UserId { get; }
        public string Username { get; }
        public string Name { get; }
        public string Culture { get; }
        public string UiCulture { get; }
        public IEnumerable<RoleEnum> Roles { get; }
        public ThemeEnum Theme { get; }
    }
}