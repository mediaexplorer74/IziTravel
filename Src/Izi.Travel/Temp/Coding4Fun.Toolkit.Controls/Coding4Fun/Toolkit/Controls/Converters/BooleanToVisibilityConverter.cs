// Decompiled with JetBrains decompiler
// Type: Coding4Fun.Toolkit.Controls.Converters.BooleanToVisibilityConverter
// Assembly: Coding4Fun.Toolkit.Controls, Version=2.0.7.0, Culture=neutral, PublicKeyToken=c5fd7b72b1a17ce4
// MVID: FA82EF8B-B083-4BA3-8FA6-4342AD0FAB1C
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Coding4Fun.Toolkit.Controls.dll

using System;
using System.Globalization;
using System.Windows;

#nullable disable
namespace Coding4Fun.Toolkit.Controls.Converters
{
  public class BooleanToVisibilityConverter : ValueConverter
  {
    public override object Convert(
      object value,
      Type targetType,
      object parameter,
      CultureInfo culture,
      string language)
    {
      bool flag = System.Convert.ToBoolean(value);
      if (parameter != null)
        flag = !flag;
      return (object) (Visibility) (flag ? 0 : 1);
    }

    public override object ConvertBack(
      object value,
      Type targetType,
      object parameter,
      CultureInfo culture,
      string language)
    {
      return (object) value.Equals((object) Visibility.Visible);
    }
  }
}
