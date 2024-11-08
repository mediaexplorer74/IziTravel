// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Entities.Download.DownloadPackageItem
// Assembly: Izi.Travel.Business.Entities, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: DDED5915-8B3A-4C03-AAF5-BE6B16E9CC4A
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.Entities.dll

using Izi.Travel.Business.Entities.Data;
using System.Collections.Generic;

#nullable disable
namespace Izi.Travel.Business.Entities.Download
{
  public class DownloadPackageItem
  {
    public DownloadObjectInfo LocalObject { get; set; }

    public MtgObject RemoteObject { get; set; }

    public Dictionary<string, int> LocalChidlrenUidList { get; private set; }

    public List<string> RemoteChildrenUidList { get; private set; }

    public List<DownloadPackageMediaItem> MediaItems { get; private set; }

    public DownloadPackageItemAction Action { get; set; }

    public DownloadPackageItem()
    {
      this.LocalChidlrenUidList = new Dictionary<string, int>();
      this.RemoteChildrenUidList = new List<string>();
      this.MediaItems = new List<DownloadPackageMediaItem>();
    }
  }
}
