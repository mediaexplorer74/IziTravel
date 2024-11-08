// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Mapping.Entity.MtgChildrenListResultCompactMapper
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
  internal class MtgChildrenListResultCompactMapper : 
    MapperBase<MtgChildrenListResult, MtgObjectChildrenListResult<MtgObjectCompact>>
  {
    private readonly MtgChildrenListResultMetadataMapper _mtgChildrenListResultMetadataMapper;
    private readonly MtgObjectCompactMapper _mtgObjectCompactMapper;

    public MtgChildrenListResultCompactMapper(
      MtgChildrenListResultMetadataMapper mtgChildrenListResultMetadataMapper,
      MtgObjectCompactMapper mtgObjectCompactMapper)
    {
      this._mtgChildrenListResultMetadataMapper = mtgChildrenListResultMetadataMapper;
      this._mtgObjectCompactMapper = mtgObjectCompactMapper;
    }

    public override MtgObjectChildrenListResult<MtgObjectCompact> Convert(
      MtgChildrenListResult source)
    {
      throw new NotImplementedException();
    }

    public override MtgChildrenListResult ConvertBack(
      MtgObjectChildrenListResult<MtgObjectCompact> target)
    {
      if (target == null)
        return (MtgChildrenListResult) null;
      return new MtgChildrenListResult()
      {
        Metadata = this._mtgChildrenListResultMetadataMapper.ConvertBack(target.Metadata),
        Data = target.Data != null ? ((IEnumerable<MtgObjectCompact>) target.Data).Select<MtgObjectCompact, MtgObject>((Func<MtgObjectCompact, MtgObject>) (x => this._mtgObjectCompactMapper.ConvertBack(x))).ToArray<MtgObject>() : (MtgObject[]) null
      };
    }
  }
}
