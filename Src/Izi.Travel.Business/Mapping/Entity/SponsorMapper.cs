// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Mapping.Entity.SponsorMapper
// Assembly: Izi.Travel.Business, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: ABF4D74A-55A9-49E1-BE11-CC83659F98DD
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.dll

using System;
using System.Linq;

#nullable disable
namespace Izi.Travel.Business.Mapping.Entity
{
  internal class SponsorMapper : MapperBase<Izi.Travel.Business.Entities.Data.Sponsor, Izi.Travel.Client.Entities.Sponsor>
  {
    private readonly MediaMapper _mediaMapper;

    public SponsorMapper(MediaMapper mediaMapper) => this._mediaMapper = mediaMapper;

    public override Izi.Travel.Client.Entities.Sponsor Convert(Izi.Travel.Business.Entities.Data.Sponsor source)
    {
      throw new NotImplementedException();
    }

    public override Izi.Travel.Business.Entities.Data.Sponsor ConvertBack(Izi.Travel.Client.Entities.Sponsor target)
    {
      if (target == null)
        return (Izi.Travel.Business.Entities.Data.Sponsor) null;
      return new Izi.Travel.Business.Entities.Data.Sponsor()
      {
        Images = target.Images != null ? target.Images.Select<Izi.Travel.Client.Entities.Media, Izi.Travel.Business.Entities.Data.Media>((Func<Izi.Travel.Client.Entities.Media, Izi.Travel.Business.Entities.Data.Media>) (x => this._mediaMapper.ConvertBack(x))).ToArray<Izi.Travel.Business.Entities.Data.Media>() : (Izi.Travel.Business.Entities.Data.Media[]) null,
        Order = target.Order,
        Name = target.Name,
        Website = target.Website
      };
    }
  }
}
