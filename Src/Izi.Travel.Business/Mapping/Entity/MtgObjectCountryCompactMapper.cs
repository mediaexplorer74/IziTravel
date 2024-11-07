// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Mapping.Entity.MtgObjectCountryCompactMapper
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
  internal class MtgObjectCountryCompactMapper : MapperBase<MtgObject, CountryCompact>
  {
    private readonly MtgObjectTypeMapper _mtgObjectTypeMapper;
    private readonly MtgObjectStatusMapper _mtgObjectStatusMapper;
    private readonly LocationMapper _locationMapper;
    private readonly MapMapper _mapMapper;

    public MtgObjectCountryCompactMapper(
      MtgObjectTypeMapper mtgObjectTypeMapper,
      MtgObjectStatusMapper mtgObjectStatusMapper,
      LocationMapper locationMapper,
      MapMapper mapMapper)
    {
      this._mtgObjectTypeMapper = mtgObjectTypeMapper;
      this._mtgObjectStatusMapper = mtgObjectStatusMapper;
      this._locationMapper = locationMapper;
      this._mapMapper = mapMapper;
    }

    public override CountryCompact Convert(MtgObject source) => throw new NotImplementedException();

    public override MtgObject ConvertBack(CountryCompact target)
    {
      if (target == null)
        return (MtgObject) null;
      Content content = new Content()
      {
        Title = target.Title,
        Language = target.Language,
        Summary = target.Summary
      };
      MtgObject mtgObject = new MtgObject();
      mtgObject.Uid = target.Uid;
      mtgObject.Type = this._mtgObjectTypeMapper.ConvertBack(target.Type);
      mtgObject.Languages = target.Languages != null ? target.Languages.ToArray() : (string[]) null;
      mtgObject.Status = this._mtgObjectStatusMapper.ConvertBack(target.Status);
      mtgObject.CountryCode = target.Code;
      mtgObject.Content = new Content[1]{ content };
      mtgObject.Location = this._locationMapper.ConvertBack(target.Location);
      mtgObject.Map = this._mapMapper.ConvertBack(target.Map);
      mtgObject.ChildrenCount = target.ChildrenCount;
      mtgObject.Hash = target.Hash;
      return mtgObject;
    }
  }
}
