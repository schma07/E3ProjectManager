using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Schma.E3ProjectManager.Presentation.Web.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class DeveloperController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
