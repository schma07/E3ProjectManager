using Microsoft.AspNetCore.Mvc;
using Schma.E3ProjectManager.Core.Application;

namespace Schma.E3ProjectManager.Presentation.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        protected readonly INotificationService NotificationService;

        public BaseController(INotificationService notificationService)
        {
            NotificationService = notificationService;
        }
    }
}
