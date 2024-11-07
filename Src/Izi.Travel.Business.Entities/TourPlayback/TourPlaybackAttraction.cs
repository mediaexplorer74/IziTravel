// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Entities.TourPlayback.TourPlaybackAttraction
// Assembly: Izi.Travel.Business.Entities, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: DDED5915-8B3A-4C03-AAF5-BE6B16E9CC4A
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.Entities.dll

using Izi.Travel.Business.Entities.Media;
using System.Collections.Generic;
using System.Device.Location;

#nullable disable
namespace Izi.Travel.Business.Entities.TourPlayback
{
  public class TourPlaybackAttraction
  {
    private readonly Izi.Travel.Business.Entities.TourPlayback.TourPlayback _tourPlayback;

    public Izi.Travel.Business.Entities.TourPlayback.TourPlayback TourPlayback
    {
      get => this._tourPlayback;
    }

    public string Uid { get; internal set; }

    public int Order { get; internal set; }

    public string Title { get; internal set; }

    public GeoCoordinate Location { get; internal set; }

    public AudioTrackInfo AudioTrackInfo { get; internal set; }

    public List<TourPlaybackTriggerZone> TriggerZones { get; private set; }

    public bool IsPlaying { get; internal set; }

    public bool IsVisited { get; internal set; }

    public bool IsHidden { get; internal set; }

    public TourPlaybackAttraction(Izi.Travel.Business.Entities.TourPlayback.TourPlayback tourPlayback)
    {
      this._tourPlayback = tourPlayback;
      this.TriggerZones = new List<TourPlaybackTriggerZone>();
    }
  }
}
