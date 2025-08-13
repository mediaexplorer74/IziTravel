// ********************************************************************
// Type: Izi.Travel.Core.Converters.BoolToInvisibilityConverter
// Assembly: Izi.Travel, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.dll

using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Izi.Travel.Core.Converters
{
  /// <summary>
  /// Converts a boolean value to a Visibility value (Collapsed when true, Visible when false)
  /// </summary>
  public sealed class BoolToInvisibilityConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, string language)
    {
      if (!(value is bool boolValue))
        return Visibility.Visible;
                
      // If parameter is "Inverse", invert the boolean value
      if (parameter is string param && param.Equals("Inverse", StringComparison.OrdinalIgnoreCase))
        boolValue = !boolValue;
                
      return boolValue ? Visibility.Collapsed : Visibility.Visible;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
      if (!(value is Visibility visibility))
        return false;
                
      bool result = visibility != Visibility.Visible;
                
      // If parameter is "Inverse", invert the boolean value
      if (parameter is string param && param.Equals("Inverse", StringComparison.OrdinalIgnoreCase))
        result = !result;
                
      return result;
    }
  }
}
