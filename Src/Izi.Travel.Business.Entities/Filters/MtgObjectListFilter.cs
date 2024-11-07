// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Entities.Filters.MtgObjectListFilter
// Assembly: Izi.Travel.Business.Entities, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: DDED5915-8B3A-4C03-AAF5-BE6B16E9CC4A
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.Entities.dll

using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Business.Entities.Filters.Base;

#nullable disable
namespace Izi.Travel.Business.Entities.Filters
{
  public class MtgObjectListFilter : MtgObjectListFilterBase
  {
    public string[] UidList { get; set; }

    public uint? Radius { get; set; }

    public GeoLocation Location { get; set; }

    public uint? ExclusionRadius { get; set; }

    public GeoLocation ExclusionLocation { get; set; }

    public string Query { get; set; }

    public virtual MtgObjectType[] Types { get; set; }

    public string Region { get; set; }
  }
}
