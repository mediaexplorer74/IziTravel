// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Mapping.Entity.MediaMapper
// Assembly: Izi.Travel.Business, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: ABF4D74A-55A9-49E1-BE11-CC83659F98DD
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.dll

using Izi.Travel.Business.Entities.Media;
using Izi.Travel.Business.Mapping.Enum;
using System;

#nullable disable
namespace Izi.Travel.Business.Mapping.Entity
{
  internal class MediaMapper : MapperBase<Izi.Travel.Business.Entities.Data.Media, Izi.Travel.Client.Entities.Media>
  {
    private readonly MediaTypeMapper _mediaTypeMapper;

    public MediaMapper(MediaTypeMapper mediaTypeMapper) => this._mediaTypeMapper = mediaTypeMapper;

    public override Izi.Travel.Client.Entities.Media Convert(Izi.Travel.Business.Entities.Data.Media source)
    {
      throw new NotImplementedException();
    }

    public override Izi.Travel.Business.Entities.Data.Media ConvertBack(Izi.Travel.Client.Entities.Media target)
    {
      return this.ConvertBack(target, MediaFormat.Image);
    }

    public Izi.Travel.Business.Entities.Data.Media ConvertBack(Izi.Travel.Client.Entities.Media target, MediaFormat format)
    {
      if (target == null)
        return (Izi.Travel.Business.Entities.Data.Media) null;
      Izi.Travel.Business.Entities.Data.Media media = new Izi.Travel.Business.Entities.Data.Media();
      media.Uid = target.Uid;
      media.Hash = target.Hash;
      media.Type = this._mediaTypeMapper.ConvertBack(target.Type);
      media.Duration = target.Duration;
      media.Order = target.Order;
      media.Size = target.Size;
      media.Title = target.Title;
      media.Url = target.Url;
      media.Format = format;
      return media;
    }
  }
}
