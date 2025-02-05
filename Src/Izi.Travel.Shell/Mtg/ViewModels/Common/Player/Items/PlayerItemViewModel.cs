// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.ViewModels.Common.Player.Items.PlayerItemViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;
using Izi.Travel.Business.Entities.Analytics.Parameters;
using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Business.Entities.Media;
using Izi.Travel.Business.Services;
using Izi.Travel.Shell.Common.Controls;
using Izi.Travel.Shell.Core.Command;
using Izi.Travel.Shell.Core.Extensions;
using Izi.Travel.Shell.Core.Resources;
using Izi.Travel.Shell.Media.ViewModels.Audio;
using Izi.Travel.Shell.Mtg.Commands;
using Izi.Travel.Shell.Mtg.ViewModels.Quiz;
using System;
using System.Linq.Expressions;

#nullable disable
namespace Izi.Travel.Shell.Mtg.ViewModels.Common.Player.Items
{
  public abstract class PlayerItemViewModel : PropertyChangedBase
  {
    private readonly PlayerViewModel _playerViewModel;
    private readonly int _index;
    private readonly MtgObject _mtgObjectRoot;
    private readonly MtgObject _mtgObjectParent;
    private AudioContentViewModel _audioViewModel;
    private MtgObject _mtgObject;
    private string _imageUrl;
    private string _previewUrl;
    private bool _imageVisible;
    private bool _previewVisible;
    private bool _isNowPlaying;
    private bool _isQuizExpanded;
    private QuizViewModel _quizViewModel;
    private RelayCommand _expandQuizCommand;
    private OpenReviewListCommand _openReviewListCommand;
    private RateCommand _rateCommand;
    private OpenQuizCommand _openQuizCommand;
    private ShareCommand _shareCommand;
    private ToggleBookmarkCommand _toggleBookmarkCommand;
    private OpenVideoCommand _openVideoCommand;
    private RelayCommand _showInfoCommand;

    public PlayerViewModel PlayerViewModel => this._playerViewModel;

    public AudioContentViewModel AudioViewModel
    {
      get
      {
        AudioContentViewModel audioViewModel1 = this._audioViewModel;
        if (audioViewModel1 != null)
          return audioViewModel1;
        AudioContentViewModel contentViewModel = new AudioContentViewModel();
        contentViewModel.CreateHistory = true;
        AudioContentViewModel audioViewModel2 = contentViewModel;
        this._audioViewModel = contentViewModel;
        return audioViewModel2;
      }
    }

    public int Index => this._index;

    public MtgObject MtgObjectRoot => this._mtgObjectRoot;

    public MtgObject MtgObjectParent => this._mtgObjectParent;

    public MtgObject MtgObject => this._mtgObject;

    public string Uid => this.MtgObject == null ? string.Empty : this.MtgObject.Uid;

    public string Language
    {
      get
      {
        return this.MtgObject == null || this.MtgObject.MainContent == null ? string.Empty : this.MtgObject.MainContent.Language;
      }
    }

    public string Title
    {
      get
      {
        string forcedTitle = this.ForcedTitle;
        if (forcedTitle != null)
          return forcedTitle;
        return this.MtgObject == null || this.MtgObject.MainContent == null ? (string) null : this.MtgObject.MainContent.Title;
      }
    }

    public string ImageUrl
    {
      get => !this._imageVisible ? (string) null : this._imageUrl;
      set
      {
        this.SetProperty<string, bool>(ref this._imageUrl, value, (Expression<Func<bool>>) (() => this.HasImage), propertyName: nameof (ImageUrl));
      }
    }

    public bool HasImage => !string.IsNullOrWhiteSpace(this._imageUrl);

    public bool ImageVisible
    {
      get => this._imageVisible;
      set
      {
        this.SetProperty<bool, string>(ref this._imageVisible, value, (Expression<Func<string>>) (() => this.ImageUrl), propertyName: nameof (ImageVisible));
      }
    }

    public string PreviewUrl
    {
      get => this._previewUrl;
      set
      {
        this.SetProperty<string>(ref this._previewUrl, value, propertyName: nameof (PreviewUrl));
      }
    }

    public bool PreviewVisible
    {
      get => this._previewVisible;
      set
      {
        this.SetProperty<bool, string>(ref this._previewVisible, value, (Expression<Func<string>>) (() => this.PreviewUrl), propertyName: nameof (PreviewVisible));
      }
    }

    public string Number
    {
      get
      {
        return this.MtgObject == null || this.MtgObject.Location == null || this.MtgObject.Location.Number == null ? (string) null : "#" + this.MtgObject.Location.Number;
      }
    }

    public bool IsLoaded => this._mtgObject != null;

    public string ForcedTitle { get; set; }

    public bool IsNowPlaying
    {
      get => this._isNowPlaying;
      set
      {
        this.SetProperty<bool>(ref this._isNowPlaying, value, propertyName: nameof (IsNowPlaying));
      }
    }

    public QuizViewModel QuizViewModel => this._quizViewModel;

    public bool IsQuizExpanded
    {
      get => this._isQuizExpanded;
      set
      {
        if (this._isQuizExpanded == value)
          return;
        this._isQuizExpanded = value;
        this.NotifyOfPropertyChange(nameof (IsQuizExpanded));
        if (this._isQuizExpanded)
          ScreenExtensions.TryActivate((object) this.QuizViewModel);
        else
          ScreenExtensions.TryDeactivate((object) this.QuizViewModel, false);
      }
    }

