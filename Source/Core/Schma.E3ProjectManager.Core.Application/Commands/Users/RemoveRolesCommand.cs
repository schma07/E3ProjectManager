using System.Collections.Generic;
using System.Threading.Tasks;
using Schma.E3ProjectManager.Common;
using Schma.E3ProjectManager.Core.Application.DTOs;
using Schma.Messaging.Abstractions;

namespace Schma.E3ProjectManager.Core.Application.Commands
{
    public record RemoveRolesCommand(string Username, List<string> Roles) : IRequest<Result>
    {
    }

    /// <summary>
    /// Remove role from user command handler
    /// </summary>
    public class RemoveRolesCommandHandler : BaseMessageHandler<RemoveRolesCommand, Result>
    {
        private readonly IApplicationUserService _applicationUserService;

        public RemoveRolesCommandHandler(IApplicationUserService applicationUserService)
        {
            _applicationUserService = applicationUserService;
        }

        public override async Task<Result> HandleAsync(RemoveRolesCommand command)
        {
            var request = new RoleAssignmentRequestDTO
            {
                Username = command.Username,
                Roles = command.Roles
            };

            return await _applicationUserService.RemoveRoles(request);
        }
    }
}