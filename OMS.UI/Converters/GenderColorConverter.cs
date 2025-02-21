using OMS.Common.Enums;
using System.Globalization;
using System.Windows.Data;

namespace OMS.UI.Converters
{
    public class GenderColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (EnGender)value == EnGender.Male ? "#F0FFFF" : "#FFF0FF";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class GenderPhotoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (EnGender)value == EnGender.Male ? "/Resources/Images/Male.jpg" : "/Resources/Images/Female.png";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
