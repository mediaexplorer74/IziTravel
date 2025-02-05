// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Media.ViewModels.Audio.AudioContentViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;
using Izi.Travel.Business.Entities.Analytics.Parameters;
using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Business.Entities.Filters;
using Izi.Travel.Business.Entities.Media;
using Izi.Travel.Business.Extensions;
using Izi.Travel.Business.Helper;
using Izi.Travel.Business.Services;
using Izi.Travel.Business.Services.Contract;
using Izi.Travel.Shell.Common.Controls;
using Izi.Travel.Shell.Core.Command;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Windows.Threading;
using Windows.Foundation;

#nullable disable
namespace Izi.Travel.Shell.Media.ViewModels.Audio
{
  public sealed class AudioContentViewModel : PropertyChangedBase
  {
    private readonly ILog _logger = LogManager.GetLog(typeof (AudioContentViewModel));
    private MtgObject _mtgObject;
    private MtgObject _mtgObjectParent;
    private MtgObject _mtgObjectRoot;
    private AudioTrackInfo _audioTrackInfo;
    private AudioContentPlayState _playState;
    private double _duration;
    private double _position;
    private bool _isSeeking;
    private readonly DispatcherTimer _refreshTimer;
    private RelayCommand _playCommand;
    private RelayCommand _pauseCommand;
    private RelayCommand _seekStartCommand;
    private RelayCommand _seekEndCommand;

    public event TypedEventHandler<AudioContentViewModel, AudioContentPlayState> PlayStateChanged
    {
      add
      {
        TypedEventHandler<AudioContentViewModel, AudioContentPlayState> typedEventHandler1 = this.PlayStateChanged;
        TypedEventHandler<AudioContentViewModel, AudioContentPlayState> typedEventHandler2;
        do
        {
          typedEventHandler2 = typedEventHandler1;
          typedEventHandler1 = Interlocked.CompareExchange<TypedEventHandler<AudioContentViewModel, AudioContentPlayState>>(ref this.PlayStateChanged, (TypedEventHandler<AudioContentViewModel, AudioContentPlayState>) Delegate.Combine((Delegate) typedEventHandler2, (Delegate) value), typedEventHandler2);
        }
        while (typedEventHandler1 != typedEventHandler2);
      }
      remove
      {
        TypedEventHandler<AudioContentViewModel, AudioContentPlayState> typedEventHandler1 = this.PlayStateChanged;
        TypedEventHandler<AudioContentViewModel, AudioContentPlayState> typedEventHandler2;
        do
        {
          typedEventHandler2 = typedEventHandler1;
          typedEventHandler1 = Interlocked.CompareExchange<TypedEventHandler<AudioContentViewModel, AudioContentPlayState>>(ref this.PlayStateChanged, (TypedEventHandler<AudioContentViewModel, AudioContentPlayState>) Delegate.Remove((Delegate) typedEventHandler2, (Delegate) value), typedEventHandler2);
        }
        while (typedEventHandler1 != typedEventHandler2);
      }
    }

    public AudioContentPlayState PlayState
    {
      get => this._playState;
      set
      {
        if (this._playState == value)
          return;
        this._playState = value;
        this.NotifyOfPropertyChange<AudioContentPlayState>((Expression<Func<AudioContentPlayState>>) (() => this.PlayState));
        this.NotifyOfPropertyChange<bool>((Expression<Func<bool>>) (() => this.IsPlayEnabled));
        this.NotifyOfPropertyChange<bool>((Expression<Func<bool>>) (() => this.IsPauseEnabled));
        this.NotifyOfPropertyChange<bool>((Expression<Func<bool>>) (() => this.IsStarted));
        this.NotifyOfPropertyChange<bool>((Expression<Func<bool>>) (() => this.IsPlaying));
        this.NotifyOfPropertyChange<string>((Expression<Func<string>>) (() => this.ActionImageUrl));
        this.NotifyOfPropertyChange<bool>((Expression<Func<bool>>) (() => this.IsVisible));
        this.PlayCommand.RaiseCanExecuteChanged();
        this.PauseCommand.RaiseCanExecuteChanged();
        this.NotifyOfPropertyChange<RelayCommand>((Expression<Func<RelayCommand>>) (() => this.ToggleCommand));
      }
    }

