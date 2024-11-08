// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Entities.TourPlayback.TourPlayback
// Assembly: Izi.Travel.Business.Entities, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: DDED5915-8B3A-4C03-AAF5-BE6B16E9CC4A
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.Entities.dll

using System.Collections.Generic;
using Izi.Travel.Data.Entities.Common;//using System.Device.Location; //RnD

#nullable disable
namespace Izi.Travel.Business.Entities.TourPlayback
{
  public class TourPlayback
  {
    public string Uid { get; internal set; }

    public string Title { get; internal set; }

    public string Language { get; internal set; }

    public string ContentProviderUid { get; internal set; }

    public GeoCoordinate[] Route { get; internal set; }

    public List<TourPlaybackAttraction> Attractions { get; private set; }

    public TourPlayback() => this.Attractions = new List<TourPlaybackAttraction>();
  }
}
