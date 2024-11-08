// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Services.Implementation.AudioService
// Assembly: Izi.Travel.Business, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: ABF4D74A-55A9-49E1-BE11-CC83659F98DD
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.dll

using Caliburn.Micro;
using Izi.Travel.Agent.Audio;
using Izi.Travel.Business.Entities.Analytics.Parameters;
using Izi.Travel.Business.Entities.Media;
using Izi.Travel.Business.Entities.Settings;
using Izi.Travel.Business.Helper;
using Izi.Travel.Business.Services.Contract;
//using Microsoft.Phone.BackgroundAudio;
//using Microsoft.Phone.Shell;
using System;
using System.Threading;
using Windows.Foundation;
//using Windows.Media.Core;

#nullable disable
namespace Izi.Travel.Business.Services.Implementation
{
  public class AudioService : IAudioService
  {
    private readonly ILog _logger = LogManager.GetLog(typeof (AudioService));
    private AudioTrackInfo _playEventAudioInfo;
    private AudioTrackInfo _nowPlaying;
    private AudioServiceState _state;

    public event TypedEventHandler<IAudioService, AudioTrackInfo> NowPlayingChanged
    {
      add
      {
        TypedEventHandler<IAudioService, AudioTrackInfo> typedEventHandler1 = default;//this.NowPlayingChanged;
        TypedEventHandler<IAudioService, AudioTrackInfo> typedEventHandler2;
        do
        {
          typedEventHandler2 = typedEventHandler1;
                    typedEventHandler1 = default;//Interlocked.CompareExchange<TypedEventHandler<IAudioService, AudioTrackInfo>>
                        //(ref this.NowPlayingChanged, (TypedEventHandler<IAudioService, AudioTrackInfo>) Delegate.Combine((Delegate) typedEventHandler2, (Delegate) value), typedEventHandler2);
        }
        while (typedEventHandler1 != typedEventHandler2);
      }
      remove
      {
                TypedEventHandler<IAudioService, AudioTrackInfo> typedEventHandler1 = default;//this.NowPlayingChanged;
        TypedEventHandler<IAudioService, AudioTrackInfo> typedEventHandler2;
        do
        {
          typedEventHandler2 = typedEventHandler1;
          typedEventHandler1 = default;//Interlocked.CompareExchange<TypedEventHandler<IAudioService, AudioTrackInfo>>(ref this.NowPlayingChanged, (TypedEventHandler<IAudioService, AudioTrackInfo>) Delegate.Remove((Delegate) typedEventHandler2, (Delegate) value), typedEventHandler2);
        }
        while (typedEventHandler1 != typedEventHandler2);
      }
    }

    public event TypedEventHandler<IAudioService, AudioServiceState> StateChanged
    {
       add 
        {
            //TypedEventHandler<IAudioService, AudioServiceState> typedEventHandler1
            //    += (sender, e) => this.StateChanged(sender, e);

        }
       remove 
        {
            //TypedEventHandler<IAudioService, AudioServiceState> typedEventHandler1
            //    -= (sender, e) => this.StateChanged(sender, e);
            }
      /*
      add
      {
        TypedEventHandler<IAudioService, AudioServiceState> typedEventHandler1 = default;//this.StateChanged;
        TypedEventHandler<IAudioService, AudioServiceState> typedEventHandler2;
        do
        {
          typedEventHandler2 = typedEventHandler1;
          typedEventHandler1 = Interlocked.CompareExchange<TypedEventHandler<IAudioService, AudioServiceState>>(ref this.StateChanged, (TypedEventHandler<IAudioService, AudioServiceState>) Delegate.Combine((Delegate) typedEventHandler2, (Delegate) value), typedEventHandler2);
        }
        while (typedEventHandler1 != typedEventHandler2);
      }
      remove
      {
        TypedEventHandler<IAudioService, AudioServiceState> typedEventHandler1 = this.StateChanged;
        TypedEventHandler<IAudioService, AudioServiceState> typedEventHandler2;
        do
        {
          typedEventHandler2 = typedEventHandler1;
          typedEventHandler1 = Interlocked.CompareExchange<TypedEventHandler<IAudioService, AudioServiceState>>(ref this.StateChanged, (TypedEventHandler<IAudioService, AudioServiceState>) Delegate.Remove((Delegate) typedEventHandler2, (Delegate) value), typedEventHandler2);
        }
        while (typedEventHandler1 != typedEventHandler2);
      }*/
    }

