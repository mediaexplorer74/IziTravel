﻿// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Core.Converters.BoolToOppositeConverter
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using System;
using System.Globalization;
using System.Windows.Data;

#nullable disable
namespace Izi.Travel.Shell.Core.Converters
{
  public class BoolToOppositeConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      return (object) (bool) (!(value is bool flag) ? 0 : (!flag ? 1 : 0));
    }

    public object ConvertBack(
      object value,
      Type targetType,
      object parameter,
      CultureInfo culture)
    {
      return (object) (bool) (!(value is bool flag) ? 0 : (!flag ? 1 : 0));
    }
  }
}
