using System.Collections.Generic;
using FluentAssertions;
using Schma.E3ProjectManager.Core.Application.Commands;
using Schma.E3ProjectManager.Core.Domain;
using Xunit;

namespace Schma.E3ProjectManager.Tests.Application.Commands
{
    public class AddRolesTests : TestBase
    {
        public AddRolesTests()
        {

        }
        [Fact]
        public async void AddRoles_WhenRoleIsValidAndNotAssigned_ShouldSucceed()
        {
            var command = new AddRolesCommand("basicUser", new List<string>
                {
                    RoleEnum.SuperAdmin.ToString()
                });

            var result = await ServiceBus.SendAsync(command);

            result.Succeeded.Should().BeTrue();
        }

        [Fact]
        public async void AddRoles_WhenRoleIsInvalid_ShouldFail()
        {
            var command = new AddRolesCommand("basicUser", new List<string>
                {
                    "InvalidRole"
                });

            var result = await ServiceBus.SendAsync(command);

            result.Succeeded.Should().BeFalse();
            result.Errors.Should().Contain(e => e.Error == $"Role InvalidRole is invalid.");
        }
    }
}