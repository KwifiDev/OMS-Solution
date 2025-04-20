using System.Globalization;
using System.Windows.Data;

namespace OMS.UI.Converters
{
    public class DecimalToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null ? (decimal)value : 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null && !value.Equals("") ? value : 0;
        }
    }
}
