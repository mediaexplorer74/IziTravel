// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.Converters.DistanceToStringConverter
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Izi.Travel.Shell.Core.Resources;
using System;
using System.Globalization;
using System.Windows.Data;

#nullable disable
namespace Izi.Travel.Shell.Mtg.Converters
{
  public class DistanceToStringConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      return value is double distance ? (object) DistanceToStringConverter.DistanceToString(distance) : (object) null;
    }

    public object ConvertBack(
      object value,
      Type targetType,
      object parameter,
      CultureInfo culture)
    {
      throw new NotImplementedException();
    }

    public static string DistanceToString(double distance)
    {
      if (distance < double.Epsilon)
        return (string) null;
      if (distance < 1000.0)
        return string.Format("{0} {1}", (object) (Math.Round(distance / 10.0) * 10.0), (object) AppResources.StringDistanceMeters);
      double num = Math.Ceiling(distance / 100.0) * 0.1;
      return string.Format(num < 10.0 ? "{0:F1} {1}" : "{0:######} {1}", (object) num, (object) AppResources.StringDistanceKilometers);
    }
  }
}
