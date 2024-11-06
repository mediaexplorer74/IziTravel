// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Mapping.Enum.MediaTypeMapper
// Assembly: Izi.Travel.Business, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: ABF4D74A-55A9-49E1-BE11-CC83659F98DD
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.dll

#nullable disable
namespace Izi.Travel.Business.Mapping.Enum
{
  internal class MediaTypeMapper : MapperBase<Izi.Travel.Business.Entities.Data.MediaType, Izi.Travel.Client.Entities.MediaType>
  {
    public override Izi.Travel.Client.Entities.MediaType Convert(Izi.Travel.Business.Entities.Data.MediaType source)
    {
      switch (source)
      {
        case Izi.Travel.Business.Entities.Data.MediaType.Story:
          return Izi.Travel.Client.Entities.MediaType.Story;
        case Izi.Travel.Business.Entities.Data.MediaType.Map:
          return Izi.Travel.Client.Entities.MediaType.Map;
        case Izi.Travel.Business.Entities.Data.MediaType.BrandLogo:
          return Izi.Travel.Client.Entities.MediaType.BrandLogo;
        case Izi.Travel.Business.Entities.Data.MediaType.BrandCover:
          return Izi.Travel.Client.Entities.MediaType.BrandCover;
        case Izi.Travel.Business.Entities.Data.MediaType.SponsorLogo:
          return Izi.Travel.Client.Entities.MediaType.SponsorLogo;
        case Izi.Travel.Business.Entities.Data.MediaType.Featured:
          return Izi.Travel.Client.Entities.MediaType.Featured;
        case Izi.Travel.Business.Entities.Data.MediaType.YouTube:
          return Izi.Travel.Client.Entities.MediaType.YouTube;
        default:
          return Izi.Travel.Client.Entities.MediaType.Unknown;
      }
    }

    public override Izi.Travel.Business.Entities.Data.MediaType ConvertBack(Izi.Travel.Client.Entities.MediaType target)
    {
      switch (target)
      {
        case Izi.Travel.Client.Entities.MediaType.Story:
          return Izi.Travel.Business.Entities.Data.MediaType.Story;
        case Izi.Travel.Client.Entities.MediaType.Map:
          return Izi.Travel.Business.Entities.Data.MediaType.Map;
        case Izi.Travel.Client.Entities.MediaType.BrandLogo:
          return Izi.Travel.Business.Entities.Data.MediaType.BrandLogo;
        case Izi.Travel.Client.Entities.MediaType.BrandCover:
          return Izi.Travel.Business.Entities.Data.MediaType.BrandCover;
        case Izi.Travel.Client.Entities.MediaType.SponsorLogo:
          return Izi.Travel.Business.Entities.Data.MediaType.SponsorLogo;
        case Izi.Travel.Client.Entities.MediaType.Featured:
          return Izi.Travel.Business.Entities.Data.MediaType.Featured;
        case Izi.Travel.Client.Entities.MediaType.YouTube:
          return Izi.Travel.Business.Entities.Data.MediaType.YouTube;
        default:
          return Izi.Travel.Business.Entities.Data.MediaType.Unknown;
      }
    }
  }
}
