using System.Globalization;
using System.Windows.Data;

namespace OMS.UI.Converters
{
    public class ShortToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null ? (short)value : 1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null && !value.Equals("") ? value : 1;
        }
    }
}
