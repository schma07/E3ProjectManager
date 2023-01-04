using System;
using Schma.Domain.Abstractions;

namespace Schma.E3ProjectManager.Core.Domain
{
    /// <summary>
    /// Customer Created domain event
    /// </summary>
    public class CustomerCreatedEvent : DomainEventBase<Guid>
    {
       public string Name { get; set; }

        /// <summary>
        /// Needed for serialization
        /// </summary>
        internal CustomerCreatedEvent()
        {
        }

        public CustomerCreatedEvent(string name)
        {
            Name = name;
        }

        public CustomerCreatedEvent(Guid aggregateId, int aggregateVersion, string name)
            : base(aggregateId, aggregateVersion)
        {
            Name = name;
        }

        public override IDomainEvent<Guid> WithAggregate(Guid aggregateId, int aggregateVersion)
        {
            return new CustomerCreatedEvent(aggregateId, aggregateVersion, Name);
        }
    }
}