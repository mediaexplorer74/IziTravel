// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Entities.Data.TriggerZone
// Assembly: Izi.Travel.Business.Entities, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: DDED5915-8B3A-4C03-AAF5-BE6B16E9CC4A
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.Entities.dll

using Izi.Travel.Geofencing.Primitives;
using Izi.Travel.Geofencing.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Izi.Travel.Business.Entities.Data
{
  public class TriggerZone
  {
    public TriggerZoneType Type { get; set; }

    public GeoLocation[] PolygonPath { get; set; }

    public GeoLocation CircleCenter { get; set; }

    public double CircleRadius { get; set; }

    public IGeoshape Geoshape
    {
      get
      {
        if (this.Type == TriggerZoneType.Circle && this.CircleCenter != null)
          return (IGeoshape) new Geocircle(new Geolocation(this.CircleCenter.Latitude, this.CircleCenter.Longitude), this.CircleRadius);
        return this.Type == TriggerZoneType.Polygon && this.PolygonPath != null && this.PolygonPath.Length != 0 ? (IGeoshape) new Geopath(((IEnumerable<GeoLocation>) this.PolygonPath).Where<GeoLocation>((Func<GeoLocation, bool>) (x => x != null)).Select<GeoLocation, Geolocation>((Func<GeoLocation, Geolocation>) (x => new Geolocation(x.Latitude, x.Longitude)))) : (IGeoshape) null;
      }
    }
  }
}
