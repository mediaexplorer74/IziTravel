using System;
using Windows.Foundation;
using Windows.UI.Xaml.Data;

namespace Izi.Travel.Media.Converters
{
    public class PercentageToPointConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is double percentage)
            {
                // Convert percentage (0-100) to angle in radians (0-2π)
                double angle = (percentage / 100.0) * 2 * Math.PI - (Math.PI / 2);
                
                // Calculate point on circumference of a circle with radius 20 (40x40 circle)
                double x = 20 + (20 * Math.Cos(angle));
                double y = 20 + (20 * Math.Sin(angle));
                
                return new Point(x, y);
            }
            return new Point(20, 0); // Default to top of circle
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
