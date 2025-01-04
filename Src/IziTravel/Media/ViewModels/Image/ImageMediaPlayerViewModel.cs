// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Media.ViewModels.Image.ImageMediaPlayerViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Izi.Travel.Business.Entities.Media;
using Izi.Travel.Shell.Core.Extensions;
using Izi.Travel.Shell.Core.Resources;
using Izi.Travel.Shell.Media.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

#nullable disable
namespace Izi.Travel.Shell.Media.ViewModels.Image
{
  public class ImageMediaPlayerViewModel : MediaPlayerViewModel
  {
    private ImageMediaPlayerItemViewModel[] _images;
    private ImageMediaPlayerItemViewModel _selectedImage;

    public ImageMediaPlayerItemViewModel[] Images
    {
      get => this._images;
      set
      {
        this.SetProperty<ImageMediaPlayerItemViewModel[], string>(ref this._images, value, (Expression<Func<string>>) (() => this.SelectedImageIndex), propertyName: nameof (Images));
      }
    }

    public ImageMediaPlayerItemViewModel SelectedImage
    {
      get => this._selectedImage;
      set
      {
        this.SetProperty<ImageMediaPlayerItemViewModel, string>(ref this._selectedImage, value, (Expression<Func<string>>) (() => this.SelectedImageIndex), (Action) (() =>
        {
          if (this._selectedImage != null)
            MediaPlayerDataProvider.Instance.MediaDataUid = this._selectedImage.Uid;
          foreach (ImageMediaPlayerItemViewModel image in this.Images)
            image.Scale = 1.0;
        }), nameof (SelectedImage));
      }
    }

    public string SelectedImageIndex
    {
      get
      {
        if (this.Images == null || this.Images.Length == 0)
          return string.Empty;
        return (Math.Max(Array.IndexOf<ImageMediaPlayerItemViewModel>(this.Images, this.SelectedImage), 0) + 1).ToString() + " " + AppResources.StringOf + " " + (object) this.Images.Length;
      }
    }

    protected override void OnInitialize()
    {
      base.OnInitialize();
      MediaInfo[] mediaData = MediaPlayerDataProvider.Instance.MediaData;
      string mediaDataUid = MediaPlayerDataProvider.Instance.MediaDataUid;
      if (mediaData == null || mediaData.Length == 0)
        return;
      MediaInfo[] array = ((IEnumerable<MediaInfo>) mediaData).Where<MediaInfo>((Func<MediaInfo, bool>) (x => x.MediaFormat == MediaFormat.Image)).ToArray<MediaInfo>();
      if (array.Length == 0)
        return;
      this.Images = ((IEnumerable<MediaInfo>) array).Select<MediaInfo, ImageMediaPlayerItemViewModel>((Func<MediaInfo, ImageMediaPlayerItemViewModel>) (x => new ImageMediaPlayerItemViewModel(x))).ToArray<ImageMediaPlayerItemViewModel>();
      this.SelectedImage = ((IEnumerable<ImageMediaPlayerItemViewModel>) this.Images).FirstOrDefault<ImageMediaPlayerItemViewModel>((Func<ImageMediaPlayerItemViewModel, bool>) (x => string.IsNullOrWhiteSpace(mediaDataUid) || x.MediaInfo.MediaUid == mediaDataUid));
    }
  }
}
