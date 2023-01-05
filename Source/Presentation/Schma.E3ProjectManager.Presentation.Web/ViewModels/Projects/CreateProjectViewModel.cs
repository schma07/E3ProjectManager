using System.ComponentModel.DataAnnotations;
using Schma.E3ProjectManager.Infrastructure.Resources;

namespace Schma.E3ProjectManager.Presentation.Web.ViewModels.Projects
{
    public class CreateProjectViewModel
    {
        [Required(ErrorMessage = ResourceKeys.Validations_Required)]
        [Display(Name = ResourceKeys.Labels_Name)]
        public string Name { get; set; }

        [Required(ErrorMessage = ResourceKeys.Validations_Required)]
        // TODO: Add ResourceKeys for labels
        [EmailAddress(ErrorMessage = ResourceKeys.Validations_EmailFormat)]
        public string CustomerName { get; set; }               
    }
}
