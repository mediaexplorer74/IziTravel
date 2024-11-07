// IziTravel.Converters.NullToVisibilityConverter

using System;
using System.Globalization;
using System.Windows;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

#nullable disable
namespace IziTravel.Converters
{
  public class NullToVisibilityConverter : IValueConverter
  {  

    object IValueConverter.Convert(object value, Type targetType, object parameter, string language)
    {
        return value == null ? (object)(Visibility)1 : (object)(Visibility)0;
    }

    object IValueConverter.ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
  }
}
