using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Schma.Data.Abstractions;

namespace Schma.E3ProjectManager.Infrastructure.Models.Application.Project
{
    /// <summary>
    /// Data entity for Project
    /// </summary>
    public class ProjectEntity : DataEntityBase<Guid>
    {
        /// <summary>
        /// The project's tracking number
        /// </summary>
        public string TrackingNumber { get; set; }

        /// <summary>
        /// The project's name
        /// </summary>
        public string ProjectName { get; set; }        

        /// <summary>
        /// Address country
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// Navigation property for the project's device
        /// </summary>
        public virtual ICollection<ProjectDeviceEntity> ProjectDevices { get; set; } = new List<ProjectDeviceEntity>();     
    }
}
