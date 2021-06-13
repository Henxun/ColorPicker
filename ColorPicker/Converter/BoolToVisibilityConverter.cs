using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ColorPicker.Converter
{
    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (null == value)
                throw new ArgumentNullException();
            bool isVisible = (bool) value;

            if (parameter?.ToString() == "Revert")
            {
                return isVisible ? Visibility.Collapsed : Visibility.Visible;
            }

            return isVisible ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
