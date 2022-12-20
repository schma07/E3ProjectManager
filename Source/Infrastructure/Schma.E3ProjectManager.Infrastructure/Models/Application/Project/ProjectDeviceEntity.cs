using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Schma.Data.Abstractions;

namespace Schma.E3ProjectManager.Infrastructure.Models
{
    public class ProjectDeviceEntity : DataEntityBase<Guid>
    {
        /// <summary>
        /// FK of Project for which this device belongs to
        /// </summary>
        public Guid ProjectId { get; set; }

        /// <summary>
        /// The order item product name
        /// </summary>
        public string SupplierArticleNumber { get; set; }

        /// <summary>
        /// The project device name in E3 (HLA)
        /// </summary>
        public string DeviceName { get; set; }

        /// <summary>
        /// The project device location in E3 (HLA)
        /// </summary>
        public string DeviceLocation { get; set; }
        
        /// <summary>
        /// The project device function in E3 (HLA)
        /// </summary>
        public string DeviceFunction { get; set; }

        /// <summary>
        /// The quantity/length of Device item(s) 
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Navigation property for this device's Project.
        /// </summary>
        public virtual ProjectEntity Project { get; set; }
        

    }
}
