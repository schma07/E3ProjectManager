using System;
using System.Collections.Generic;

namespace Schma.E3ProjectManager.Core.Application.ReadModels.Projects
{
    public class ProjectReadModel
    {
        public Guid Id { get; set; }
        public string TrackingNumber { get; set; }
        public string ProjectName { get; set; }
        public List<ProjectDeviceReadModel> ProjectDevices { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
