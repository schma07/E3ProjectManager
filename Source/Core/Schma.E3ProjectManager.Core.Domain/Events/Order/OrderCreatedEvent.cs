using System;
using Schma.Domain.Abstractions;

namespace Schma.E3ProjectManager.Core.Domain
{
    public class OrderCreatedEvent : DomainEventBase<Guid>
    {
        public string TrackingNumber { get; set; }

        /// <summary>
        /// Needed for serialization
        /// </summary>
        internal OrderCreatedEvent()
        {
        }

        public OrderCreatedEvent(string trackingNumber) : base(Guid.NewGuid())
        {
            TrackingNumber = trackingNumber;
        }

        public OrderCreatedEvent(Guid aggregateId, int aggregateVersion, string trackingNumber)
            : base(aggregateId, aggregateVersion)
        {
            TrackingNumber = trackingNumber;
        }

        public override IDomainEvent<Guid> WithAggregate(Guid aggregateId, int aggregateVersion)
        {
            return new OrderCreatedEvent(aggregateId, aggregateVersion, TrackingNumber);
        }
    }
}
