using System.Globalization;
using System.Windows.Data;
using static OMS.UI.Services.StatusManagement.AddEditStatus;

namespace OMS.UI.Converters
{
    public class BooleanAndConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length != 2 || values[0] is not bool || values[1] is not EnMode)
                return false;

            return (bool)values[0] && !((EnMode)values[1] == EnMode.Edit);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