    public AudioTrackInfo NowPlaying
    {
      get => this._nowPlaying;
      private set
      {
        if (this._nowPlaying == value)
          return;
        this._nowPlaying = value;
        
        //RnD
        //if (this.NowPlayingChanged == null)
        //  return;

        //this.NowPlayingChanged = () => this._nowPlaying;
      }
    }

    public AudioServiceState State
    {
      get => this._state;
      private set
      {
        this._state = value;

        //RnD
        //if (this.StateChanged == null)
        //  return;
        //this.StateChanged = () => this._state;
      }
    }

    public TimeSpan Position
    {
      get
      {
        TimeSpan position = new TimeSpan();
        try
        {
          position = BackgroundAudioPlayer.Instance.Position;
        }
        catch (Exception ex)
        {
          this._logger.Error(ex);
        }
        return position;
      }
      set
      {
        try
        {
          BackgroundAudioPlayer.Instance.Position = value;
        }
        catch (Exception ex)
        {
          this._logger.Error(ex);
        }
      }
    }

    public TimeSpan Duration
    {
      get
      {
        TimeSpan duration = new TimeSpan();
        try
        {
          if (BackgroundAudioPlayer.Instance.Track != null)
            duration = BackgroundAudioPlayer.Instance.Track.Duration;
        }
        catch (Exception ex)
        {
          this._logger.Error(ex);
        }
        return duration;
      }
    }

    public AudioService()
    {
            //RnD
            this.NowPlaying = this.GetCurrentTrackInfo();
               // ?? ServiceFacade.SettingsService.GetAppSettings().NowPlaying;

      this.State = AudioService.ToAudioServiceState(BackgroundAudioPlayer.Instance.PlayerState);

      PhoneApplicationService.Current.Closing += (EventHandler<ClosingEventArgs>)
                ((s, e) => this.Stop());

      BackgroundAudioPlayer.Instance.PlayStateChanged += (EventHandler) 
                ((s, e) => this.OnBackgroundAudioPlayerPlayStateChanged(s, e as PlayStateChangedEventArgs));
    }

    public void Play(AudioTrackInfo audioTrackInfo)
    {
      if (audioTrackInfo == null)
        return;
      if (!this.IsNowPlaying(audioTrackInfo.MtgObjectUid, audioTrackInfo.Language))
        BackgroundAudioPlayer.Instance.Track = 
                    new AudioTrack( new Uri(audioTrackInfo.AudioUrl), 
                    audioTrackInfo.Title, 
                    (string) null,
                    (string) null,
                    (Uri) null, 
                    audioTrackInfo.ToTag(),
                    EnabledPlayerControls.Pause);
      this.Play();
    }

    public void Play()
    {
      try
      {
        AnalyticsHelper.SendPlay();
        BackgroundAudioPlayer.Instance.Play();
      }
      catch (Exception ex)
      {
        this._logger.Error(ex);
      }
    }

    public void Pause()
    {
      try
      {
        if (!BackgroundAudioPlayer.Instance.CanPause)
          return;
        BackgroundAudioPlayer.Instance.Pause();
      }
      catch (Exception ex)
      {
        this._logger.Error(ex);
      }
    }

    public void Stop()
    {
      try
      {
        BackgroundAudioPlayer.Instance.Close();
        this.State = AudioService.ToAudioServiceState(BackgroundAudioPlayer.Instance.PlayerState);
      }
      catch (Exception ex)
      {
        this._logger.Error(ex);
      }
    }

