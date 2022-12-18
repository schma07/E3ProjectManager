﻿using System;
using Schma.Domain.Abstractions;

namespace Schma.E3ProjectManager.Core.Domain
{
    public class OrderItemQuantityUpdatedEvent : DomainEventBase<Guid>
    {
        public Guid OrderItemId { get; private set; }
        public int Quantity { get; private set; }

        /// <summary>
        /// Needed for serialization
        /// </summary>
        internal OrderItemQuantityUpdatedEvent()
        {
        }

        public OrderItemQuantityUpdatedEvent(Guid orderItemId, int quantity)
        {
            OrderItemId = orderItemId;
            Quantity = quantity;
        }

        public OrderItemQuantityUpdatedEvent(Guid aggregateId, int aggregateVersion, Guid orderItemId, int quantity)
            : base(aggregateId, aggregateVersion)
        {
            OrderItemId = orderItemId;
            Quantity = quantity;
        }

        public override IDomainEvent<Guid> WithAggregate(Guid aggregateId, int aggregateVersion)
        {
            return new OrderItemQuantityUpdatedEvent(aggregateId, aggregateVersion, OrderItemId, Quantity);
        }
    }
}
