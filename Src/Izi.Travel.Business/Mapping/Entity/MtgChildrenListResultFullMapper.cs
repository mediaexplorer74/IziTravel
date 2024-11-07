// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Mapping.Entity.MtgChildrenListResultFullMapper
// Assembly: Izi.Travel.Business, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: ABF4D74A-55A9-49E1-BE11-CC83659F98DD
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.dll

using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Client.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Izi.Travel.Business.Mapping.Entity
{
  internal class MtgChildrenListResultFullMapper : 
    MapperBase<MtgChildrenListResult, MtgObjectChildrenListResult<MtgObjectFull>>
  {
    private readonly MtgChildrenListResultMetadataMapper _mtgChildrenListResultMetadataMapper;
    private readonly MtgObjectFullMapper _mtgObjectFullMapper;

    public MtgChildrenListResultFullMapper(
      MtgChildrenListResultMetadataMapper mtgChildrenListResultMetadataMapper,
      MtgObjectFullMapper mtgObjectFullMapper)
    {
      this._mtgChildrenListResultMetadataMapper = mtgChildrenListResultMetadataMapper;
      this._mtgObjectFullMapper = mtgObjectFullMapper;
    }

    public override MtgObjectChildrenListResult<MtgObjectFull> Convert(MtgChildrenListResult source)
    {
      throw new NotImplementedException();
    }

    public override MtgChildrenListResult ConvertBack(
      MtgObjectChildrenListResult<MtgObjectFull> target)
    {
      if (target == null)
        return (MtgChildrenListResult) null;
      return new MtgChildrenListResult()
      {
        Metadata = this._mtgChildrenListResultMetadataMapper.ConvertBack(target.Metadata),
        Data = target.Data != null ? ((IEnumerable<MtgObjectFull>) target.Data).Select<MtgObjectFull, MtgObject>((Func<MtgObjectFull, MtgObject>) (x => this._mtgObjectFullMapper.ConvertBack(x))).ToArray<MtgObject>() : (MtgObject[]) null
      };
    }
  }
}