    public RelayCommand ExpandQuizCommand
    {
      get
      {
        return this._expandQuizCommand ?? (this._expandQuizCommand = new RelayCommand((Action<object>) (x => { }), (Func<object, bool>) (x => PurchaseFlyoutDialog.ConditionalShow(this.MtgObjectRoot))));
      }
    }

    public OpenReviewListCommand OpenReviewListCommand
    {
      get
      {
        return this._openReviewListCommand ?? (this._openReviewListCommand = new OpenReviewListCommand(this.MtgObject));
      }
    }

    public RateCommand RateCommand
    {
      get => this._rateCommand ?? (this._rateCommand = new RateCommand(this.MtgObject));
    }

    public OpenQuizCommand OpenQuizCommand
    {
      get
      {
        return this._openQuizCommand ?? (this._openQuizCommand = new OpenQuizCommand(this.MtgObject, this.MtgObjectRoot));
      }
    }

    public ShareCommand ShareCommand
    {
      get
      {
        return this._shareCommand ?? (this._shareCommand = new ShareCommand(this.MtgObject, this.MtgObjectParent));
      }
    }

    public ToggleBookmarkCommand ToggleBookmarkCommand
    {
      get
      {
        ToggleBookmarkCommand toggleBookmarkCommand1 = this._toggleBookmarkCommand;
        if (toggleBookmarkCommand1 != null)
          return toggleBookmarkCommand1;
        ToggleBookmarkCommand toggleBookmarkCommand2 = new ToggleBookmarkCommand(this.MtgObject, this.MtgObjectParent != null ? this.MtgObjectParent.Uid : (string) null);
        toggleBookmarkCommand2.AddBookmarkLabel = AppResources.PlayerAddBookmarkCommand.ToLower();
        toggleBookmarkCommand2.RemoveBookmarkLabel = AppResources.PlayerRemoveBookmarkCommand.ToLower();
        ToggleBookmarkCommand toggleBookmarkCommand3 = toggleBookmarkCommand2;
        this._toggleBookmarkCommand = toggleBookmarkCommand2;
        return toggleBookmarkCommand3;
      }
    }

    public OpenVideoCommand OpenVideoCommand
    {
      get
      {
        return this._openVideoCommand ?? (this._openVideoCommand = new OpenVideoCommand(this.MtgObject, this.MtgObjectRoot));
      }
    }

    public RelayCommand ShowInfoCommand
    {
      get
      {
        return this._showInfoCommand ?? (this._showInfoCommand = new RelayCommand(new Action<object>(this.ShowInfo)));
      }
    }

    private void ShowInfo(object parameter)
    {
      InfoPartViewModel.Navigate(this.MtgObjectRoot, this.MtgObjectParent, this.MtgObject);
    }

    protected PlayerItemViewModel(
      PlayerViewModel playerViewModel,
      int index,
      MtgObject mtgObjectRoot,
      MtgObject mtgObjectParent,
      MtgObject mtgObject)
    {
      this._playerViewModel = playerViewModel;
      this._index = index;
      this._mtgObjectRoot = mtgObjectRoot;
      this._mtgObjectParent = mtgObjectParent;
      this.SetMtgObject(mtgObject);
    }

    public void Activate() => this.OnActivated();

    public void Deactivate() => this.OnDeactivating();

    public void SetMtgObject(MtgObject mtgObject)
    {
      if (mtgObject == null)
        return;
      this._mtgObject = mtgObject;
      this.ImageUrl = this.GetImageUrl();
      this.PreviewUrl = this.GetPreviewUrl();
      this._quizViewModel = new QuizViewModel(this.MtgObject);
      this.OnDataRefresh();
    }

    protected virtual void OnActivated()
    {
      if (this.MtgObject == null)
        return;
      this.RateCommand.RaiseCanExecuteChanged();
      this.ToggleBookmarkCommand.UpdateHasBookmark();
      this.AudioViewModel.Activate(this.MtgObject, this.MtgObjectParent, this.MtgObjectRoot, ActivationTypeParameter.Manual);
    }

    protected virtual void OnDeactivating() => this.AudioViewModel.Deactivate();

    protected virtual void OnDataRefresh()
    {
      this.NotifyOfPropertyChange<string>((Expression<Func<string>>) (() => this.Uid));
      this.NotifyOfPropertyChange<string>((Expression<Func<string>>) (() => this.Title));
      this.NotifyOfPropertyChange<string>((Expression<Func<string>>) (() => this.Number));
      this.Activate();
      AudioTrackInfo currentTrackInfo = ServiceFacade.AudioService.GetCurrentTrackInfo();
      this.IsNowPlaying = currentTrackInfo != null && currentTrackInfo.Key == this.MtgObject.Key;
    }

    protected virtual string GetImageUrl()
    {
      return this.MtgObject == null || this.MtgObject.MainImageMedia == null || this.MtgObject.ContentProvider == null ? (string) null : ServiceFacade.MediaService.GetImageUrl(this.MtgObject.MainImageMedia.Uid, this.MtgObject.ContentProvider.Uid, ImageFormat.Low480X360);
    }

    protected virtual string GetPreviewUrl()
    {
      return this.MtgObject == null || this.MtgObject.MainImageMedia == null || this.MtgObject.ContentProvider == null ? (string) null : ServiceFacade.MediaService.GetImageUrl(this.MtgObject.MainImageMedia.Uid, this.MtgObject.ContentProvider.Uid, ImageFormat.Low120X90);
    }
  }
}
