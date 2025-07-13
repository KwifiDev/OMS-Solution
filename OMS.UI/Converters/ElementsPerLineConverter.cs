using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace OMS.UI.Converters
{
    public class ElementsPerLineConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
                return 250;

            if (parameter == null || !int.TryParse(parameter.ToString(), out int elementsPerLine) || elementsPerLine <= 0)
                return 250;

            if (!(value is double width) || width <= 0 || double.IsNaN(width))
                return 250;

            double marginBetweenCards = 10;
            double totalMargins = (elementsPerLine - 1) * marginBetweenCards;
            double cardWidth = (width - totalMargins) / elementsPerLine;

            return cardWidth > 0 ? cardWidth : 250;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}