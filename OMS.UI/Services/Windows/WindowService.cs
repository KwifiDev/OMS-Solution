using System.Windows;
using System.Windows.Input;

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

        public void DragMove()
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                var activeWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                activeWindow?.DragMove();
            }
        }

        public void Close()
        {
            Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive)?.Close();
        }
    }
}
