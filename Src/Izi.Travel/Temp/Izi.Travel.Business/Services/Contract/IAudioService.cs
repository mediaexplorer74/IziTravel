// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Services.Contract.IAudioService
// Assembly: Izi.Travel.Business, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: ABF4D74A-55A9-49E1-BE11-CC83659F98DD
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.dll

using Izi.Travel.Business.Entities.Media;
using System;
using Windows.Foundation;

#nullable disable
namespace Izi.Travel.Business.Services.Contract
{
  public interface IAudioService
  {
    event TypedEventHandler<IAudioService, AudioTrackInfo> NowPlayingChanged;

    event TypedEventHandler<IAudioService, AudioServiceState> StateChanged;

    AudioTrackInfo NowPlaying { get; }

    AudioServiceState State { get; }

    TimeSpan Position { get; set; }

    TimeSpan Duration { get; }

    void Play(AudioTrackInfo audioTrackInfo);

    void Play();

    void Pause();

    void Stop();

    void SetNowPlaying(AudioTrackInfo nowPlaying);

    bool IsNowPlaying(string uid, string language);

    AudioTrackInfo GetCurrentTrackInfo();
  }
}
