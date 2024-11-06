// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Media.ViewModels.Video.VideoMediaPlayerItemViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Izi.Travel.Business.Entities.Media;
using Izi.Travel.Business.Services;
using Izi.Travel.Shell.Core.Extensions;

#nullable disable
namespace Izi.Travel.Shell.Media.ViewModels.Video
{
  public class VideoMediaPlayerItemViewModel : MediaPlayerItemViewModel
  {
    private bool _isExternal;
    private string _url;

    public bool IsExternal
    {
      get => this._isExternal;
      private set
      {
        this.SetProperty<bool>(ref this._isExternal, value, propertyName: nameof (IsExternal));
      }
    }

    public string Url
    {
      get => this._url;
      private set => this.SetProperty<string>(ref this._url, value, propertyName: nameof (Url));
    }

    public VideoMediaPlayerItemViewModel(MediaInfo mediaInfo)
      : base(mediaInfo)
    {
      this.Initialize();
    }

    private void Initialize()
    {
      if (this.MediaInfo == null)
        return;
      this.IsExternal = !string.IsNullOrWhiteSpace(this.MediaInfo.VideoUrl);
      this.Url = !this.IsExternal ? ServiceFacade.MediaService.GetVideoUrl(this.MediaInfo.MediaUid, this.MediaInfo.ContentProviderUid) : this.MediaInfo.VideoUrl;
    }
  }
}
