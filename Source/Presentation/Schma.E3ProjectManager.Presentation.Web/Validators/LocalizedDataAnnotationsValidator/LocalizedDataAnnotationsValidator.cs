using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Schma.E3ProjectManager.Core.Application;

namespace Schma.E3ProjectManager.Presentation.Web.Validators
{
    public class LocalizedDataAnnotationsValidator : ComponentBase
    {
        [Inject]
        private ILocalizationService Localizer { get; set; }

        [CascadingParameter]
        private EditContext CurrentEditContext { get; set; }

        protected override void OnInitialized()
        {
            CurrentEditContext.AddLocalizedDataAnnotationsValidation(Localizer);
        }
    }
}
