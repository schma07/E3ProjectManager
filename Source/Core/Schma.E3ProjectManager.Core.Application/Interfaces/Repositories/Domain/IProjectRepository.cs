using System;
using System.Threading.Tasks;
using Schma.Data.Abstractions;
using Schma.E3ProjectManager.Core.Domain.Entities.ProjectAggregate;

namespace Schma.E3ProjectManager.Core.Application
{
    public interface IProjectRepository : IAggregateRepository<Project, Guid>
    {
        Task SaveToEventStoreAsync(Project project);
    }
}