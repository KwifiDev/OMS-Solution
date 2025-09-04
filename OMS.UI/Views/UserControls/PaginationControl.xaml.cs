using OMS.UI.Models.Others;
using System.Windows;
using System.Windows.Controls;

namespace OMS.UI.Views.UserControls
{
    /// <summary>
    /// Interaction logic for PaginationControl.xaml
    /// </summary>
    public partial class PaginationControl : UserControl
    {
        public static readonly DependencyProperty PaginationInfoProperty =
        DependencyProperty.Register("PaginationInfo", typeof(PaginationInfo), typeof(PaginationControl));

        public PaginationInfo PaginationInfo
        {
            get => (PaginationInfo)GetValue(PaginationInfoProperty);
            set => SetValue(PaginationInfoProperty, value);
        }

        public PaginationControl()
        {
            InitializeComponent();
        }
    }
}
