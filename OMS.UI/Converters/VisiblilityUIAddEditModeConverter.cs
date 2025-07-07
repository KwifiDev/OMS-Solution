using OMS.UI.Services.StatusManagement;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace OMS.UI.Converters
{
    public class VisiblilityUIAddEditModeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var mode = (AddEditStatus.EnMode)value;
            bool IsEditMode = (mode == AddEditStatus.EnMode.Edit);

            if (!IsEditMode) return Visibility.Visible;

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