    public double Duration
    {
      get => this._duration;
      set
      {
        if (Math.Abs(this._duration - value) <= double.Epsilon)
          return;
        this._duration = value;
        this.NotifyOfPropertyChange<double>((Expression<Func<double>>) (() => this.Duration));
        this.NotifyOfPropertyChange<double>((Expression<Func<double>>) (() => this.Percentage));
        this.NotifyOfPropertyChange<double>((Expression<Func<double>>) (() => this.SecondsFromStart));
        this.NotifyOfPropertyChange<string>((Expression<Func<string>>) (() => this.SecondsFromStartString));
        this.NotifyOfPropertyChange<double>((Expression<Func<double>>) (() => this.SecondsFromEnd));
        this.NotifyOfPropertyChange<string>((Expression<Func<string>>) (() => this.SecondsFromEndString));
      }
    }

    public double Position
    {
      get => this._position;
      set
      {
        if (Math.Abs(this._position - value) <= double.Epsilon)
          return;
        this._position = value;
        this.NotifyOfPropertyChange<double>((Expression<Func<double>>) (() => this.Position));
        this.NotifyOfPropertyChange<double>((Expression<Func<double>>) (() => this.Percentage));
        this.NotifyOfPropertyChange<double>((Expression<Func<double>>) (() => this.SecondsFromStart));
        this.NotifyOfPropertyChange<string>((Expression<Func<string>>) (() => this.SecondsFromStartString));
        this.NotifyOfPropertyChange<double>((Expression<Func<double>>) (() => this.SecondsFromEnd));
        this.NotifyOfPropertyChange<string>((Expression<Func<string>>) (() => this.SecondsFromEndString));
      }
    }

    public double Percentage => this.Duration <= 0.0 ? 0.0 : this.Position / this.Duration * 100.0;

    public double SecondsFromStart
    {
      get => this.Duration * (this.Duration > 0.0 ? this.Position / this.Duration : 0.0);
    }

    public double SecondsFromEnd => this.Duration - this.SecondsFromStart;

    public string SecondsFromStartString
    {
      get => TimeSpan.FromSeconds(this.SecondsFromStart).ToString("mm\\:ss");
    }

    public string SecondsFromEndString
    {
      get => "-" + TimeSpan.FromSeconds(this.SecondsFromEnd).ToString("mm\\:ss");
    }

    public bool IsPlayEnabled
    {
      get
      {
        return this.PlayState == AudioContentPlayState.Initialized || this.PlayState == AudioContentPlayState.Paused;
      }
    }

    public bool IsPauseEnabled => this.PlayState == AudioContentPlayState.Playing;

    public bool IsPlaying => this.PlayState == AudioContentPlayState.Playing;

    public bool IsStarted
    {
      get
      {
        return this.PlayState == AudioContentPlayState.Playing || this.PlayState == AudioContentPlayState.Paused;
      }
    }

    public bool IsSeeking => this._isSeeking;

    public bool IsVisible
    {
      get
      {
        return this.PlayState != AudioContentPlayState.None && this.PlayState != AudioContentPlayState.Error;
      }
    }

    public bool HasAudio => this._mtgObject != null && this._mtgObject.MainAudioMedia != null;

    public double MainAudioMediaDuration
    {
      get
      {
        return this._mtgObject == null || this._mtgObject.MainAudioMedia == null ? 0.0 : (double) this._mtgObject.MainAudioMedia.Duration;
      }
    }

    public string ActionImageUrl
    {
      get
      {
        switch (this.PlayState)
        {
          case AudioContentPlayState.Initialized:
          case AudioContentPlayState.Paused:
            return "/Assets/Icons/appbar.play.png";
          case AudioContentPlayState.Playing:
            return "/Assets/Icons/appbar.pause.png";
          default:
            return (string) null;
        }
      }
    }

