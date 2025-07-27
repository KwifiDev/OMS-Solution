using OMS.UI.Models.Views;
using System.Windows;
using System.Windows.Controls;

namespace OMS.UI.Views.UserControls
{
    /// <summary>
    /// Interaction logic for ClientAccountDetailsControl.xaml
    /// </summary>
    public partial class ClientAccountDetailsControl : UserControl
    {
        public static readonly DependencyProperty UserAccountProperty =
        DependencyProperty.Register("UserAccount", typeof(UserAccountModel), typeof(ClientAccountDetailsControl));

        public UserAccountModel UserAccount
        {
            get => (UserAccountModel)GetValue(UserAccountProperty);
            set => SetValue(UserAccountProperty, value);
        }

        public ClientAccountDetailsControl()
        {
            InitializeComponent();
        }
    }
}
