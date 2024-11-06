// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Mapping.Entity.MtgObjectPublisherCompactMapper
// Assembly: Izi.Travel.Business, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: ABF4D74A-55A9-49E1-BE11-CC83659F98DD
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.dll

using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Business.Mapping.Enum;
using Izi.Travel.Client.Entities;
using System;
using System.Linq;

#nullable disable
namespace Izi.Travel.Business.Mapping.Entity
{
  internal class MtgObjectPublisherCompactMapper : MapperBase<MtgObject, PublisherCompact>
  {
    private readonly MtgObjectTypeMapper _mtgObjectTypeMapper;
    private readonly MediaMapper _mediaMapper;
    private readonly MtgObjectStatusMapper _mtgObjectStatusMapper;
    private readonly ContentProviderMapper _contentProviderMapper;

    public MtgObjectPublisherCompactMapper(
      MtgObjectTypeMapper mtgObjectTypeMapper,
      MediaMapper mediaMapper,
      MtgObjectStatusMapper mtgObjectStatusMapper,
      ContentProviderMapper contentProviderMapper)
    {
      this._mtgObjectTypeMapper = mtgObjectTypeMapper;
      this._mediaMapper = mediaMapper;
      this._mtgObjectStatusMapper = mtgObjectStatusMapper;
      this._contentProviderMapper = contentProviderMapper;
    }

    public override PublisherCompact Convert(MtgObject source)
    {
      throw new NotImplementedException();
    }

    public override MtgObject ConvertBack(PublisherCompact target)
    {
      if (target == null)
        return (MtgObject) null;
      Content content = new Content()
      {
        Title = target.Title,
        Language = target.Language,
        Summary = target.Summary
      };
      if (target.Images != null && target.Images.Count > 0)
        content.Images = target.Images.Select<Izi.Travel.Client.Entities.Media, Izi.Travel.Business.Entities.Data.Media>((Func<Izi.Travel.Client.Entities.Media, Izi.Travel.Business.Entities.Data.Media>) (x => this._mediaMapper.ConvertBack(x))).ToArray<Izi.Travel.Business.Entities.Data.Media>();
      MtgObject mtgObject = new MtgObject();
      mtgObject.Uid = target.Uid;
      mtgObject.Languages = target.Languages != null ? target.Languages.ToArray() : (string[]) null;
      mtgObject.Type = this._mtgObjectTypeMapper.ConvertBack(target.Type);
      mtgObject.Status = this._mtgObjectStatusMapper.ConvertBack(target.Status);
      mtgObject.ContentProvider = this._contentProviderMapper.ConvertBack(target.ContentProvider);
      mtgObject.Content = new Content[1]{ content };
      mtgObject.Hash = target.Hash;
      mtgObject.AccessType = MtgObjectAccessType.Online;
      return mtgObject;
    }
  }
}
