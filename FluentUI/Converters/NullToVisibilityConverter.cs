using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace FluentUI.Converters
{
    public class NullToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value != null) ? Visibility.Visible : ((parameter is Visibility visibility) ? visibility : Visibility.Collapsed);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
