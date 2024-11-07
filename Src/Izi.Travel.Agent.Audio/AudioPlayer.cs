// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Agent.Audio.AudioPlayer
// Assembly: Izi.Travel.Agent.Audio, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: 875A72B4-019D-472E-B658-6D92A86F5AA5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Agent.Audio.dll

using Izi.Travel.Business.Entities.Media;
using Izi.Travel.Data.Entities.Local;
using Izi.Travel.Data.Entities.Local.Query;
using Izi.Travel.Data.Services.Contract;
using Izi.Travel.Data.Services.Implementation;
using Microsoft.Phone.BackgroundAudio;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;

#nullable disable
namespace Izi.Travel.Agent.Audio
{
  public sealed class AudioPlayer : AudioPlayerAgent
  {
    private static readonly ILocalDataService DataService = (ILocalDataService) new LocalDataService();
    private static AudioTrackInfo _lastAudioTrackInfo;

    static AudioPlayer()
    {
      Deployment.Current.Dispatcher.BeginInvoke((Action) (() => Application.Current.UnhandledException += new EventHandler<ApplicationUnhandledExceptionEventArgs>(AudioPlayer.UnhandledException)));
    }

    protected override void OnUserAction(
      BackgroundAudioPlayer player,
      AudioTrack track,
      UserAction action,
      object param)
    {
      switch (action)
      {
        case UserAction.Stop:
          this.Stop(player);
          break;
        case UserAction.Pause:
          player.Pause();
          break;
        case UserAction.Play:
          player.Play();
          break;
        case UserAction.SkipNext:
          player.Track = (AudioTrack) null;
          break;
        case UserAction.SkipPrevious:
          player.Track = (AudioTrack) null;
          break;
        case UserAction.FastForward:
          player.FastForward();
          break;
        case UserAction.Rewind:
          player.Rewind();
          break;
        case UserAction.Seek:
          player.Position = (TimeSpan) param;
          break;
      }
      this.NotifyComplete();
    }

    protected override void OnPlayStateChanged(
      BackgroundAudioPlayer player,
      AudioTrack track,
      PlayState playState)
    {
      switch (playState)
      {
        case PlayState.Playing:
          AudioPlayer._lastAudioTrackInfo = AudioPlayer.GetAudioTrackInfo(player);
          break;
        case PlayState.TrackReady:
          player.Play();
          break;
        case PlayState.TrackEnded:
          player.Track = AudioPlayer.GetNextTrack();
          if (player.Track != null)
          {
            player.Play();
            break;
          }
          break;
      }
      this.NotifyComplete();
    }

    protected override void OnError(
      BackgroundAudioPlayer player,
      AudioTrack track,
      Exception error,
      bool isFatal)
    {
      if (isFatal)
        this.Abort();
      else
        this.NotifyComplete();
    }

    protected override void OnCancel()
    {
    }

    private void Stop(BackgroundAudioPlayer player)
    {
      if (player.PlayerState != PlayState.Paused && player.PlayerState != PlayState.Playing)
        return;
      player.Stop();
    }

    private static AudioTrack GetNextTrack()
    {
      AudioTrackData[] audioTrackList = AudioPlayer.DataService.GetAudioTrackList(new AudioTrackListQuery());
      if (audioTrackList == null || audioTrackList.Length == 0)
        return (AudioTrack) null;
      AudioTrackData audioTrackData1 = audioTrackList[0];
      if (AudioPlayer._lastAudioTrackInfo != null)
      {
        AudioTrackData audioTrackData2 = ((IEnumerable<AudioTrackData>) audioTrackList).LastOrDefault<AudioTrackData>((Func<AudioTrackData, bool>) (x => x.Uid == AudioPlayer._lastAudioTrackInfo.MtgObjectUid));
        if (audioTrackData2 != null)
        {
          int num = Array.IndexOf<AudioTrackData>(audioTrackList, audioTrackData2);
          if (num != -1 && audioTrackList.Length > num + 1)
            audioTrackData1 = audioTrackList[num + 1];
        }
      }
      return new AudioTrack(new Uri(audioTrackData1.Url), audioTrackData1.Title, (string) null, (string) null, (Uri) null, audioTrackData1.Tag, EnabledPlayerControls.Pause);
    }

    private static AudioTrackInfo GetAudioTrackInfo(BackgroundAudioPlayer player)
    {
      try
      {
        return player.Track != null ? AudioTrackInfo.FromTag(player.Track.Tag) : (AudioTrackInfo) null;
      }
      catch
      {
        return (AudioTrackInfo) null;
      }
    }

    private static void UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
    {
      if (!Debugger.IsAttached)
        return;
      Debugger.Break();
    }
  }
}
