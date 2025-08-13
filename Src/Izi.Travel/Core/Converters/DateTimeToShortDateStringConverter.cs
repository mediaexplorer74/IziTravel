// ********************************************************************
// Type: Izi.Travel.Core.Converters.DateTimeToShortDateStringConverter
// Assembly: Izi.Travel, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.dll

using System;
using Windows.UI.Xaml.Data;
#nullable disable
namespace Izi.Travel.Core.Converters
{
  public class DateTimeToShortDateStringConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, string language)
    {
      // In UWP, use ToString with a format string instead of ToShortDateString()
      return value is DateTime dateTime ? dateTime.ToString("d") : null;
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

