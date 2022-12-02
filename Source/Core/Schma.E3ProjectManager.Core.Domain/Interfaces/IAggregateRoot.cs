using System.Collections.Generic;

namespace Schma.E3ProjectManager.Core.Domain
{
    /// <summary>
    /// Interface for aggregate root
    /// </summary>
    public interface IAggregateRoot<TId> : IEntity<TId>
    {
        int Version { get; }

        void ApplyEvent(IDomainEvent<TId> @event, int version);

        IEnumerable<IDomainEvent<TId>> GetUncommittedEvents();

        void ClearUncommittedEvents();

        bool IsDeleted { get; }
    }
}