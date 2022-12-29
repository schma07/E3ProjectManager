using AutoMapper;
using Schma.E3ProjectManager.Core.Application.ReadModels.Projects;
using Schma.E3ProjectManager.Core.Domain.Entities.ProjectAggregate;

namespace Schma.E3ProjectManager.Core.Application.Mappings
{
    internal class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<Project, ProjectReadModel>();
            CreateMap<ProjectDevice, ProjectDeviceReadModel>();            
        }
    }
}
