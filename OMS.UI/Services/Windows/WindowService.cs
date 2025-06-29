using CommunityToolkit.Mvvm.DependencyInjection;
using OMS.UI.Views.Windows;
using System.Windows;
using System.Windows.Threading;

namespace OMS.UI.Services.Windows
{
    public class WindowService : IWindowService
    {
        private readonly Dispatcher _dispatcher;

        public WindowService()
        {
            _dispatcher = Application.Current.Dispatcher;
        }

        public void Maximize()
        {
            _dispatcher.Invoke(() =>
            {
                var activeWindow = GetActiveWindow();
                if (activeWindow != null)
                {
                    activeWindow.WindowState = activeWindow.WindowState == WindowState.Normal
                        ? WindowState.Maximized
                        : WindowState.Normal;
                }
            });
        }

        public void Minimize()
        {
            _dispatcher.Invoke(() =>
            {
                var activeWindow = GetActiveWindow();
                if (activeWindow != null)
                    activeWindow.WindowState = WindowState.Minimized;
            });
        }

        public void Open<TWindow>() where TWindow : Window
        {
            _dispatcher.Invoke(() =>
            {
                var window = Ioc.Default.GetRequiredService<TWindow>();
                window.Show();
            });
        }

        public void Close()
        {
            _dispatcher.Invoke(() =>
            {
                var activeWindow = GetActiveWindow();
                activeWindow?.Close();
            });
        }

        public void Exit()
        {
            _dispatcher.Invoke(() =>
            {
                Application.Current.Shutdown();
            });
        }

        public void Hide()
        {
            _dispatcher.Invoke(() =>
            {
                var activeWindow = GetActiveWindow();
                activeWindow?.Hide();
            });
        }

        public void HideLoginWindow()
        {
            _dispatcher.Invoke(() =>
            {
                var loginWindow = Application.Current.Windows.OfType<LoginWindow>().FirstOrDefault();
                loginWindow?.Hide();
            });
        }

        private Window? GetActiveWindow() => Application.Current.Windows.OfType<Window>().FirstOrDefault(x => x.IsActive);
    }
}