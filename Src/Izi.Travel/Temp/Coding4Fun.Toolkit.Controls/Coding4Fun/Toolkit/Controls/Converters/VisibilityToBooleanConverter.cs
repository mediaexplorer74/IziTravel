// Decompiled with JetBrains decompiler
// Type: Coding4Fun.Toolkit.Controls.Converters.VisibilityToBooleanConverter
// Assembly: Coding4Fun.Toolkit.Controls, Version=2.0.7.0, Culture=neutral, PublicKeyToken=c5fd7b72b1a17ce4
// MVID: FA82EF8B-B083-4BA3-8FA6-4342AD0FAB1C
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Coding4Fun.Toolkit.Controls.dll

using System;
using System.Globalization;

#nullable disable
namespace Coding4Fun.Toolkit.Controls.Converters
{
  public class VisibilityToBooleanConverter : ValueConverter
  {
    private static readonly BooleanToVisibilityConverter BoolToVis = new BooleanToVisibilityConverter();

    public override object Convert(
      object value,
      Type targetType,
      object parameter,
      CultureInfo culture,
      string language)
    {
      return VisibilityToBooleanConverter.BoolToVis.ConvertBack(value, targetType, parameter, culture);
    }

    public override object ConvertBack(
      object value,
      Type targetType,
      object parameter,
      CultureInfo culture,
      string language)
    {
      return VisibilityToBooleanConverter.BoolToVis.Convert(value, targetType, parameter, culture);
    }
  }
}
