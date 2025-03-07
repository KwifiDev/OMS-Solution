﻿using OMS.Common.Enums;
using System.Globalization;
using System.Windows.Data;

namespace OMS.UI.Converters
{
    public class GenderColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (EnGender)value == EnGender.Male ? "#C4E4FF" : "#FFD2FA";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
