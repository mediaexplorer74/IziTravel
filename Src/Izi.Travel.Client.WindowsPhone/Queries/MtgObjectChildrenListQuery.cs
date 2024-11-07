// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Client.Queries.MtgObjectChildrenListQuery
// Assembly: Izi.Travel.Client.WindowsPhone, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: C73077B8-157D-49B0-834E-CAFCC993728A
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Client.WindowsPhone.dll

using Izi.Travel.Client.Entities;
using Izi.Travel.Client.Queries.Base;

#nullable disable
namespace Izi.Travel.Client.Queries
{
  public class MtgObjectChildrenListQuery : MtgObjectListQueryBase
  {
    public string Uid { get; set; }

    public MtgObjectType[] Types { get; set; }

    public string PageUid { get; set; }

    public string PageExhibitNumber { get; set; }

    public string SortExhibits { get; set; }

    public bool IncludeHidden { get; set; }
  }
}
