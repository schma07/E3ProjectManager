using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Schma.E3ProjectManager.Common;
using Schma.E3ProjectManager.Core.Application;
using Schma.E3ProjectManager.Core.Application.Commands;
using Schma.Messaging.Abstractions;

namespace Schma.E3ProjectManager.Presentation.Web.Controllers
{
    [Route("Test")]
    public class TestController : Controller
    {
        private readonly IServiceBus _serviceBus;

        public TestController(IServiceBus serviceBus)
        {
            _serviceBus = serviceBus;
        }

        [Route("CreateUser")]
        public async Task<IActionResult> CreateUser()
        {
            Result result = await _serviceBus.SendAsync(
                new CreateUserCommand("dev", "Developer User", "dev@dev.dev", "12345678!!", new List<string>() { "SuperAdmin", "Admin" }));

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return NotFound($"{string.Join(',', result.Errors)}");
            }
        }

        [Route("CreateOrder")]
        public async Task<IActionResult> CreateOrder()
        {
            Result result = await _serviceBus.SendAsync(new CreateNewOrderCommand("TRACKING NUMBER HERE"));
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return NotFound($"{string.Join(',', result.Errors)}");
            }
        }
        [Route("CreateProject")]
        public async Task<IActionResult> CreateProject()
        {
            Result result = await _serviceBus.SendAsync(new CreateNewProjectCommand("TRACKING NUMBER HERE"));
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return NotFound($"{string.Join(',', result.Errors)}");
            }
        }
        [Route("CreateCustomer")]
        public async Task<IActionResult> CreateCustomer()
        {
            Result result = await _serviceBus.SendAsync(new CreateNewCustomerCommand("CUSTOMER NAME HERE"));
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return NotFound($"{string.Join(',', result.Errors)}");
            }
        }
    }
}