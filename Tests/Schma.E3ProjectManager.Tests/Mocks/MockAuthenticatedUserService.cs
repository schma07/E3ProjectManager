using Schma.E3ProjectManager.Common;
using Schma.E3ProjectManager.Core.Application;
using Schma.E3ProjectManager.Core.Domain;

namespace Schma.E3ProjectManager.Tests.Mocks
{
    internal class MockAuthenticatedUserService : IAuthenticatedUserService
    {
        public string UserId => "TestingUser";

        public string Username => "Mr.Tester";

        public string Name => "Tester Tester";

        public IEnumerable<RoleEnum> Roles { get; set; } = new List<RoleEnum>();
        public string Culture => "en-GB";

        public string UiCulture => "en-GB";

        public ThemeEnum Theme => ThemeEnum.Dark;
    }
}
