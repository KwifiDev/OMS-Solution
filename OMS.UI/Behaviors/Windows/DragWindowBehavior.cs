using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Input;

namespace OMS.UI.Behaviors.Windows
{
    public class DragWindowBehavior : Behavior<FrameworkElement>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.MouseLeftButtonDown += DragWindow;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.MouseLeftButtonDown -= DragWindow;
        }

        private void DragWindow(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var window = Window.GetWindow(AssociatedObject);
                window?.DragMove();
            }
        }
    }
}
