using FluentAssertions;
using Schma.E3ProjectManager.Core.Application.Commands;
using Schma.E3ProjectManager.Tests;
using Xunit;

namespace Schma.E3ProjectManager.Core.Tests.Application.Commands
{
    public class DeactivateUserCommandTests : TestBase
    {
        [Fact]
        public async void DeactivateUser_WhenUserDoesNotExit_ShouldFailWithMessage()
        {
            var deactivateUserCommand = new DeactivateUserCommand("unknownUser");
            var result = await ServiceBus.SendAsync(deactivateUserCommand);

            result.Failed.Should().BeTrue();
            result.Message.Should().Be("User not found.");
        }

        [Fact]
        public async void DeactivateUser_WhenUserNotActive_ShouldFailWithMessage()
        {
            var deactivateUserCommand = new DeactivateUserCommand("inactiveUser");
            var result = await ServiceBus.SendAsync(deactivateUserCommand);

            result.Failed.Should().BeTrue();
            result.Message.Should().Be("User is not active.");
        }

        [Fact]
        public async void DeactivateUser_WhenUserActive_ShouldSucceed()
        {
            var deactivateUserCommand = new DeactivateUserCommand("activeUser");
            var result = await ServiceBus.SendAsync(deactivateUserCommand);

            result.Succeeded.Should().BeTrue();
        }

        [Fact]
        public async void DeactivateUser_WhenServiceThrowsException_ShouldFail()
        {

            var deactivateUserCommand = new DeactivateUserCommand("throwsException");
            var result = await ServiceBus.SendAsync(deactivateUserCommand);

            result.Succeeded.Should().BeFalse();
            result.Message.Should().Be("Error while trying to deactivate user.");
            result.Exception.Should().NotBeNull();
        }
    }
}