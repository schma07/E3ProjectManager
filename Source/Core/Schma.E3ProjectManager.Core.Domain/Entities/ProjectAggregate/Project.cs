using System;
using System.Collections.Generic;
using System.Linq;
using Ardalis.GuardClauses;
using Schma.Domain.Abstractions;

namespace Schma.E3ProjectManager.Core.Domain.Entities.ProjectAggregate
{
    public class Project : AggregateRootBase<Guid>,
         IDomainEventHandler<ProjectCreatedEvent>,
         IDomainEventHandler<ProjectDeviceAddedEvent>,
         IDomainEventHandler<ProjectDeviceQuantityUpdatedEvent>
    {
        private List<ProjectDevice> _projectDevices = new List<ProjectDevice>();
                        
        public string TrackingNumber { get; private set; }
        public string Name { get; private set; }
        public string CustomerName { get; private set; }
        public bool IsActive { get; private set; }

        public IReadOnlyCollection<ProjectDevice> ProjectDevices { get { return _projectDevices.AsReadOnly(); } private set { _projectDevices = value.ToList(); } }

        public Project(string trackingNumber)
        {
            RaiseEvent(new ProjectCreatedEvent(trackingNumber));
        }
        
        public void AddProjectDevice(string supplierArticleNumber, string deviceName, string deviceLocation, string deviceFunction, decimal quantity)
        {
            Guard.Against.NegativeOrZero(quantity, nameof(quantity), "Device quantity cannot be 0 or negative");
            RaiseEvent(new ProjectDeviceAddedEvent(supplierArticleNumber, deviceName, deviceLocation, deviceFunction, quantity));
        }

        public void UpdateProjectDeviceQuantity(Guid projectDeviceId, decimal quantity)
        {
            Guard.Against.NegativeOrZero(quantity, nameof(quantity), "Device quantity cannot be 0 or negative");
            RaiseEvent(new ProjectDeviceQuantityUpdatedEvent(projectDeviceId, quantity));
        }

        void IDomainEventHandler<ProjectCreatedEvent>.Apply(ProjectCreatedEvent @event)
        {
            Id = @event.AggregateId;            
            TrackingNumber = @event.TrackingNumber;
        }

        void IDomainEventHandler<ProjectDeviceAddedEvent>.Apply(ProjectDeviceAddedEvent @event)
        {
            _projectDevices.Add(new ProjectDevice(@event.SupplierArticleNumber, @event.DeviceName, @event.DeviceLocation, @event.DeviceFunction, @event.Quantity));
        }

        void IDomainEventHandler<ProjectDeviceQuantityUpdatedEvent>.Apply(ProjectDeviceQuantityUpdatedEvent @event)
        {
            var projectItem = _projectDevices.Find(oi => oi.Id == @event.ProjectDeviceId);
            if (projectItem == null)
                throw new NullReferenceException($"Project device with id {@event.ProjectDeviceId} not found in project {Id}");
            projectItem.UpdateQuantity(@event.Quantity);
        }
    }
}