    public bool CreateHistory { get; set; }

    public AudioContentViewModel()
    {
      this.SetPlayState(AudioContentPlayState.None);
      this._refreshTimer = new DispatcherTimer()
      {
        Interval = TimeSpan.FromMilliseconds(500.0)
      };
      this._refreshTimer.Tick += new EventHandler(this.OnRefreshTimerTick);
    }

    public RelayCommand ToggleCommand => !this.IsPlayEnabled ? this.PauseCommand : this.PlayCommand;

    public RelayCommand PlayCommand
    {
      get
      {
        return this._playCommand ?? (this._playCommand = new RelayCommand(new Action<object>(this.ExecutePlayCommand), new Func<object, bool>(this.CanExecutePlayCommand)));
      }
    }

    private bool CanExecutePlayCommand(object parameter) => this.IsPlayEnabled;

    private async void ExecutePlayCommand(object parameter)
    {
      if (this._mtgObject == null || this._mtgObject.MainContent == null || !this._mtgObject.IsParentType() && !PurchaseFlyoutDialog.ConditionalShow(this._mtgObjectRoot))
        return;
      this.PlayState = AudioContentPlayState.Playing;
      if (this._audioTrackInfo == null)
      {
        if (this._mtgObject.MainContent.Audio == null)
        {
          MtgObjectFilter filter = new MtgObjectFilter(this._mtgObject.Uid, this._mtgObject.Language);
          filter.Includes = ContentSection.None;
          filter.Excludes = ContentSection.All;
          AudioContentViewModel contentViewModel = this;
          MtgObject mtgObject = contentViewModel._mtgObject;
          MtgObject mtgObjectAsync = await MtgObjectServiceHelper.GetMtgObjectAsync(filter);
          contentViewModel._mtgObject = mtgObjectAsync;
          contentViewModel = (AudioContentViewModel) null;
        }
        this._audioTrackInfo = AudioTrackInfoHelper.FromMtgObject(this._mtgObject, this._mtgObjectParent, ActivationTypeParameter.Manual);
      }
      if (this._audioTrackInfo == null)
      {
        this.PlayState = AudioContentPlayState.Initialized;
      }
      else
      {
        ServiceFacade.AudioService.Play(this._audioTrackInfo);
        if (!this.CreateHistory)
          return;
        await ServiceFacade.MtgObjectService.CreateOrUpdateHistoryAsync(this._mtgObject, this._mtgObjectParent != null ? this._mtgObjectParent.Uid : (string) null);
      }
    }

    public RelayCommand PauseCommand
    {
      get
      {
        return this._pauseCommand ?? (this._pauseCommand = new RelayCommand(new Action<object>(this.ExecutePauseCommand), new Func<object, bool>(this.CanExecutePauseCommand)));
      }
    }

    private bool CanExecutePauseCommand(object parameter) => this.IsPauseEnabled;

    private void ExecutePauseCommand(object parameter)
    {
      if (this._mtgObject == null || !ServiceFacade.AudioService.IsNowPlaying(this._mtgObject.Uid, this._mtgObject.Language))
        return;
      this.PlayState = AudioContentPlayState.Paused;
      ServiceFacade.AudioService.Pause();
    }

    public RelayCommand SeekStartCommand
    {
      get
      {
        return this._seekStartCommand ?? (this._seekStartCommand = new RelayCommand(new Action<object>(this.ExecuteSeekStartCommand)));
      }
    }

    private void ExecuteSeekStartCommand(object parameter)
    {
      this._isSeeking = true;
      this._refreshTimer.Stop();
    }

    public RelayCommand SeekEndCommand
    {
      get
      {
        return this._seekEndCommand ?? (this._seekEndCommand = new RelayCommand(new Action<object>(this.ExecuteSeekEndCommand)));
      }
    }

