// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Data.Entities.Download.DownloadObjectChildrenExtendedResult
// Assembly: Izi.Travel.Data.Entities, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: C2535A39-73A9-477D-A740-0ABDD93ED172
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Data.Entities.dll

#nullable disable
namespace Izi.Travel.Data.Entities.Download
{
  public class DownloadObjectChildrenExtendedResult
  {
    public DownloadObject[] DownloadObjects { get; set; }

    public int Offset { get; set; }

    public int Limit { get; set; }

    public int TotalCount { get; set; }

    public int PageCurrent { get; set; }

    public int PageTotal { get; set; }

    public bool PageLeft { get; set; }

    public bool PageRight { get; set; }
  }
}
