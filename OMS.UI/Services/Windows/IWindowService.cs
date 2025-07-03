using System.Windows;

namespace OMS.UI.Services.Windows
{
    public interface IWindowService
    {
        void Open<TWindow>() where TWindow : Window;
        void Close();
        void Exit();
        void Hide();
        void Minimize();
        void Maximize();
        void HideLoginWindow();
        void CloseMainWindow();
    }
}
