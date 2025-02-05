// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Media.ViewModels.Image.ImageMediaPlayerItemViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Izi.Travel.Business.Entities.Media;
using Izi.Travel.Shell.Core.Command;
using Izi.Travel.Shell.Core.Extensions;
using System;
using System.Linq.Expressions;

#nullable disable
namespace Izi.Travel.Shell.Media.ViewModels.Image
{
  public sealed class ImageMediaPlayerItemViewModel : MediaPlayerItemViewModel
  {
    private string _urlHigh;
    private double _scale;
    private bool _isBusy;
    private RelayCommand _imageOpenedCommand;

    public string UrlHigh
    {
      get => this._urlHigh;
      set => this.SetProperty<string>(ref this._urlHigh, value, propertyName: nameof (UrlHigh));
    }

    public double Scale
    {
      get => this._scale;
      set
      {
        this.SetProperty<double, bool>(ref this._scale, value, (Expression<Func<bool>>) (() => this.IsScaled), propertyName: nameof (Scale));
      }
    }

    public bool IsBusy
    {
      get => this._isBusy;
      set => this.SetProperty<bool>(ref this._isBusy, value, propertyName: nameof (IsBusy));
    }

    public bool IsScaled => Math.Abs(1.0 - this.Scale) > 0.01;

    public RelayCommand ImageOpenedCommand
    {
      get
      {
        return this._imageOpenedCommand ?? (this._imageOpenedCommand = new RelayCommand((Action<object>) (x => this.IsBusy = false)));
      }
    }

    public ImageMediaPlayerItemViewModel(MediaInfo mediaInfo)
      : base(mediaInfo)
    {
      this.IsBusy = true;
      this._scale = 1.0;
      this.Initialize();
    }

    private void Initialize()
    {
      if (this.MediaInfo == null)
        return;
      this.UrlHigh = this.MediaInfo.ImageUrl;
    }
  }
}
