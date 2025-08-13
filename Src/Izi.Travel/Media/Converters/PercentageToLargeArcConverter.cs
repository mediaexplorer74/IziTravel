using System;
using Windows.UI.Xaml.Data;

namespace Izi.Travel.Media.Converters
{
    public class PercentageToLargeArcConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is double percentage)
            {
                // If percentage is greater than 50%, we need a large arc
                return percentage > 50.0;
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
