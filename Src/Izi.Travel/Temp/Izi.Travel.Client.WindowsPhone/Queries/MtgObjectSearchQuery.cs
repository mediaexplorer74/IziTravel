// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Client.Queries.MtgObjectSearchQuery
// Assembly: Izi.Travel.Client.WindowsPhone, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: C73077B8-157D-49B0-834E-CAFCC993728A
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Client.WindowsPhone.dll

using Izi.Travel.Client.Entities;
using Izi.Travel.Client.Queries.Base;

#nullable disable
namespace Izi.Travel.Client.Queries
{
  public class MtgObjectSearchQuery : MtgObjectListQueryBase
  {
    public MtgObjectType[] Types { get; set; }

    public uint? Radius { get; set; }

    public Geopoint Location { get; set; }

    public uint? ExclusionRadius { get; set; }

    public Geopoint ExclusionLocation { get; set; }

    public string Query { get; set; }

    public string RegionUid { get; set; }
  }
}
