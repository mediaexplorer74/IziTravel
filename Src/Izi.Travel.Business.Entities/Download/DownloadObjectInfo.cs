// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Entities.Download.DownloadObjectInfo
// Assembly: Izi.Travel.Business.Entities, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: DDED5915-8B3A-4C03-AAF5-BE6B16E9CC4A
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.Entities.dll

using Izi.Travel.Data.Entities.Download;

#nullable disable
namespace Izi.Travel.Business.Entities.Download
{
  public class DownloadObjectInfo
  {
    public int Id { get; set; }

    public string Uid { get; set; }

    public string Language { get; set; }

    public DownloadObjectType Type { get; set; }

    public string Hash { get; set; }

    public static DownloadObjectInfo FromDownloadObject(DownloadObject downloadObject)
    {
      if (downloadObject == null)
        return (DownloadObjectInfo) null;
      return new DownloadObjectInfo()
      {
        Id = downloadObject.Id,
        Uid = downloadObject.Uid,
        Language = downloadObject.Language,
        Type = downloadObject.Type,
        Hash = downloadObject.Hash
      };
    }
  }
}
