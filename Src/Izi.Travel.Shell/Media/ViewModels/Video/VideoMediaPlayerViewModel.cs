// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Media.ViewModels.Video.VideoMediaPlayerViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Izi.Travel.Business.Entities.Media;
using Izi.Travel.Shell.Media.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

#nullable disable
namespace Izi.Travel.Shell.Media.ViewModels.Video
{
  public class VideoMediaPlayerViewModel : MediaPlayerViewModel
  {
    private VideoMediaPlayerItemViewModel _video;

    public VideoMediaPlayerItemViewModel Video
    {
      get => this._video;
      private set
      {
        if (this._video == value)
          return;
        this._video = value;
        this.NotifyOfPropertyChange<VideoMediaPlayerItemViewModel>((Expression<Func<VideoMediaPlayerItemViewModel>>) (() => this.Video));
      }
    }

    protected override void OnActivate()
    {
      base.OnActivate();
      MediaInfo[] mediaData = MediaPlayerDataProvider.Instance.MediaData;
      if (mediaData == null || mediaData.Length == 0)
        return;
      MediaInfo mediaInfo = ((IEnumerable<MediaInfo>) mediaData).FirstOrDefault<MediaInfo>((Func<MediaInfo, bool>) (x => x.MediaFormat == MediaFormat.Video));
      if (mediaInfo == null)
        return;
      this.Video = new VideoMediaPlayerItemViewModel(mediaInfo);
    }
  }
}
