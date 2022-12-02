﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Schma.E3ProjectManager.Infrastructure.Resources;

namespace Schma.E3ProjectManager.Presentation.Framework.Components
{
    public partial class CRUDButton : BaseComponent
    {
        [Parameter]
        public EventCallback<MouseEventArgs> OnClickAction { get; set; }

        [Parameter]
        public string Title { get; set; }

        [Parameter]
        public CRUDElementTypeEnum Type { get; set; }

        [Parameter]
        public string CssClass { get; set; }

        private string _icon;
        private string _btn;

        protected override async Task OnParametersSetAsync()
        {

            _icon = CRUDElementsHelper.GetCRUDIconHTML(Type);
            _btn = CRUDElementsHelper.GetCRUDButtonHtml(Type);

            Title ??= GetTitle();

            await base.OnParametersSetAsync();
        }

        private string GetTitle()
        {
            switch (Type)
            {
                case CRUDElementTypeEnum.View: return LocalizationService[ResourceKeys.Buttons_Details];
                case CRUDElementTypeEnum.Delete: return LocalizationService[ResourceKeys.Buttons_Delete];
                case CRUDElementTypeEnum.Edit: return LocalizationService[ResourceKeys.Buttons_Edit];
                case CRUDElementTypeEnum.Save: return LocalizationService[ResourceKeys.Buttons_Save];
                case CRUDElementTypeEnum.Cancel: return LocalizationService[ResourceKeys.Buttons_Cancel];
                default: return "";
            }
        }
    }
}
