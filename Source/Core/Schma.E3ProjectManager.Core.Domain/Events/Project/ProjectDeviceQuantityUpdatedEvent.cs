using System;
using Schma.Domain.Abstractions;

namespace Schma.E3ProjectManager.Core.Domain
{
    public class ProjectDeviceQuantityUpdatedEvent : DomainEventBase<Guid>
    {
        public Guid ProjectDeviceId { get; private set; }
        public int Quantity { get; private set; }

        /// <summary>
        /// Needed for serialization
        /// </summary>
        internal ProjectDeviceQuantityUpdatedEvent()
        {
        }

        public ProjectDeviceQuantityUpdatedEvent(Guid projectDeviceId, int quantity)
        {
            ProjectDeviceId = projectDeviceId;
            Quantity = quantity;
        }

        public ProjectDeviceQuantityUpdatedEvent(Guid aggregateId, int aggregateVersion, Guid projectDeviceId, int quantity)
            : base(aggregateId, aggregateVersion)
        {
            ProjectDeviceId = projectDeviceId;
            Quantity = quantity;
        }

        public override IDomainEvent<Guid> WithAggregate(Guid aggregateId, int aggregateVersion)
        {
            return new OrderItemQuantityUpdatedEvent(aggregateId, aggregateVersion, ProjectDeviceId, Quantity);
        }
    }
}
