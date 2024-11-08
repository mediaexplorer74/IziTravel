// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Mapping.Enum.MtgObjectCategoryMapper
// Assembly: Izi.Travel.Business, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: ABF4D74A-55A9-49E1-BE11-CC83659F98DD
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.dll

#nullable disable
namespace Izi.Travel.Business.Mapping.Enum
{
  internal class MtgObjectCategoryMapper : MapperBase<Izi.Travel.Business.Entities.Data.MtgObjectCategory, Izi.Travel.Client.Entities.MtgObjectCategory>
  {
    public override Izi.Travel.Client.Entities.MtgObjectCategory Convert(Izi.Travel.Business.Entities.Data.MtgObjectCategory source)
    {
      switch (source)
      {
        case Izi.Travel.Business.Entities.Data.MtgObjectCategory.Walk:
          return Izi.Travel.Client.Entities.MtgObjectCategory.Walk;
        case Izi.Travel.Business.Entities.Data.MtgObjectCategory.Bike:
          return Izi.Travel.Client.Entities.MtgObjectCategory.Bike;
        case Izi.Travel.Business.Entities.Data.MtgObjectCategory.Bus:
          return Izi.Travel.Client.Entities.MtgObjectCategory.Bus;
        case Izi.Travel.Business.Entities.Data.MtgObjectCategory.Car:
          return Izi.Travel.Client.Entities.MtgObjectCategory.Car;
        case Izi.Travel.Business.Entities.Data.MtgObjectCategory.Boat:
          return Izi.Travel.Client.Entities.MtgObjectCategory.Boat;
        default:
          return Izi.Travel.Client.Entities.MtgObjectCategory.None;
      }
    }

    public override Izi.Travel.Business.Entities.Data.MtgObjectCategory ConvertBack(
      Izi.Travel.Client.Entities.MtgObjectCategory target)
    {
      switch (target)
      {
        case Izi.Travel.Client.Entities.MtgObjectCategory.Walk:
          return Izi.Travel.Business.Entities.Data.MtgObjectCategory.Walk;
        case Izi.Travel.Client.Entities.MtgObjectCategory.Bike:
          return Izi.Travel.Business.Entities.Data.MtgObjectCategory.Bike;
        case Izi.Travel.Client.Entities.MtgObjectCategory.Bus:
          return Izi.Travel.Business.Entities.Data.MtgObjectCategory.Bus;
        case Izi.Travel.Client.Entities.MtgObjectCategory.Car:
          return Izi.Travel.Business.Entities.Data.MtgObjectCategory.Car;
        case Izi.Travel.Client.Entities.MtgObjectCategory.Boat:
          return Izi.Travel.Business.Entities.Data.MtgObjectCategory.Boat;
        default:
          return Izi.Travel.Business.Entities.Data.MtgObjectCategory.None;
      }
    }
  }
}
