﻿using System;

namespace Schma.E3ProjectManager.Presentation.Framework.Services
{
    public class LoaderService
    {
        public event Action OnShow;
        public event Action OnHide;

        public void Show()
        {
            OnShow?.Invoke();
        }

        public void Hide()
        {
            OnHide?.Invoke();
        }
    }

}
