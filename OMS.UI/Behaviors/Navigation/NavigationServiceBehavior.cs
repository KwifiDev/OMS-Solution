using CommunityToolkit.Mvvm.DependencyInjection;
using OMS.UI.Services.Navigation;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace OMS.UI.Behaviors.Navigation
{
    public static class NavigationServiceBehavior
    {
        public static readonly DependencyProperty RegisterFrameProperty =
            DependencyProperty.RegisterAttached(
                "RegisterFrame",
                typeof(bool),
                typeof(NavigationServiceBehavior),
                new PropertyMetadata(false, OnRegisterFrameChanged));

        public static bool GetRegisterFrame(DependencyObject obj)
            => (bool)obj.GetValue(RegisterFrameProperty);

        public static void SetRegisterFrame(DependencyObject obj, bool value)
            => obj.SetValue(RegisterFrameProperty, value);

        private static void OnRegisterFrameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (DesignerProperties.GetIsInDesignMode(d))
                return;

            if (d is Frame frame && (bool)e.NewValue)
            {
                frame.Loaded += (sender, args) =>
                {
                    try
                    {
                        var navigationService = Ioc.Default.GetRequiredService<INavigationService>();
                        navigationService.SetFrame(frame);
                    }
                    catch (InvalidOperationException ex)
                    {
                        if (!DesignerProperties.GetIsInDesignMode(frame))
                            throw new InvalidOperationException("Navigation service not registered", ex);
                    }
                };
            }
        }
    }
}
