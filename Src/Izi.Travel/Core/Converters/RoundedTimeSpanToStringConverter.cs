// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Core.Converters.RoundedTimeSpanToStringConverter
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Izi.Travel.Shell.Core.Resources;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;

#nullable disable
namespace Izi.Travel.Shell.Core.Converters
{
  public class RoundedTimeSpanToStringConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      return value is TimeSpan timeSpan ? (object) RoundedTimeSpanToStringConverter.RoundedTimeSpanToString(timeSpan) : (object) null;
    }

    public object ConvertBack(
      object value,
      Type targetType,
      object parameter,
      CultureInfo culture)
    {
      throw new NotImplementedException();
    }

    public static string RoundedTimeSpanToString(TimeSpan timeSpan)
    {
      timeSpan = TimeSpan.FromMinutes(Math.Ceiling(Math.Max(timeSpan.TotalMinutes, 1.0) / 20.0) * 20.0);
      List<string> values = new List<string>();
      int totalHours = (int) timeSpan.TotalHours;
      if (totalHours > 0)
        values.Add(string.Format("{0} {1}", (object) totalHours, (object) AppResources.StringTimeSpanHours));
      if (timeSpan.Minutes > 0)
        values.Add(string.Format("{0} {1}", (object) timeSpan.Minutes, (object) AppResources.StringTimeSpanMinutes));
      return string.Join(" ", (IEnumerable<string>) values);
    }
  }
}
