using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Schma.Data.Abstractions;

namespace Schma.E3ProjectManager.Infrastructure.Models
{
    /// <summary>
    /// Data entity for Project
    /// </summary>
    public class ProjectEntity : DataEntityBase<Guid>
    {
        /// <summary>
        /// FK of Customer to which this project belongs to
        /// </summary>
        public Guid? CustomerId { get; set; }

        /// <summary>
        /// The project's tracking number
        /// </summary>
        public string TrackingNumber { get; set; }

        /// <summary>
        /// The project's name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The project's name
        /// </summary>
        public string Revision { get; set; }

        /// <summary>
        /// Flag indicating whether the project is active
        /// </summary>
        public bool IsActive { get; set; } = false;
        

        /// <summary>
        /// Navigation property for this project's customer.
        /// </summary>
        public virtual CustomerEntity Customer { get; set; }

        /// <summary>
        /// Navigation property for the project's device
        /// </summary>
        public virtual ICollection<ProjectDeviceEntity> ProjectDevices { get; set; } = new List<ProjectDeviceEntity>();     
    }
}
