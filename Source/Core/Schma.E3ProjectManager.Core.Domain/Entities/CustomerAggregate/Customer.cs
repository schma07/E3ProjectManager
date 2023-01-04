using System;
using System.Collections.Generic;
using Ardalis.GuardClauses;
using Schma.Domain.Abstractions;
using Schma.E3ProjectManager.Core.Domain.Entities.ProjectAggregate;

namespace Schma.E3ProjectManager.Core.Domain.Entities.CustomerAggregate
{
    public class Customer : AggregateRootBase<Guid>, IDomainEventHandler<CustomerCreatedEvent>
    {
        public string Name { get; private set; }
        
        public IReadOnlyCollection<Project> Projects { get; private set; }
        
        private Customer()
        {

        }

        public Customer(string customername)
        {
            Guard.Against.NullOrWhiteSpace(customername, nameof(customername));
           
            RaiseEvent(new CustomerCreatedEvent(customername));
        }

        void IDomainEventHandler<CustomerCreatedEvent>.Apply(CustomerCreatedEvent @event)
        {
            Id = @event.AggregateId;
            Name = @event.Name;            
        }
    }
}
