using System;
using System.Threading.Tasks;
using AutoMapper;
using Schma.Data.Abstractions;
using Schma.E3ProjectManager.Core.Application;
using Schma.E3ProjectManager.Core.Domain.Entities.ProjectAggregate;
using Schma.E3ProjectManager.Infrastructure.Models;

namespace Schma.E3ProjectManager.Infrastructure
{
    /// <summary>
    /// Implementation of <see cref="IProjectRepository"/> which allows persistence on both EventStore and relational store.
    /// </summary>
    internal class ProjectRepository : EFRepository<Project, ProjectEntity, Guid>, IProjectRepository
    {
        private readonly IESRepository<Project, Guid> _eventStoreRepository;

        public ProjectRepository(IMapper mapper, IEntityRepository<ProjectEntity, Guid> persistenceRepo, IESRepository<Project, Guid> esRepository)
            : base(mapper, persistenceRepo)
        {

            _eventStoreRepository = esRepository;
        }

        public async Task SaveToEventStoreAsync(Project project)
        {
            await _eventStoreRepository.SaveAsync(project);
        }
    }
}
