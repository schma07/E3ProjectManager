using System;
using System.Collections.Generic;

namespace Schma.E3ProjectManager.Core.Application.ReadModels.Projects
{
    public class ProjectReadModel
    {
        public Guid Id { get; set; }        
        public string TrackingNumber { get; set; }
        public string ProjectName { get; set; }
        public string ProjectRevision { get; set; }
        public string CustomerName { get; set; }
        public bool IsActive { get; set; }
        public List<ProjectDeviceReadModel> ProjectDevices { get; set; }        
    }
}
