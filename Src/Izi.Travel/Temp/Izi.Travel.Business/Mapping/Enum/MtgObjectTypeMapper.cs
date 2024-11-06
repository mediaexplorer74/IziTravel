// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Mapping.Enum.MtgObjectTypeMapper
// Assembly: Izi.Travel.Business, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: ABF4D74A-55A9-49E1-BE11-CC83659F98DD
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.dll

#nullable disable
namespace Izi.Travel.Business.Mapping.Enum
{
  internal class MtgObjectTypeMapper : MapperBase<Izi.Travel.Business.Entities.Data.MtgObjectType, Izi.Travel.Client.Entities.MtgObjectType>
  {
    public override Izi.Travel.Client.Entities.MtgObjectType Convert(Izi.Travel.Business.Entities.Data.MtgObjectType source)
    {
      switch (source)
      {
        case Izi.Travel.Business.Entities.Data.MtgObjectType.Museum:
          return Izi.Travel.Client.Entities.MtgObjectType.Museum;
        case Izi.Travel.Business.Entities.Data.MtgObjectType.Exhibit:
          return Izi.Travel.Client.Entities.MtgObjectType.Exhibit;
        case Izi.Travel.Business.Entities.Data.MtgObjectType.StoryNavigation:
          return Izi.Travel.Client.Entities.MtgObjectType.NavigationStory;
        case Izi.Travel.Business.Entities.Data.MtgObjectType.Tour:
          return Izi.Travel.Client.Entities.MtgObjectType.Tour;
        case Izi.Travel.Business.Entities.Data.MtgObjectType.TouristAttraction:
          return Izi.Travel.Client.Entities.MtgObjectType.TouristAttraction;
        case Izi.Travel.Business.Entities.Data.MtgObjectType.Collection:
          return Izi.Travel.Client.Entities.MtgObjectType.Collection;
        case Izi.Travel.Business.Entities.Data.MtgObjectType.Country:
          return Izi.Travel.Client.Entities.MtgObjectType.Country;
        case Izi.Travel.Business.Entities.Data.MtgObjectType.City:
          return Izi.Travel.Client.Entities.MtgObjectType.City;
        case Izi.Travel.Business.Entities.Data.MtgObjectType.Publisher:
          return Izi.Travel.Client.Entities.MtgObjectType.Publisher;
        default:
          return Izi.Travel.Client.Entities.MtgObjectType.Unknown;
      }
    }

    public override Izi.Travel.Business.Entities.Data.MtgObjectType ConvertBack(Izi.Travel.Client.Entities.MtgObjectType target)
    {
      switch (target)
      {
        case Izi.Travel.Client.Entities.MtgObjectType.Museum:
          return Izi.Travel.Business.Entities.Data.MtgObjectType.Museum;
        case Izi.Travel.Client.Entities.MtgObjectType.Collection:
          return Izi.Travel.Business.Entities.Data.MtgObjectType.Collection;
        case Izi.Travel.Client.Entities.MtgObjectType.Exhibit:
          return Izi.Travel.Business.Entities.Data.MtgObjectType.Exhibit;
        case Izi.Travel.Client.Entities.MtgObjectType.Tour:
          return Izi.Travel.Business.Entities.Data.MtgObjectType.Tour;
        case Izi.Travel.Client.Entities.MtgObjectType.TouristAttraction:
          return Izi.Travel.Business.Entities.Data.MtgObjectType.TouristAttraction;
        case Izi.Travel.Client.Entities.MtgObjectType.NavigationStory:
          return Izi.Travel.Business.Entities.Data.MtgObjectType.StoryNavigation;
        case Izi.Travel.Client.Entities.MtgObjectType.City:
          return Izi.Travel.Business.Entities.Data.MtgObjectType.City;
        case Izi.Travel.Client.Entities.MtgObjectType.Country:
          return Izi.Travel.Business.Entities.Data.MtgObjectType.Country;
        case Izi.Travel.Client.Entities.MtgObjectType.Publisher:
          return Izi.Travel.Business.Entities.Data.MtgObjectType.Publisher;
        default:
          return Izi.Travel.Business.Entities.Data.MtgObjectType.Unknown;
      }
    }
  }
}
