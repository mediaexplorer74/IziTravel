// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Mapping.Entity.PublisherContentMapper
// Assembly: Izi.Travel.Business, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: ABF4D74A-55A9-49E1-BE11-CC83659F98DD
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.dll

using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Client.Entities;
using System;
using System.Linq;

#nullable disable
namespace Izi.Travel.Business.Mapping.Entity
{
  internal class PublisherContentMapper : MapperBase<Content, PublisherContent>
  {
    private readonly MediaMapper _mediaMapper;
    private readonly MtgObjectCompactMapper _mtgObjectCompactMapper;

    public PublisherContentMapper(
      MediaMapper mediaMapper,
      MtgObjectCompactMapper mtgObjectCompactMapper)
    {
      this._mediaMapper = mediaMapper;
      this._mtgObjectCompactMapper = mtgObjectCompactMapper;
    }

    public override PublisherContent Convert(Content source) => throw new NotImplementedException();

    public override Content ConvertBack(PublisherContent target)
    {
      if (target == null)
        return (Content) null;
      Content content = new Content()
      {
        Language = target.Language,
        Title = target.Title,
        Summary = target.Summary,
        Description = target.Description
      };
      if (target.Images != null)
        content.Images = target.Images.Select<Izi.Travel.Client.Entities.Media, Izi.Travel.Business.Entities.Data.Media>((Func<Izi.Travel.Client.Entities.Media, Izi.Travel.Business.Entities.Data.Media>) (x => this._mediaMapper.ConvertBack(x))).ToArray<Izi.Travel.Business.Entities.Data.Media>();
      if (target.Children != null)
        content.Children = target.Children.Select<MtgObjectCompact, MtgObject>((Func<MtgObjectCompact, MtgObject>) (x => this._mtgObjectCompactMapper.ConvertBack(x))).ToArray<MtgObject>();
      return content;
    }
  }
}
