using FluentValidation;
using Schma.E3ProjectManager.Presentation.Web.ViewModels;

namespace Schma.E3ProjectManager.Presentation.Web.Validators
{
    public partial class LoginValidator : AbstractValidator<LoginViewModel>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Username).NotEmpty().WithMessage("Username is required");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required");
        }
    }
}
