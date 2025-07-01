using MaterialDesignThemes.Wpf;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace OMS.UI.Views.UserControls
{
    /// <summary>
    /// Interaction logic for CardInfoControl.xaml
    /// </summary>
    public partial class CardInfoControl : UserControl
    {

        public static readonly DependencyProperty ColorProperty =
            DependencyProperty.Register("Color", typeof(Brush), typeof(CardInfoControl));

        public static readonly DependencyProperty ImageProperty =
          DependencyProperty.Register("Image", typeof(PackIconKind), typeof(CardInfoControl));

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(CardInfoControl));

        public static readonly DependencyProperty DescriptionProperty =
            DependencyProperty.Register("Description", typeof(string), typeof(CardInfoControl));

        public Brush Color
        {
            get => (Brush)GetValue(ColorProperty);
            set => SetValue(ColorProperty, value);
        }

        public PackIconKind Image
        {
            get => (PackIconKind)GetValue(ImageProperty);
            set => SetValue(ImageProperty, value);
        }

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public string Description
        {
            get => (string)GetValue(DescriptionProperty);
            set => SetValue(DescriptionProperty, value);
        }

        public CardInfoControl()
        {
            InitializeComponent();
        }
    }
}
