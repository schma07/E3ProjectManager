using System;
using System.Threading.Tasks;
using AutoMapper;
using Schma.E3ProjectManager.Common;
using Schma.E3ProjectManager.Core.Application.ReadModels.Orders;
using Schma.E3ProjectManager.Core.Application.ReadModels.Projects;
using Schma.Messaging.Abstractions;

namespace Schma.E3ProjectManager.Core.Application.Queries.Projects
{
    public class GetProjectByIdQuery : IRequest<Result<ProjectReadModel>>
    {
        public Guid ProjectId { get; private set; }

        public GetProjectByIdQuery(Guid projectId)
        {
            ProjectId = projectId;
        }
    }

    public class GetProjectByIdQueryHandler : BaseMessageHandler<GetProjectByIdQuery, Result<ProjectReadModel>>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public GetProjectByIdQueryHandler(IMapper mapper, IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        public override async Task<Result<ProjectReadModel>> HandleAsync(GetProjectByIdQuery query)
        {
            var result = new Result<ProjectReadModel>();
            var order = await _projectRepository.FindAsync(query.ProjectId);

            result.Data = _mapper.Map<ProjectReadModel>(order);

            return result;
        }
    }
}
