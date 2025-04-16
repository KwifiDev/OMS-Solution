using MaterialDesignThemes.Wpf;
using System.Globalization;
using System.Windows.Data;

namespace OMS.UI.Converters
{
    public class HasAccountPhotoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null ? PackIconKind.None : PackIconKind.Star;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
