// ********************************************************************
// Type: Izi.Travel.Shell.Core.Converters.NullToInvisibilityConverter
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
#nullable disable
namespace Izi.Travel.Shell.Core.Converters
{
  /// <summary>
  /// Converts null to a Visibility value (Visible when null, Collapsed when not null)
  /// </summary>
  public class NullToInvisibilityConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, string language)
    {
      bool isNull = value == null;
      // If parameter is "Inverse", invert the logic
      if (parameter is string param && param.Equals("Inverse", StringComparison.OrdinalIgnoreCase))
        isNull = !isNull;
      return isNull ? Visibility.Visible : Visibility.Collapsed;
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
