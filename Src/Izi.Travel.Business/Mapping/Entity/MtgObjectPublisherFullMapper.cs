// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Mapping.Entity.MtgObjectPublisherFullMapper
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
  internal class MtgObjectPublisherFullMapper : MapperBase<MtgObject, PublisherFull>
  {
    private readonly MtgObjectTypeMapper _mtgObjectTypeMapper;
    private readonly MtgObjectStatusMapper _mtgObjectStatusMapper;
    private readonly ContentProviderMapper _contentProviderMapper;
    private readonly PublisherContentMapper _publisherContentMapper;
    private readonly PublisherContactsMapper _publisherContactsMapper;

    public MtgObjectPublisherFullMapper(
      MtgObjectTypeMapper mtgObjectTypeMapper,
      MtgObjectStatusMapper mtgObjectStatusMapper,
      ContentProviderMapper contentProviderMapper,
      PublisherContentMapper publisherContentMapper,
      PublisherContactsMapper publisherContactsMapper)
    {
      this._mtgObjectTypeMapper = mtgObjectTypeMapper;
      this._mtgObjectStatusMapper = mtgObjectStatusMapper;
      this._contentProviderMapper = contentProviderMapper;
      this._publisherContentMapper = publisherContentMapper;
      this._publisherContactsMapper = publisherContactsMapper;
    }

    public override PublisherFull Convert(MtgObject source) => throw new NotImplementedException();

    public override MtgObject ConvertBack(PublisherFull target)
    {
      if (target == null)
        return (MtgObject) null;
      MtgObject mtgObject = new MtgObject();
      mtgObject.Uid = target.Uid;
      mtgObject.Contacts = this._publisherContactsMapper.ConvertBack(target.Contacts);
      mtgObject.Type = this._mtgObjectTypeMapper.ConvertBack(target.Type);
      mtgObject.Languages = target.Languages != null ? target.Languages.ToArray() : (string[]) null;
      mtgObject.Status = this._mtgObjectStatusMapper.ConvertBack(target.Status);
      mtgObject.Content = target.Content != null ? target.Content.Select<PublisherContent, Content>((Func<PublisherContent, Content>) (x => this._publisherContentMapper.ConvertBack(x))).ToArray<Content>() : (Content[]) null;
      mtgObject.ContentProvider = this._contentProviderMapper.ConvertBack(target.ContentProvider);
      mtgObject.Hash = target.Hash;
      mtgObject.AccessType = MtgObjectAccessType.Online;
      return mtgObject;
    }
  }
}
