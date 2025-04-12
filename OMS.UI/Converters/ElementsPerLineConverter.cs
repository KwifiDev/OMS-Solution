using System.Globalization;
using System.Windows.Data;

namespace OMS.UI.Converters
{
    public class ElementsPerLineConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!int.TryParse(parameter.ToString(), out int numberOfElements)) return double.NaN;


            if (value is double width && width > 0)
            {
                double totalMargin = 20;
                return (width - totalMargin) / numberOfElements;
            }
            return double.NaN; 
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
