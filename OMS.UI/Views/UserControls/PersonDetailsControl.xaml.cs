using OMS.UI.Models;
using System.Windows;
using System.Windows.Controls;

namespace OMS.UI.Views.UserControls
{
    /// <summary>
    /// Interaction logic for PersonDetailsControl.xaml
    /// </summary>
    public partial class PersonDetailsControl : UserControl
    {
        public static readonly DependencyProperty PersonProperty =
        DependencyProperty.Register("Person", typeof(PersonModel), typeof(PersonDetailsControl));

        public PersonModel Person
        {
            get => (PersonModel)GetValue(PersonProperty);
            set => SetValue(PersonProperty, value);
        }
        public PersonDetailsControl()
        {
            InitializeComponent();
        }
    }
}
