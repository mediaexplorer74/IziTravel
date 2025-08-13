using System;
using Windows.UI.Xaml.Data;

namespace Izi.Travel.Core.Converters
{
    public class DateTimeToFormattedStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is DateTime dateTime)
            {
                string formatString = parameter as string ?? "d";
                // Handle the case where the format string includes the prefix (e.g., ' - {0:d}')
                if (formatString.Contains("{0}"))
                {
                    return string.Format(formatString, dateTime);
                }
                return dateTime.ToString(formatString);
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotSupportedException();
        }
    }
}
