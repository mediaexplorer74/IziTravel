// IziTravel.Converters.BooleanToDataVirtualizationModeConverter

using System;
using System.Globalization;
using Windows.UI.Xaml.Data;
//using System.Windows.Data;
//using Telerik.Windows.Controls;

#nullable disable
namespace IziTravel.Converters
{
  public class BooleanToDataVirtualizationModeConverter : IValueConverter
  {
        object IValueConverter.Convert(object value, Type targetType, object parameter, string language)
        {
            return default;//(object) (DataVirtualizationMode) ((bool) value ? 3 : 0);
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    
    }
}