    private void ExecuteSeekEndCommand(object parameter)
    {
      ServiceFacade.AudioService.Position = TimeSpan.FromSeconds((double) parameter);
      this._isSeeking = false;
      this._refreshTimer.Start();
    }

    public void Activate(
      MtgObject mtgObject,
      MtgObject mtgObjectParent,
      MtgObject mtgObjectRoot,
      ActivationTypeParameter activationType)
    {
      if (this.PlayState == AudioContentPlayState.None)
      {
        this._mtgObject = mtgObject;
        this._mtgObjectParent = mtgObjectParent;
        this._mtgObjectRoot = mtgObjectRoot;
      }
      this.NotifyOfPropertyChange<bool>((Expression<Func<bool>>) (() => this.HasAudio));
      this.RefreshState();
      this._refreshTimer.Start();
      // ISSUE: method pointer
      ServiceFacade.AudioService.StateChanged += new TypedEventHandler<IAudioService, AudioServiceState>((object) this, __methodptr(OnAudioPlayerStateChanged));
    }

    public void Deactivate()
    {
      this._refreshTimer.Stop();
      // ISSUE: method pointer
      ServiceFacade.AudioService.StateChanged -= new TypedEventHandler<IAudioService, AudioServiceState>((object) this, __methodptr(OnAudioPlayerStateChanged));
      this._mtgObject = (MtgObject) null;
      this._audioTrackInfo = (AudioTrackInfo) null;
      this.PlayState = AudioContentPlayState.None;
      this.NotifyOfPropertyChange<bool>((Expression<Func<bool>>) (() => this.HasAudio));
    }

    public void RefreshState()
    {
      try
      {
        AudioServiceState state = ServiceFacade.AudioService.State;
        AudioTrackInfo currentTrackInfo = ServiceFacade.AudioService.GetCurrentTrackInfo();
        if (this._mtgObject == null || currentTrackInfo == null || state == AudioServiceState.Error || currentTrackInfo.MtgObjectUid != this._mtgObject.Uid || currentTrackInfo.Language != this._mtgObject.Language)
        {
          this.Position = 0.0;
          this.Duration = this.MainAudioMediaDuration;
          this.SetPlayState(this._mtgObject != null ? AudioContentPlayState.Initialized : AudioContentPlayState.None);
        }
        else
        {
          switch (state)
          {
            case AudioServiceState.Paused:
              this.SetPlayState(AudioContentPlayState.Paused);
              break;
            case AudioServiceState.Playing:
              this.SetPlayState(AudioContentPlayState.Playing);
              break;
            default:
              this.SetPlayState(AudioContentPlayState.Initialized);
              break;
          }
          double totalSeconds = ServiceFacade.AudioService.Duration.TotalSeconds;
          this.Duration = totalSeconds > 0.0 ? totalSeconds : this.MainAudioMediaDuration;
          this.Position = ServiceFacade.AudioService.Position.TotalSeconds;
        }
      }
      catch (Exception ex)
      {
        this._logger.Error(ex);
      }
    }

    private void SetPlayState(AudioContentPlayState playState)
    {
      if (this.PlayState == playState)
        return;
      this.PlayState = playState;
      // ISSUE: reference to a compiler-generated field
      this.PlayStateChanged?.Invoke(this, this.PlayState);
    }

    private void OnAudioPlayerStateChanged(object sender, AudioServiceState eventArgs)
    {
      this.RefreshState();
    }

    private void OnRefreshTimerTick(object sender, EventArgs eventArgs)
    {
      if (this._mtgObject == null || this.PlayState != AudioContentPlayState.Playing)
        return;
      AudioTrackInfo currentTrackInfo = ServiceFacade.AudioService.GetCurrentTrackInfo();
      if (currentTrackInfo == null || currentTrackInfo.MtgObjectUid != this._mtgObject.Uid && currentTrackInfo.Language != this._mtgObject.Language)
        return;
      this.Position = ServiceFacade.AudioService.Position.TotalSeconds;
    }
  }
}
