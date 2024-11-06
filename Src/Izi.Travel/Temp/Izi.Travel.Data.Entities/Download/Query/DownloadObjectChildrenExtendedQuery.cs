// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Data.Entities.Download.Query.DownloadObjectChildrenExtendedQuery
// Assembly: Izi.Travel.Data.Entities, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: C2535A39-73A9-477D-A740-0ABDD93ED172
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Data.Entities.dll

#nullable disable
namespace Izi.Travel.Data.Entities.Download.Query
{
  public class DownloadObjectChildrenExtendedQuery
  {
    public string PageUid { get; set; }

    public string PageExhibitNumber { get; set; }

    public int? Offset { get; set; }

    public int? Limit { get; set; }

    public string ParentUid { get; set; }

    public string[] Languages { get; set; }

    public DownloadObjectType[] Types { get; set; }

    public DownloadStatus[] Statuses { get; set; }

    public string[] Order { get; set; }
  }
}
