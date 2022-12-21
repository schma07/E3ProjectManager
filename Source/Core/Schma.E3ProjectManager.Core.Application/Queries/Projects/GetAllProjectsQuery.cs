using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Schma.E3ProjectManager.Common;
using Schma.E3ProjectManager.Core.Application.ReadModels.Projects;
using Schma.Messaging.Abstractions;

namespace Schma.E3ProjectManager.Core.Application.Queries.Projects
{
    public class GetAllProjectsQuery : IRequest<Result<List<ProjectReadModel>>>
    {
    }

    public class GetAllProjectsQueryHandler : BaseMessageHandler<GetAllProjectsQuery, Result<List<ProjectReadModel>>>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public GetAllProjectsQueryHandler(IMapper mapper, IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        public override async Task<Result<List<ProjectReadModel>>> HandleAsync(GetAllProjectsQuery query)
        {
            var result = new Result<List<ProjectReadModel>>();
            var projects = _projectRepository.GetAll();

            result.Data = _mapper.Map<List<ProjectReadModel>>(projects);

            return result;
        }
    }
}