// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Mapping.Entity.MtgChildrenListResultMetadataMapper
// Assembly: Izi.Travel.Business, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: ABF4D74A-55A9-49E1-BE11-CC83659F98DD
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.dll

using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Client.Entities;
using System;

#nullable disable
namespace Izi.Travel.Business.Mapping.Entity
{
  internal class MtgChildrenListResultMetadataMapper : 
    MapperBase<MtgChildrenListResultMetadata, MtgObjectChildrenListMetadata>
  {
    public override MtgObjectChildrenListMetadata Convert(MtgChildrenListResultMetadata source)
    {
      throw new NotImplementedException();
    }

    public override MtgChildrenListResultMetadata ConvertBack(MtgObjectChildrenListMetadata target)
    {
      if (target == null)
        return (MtgChildrenListResultMetadata) null;
      return new MtgChildrenListResultMetadata()
      {
        Offset = target.Offset,
        Limit = target.Limit,
        TotalCount = target.TotalCount,
        ReturnedCount = target.ReturnedCount,
        Spent = target.Spent,
        PageCurrent = target.PageCurrent,
        PageTotal = target.PageTotal,
        PageLeft = target.PageLeft,
        PageRight = target.PageRight,
        PageOutOfBounds = target.PageOutOfBounds
      };
    }
  }
}
