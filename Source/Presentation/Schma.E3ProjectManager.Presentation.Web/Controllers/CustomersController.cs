using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Schma.E3ProjectManager.Common;
using Schma.E3ProjectManager.Core.Application;
using Schma.E3ProjectManager.Core.Application.Authorization;
using Schma.E3ProjectManager.Core.Application.Commands;
using Schma.E3ProjectManager.Core.Application.Queries;
using Schma.E3ProjectManager.Core.Application.Queries.Customers;
using Schma.E3ProjectManager.Infrastructure.Resources;
using Schma.E3ProjectManager.Presentation.Framework;
using Schma.E3ProjectManager.Presentation.Web.ViewModels;
using Schma.E3ProjectManager.Presentation.Web.ViewModels.Customers;
using Schma.Messaging.Abstractions;

namespace Schma.E3ProjectManager.Presentation.Web.Controllers
{
    [Route("Customers")]
    [Authorize]
    public class CustomersController : Controller
    {
        private readonly IServiceBus _serviceBus;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IMapper _mapper;
        private readonly ILocalizationService _localizer;
        private readonly INotificationService _notificationService;

        public CustomersController(IServiceBus serviceBus,
                                   IHttpContextAccessor contextAccessor,
                                   IMapper mapper,
                                   ILocalizationService localizer,
                                   INotificationService notificationService)
        {
            _serviceBus = serviceBus;
            _contextAccessor = contextAccessor;
            _mapper = mapper;
            _localizer = localizer;
            _notificationService = notificationService;
        }

        [HttpPost]
        public async Task<IActionResult> LoadData([FromForm] DataTablesParameters parameters)
        {
            // Getting all customers
            var queryOptions = parameters.ToQueryOptions();
            var customersQuery = await _serviceBus.SendAsync(new GetAllCustomersQuery { Options = queryOptions });

            if (customersQuery.Failed) return null;

            var recordCount = customersQuery.GetMetadata<int>("RecordCount");
            //Returning Json Data  
            return Json(new { draw = parameters.Draw, recordsFiltered = recordCount, recordsTotal = recordCount, data = customersQuery.Data });
        }
            /// <summary>
            /// Returns a view of all projects
            /// </summary>
            /// <returns></returns>
            public async Task<IActionResult> Index()
        {
            IndexViewModel model = new();
            var customers = await _serviceBus.SendAsync(new GetAllCustomersQuery());

            model.Customers = customers.Data
                .Select(c => new CustomerViewModel() { Id = c.Id, Name = c.Name })
                .ToList();

            return View(model);
        }

        [Route("Create")]
        public IActionResult Create()
        {
            return View(new CreateCustomerViewModel());
        }

        /// <summary>
        /// Creates a new customer
        /// </summary>
        /// <param name="createCustomerModel">The details of the customer to be created</param>
        /// <returns></returns>
        [Route("Create")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateCustomerViewModel createCustomerModel)
        {          

            if (!ModelState.IsValid)
                return View(createCustomerModel);

            Result result = await _serviceBus.SendAsync(_mapper.Map<CreateNewCustomerCommand>(createCustomerModel));

            if (result.Succeeded)
            {
                var returnUrl = _contextAccessor.HttpContext.Request.Query["ReturnUrl"];
                if (!string.IsNullOrWhiteSpace(returnUrl))
                    return Redirect(returnUrl);

                _notificationService.SuccessNotification(string.Format(_localizer[ResourceKeys.Notifications_CustomerCreated_Success], createCustomerModel.Name));
                return RedirectToAction(nameof(Index));
            }
            else
            {
                _notificationService.ErrorNotification(result.MessageWithErrors);
                ModelState.AddModelError("", "Customer creation failed.");
            }

            return View();
        }
    }
}
