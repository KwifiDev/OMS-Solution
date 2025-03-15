using CommunityToolkit.Mvvm.DependencyInjection;
using System.Windows;

namespace OMS.UI.Services.Windows
{
    public class WindowService : IWindowService
    {
        public void Maximize()
        {
            var activeWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            if (activeWindow != null)
            {
                if (activeWindow.WindowState == WindowState.Normal)
                {
                    activeWindow.WindowState = WindowState.Maximized;
                }
                else if (activeWindow.WindowState == WindowState.Maximized)
                {
                    activeWindow.WindowState = WindowState.Normal;
                }
            }
        }

        public void Minimize()
        {
            var activeWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            if (activeWindow != null)
            {
                activeWindow.WindowState = WindowState.Minimized;
            }
        }

        public void Open<TWindow>() where TWindow : Window
        {
            Ioc.Default.GetRequiredService<TWindow>().Show();
        }

        public void Close()
        {
            Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive)?.Close();
        }

        public void Exit()
        {
            Application.Current.Shutdown();
        }

        public void Hide()
        {
            Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive)?.Hide();
        }


    }
}
