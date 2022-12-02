using Microsoft.AspNetCore.Components;
using Schma.E3ProjectManager.Core.Application;
using Schma.E3ProjectManager.Infrastructure.Resources;
using Schma.E3ProjectManager.Presentation.Framework;

namespace Schma.E3ProjectManager.Presentation.Web.Services
{
    public class ToastService : IToastService
    {
        #region Private Members

        private readonly Blazored.Toast.Services.IToastService _blazoredService;
        private readonly ILocalizationService _localizer;

        #endregion

        #region Constructor

        public ToastService(ILocalizationService localizer,
            Blazored.Toast.Services.IToastService blazoredService)
        {
            _localizer = localizer;
            _blazoredService = blazoredService;
        }

        #endregion

        #region Public Methods

        public void ShowSuccess(string message)
        {
            _blazoredService.ShowSuccess(message, _localizer[ResourceKeys.Notifications_Success]);
        }

        public void ShowError(string message)
        {
            _blazoredService.ShowError(message ?? _localizer[ResourceKeys.Common_SomethingWentWrong], _localizer[ResourceKeys.Notifications_Error]);
        }

        public void ShowError(RenderFragment message)
        {
            _blazoredService.ShowError(message);
        }

        public void ShowWarning(string message)
        {
            _blazoredService.ShowWarning(message, _localizer[ResourceKeys.Notifications_Warning]);
        }

        #endregion
    }
}
