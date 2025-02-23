using MaterialDesignThemes.Wpf;
using OMS.Common.Enums;
using System.Globalization;
using System.Windows.Data;

namespace OMS.UI.Converters
{
    public class GenderPhotoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (EnGender)value == EnGender.Male ? PackIconKind.FaceMale : PackIconKind.FaceFemale;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