    public void SetNowPlaying(AudioTrackInfo nowPlaying)
    {
      this.NowPlaying = nowPlaying;
            AppSettings appSettings = default;//ServiceFacade.SettingsService.GetAppSettings();
      appSettings.NowPlaying = this.NowPlaying;
      //ServiceFacade.SettingsService.SaveAppSettings(appSettings);
    }

    public bool IsNowPlaying(string uid, string language)
    {
      AudioTrackInfo currentTrackInfo = this.GetCurrentTrackInfo();
      return currentTrackInfo != null 
             && currentTrackInfo.MtgObjectUid == uid
             && currentTrackInfo.Language == language;
    }

    public AudioTrackInfo GetCurrentTrackInfo()
    {
      try
      {
        return BackgroundAudioPlayer.Instance.Track != null 
                    ? AudioTrackInfo.FromTag(BackgroundAudioPlayer.Instance.Track.Tag)
                    : (AudioTrackInfo) null;
      }
      catch (Exception ex)
      {
        BackgroundAudioPlayer.Instance.Close();
        this._logger.Error(ex);
        return (AudioTrackInfo) null;
      }
    }

    private void OnBackgroundAudioPlayerPlayStateChanged(object sender, PlayStateChangedEventArgs e)
    {
      PlayState playState = e.CurrentPlayState;

      if (e.CurrentPlayState == PlayState.Unknown && e.IntermediatePlayState == PlayState.TrackReady)
        playState = PlayState.Playing;

      this.State = AudioService.ToAudioServiceState(playState);

      this._logger.Info("State: {0} [{1} -> {2}]", 
          (object) this.State, (object) e.IntermediatePlayState, (object) e.CurrentPlayState);

      if (this.State == AudioServiceState.Unknown 
                || this.State == AudioServiceState.Error 
                || this.State == AudioServiceState.Stopped)
      {
        CompletionReasonParameter completionReason;
        switch (this.State)
        {
          case AudioServiceState.Stopped:
            completionReason = CompletionReasonParameter.Finished;
            break;
          case AudioServiceState.Error:
            completionReason = CompletionReasonParameter.Error;
            break;
          default:
            completionReason = CompletionReasonParameter.Interrupted;
            break;
        }
        this.SendPlayEvent(completionReason);
      }
      AudioTrackInfo currentTrackInfo = this.GetCurrentTrackInfo();
      if (currentTrackInfo == null || this.NowPlaying != null
                && !(currentTrackInfo.AudioUrl != this.NowPlaying.AudioUrl))
        return;
      this.SendPlayEvent(CompletionReasonParameter.Interrupted);
      this.SetNowPlaying(currentTrackInfo);
    }

    private static AudioServiceState ToAudioServiceState(PlayState playState)
    {
      switch (playState)
      {
        case PlayState.Stopped:
        case PlayState.TrackEnded:
        case PlayState.Shutdown:
          return AudioServiceState.Stopped;
        case PlayState.Paused:
          return AudioServiceState.Paused;
        case PlayState.Playing:
        case PlayState.BufferingStarted:
        case PlayState.BufferingStopped:
        case PlayState.TrackReady:
        case PlayState.Rewinding:
        case PlayState.FastForwarding:
          return AudioServiceState.Playing;
        case PlayState.Error:
          return AudioServiceState.Error;
        default:
          return AudioServiceState.Unknown;
      }
    }

    private void SendPlayEvent(CompletionReasonParameter completionReason)
    {
      if (this.NowPlaying != null && this._playEventAudioInfo != null 
                && this.NowPlaying.MtgObjectUid 
                == this._playEventAudioInfo.MtgObjectUid && this.NowPlaying.Language
                == this._playEventAudioInfo.Language)
        return;
      AnalyticsHelper.SendPlayEnd(this.NowPlaying, completionReason);
      this._playEventAudioInfo = this.NowPlaying;
    }
  }

    internal class EnabledPlayerControls
    {
        internal static object Pause;
    }
}
