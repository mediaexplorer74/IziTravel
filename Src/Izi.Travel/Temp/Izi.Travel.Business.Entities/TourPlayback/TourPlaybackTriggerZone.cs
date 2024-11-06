﻿// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Entities.TourPlayback.TourPlaybackTriggerZone
// Assembly: Izi.Travel.Business.Entities, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: DDED5915-8B3A-4C03-AAF5-BE6B16E9CC4A
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.Entities.dll

using Izi.Travel.Geofencing.Shapes;

#nullable disable
namespace Izi.Travel.Business.Entities.TourPlayback
{
  public class TourPlaybackTriggerZone
  {
    private readonly TourPlaybackAttraction _tourAttraction;

    public TourPlaybackAttraction TourAttraction => this._tourAttraction;

    public string Uid { get; internal set; }

    public TourPlaybackTriggerZoneState State { get; internal set; }

    public IGeoshape Geoshape { get; internal set; }

    public TourPlaybackTriggerZone(TourPlaybackAttraction tourAttraction)
    {
      this._tourAttraction = tourAttraction;
    }
  }
}
