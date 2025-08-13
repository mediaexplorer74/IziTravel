// ********************************************************************
// Type: Izi.Travel.Converters.ExploreMapClusterTypeToObjectConverter
// Assembly: Izi.Travel, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.dll

using Izi.Travel.Model.Explore;
using System;
using Windows.UI.Xaml.Data;
#nullable disable
namespace Izi.Travel.Converters
{
  public class ExploreMapClusterTypeToObjectConverter : IValueConverter
  {
    public object Small { get; set; }

    public object Medium { get; set; }

    public object Large { get; set; }

    public object Convert(object value, Type targetType, object parameter, string language)
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
        return null;//argetType.IsValueType ? Activator.CreateInstance(targetType) : (object) null;
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

