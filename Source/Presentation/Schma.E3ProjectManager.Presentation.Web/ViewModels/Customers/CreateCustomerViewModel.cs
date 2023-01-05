using System.ComponentModel.DataAnnotations;
using Schma.E3ProjectManager.Infrastructure.Resources;

namespace Schma.E3ProjectManager.Presentation.Web.ViewModels.Customers
{
    public class CreateCustomerViewModel
    {
        [Required(ErrorMessage = ResourceKeys.Validations_Required)]
        [Display(Name = ResourceKeys.Labels_Name)]
        public string Name { get; set; }               
    }
}
