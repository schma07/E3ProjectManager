using System;
using System.Threading.Tasks;
using Schma.E3ProjectManager.Common;
using Schma.Messaging.Abstractions;

namespace Schma.E3ProjectManager.Core.Application.Commands
{
    public record AddProjectDeviceCommand(Guid ProjectId, string SupplierArticleNumber, string DeviceName, string DeviceLocation, string DeviceFunction, int Quantity) : IRequest<Result>
    {
    }

    /// <summary>
    /// Add Project-Device Command Handler
    /// </summary>
    public class AddProjectDeviceCommandHandler : BaseMessageHandler<AddProjectDeviceCommand, Result>
    {
        private readonly IProjectRepository _projectRepository;

        public AddProjectDeviceCommandHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public override async Task<Result> HandleAsync(AddProjectDeviceCommand command)
        {
            var project = await _projectRepository.FindAsync(command.ProjectId);
            project.AddProjectDevice(command.SupplierArticleNumber, command.DeviceName, command.DeviceLocation, command.DeviceFunction, command.Quantity);
            _projectRepository.Update(project);
            await _projectRepository.UnitOfWork.SaveChangesAsync();
            return new Result().Successful();
        }
    }
}
