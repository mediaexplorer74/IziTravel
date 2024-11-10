// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Core.Converters.TimeSpanToSecondsConverter
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using System;
using System.Globalization;
using System.Windows.Data;

#nullable disable
namespace Izi.Travel.Shell.Core.Converters
{
  public class TimeSpanToSecondsConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      return (object) (value is TimeSpan timeSpan ? timeSpan.TotalSeconds : 0.0);
    }

    public object ConvertBack(
      object value,
      Type targetType,
      object parameter,
      CultureInfo culture)
    {
      return (object) (value is double num ? TimeSpan.FromSeconds(num) : TimeSpan.Zero);
    }
  }
}
