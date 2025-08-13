// ********************************************************************
// Type: Izi.Travel.Media.ViewModels.Common.MediaItemViewModel
// Assembly: Izi.Travel, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.dll

using Caliburn.Micro;
using Izi.Travel.Business.Entities.Media;
using Izi.Travel.Core.Extensions;

#nullable disable
namespace Izi.Travel.Media.ViewModels.Common
{
  public class MediaItemViewModel : PropertyChangedBase
  {
    private readonly MediaInfo _mediaInfo;
    private bool _isSelected;

    public MediaInfo MediaInfo => this._mediaInfo;

    public bool IsSelected
    {
      get => this._isSelected;
      set => this.SetProperty<bool>(ref this._isSelected, value, propertyName: nameof (IsSelected));
    }

    public string Uid => this.MediaInfo == null ? (string) null : this.MediaInfo.MediaUid;

    public string ImageUrl => this.MediaInfo == null ? (string) null : this.MediaInfo.ImageUrl;

    public string PreviewUrl => this.MediaInfo == null ? (string) null : this.MediaInfo.PreviewUrl;

    public MediaItemViewModel(MediaInfo mediaInfo) => this._mediaInfo = mediaInfo;
  }
}
