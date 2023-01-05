using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Schma.E3ProjectManager.Core.Domain;
using Schma.E3ProjectManager.Infrastructure.Resources;

namespace Schma.E3ProjectManager.Presentation.Web.ViewModels.Projects
{
    public class EditProjectViewModel
    {
        public string Id { get; set; }
        public string TrackingNumber { get; set; }
                
        [Required(ErrorMessage = ResourceKeys.Validations_Required)]
        [Display(Name = ResourceKeys.Labels_Name)]
        public string Name { get; set; }

        [Required(ErrorMessage = ResourceKeys.Validations_Required)]
        // TODO: Add ResourceKeys for labels
        [EmailAddress(ErrorMessage = ResourceKeys.Validations_EmailFormat)]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = ResourceKeys.Validations_Required)]
        // TODO: Add ResourceKeys for labels
        public string Revision { get; set; }

        [Display(Name = ResourceKeys.Labels_Active)]
        [Required(ErrorMessage = ResourceKeys.Validations_Required)]
        public bool IsActive { get; set; }
    }
}
