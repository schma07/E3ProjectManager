﻿using System;
using System.Threading.Tasks;
using FluentAssertions;
using Schma.E3ProjectManager.Common;
using Schma.E3ProjectManager.Core.Application.Commands;
using Schma.E3ProjectManager.Tests;
using Xunit;

namespace Schma.E3ProjectManager.Core.Tests.Application.Commands
{
    public class CreateUserTests : TestBase
    {
        public CreateUserTests()
        {

        }
        [Fact]
        public async Task CreateUser_WhenPasswordIsNull_ShouldFail()
        {
            var command = new CreateUserCommand(
                "testUser",
                "testName",
                "test@test.com",
                null,
                null);

            Result result = await ServiceBus.SendAsync(command);

            result.Succeeded.Should().BeFalse();
            result.Exception.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public async Task CreateUser_WhenRequestIsValid_ShouldSucceed()
        {
            var command = new CreateUserCommand(
                "testUser",
                Guid.NewGuid().ToString("N"),
                 "test@test.com",
                "test test",
                null);

            Result result = await ServiceBus.SendAsync(command);

            result.Succeeded.Should().BeTrue();
            result.Exception.Should().BeNull();
        }
    }
}