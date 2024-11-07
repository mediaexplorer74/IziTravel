// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Data.Context.DownloadDataContext
// Assembly: Izi.Travel.Data, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: 9765AC3B-732C-4703-A0F8-C0EBF29D8E89
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Data.dll

using Izi.Travel.Data.Entities.Download;
using System.Data.Linq;

#nullable disable
namespace Izi.Travel.Data.Context
{
  public class DownloadDataContext : DataContext
  {
    public const string ConnectionString = "DataSource=isostore:/IziTravelDownload.sdf";
    public Table<DownloadObject> DownloadObjectTable;
    public Table<DownloadObjectLink> DownloadObjectLinkTable;
    public Table<DownloadMedia> DownloadMediaTable;

    public DownloadDataContext()
      : this("DataSource=isostore:/IziTravelDownload.sdf")
    {
    }

    private DownloadDataContext(string connectionString)
      : base(connectionString)
    {
    }
  }
}
