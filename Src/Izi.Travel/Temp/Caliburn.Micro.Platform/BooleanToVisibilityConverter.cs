// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.BooleanToVisibilityConverter
// Assembly: Caliburn.Micro.Platform, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 5D8D2AFD-482F-40D3-8F5B-6788C31BBFD5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Platform.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Platform.xml

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>
  /// An <see cref="T:System.Windows.Data.IValueConverter" /> which converts <see cref="T:System.Boolean" /> to <see cref="T:System.Windows.Visibility" />.
  /// </summary>
  public class BooleanToVisibilityConverter : IValueConverter
  {
    /// <summary>
    /// Converts a boolean value to a <see cref="T:System.Windows.Visibility" /> value.
    /// </summary>
    /// <param name="value">The value produced by the binding source.</param>
    /// <param name="targetType">The type of the binding target property.</param>
    /// <param name="parameter">The converter parameter to use.</param>
    /// <param name="culture">The culture to use in the converter.</param>
    /// <returns>
    /// A converted value. If the method returns null, the valid null value is used.
    /// </returns>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      return (object) (Visibility) ((bool) value ? 0 : 1);
    }

    /// <summary>
    /// Converts a value <see cref="T:System.Windows.Visibility" /> value to a boolean value.
    /// </summary>
    /// <param name="value">The value that is produced by the binding target.</param>
    /// <param name="targetType">The type to convert to.</param>
    /// <param name="parameter">The converter parameter to use.</param>
    /// <param name="culture">The culture to use in the converter.</param>
    /// <returns>
    /// A converted value. If the method returns null, the valid null value is used.
    /// </returns>
    public object ConvertBack(
      object value,
      Type targetType,
      object parameter,
      CultureInfo culture)
    {
      return (object) ((Visibility) value == Visibility.Visible);
    }
  }
}
