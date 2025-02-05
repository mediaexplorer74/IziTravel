// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Core.Converters.TimeSpanToStringConverter
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Izi.Travel.Shell.Core.Resources;
using System;
using System.Globalization;
using System.Windows.Data;

#nullable disable
namespace Izi.Travel.Shell.Core.Converters
{
  public class TimeSpanToStringConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      if (!(value is TimeSpan timeSpan))
        return (object) string.Empty;
      bool result;
      bool.TryParse(parameter as string, out result);
      string empty = string.Empty;
      if (timeSpan.Days > 0)
        empty += string.Format("{0} {1} ", (object) timeSpan.Days, (object) AppResources.StringTimeSpanDays);
      if (timeSpan.Hours > 0)
        empty += string.Format("{0} {1} ", (object) timeSpan.Hours, (object) AppResources.StringTimeSpanHours);
      if (timeSpan.Minutes > 0)
        empty += string.Format("{0} {1} ", (object) timeSpan.Minutes, (object) AppResources.StringTimeSpanMinutes);
      if (timeSpan.Seconds > 0)
        empty += string.Format("{0} {1} ", (object) timeSpan.Seconds, (object) AppResources.StringTimeSpanSeconds);
      string str = empty.Trim();
      return !result ? (object) str : (object) str.ToUpper();
    }

    public object ConvertBack(
      object value,
      Type targetType,
      object parameter,
      CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}
