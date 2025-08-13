// ********************************************************************
// Type: Izi.Travel.Mtg.Converters.CategoryToStringConverter
// Assembly: Izi.Travel, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.dll

using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Core.Resources;
using System;
using Windows.UI.Xaml.Data;
#nullable disable
namespace Izi.Travel.Mtg.Converters
{
  public class CategoryToStringConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, string language)
    {
      if (value is MtgObjectCategory mtgObjectCategory)
      {
        switch (mtgObjectCategory)
        {
          case MtgObjectCategory.Walk:
            return (object) AppResources.EnumCategoryWalk;
          case MtgObjectCategory.Bike:
            return (object) AppResources.EnumCategoryBike;
          case MtgObjectCategory.Bus:
            return (object) AppResources.EnumCategoryBus;
          case MtgObjectCategory.Car:
            return (object) AppResources.EnumCategoryCar;
          case MtgObjectCategory.Boat:
            return (object) AppResources.EnumCategoryBoat;
        }
      }
      return (object) AppResources.StringEmpty;
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

