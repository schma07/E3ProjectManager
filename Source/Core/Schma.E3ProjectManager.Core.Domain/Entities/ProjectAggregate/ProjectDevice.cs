using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Schma.Domain.Abstractions;

namespace Schma.E3ProjectManager.Core.Domain.Entities.ProjectAggregate
{
    public class ProjectDevice : IEntity<Guid>
    {
        public Guid Id { get; private set; }
        public string SupplierArticleNumber { get; private set; }
        public string DeviceName { get; private set; }
        public string DeviceLocation { get; private set; }
        public string DeviceFunction { get; private set; }
        public decimal Quantity { get; set; }

        public ProjectDevice(string supplierArticleNumber, string deviceName, string deviceLocation, string deviceFunction, decimal quantity)
        {
            SupplierArticleNumber = supplierArticleNumber;
            DeviceName = deviceName;
            DeviceLocation = deviceLocation;
            DeviceFunction = deviceFunction;
            Quantity = quantity;
        }


        internal void UpdateQuantity(decimal quantity)
        {
            Quantity = quantity;
        }
    }
}
