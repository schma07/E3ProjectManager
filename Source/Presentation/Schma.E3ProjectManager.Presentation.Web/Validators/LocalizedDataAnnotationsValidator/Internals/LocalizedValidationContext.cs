﻿using System.ComponentModel.DataAnnotations;
using Schma.E3ProjectManager.Core.Application;

namespace Schma.E3ProjectManager.Presentation.Web.Validators.Internals
{
    internal class LocalizedValidationContext
    {
        internal ILocalizationService Localizer { get; }

        internal ValidationContext Context { get; }

        internal LocalizedValidationContext(ILocalizationService localizer, object instance)
        {
            Localizer = localizer;
            Context = new ValidationContext(instance);
        }

        internal LocalizedValidationContext(ILocalizationService localizer, ValidationContext context)
        {
            Localizer = localizer;
            Context = context;
        }
    }
}
