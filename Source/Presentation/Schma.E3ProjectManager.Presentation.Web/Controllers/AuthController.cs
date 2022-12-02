using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Schma.E3ProjectManager.Common;
using Schma.E3ProjectManager.Core.Application;
using Schma.E3ProjectManager.Core.Application.Commands;
using Schma.E3ProjectManager.Core.Application.DTOs;
using Schma.E3ProjectManager.Infrastructure.Resources;
using Schma.E3ProjectManager.Presentation.Web.ViewModels;

namespace Schma.E3ProjectManager.Presentation.Web.Controllers
{
    [Route("Auth")]
    public class AuthController : Controller
    {
        private readonly IServiceBus _serviceBus;
        private readonly IApplicationUserService _applicationUserService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly INotificationService _notificationService;
        private readonly ILocalizationService _localizer;
        private readonly IAuthenticatedUserService _userService;
        private readonly IUserAuthenticationService _userAuthenticationService;

        public AuthController(IServiceBus serviceBus,
            IApplicationUserService applicationUserService,
            IMapper mapper,
            IHttpContextAccessor contextAccessor,
            INotificationService notificationService,
            ILocalizationService localizer,
            IAuthenticatedUserService userService,
            IUserAuthenticationService userAuthenticationService)
        {
            _serviceBus = serviceBus;
            _applicationUserService = applicationUserService;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
            _notificationService = notificationService;
            _localizer = localizer;
            _userService = userService;
            _userAuthenticationService = userAuthenticationService;
        }

        /// <summary>
        /// Returns the login view
        /// </summary>
        /// <returns></returns>
        [Route("Login")]
        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Authenticate user
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns></returns>
        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginModel)
        {
            if (!ModelState.IsValid)
                return View();

            Result<string> result = await _userAuthenticationService.Login(_mapper.Map<LoginRequestDTO>(loginModel));

            if (result.Succeeded)
            {
                var returnUrl = _contextAccessor.HttpContext.Request.Query["ReturnUrl"];
                if (!string.IsNullOrWhiteSpace(returnUrl))
                    return Redirect(returnUrl);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Login failed.  Please check your credentials.");
            }

            return View();
        }

        /// <summary>
        /// Logout user
        /// </summary>
        /// <returns></returns>
        [Route("Logout")]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            if (!ModelState.IsValid)
                return View();

            await _userAuthenticationService.Logout();

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [Route("ChangePassword")]
        public IActionResult ChangePassword()
        {
            var model = new ChangePasswordModel
            {
                Username = _userService.Username
            };

            return View(model);
        }

        [Route("ChangePassword")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel model)
        {
            var changePasswordCommand = new ChangeUserPasswordCommand(_userService.Username, model.NewPassword);

            var result = await _serviceBus.SendAsync(changePasswordCommand);

            if (result.Failed)
            {
                _notificationService.ErrorNotification(result.Message);
                model.Username = _userService.Username;
                return View(model);
            }

            _notificationService.SuccessNotification(_localizer[ResourceKeys.Notifications_PasswordChange_Success]);
            return RedirectToAction("Index", "Home");
        }

    }
}