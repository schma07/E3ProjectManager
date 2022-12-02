﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Schma.E3ProjectManager.Core.Application;
using Schma.E3ProjectManager.Core.Domain;

namespace Schma.E3ProjectManager.Presentation.Web.Helpers
{
    public class UserHelper
    {
        private readonly ILocalizationService _localizer;
        private readonly ILocalizationKeyProvider _localizationKeyProvider;
        private readonly IAuthenticatedUserService _userService;

        public UserHelper(IAuthenticatedUserService userService, ILocalizationService localizer, ILocalizationKeyProvider localizationKeyProvider)
        {
            _localizer = localizer;
            _localizationKeyProvider = localizationKeyProvider;
            _userService = userService;
        }

        public IEnumerable<SelectListItem> GetAvailableRoles()
        {
            var currentUserHighestRole = _userService.Roles.Max();

            return Enum.GetValues(typeof(RoleEnum))
                                 .Cast<RoleEnum>()
                                 .Where(e => e <= currentUserHighestRole)
                                 .Select(e => new SelectListItem
                                 {
                                     Value = ((int)e).ToString(),
                                     Text = _localizer[_localizationKeyProvider.GetRoleLocalizationKey(e)]
                                 });

        }
    }
}