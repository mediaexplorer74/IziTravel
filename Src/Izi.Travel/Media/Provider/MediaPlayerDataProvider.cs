// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Media.Provider.MediaPlayerDataProvider
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Izi.Travel.Business.Entities.Media;

#nullable disable
namespace Izi.Travel.Shell.Media.Provider
{
  public class MediaPlayerDataProvider
  {
    private static volatile MediaPlayerDataProvider _instance;
    private static readonly object SyncRoot = new object();

    private MediaPlayerDataProvider()
    {
    }

    public static MediaPlayerDataProvider Instance
    {
      get
      {
        if (MediaPlayerDataProvider._instance == null)
        {
          lock (MediaPlayerDataProvider.SyncRoot)
          {
            if (MediaPlayerDataProvider._instance == null)
              MediaPlayerDataProvider._instance = new MediaPlayerDataProvider();
          }
        }
        return MediaPlayerDataProvider._instance;
      }
    }

    public MediaInfo[] MediaData { get; set; }

    public string MediaDataUid { get; set; }
  }
}
