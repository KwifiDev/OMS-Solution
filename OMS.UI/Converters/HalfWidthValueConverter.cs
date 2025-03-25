using System.Globalization;
using System.Windows.Data;

namespace OMS.UI.Converters
{
    public class HalfWidthValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double width && width > 0)
            {
                double totalMargin = 20;
                return (width - totalMargin) / 3;
            }
            return double.NaN; 
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
