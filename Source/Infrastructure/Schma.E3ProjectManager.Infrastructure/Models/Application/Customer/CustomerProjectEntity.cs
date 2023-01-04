using System;
using Schma.Data.Abstractions;

namespace Schma.E3ProjectManager.Infrastructure.Models
{
    public class CustomerProjectEntity : DataEntityBase<Guid>
    {
        /// <summary>
        /// FK of Customer for which this CustomerProject belongs to
        /// </summary>
        public Guid CustomerId { get; set; }

        /// <summary>
        /// FK of Project for which this CustomerProject belongs to
        /// </summary>
        public Guid ProjectId { get; set; }


        /// <summary>
        /// Navigation property for this customerprojects's customer.
        /// </summary>
        public virtual CustomerEntity Customer { get; set; }

        /// <summary>
        /// Navigation property for this customerprojects's project.
        /// </summary>
        public virtual ProjectEntity Project { get; set; }
    }
}
