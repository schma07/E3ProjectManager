﻿using System.ComponentModel.DataAnnotations;
using Schma.E3ProjectManager.Infrastructure.Resources;

namespace Schma.E3ProjectManager.Presentation.Web.ViewModels
{
    public class ChangePasswordModel
    {
        [Display(Name = ResourceKeys.Labels_Username)]
        public string Username { get; set; }

        [Required]
        [MinLength(10, ErrorMessage = ResourceKeys.Validations_FieldLength)]
        [DataType(DataType.Password)]
        [Display(Name = ResourceKeys.Labels_Password)]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = ResourceKeys.Labels_ConfirmPassword)]
        [Compare("NewPassword", ErrorMessage = ResourceKeys.Validations_PasswordsDoNotMatch)]
        public string ConfirmPassword { get; set; }

        public byte[] RowVersion { get; set; }
    }
}
