// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Mapping.Entity.MtgObjectCityCompactMapper
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
  internal class MtgObjectCityCompactMapper : MapperBase<MtgObject, CityCompact>
  {
    private readonly MtgObjectTypeMapper _mtgObjectTypeMapper;
    private readonly MtgObjectStatusMapper _mtgObjectStatusMapper;
    private readonly LocationMapper _locationMapper;
    private readonly MapMapper _mapMapper;
    private readonly MediaMapper _mediaMapper;

    public MtgObjectCityCompactMapper(
      MtgObjectTypeMapper mtgObjectTypeMapper,
      MtgObjectStatusMapper mtgObjectStatusMapper,
      LocationMapper locationMapper,
      MapMapper mapMapper,
      MediaMapper mediaMapper)
    {
      this._mtgObjectTypeMapper = mtgObjectTypeMapper;
      this._mtgObjectStatusMapper = mtgObjectStatusMapper;
      this._locationMapper = locationMapper;
      this._mapMapper = mapMapper;
      this._mediaMapper = mediaMapper;
    }

    public override CityCompact Convert(MtgObject source) => throw new NotImplementedException();

    public override MtgObject ConvertBack(CityCompact target)
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
      mtgObject.Type = this._mtgObjectTypeMapper.ConvertBack(target.Type);
      mtgObject.Languages = target.Languages != null ? target.Languages.ToArray() : (string[]) null;
      mtgObject.Status = this._mtgObjectStatusMapper.ConvertBack(target.Status);
      mtgObject.Content = new Content[1]{ content };
      mtgObject.Location = this._locationMapper.ConvertBack(target.Location);
      mtgObject.Map = this._mapMapper.ConvertBack(target.Map);
      mtgObject.ChildrenCount = target.ChildrenCount;
      mtgObject.Hash = target.Hash;
      return mtgObject;
    }
  }
}
