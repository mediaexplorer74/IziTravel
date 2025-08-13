// ********************************************************************
// Type: Izi.Travel.Core.Converters.NullToVisibilityConverter
// Assembly: Izi.Travel, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.dll

using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
#nullable disable
namespace Izi.Travel.Core.Converters
{
  /// <summary>
  /// Converts null to a Visibility value (Collapsed when null, Visible when not null)
  /// </summary>
  public class NullToVisibilityConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, string language)
    {
      bool isNull = value == null;
      // If parameter is "Inverse", invert the logic
      if (parameter is string param && param.Equals("Inverse", StringComparison.OrdinalIgnoreCase))
        isNull = !isNull;
      return isNull ? Visibility.Collapsed : Visibility.Visible;
    }

    public object ConvertBack(
      object value,
      Type targetType,
      object parameter,
      string language)
    {
      throw new NotImplementedException();
    }
  }
}
