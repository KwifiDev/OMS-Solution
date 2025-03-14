﻿using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace OMS.UI.Converters
{
    internal class VisiblilityUIConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool IsModifiable = (bool)value;

            if (IsModifiable) return Visibility.Visible;

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
