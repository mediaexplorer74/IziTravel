// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Mapping.Entity.MtgObjectFeaturedContentMapper
// Assembly: Izi.Travel.Business, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: ABF4D74A-55A9-49E1-BE11-CC83659F98DD
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.dll

using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Business.Mapping.Enum;
using Izi.Travel.Client.Entities;
using System;

#nullable disable
namespace Izi.Travel.Business.Mapping.Entity
{
  internal class MtgObjectFeaturedContentMapper : MapperBase<MtgObject, FeaturedContent>
  {
    private readonly MtgObjectTypeMapper _mtgObjectTypeMapper;
    private readonly MtgObjectCategoryMapper _mtgObjectCategoryMapper;
    private readonly MediaTypeMapper _mediaTypeMapper;

    public MtgObjectFeaturedContentMapper(
      MtgObjectTypeMapper mtgObjectTypeMapper,
      MtgObjectCategoryMapper mtgObjectCategoryMapper,
      MediaTypeMapper mediaTypeMapper)
    {
      this._mtgObjectTypeMapper = mtgObjectTypeMapper;
      this._mtgObjectCategoryMapper = mtgObjectCategoryMapper;
      this._mediaTypeMapper = mediaTypeMapper;
    }

    public override FeaturedContent Convert(MtgObject source)
    {
      throw new NotImplementedException();
    }

    public override MtgObject ConvertBack(FeaturedContent target)
    {
      if (target == null)
        return (MtgObject) null;
      Content content1 = new Content()
      {
        Title = target.Title,
        Summary = target.SubTitle
      };
      if (target.Images != null && target.Images.Length != 0)
      {
        Content content2 = content1;
        Izi.Travel.Business.Entities.Data.Media[] mediaArray = new Izi.Travel.Business.Entities.Data.Media[1];
        Izi.Travel.Business.Entities.Data.Media media = new Izi.Travel.Business.Entities.Data.Media();
        media.Uid = target.Images[0].Uid;
        media.Type = this._mediaTypeMapper.ConvertBack(target.Images[0].Type);
        mediaArray[0] = media;
        content2.Images = mediaArray;
      }
      MtgObject mtgObject = new MtgObject();
      mtgObject.Uid = target.Uid;
      mtgObject.Type = this._mtgObjectTypeMapper.ConvertBack(target.Type);
      mtgObject.Category = this._mtgObjectCategoryMapper.ConvertBack(target.Category);
      mtgObject.Location = new Izi.Travel.Business.Entities.Data.Location()
      {
        Number = target.Position.ToString("0000")
      };
      mtgObject.Content = new Content[1]{ content1 };
      return mtgObject;
    }
  }
}
