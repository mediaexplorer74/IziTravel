// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.ViewModels.Tour.Map.TourMapItemViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;
using Izi.Travel.Business.Entities.Analytics.Parameters;
using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Business.Entities.Media;
using Izi.Travel.Business.Entities.TourPlayback;
using Izi.Travel.Business.Managers;
using Izi.Travel.Business.Services;
using Izi.Travel.Shell.Core.Command;
using Izi.Travel.Shell.Core.Extensions;
using Izi.Travel.Shell.Core.Services;
using Izi.Travel.Shell.Media.ViewModels.Audio;
using Izi.Travel.Shell.Mtg.ViewModels.Common.Detail;
using Izi.Travel.Shell.Mtg.ViewModels.Common.Map;
using System;
using System.Linq.Expressions;
using Windows.Foundation;

#nullable disable
namespace Izi.Travel.Shell.Mtg.ViewModels.Tour.Map
{
  public class TourMapItemViewModel : BaseMapItemViewModel
  {
    private readonly TourMapPartViewModel _mapViewModel;
    private AudioContentViewModel _audioViewModel;
    private int _order;
    private string _imageUrl;
    private bool _isPlaying;
    private bool _isVisited;
    private bool _isHidden;
    private RelayCommand _navigateCommand;

    public TourMapPartViewModel MapViewModel => this._mapViewModel;

    public AudioContentViewModel AudioViewModel
    {
      get => this._audioViewModel ?? (this._audioViewModel = new AudioContentViewModel());
    }

    public override bool IsSelected
    {
      get => base.IsSelected;
      set
      {
        base.IsSelected = value;
        this.NotifyOfPropertyChange<TourMapItemState>((Expression<Func<TourMapItemState>>) (() => this.State));
        if (this.MapViewModel == null)
          return;
        this.MapViewModel.NotifyOfPropertyChange<BaseMapItemViewModel>((Expression<Func<BaseMapItemViewModel>>) (() => this.MapViewModel.SelectedItem));
        if (!value)
          return;
        this.MapViewModel.ViewCenter = this.Location;
      }
    }

    public int Order
    {
      get => this._order;
      set => this.SetProperty<int>(ref this._order, value, propertyName: nameof (Order));
    }

    public string ImageUrl
    {
      get => this._imageUrl;
      set => this.SetProperty<string>(ref this._imageUrl, value, propertyName: nameof (ImageUrl));
    }

    public bool IsPlaying
    {
      get => this._isPlaying;
      set
      {
        this.SetProperty<bool, TourMapItemState>(ref this._isPlaying, value, (Expression<Func<TourMapItemState>>) (() => this.State), propertyName: nameof (IsPlaying));
      }
    }

    public bool IsVisited
    {
      get => this._isVisited;
      set
      {
        this.SetProperty<bool, TourMapItemState>(ref this._isVisited, value, (Expression<Func<TourMapItemState>>) (() => this.State), propertyName: nameof (IsVisited));
      }
    }

    public bool IsHidden
    {
      get => this._isHidden;
      set => this.SetProperty<bool>(ref this._isHidden, value, propertyName: nameof (IsHidden));
    }

    public TourMapItemState State
    {
      get
      {
        if (this.IsSelected)
          return TourMapItemState.Selected;
        return this.IsVisited && !this.IsPlaying ? TourMapItemState.Visited : TourMapItemState.Idle;
      }
    }

    public RelayCommand NavigateCommand
    {
      get
      {
        return this._navigateCommand ?? (this._navigateCommand = new RelayCommand(new Action<object>(this.ExecuteNavigateCommand)));
      }
    }

    private void ExecuteNavigateCommand(object parameter)
    {
      ShellServiceFacade.NavigationService.UriFor<DetailPartViewModel>().WithParam<string>((Expression<Func<DetailPartViewModel, string>>) (x => x.Uid), this.Uid).WithParam<string>((Expression<Func<DetailPartViewModel, string>>) (x => x.Language), this.Language).WithParam<string>((Expression<Func<DetailPartViewModel, string>>) (x => x.ParentUid), this._mapViewModel.MtgObject.Uid).Navigate();
    }

    public TourMapItemViewModel(TourMapPartViewModel mapViewModel, MtgObject mtgObject, int order)
      : base(mtgObject)
    {
      this._mapViewModel = mapViewModel;
      this.Order = order;
      if (this.MtgObject == null)
        return;
      this.IsHidden = this.MtgObject.Hidden;
      this.ImageUrl = ServiceFacade.MediaService.GetImageOrPlaceholderUrl(this.MtgObject, ImageFormat.Low120X90);
    }

    public void Activate()
    {
      // ISSUE: method pointer
      this.AudioViewModel.PlayStateChanged += new TypedEventHandler<AudioContentViewModel, AudioContentPlayState>((object) this, __methodptr(OnAudioViewModelPlayStateChanged));
      this.AudioViewModel.Activate(this.MtgObject, this._mapViewModel.MtgObject, this._mapViewModel.MtgObject, ActivationTypeParameter.Manual);
      this.IsPlaying = this.AudioViewModel.PlayState == AudioContentPlayState.Playing;
    }

    public void Deactivate()
    {
      // ISSUE: method pointer
      this.AudioViewModel.PlayStateChanged -= new TypedEventHandler<AudioContentViewModel, AudioContentPlayState>((object) this, __methodptr(OnAudioViewModelPlayStateChanged));
      this.AudioViewModel.Deactivate();
    }

    public void RefreshAttractionState()
    {
      this.AudioViewModel.RefreshState();
      TourPlaybackAttraction attraction = TourPlaybackManager.GetAttraction(this.MapViewModel.Uid, this.Language, this.Uid);
      this.IsVisited = attraction != null && attraction.IsVisited;
      this.IsPlaying = this.AudioViewModel.PlayState == AudioContentPlayState.Playing;
    }

    private void OnAudioViewModelPlayStateChanged(
      AudioContentViewModel audioViewModel,
      AudioContentPlayState state)
    {
      this.RefreshAttractionState();
    }
  }
}
