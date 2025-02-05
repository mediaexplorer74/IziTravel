// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.Converters.CategoryToStringConverter
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Shell.Core.Resources;
using System;
using System.Globalization;
using System.Windows.Data;

#nullable disable
namespace Izi.Travel.Shell.Mtg.Converters
{
  public class CategoryToStringConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
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
      CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}
