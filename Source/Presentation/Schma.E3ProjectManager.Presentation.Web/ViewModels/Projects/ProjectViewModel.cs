using System;
using Schma.E3ProjectManager.Core.Application.ReadModels.Projects;
using System.Collections.Generic;

namespace Schma.E3ProjectManager.Presentation.Web.ViewModels.Projects
{
    public class ProjectViewModel
    {
        public Guid Id { get; set; }
        public string TrackingNumber { get; set; }        
        public string ProjectName { get; set; }
        public List<ProjectDeviceReadModel> ProjectDevices { get; set; }        
    }
}