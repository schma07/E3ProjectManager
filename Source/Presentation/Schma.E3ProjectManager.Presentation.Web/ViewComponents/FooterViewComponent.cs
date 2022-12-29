using System;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Schma.E3ProjectManager.Presentation.Web.ViewModels;

namespace Schma.E3ProjectManager.Presentation.Web.ViewComponents
{
    [ViewComponent(Name = "Footer")]
    public class FooterViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            FooterViewModel model = new FooterViewModel();
            model.Date = DateTime.Now;
            model.Version = Assembly.GetEntryAssembly().GetName().Version.ToString(); //TODO: Get proper version here
            return View(model);
        }
    }
}
