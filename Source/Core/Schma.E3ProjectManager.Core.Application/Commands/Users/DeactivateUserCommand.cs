using System.Threading.Tasks;
using Schma.E3ProjectManager.Common;
using Schma.Messaging.Abstractions;

namespace Schma.E3ProjectManager.Core.Application.Commands
{
    public record DeactivateUserCommand(string Username) : IRequest<Result>
    {
    }

    /// <summary>
    /// Deactivate User Command Handler
    /// </summary>
    public class DeactivateUserCommandHandler : BaseMessageHandler<DeactivateUserCommand, Result>
    {
        private readonly IApplicationUserService _applicationUserService;

        public DeactivateUserCommandHandler(IApplicationUserService applicationUserService)
        {
            _applicationUserService = applicationUserService;
        }

        public override async Task<Result> HandleAsync(DeactivateUserCommand command)
        {
            return await _applicationUserService.DeactivateUser(command.Username);
        }
    }
}