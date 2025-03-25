using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace OMS.UI.Behaviors.Windows
{
    public class FocusBehavior : Behavior<Control>
    {
        public static readonly DependencyProperty TargetButtonProperty =
            DependencyProperty.Register(
                "TargetButton",
                typeof(Button),
                typeof(FocusBehavior),
                new PropertyMetadata(null));

        public Button TargetButton
        {
            get => (Button)GetValue(TargetButtonProperty);
            set => SetValue(TargetButtonProperty, value);
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.KeyDown += OnKeyDown;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.KeyDown -= OnKeyDown;
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && TargetButton != null)
            {
                // نقل التركيز إلى الزر المحدد
                TargetButton.Focus();
            }
        }
    }
}