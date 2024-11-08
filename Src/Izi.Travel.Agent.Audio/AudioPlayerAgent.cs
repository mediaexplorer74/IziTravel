// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Agent.Audio.AudioPlayer
// Assembly: Izi.Travel.Agent.Audio, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: 875A72B4-019D-472E-B658-6D92A86F5AA5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Agent.Audio.dll

using System;
using Windows.Media.Core;

namespace Izi.Travel.Agent.Audio
{
    public class AudioPlayerAgent
    {
        public virtual void OnPlayStateChanged()
        {
            //
        }

        protected virtual void OnCancel()
        {
            //
        }

        protected virtual void OnError(
          BackgroundAudioPlayer player,
          AudioTrack track,
          Exception error,
          bool isFatal)
        {
            //
        }

        protected virtual void OnUserAction(
      BackgroundAudioPlayer player,
      AudioTrack track,
      UserAction action,
      object param)
        {
            //
        }

        protected virtual void OnPlayStateChanged(
          BackgroundAudioPlayer player,
          AudioTrack track,
          PlayState playState)
        {
        }
    }
}