using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Izi.Travel.Core.Converters
{
    public class BooleanToVisibilityConverter : IValueConverter
    {
        public bool Invert { get; set; }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            bool flag = (bool)value;
            if (this.Invert)
                flag = !flag;
            return (object)(Visibility)(flag ? 0 : 1);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
