using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Schma.E3ProjectManager.Core.Domain;
using Schma.E3ProjectManager.Infrastructure.Resources;

namespace Schma.E3ProjectManager.Presentation.Web.ViewModels
{
    public class EditUserViewModel
    {
        public string Id { get; set; }

        [Display(Name = ResourceKeys.Labels_Username)]
        public string Username { get; set; }

        [Display(Name = ResourceKeys.Labels_Password)]
        [MinLength(10, ErrorMessage = ResourceKeys.Validations_FieldLength)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = ResourceKeys.Validations_Required)]
        [Display(Name = ResourceKeys.Labels_Name)]
        public string Name { get; set; }

        [Required(ErrorMessage = ResourceKeys.Validations_Required)]
        [Display(Name = ResourceKeys.Labels_Email)]
        [EmailAddress(ErrorMessage = ResourceKeys.Validations_EmailFormat)]
        public string Email { get; set; }

        [Display(Name = ResourceKeys.Labels_Phone)]
        public string PhoneNumber { get; set; }

        [Display(Name = ResourceKeys.Labels_Roles)]
        [Required(ErrorMessage = ResourceKeys.Validations_Required)]
        public IEnumerable<RoleEnum> Roles { get; set; }

        [Display(Name = ResourceKeys.Labels_Active)]
        [Required(ErrorMessage = ResourceKeys.Validations_Required)]
        public bool IsActive { get; set; }
    }
}
