// Decompiled with JetBrains decompiler
// Type: Coding4Fun.Toolkit.Controls.Converters.SolidColorBrushToColorConverter
// Assembly: Coding4Fun.Toolkit.Controls, Version=2.0.7.0, Culture=neutral, PublicKeyToken=c5fd7b72b1a17ce4
// MVID: FA82EF8B-B083-4BA3-8FA6-4342AD0FAB1C
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Coding4Fun.Toolkit.Controls.dll

using System;
using System.Globalization;
using Windows.UI;
using Windows.UI.Xaml.Media;//using System.Windows.Media;

#nullable disable
namespace Coding4Fun.Toolkit.Controls.Converters
{
  public class SolidColorBrushToColorConverter : ValueConverter
  {
    public override object Convert(
      object value,
      Type targetType,
      object parameter,
      CultureInfo culture,
      string language)
    {
      return value is SolidColorBrush solidColorBrush 
                ? (object) solidColorBrush.Color 
                : (object) Colors.Transparent;
    }

    public override object ConvertBack(
      object value,
      Type targetType,
      object parameter,
      CultureInfo culture,
      string language)
    {
      throw new NotImplementedException();
    }
  }
}
