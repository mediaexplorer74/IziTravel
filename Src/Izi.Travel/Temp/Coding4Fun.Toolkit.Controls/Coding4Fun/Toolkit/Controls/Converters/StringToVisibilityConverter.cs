// Decompiled with JetBrains decompiler
// Type: Coding4Fun.Toolkit.Controls.Converters.StringToVisibilityConverter
// Assembly: Coding4Fun.Toolkit.Controls, Version=2.0.7.0, Culture=neutral, PublicKeyToken=c5fd7b72b1a17ce4
// MVID: FA82EF8B-B083-4BA3-8FA6-4342AD0FAB1C
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Coding4Fun.Toolkit.Controls.dll

using System;
using System.Globalization;
using System.Windows;

#nullable disable
namespace Coding4Fun.Toolkit.Controls.Converters
{
  public class StringToVisibilityConverter : ValueConverter
  {
    public bool Inverted { get; set; }

    public override object Convert(
      object value,
      Type targetType,
      object parameter,
      CultureInfo culture,
      string language)
    {
      return this.Inverted ? (object) (Visibility) (string.IsNullOrEmpty(value as string) ? 0 : 1) : (object) (Visibility) (string.IsNullOrEmpty(value as string) ? 1 : 0);
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
