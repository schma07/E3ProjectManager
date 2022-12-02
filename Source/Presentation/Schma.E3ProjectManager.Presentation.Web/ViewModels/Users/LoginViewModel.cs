using System.ComponentModel.DataAnnotations;
using Schma.E3ProjectManager.Infrastructure.Resources;

namespace Schma.E3ProjectManager.Presentation.Web.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = ResourceKeys.Validations_Required)]
        [Display(Name = ResourceKeys.Labels_Username)]
        public string Username { get; set; }
        [Required(ErrorMessage = ResourceKeys.Validations_Required)]
        [Display(Name = ResourceKeys.Labels_Password)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = ResourceKeys.Labels_RememberMe)]
        public bool RememberMe { get; set; }
    }
}
