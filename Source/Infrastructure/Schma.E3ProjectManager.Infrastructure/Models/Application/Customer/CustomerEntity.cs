using System;
using System.Collections.Generic;
using Schma.Data.Abstractions;

namespace Schma.E3ProjectManager.Infrastructure.Models
{
    /// <summary>
    /// Data entity for Customer
    /// </summary>
    public class CustomerEntity : DataEntityBase<Guid>
    {        
        /// <summary>
        /// Address country
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Navigation property for the customer's projects
        /// </summary>
        public virtual ICollection<ProjectEntity> Projects { get; set; } = new List<ProjectEntity>();
    }
}
