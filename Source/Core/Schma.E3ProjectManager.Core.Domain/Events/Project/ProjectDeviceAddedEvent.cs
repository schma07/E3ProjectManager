using System;
using Schma.Domain.Abstractions;

namespace Schma.E3ProjectManager.Core.Domain
{
    public class ProjectDeviceAddedEvent : DomainEventBase<Guid>
    {
        public string SupplierArticleNumber { get; private set; }
        public string DeviceName { get; private set; }
        public string DeviceLocation { get; private set; }
        public string DeviceFunction { get; private set; }
        public int Quantity { get; private set; }


        /// <summary>
        /// Needed for serialization
        /// </summary>
        internal ProjectDeviceAddedEvent()
        {
        }

        public ProjectDeviceAddedEvent(string supplierArticleNumber, string deviceName, string deviceLocation, string deviceFunction, int quantity)
        {
            SupplierArticleNumber = supplierArticleNumber;
            DeviceName = deviceName;
            DeviceLocation = deviceLocation;
            DeviceFunction = deviceFunction;
            Quantity = quantity;
        }

        public ProjectDeviceAddedEvent(Guid aggregateId, int aggregateVersion, string supplierArticleNumber, string deviceName, string deviceLocation, string deviceFunction, int quantity)
            : base(aggregateId, aggregateVersion)
        {
            SupplierArticleNumber = supplierArticleNumber;
            DeviceName = deviceName;
            DeviceLocation = deviceLocation;
            DeviceFunction = deviceFunction;
            Quantity = quantity;
        }

        public override IDomainEvent<Guid> WithAggregate(Guid aggregateId, int aggregateVersion)
        {
            return new ProjectDeviceAddedEvent(aggregateId, aggregateVersion, SupplierArticleNumber, DeviceName, DeviceLocation, DeviceFunction, Quantity);
        }
    }
}
