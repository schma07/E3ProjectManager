using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Schma.E3ProjectManager.Core.Application.Queries.Projects;
using Schma.E3ProjectManager.Presentation.Web.ViewModels.Projects;
using Schma.Messaging.Abstractions;

namespace Schma.E3ProjectManager.Presentation.Web.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly IServiceBus _serviceBus;

        public ProjectsController(IServiceBus serviceBus)
        {
            _serviceBus = serviceBus;
        }
        /// <summary>
        /// Returns a view of all projects
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            IndexViewModel model = new();
            var projects = await _serviceBus.SendAsync(new GetAllProjectsQuery());

            model.Projects = projects.Data
                .Select(p => new ProjectViewModel() { Id = p.Id, ProjectName = p.ProjectName, ProjectDevices = p.ProjectDevices, TotalAmount = p.TotalAmount, TrackingNumber = p.TrackingNumber })
                .ToList();

            return View(model);
        }
    }
}
