// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Converters.ExploreMapClusterTypeToObjectConverter
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Izi.Travel.Shell.Model.Explore;
using System;
using System.Globalization;
using System.Windows.Data;

#nullable disable
namespace Izi.Travel.Shell.Converters
{
  public class ExploreMapClusterTypeToObjectConverter : IValueConverter
  {
    public object Small { get; set; }

    public object Medium { get; set; }

    public object Large { get; set; }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      if (value is ExploreMapClusterType exploreMapClusterType)
      {
        switch (exploreMapClusterType)
        {
          case ExploreMapClusterType.Small:
            return this.Small;
          case ExploreMapClusterType.Medium:
            return this.Medium;
          case ExploreMapClusterType.Large:
            return this.Large;
        }
      }
      return targetType.IsValueType ? Activator.CreateInstance(targetType) : (object) null;
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
