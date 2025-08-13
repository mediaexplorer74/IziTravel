using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Izi.Travel.Mtg.Converters
{
    public class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is bool boolValue)
            {
                // Handle invert parameter (e.g., parameter="invert")
                bool invert = parameter?.ToString().ToLower() == "invert";
                if (invert)
                {
                    boolValue = !boolValue;
                }
                
                return boolValue ? Visibility.Visible : Visibility.Collapsed;
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value is Visibility visibility)
            {
                return visibility == Visibility.Visible;
            }
            return false;
        }
    }
}
