using System.Windows;
using System.Windows.Controls;

namespace OMS.UI.Views.Pages
{
    /// <summary>
    /// Interaction logic for WelcomePage.xaml
    /// </summary>
    public partial class WelcomePage : Page
    {
        public WelcomePage()
        {
            InitializeComponent();
        }

        private void WelcomeGif_MediaEnded(object sender, RoutedEventArgs e)
        {
            // إعادة تشغيل GIF عندما ينتهي
            var mediaElement = (MediaElement)sender;
            mediaElement.Position = System.TimeSpan.Zero;
            mediaElement.Play();
        }
    }
}
