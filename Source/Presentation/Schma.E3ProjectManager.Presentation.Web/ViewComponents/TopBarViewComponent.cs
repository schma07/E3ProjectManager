using Microsoft.AspNetCore.Mvc;
using Schma.E3ProjectManager.Core.Application;
using Schma.E3ProjectManager.Presentation.Web.ViewModels;

namespace Schma.E3ProjectManager.Presentation.Web.ViewComponents
{
    [ViewComponent(Name = "TopBar")]
    public class TopBarViewComponent : ViewComponent
    {
        private IAuthenticatedUserService _userService;
        public TopBarViewComponent(IAuthenticatedUserService userService)
        {
            _userService = userService;
        }

        public IViewComponentResult Invoke()
        {
            var model = new TopBarViewModel();
            model.Username = _userService.Username;
            model.Roles = _userService.Roles;

            return View(model);
        }
    }
}
