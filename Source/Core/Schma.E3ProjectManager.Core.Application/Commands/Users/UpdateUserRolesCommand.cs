﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Schma.E3ProjectManager.Common;
using Schma.Messaging.Abstractions;

namespace Schma.E3ProjectManager.Core.Application.Commands
{
    public class UpdateUserRolesCommand : IRequest<Result>
    {
        public UpdateUserRolesCommand()
        {

        }

        public string Username { get; set; }
        public List<string> Roles { get; set; }
    }

    /// <summary>
    /// Update user roles command handler
    /// </summary>
    public class UpdateUserRolesCommandHandler : BaseMessageHandler<UpdateUserRolesCommand, Result>
    {
        private readonly IApplicationUserService _applicationUserService;

        public UpdateUserRolesCommandHandler(IApplicationUserService applicationUserService)
        {
            _applicationUserService = applicationUserService;
        }

        public override async Task<Result> HandleAsync(UpdateUserRolesCommand command)
        {
            return await _applicationUserService.UpdateRoles(command.Username, command.Roles);
        }
    }
}