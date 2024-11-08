// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Data.Context.LocalDataContext
// Assembly: Izi.Travel.Data, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: 9765AC3B-732C-4703-A0F8-C0EBF29D8E89
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Data.dll

using Izi.Travel.Data.Entities.Local;
using System.Data.Linq;

#nullable disable
namespace Izi.Travel.Data.Context
{
  public class LocalDataContext : DataContext
  {
    public const string ConnectionString = "DataSource=isostore:/IziTravel.sdf";
    public Table<Purchase> Purchases;
    public Table<Bookmark> Bookmarks;
    public Table<History> Histories;
    public Table<TourPlaybackItem> TourPlaybackItems;
    public Table<AudioTrackData> Playlist;
    public Table<Izi.Travel.Data.Entities.Local.QuizData> QuizData;

    public LocalDataContext()
      : this("DataSource=isostore:/IziTravel.sdf")
    {
    }

    private LocalDataContext(string connectionString)
      : base(connectionString)
    {
    }
  }
}
