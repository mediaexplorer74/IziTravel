// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Agent.Audio.AudioPlayer
// Assembly: Izi.Travel.Agent.Audio, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: 875A72B4-019D-472E-B658-6D92A86F5AA5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Agent.Audio.dll

namespace Izi.Travel.Agent.Audio
{
    public enum PlayState
    {
        Paused,
        Playing,
        TrackReady,
        TrackEnded,
        Unknown,
        Stopped,
        Shutdown,
        BufferingStarted,
        BufferingStopped,
        Rewinding,
        FastForwarding,
        Error
    }
}