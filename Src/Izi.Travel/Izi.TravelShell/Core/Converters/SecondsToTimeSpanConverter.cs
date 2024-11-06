﻿// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Core.Converters.SecondsToTimeSpanConverter
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using System;
using System.Globalization;
using System.Windows.Data;

#nullable disable
namespace Izi.Travel.Shell.Core.Converters
{
  public class SecondsToTimeSpanConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      return (object) (value is double num ? TimeSpan.FromSeconds(num) : TimeSpan.Zero);
    }

    public object ConvertBack(
      object value,
      Type targetType,
      object parameter,
      CultureInfo culture)
    {
      return (object) (value is TimeSpan timeSpan ? timeSpan.TotalSeconds : 0.0);
    }
  }
}
