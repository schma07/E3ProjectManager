using Microsoft.AspNetCore.Components;

namespace Schma.E3ProjectManager.Presentation.Framework
{
    public interface IToastService
    {
        void ShowSuccess(string message);
        void ShowError(string message);
        void ShowError(RenderFragment message);
        void ShowWarning(string message);
    }
}