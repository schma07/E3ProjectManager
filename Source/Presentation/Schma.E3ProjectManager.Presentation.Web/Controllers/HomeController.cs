using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Schma.E3ProjectManager.Presentation.Web.ViewModels;

namespace Schma.E3ProjectManager.Presentation.Web.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var model = new HomeViewModel();

            return View(model);
        }

    }
}