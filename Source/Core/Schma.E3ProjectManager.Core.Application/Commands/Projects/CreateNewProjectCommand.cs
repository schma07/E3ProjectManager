using System.Threading.Tasks;
using Schma.E3ProjectManager.Common;
using Schma.E3ProjectManager.Core.Domain.Entities.ProjectAggregate;
using Schma.Messaging.Abstractions;

namespace Schma.E3ProjectManager.Core.Application.Commands
{
    public record CreateNewProjectCommand(string TrackingNumber) : IRequest<Result>
    {
    }

    /// <summary>
    /// Create New Project Command Handler
    /// </summary>
    public class CreateNewProjectCommandHandler : BaseMessageHandler<CreateNewProjectCommand, Result>
    {
        private readonly IProjectRepository _projectRepository;

        public CreateNewProjectCommandHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public override async Task<Result> HandleAsync(CreateNewProjectCommand command)
        {
            var project = new Project(command.TrackingNumber);
           // project.AddProjectDevice("666", "KF100", "F1", "VAC", 1);
            await _projectRepository.AddAsync(project);
            await _projectRepository.UnitOfWork.SaveChangesAsync();
            await _projectRepository.SaveToEventStoreAsync(project); //saving also to event store
            return new Result().Successful();
        }
    }
}
