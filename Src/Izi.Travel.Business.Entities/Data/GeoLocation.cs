// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Entities.Data.GeoLocation
// Assembly: Izi.Travel.Business.Entities, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: DDED5915-8B3A-4C03-AAF5-BE6B16E9CC4A
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.Entities.dll


using System;
using Izi.Travel.Data.Entities.Common;//using System.Device.Location; //RnD

#nullable disable
namespace Izi.Travel.Business.Entities.Data
{
  public class GeoLocation
  {
    public double Latitude { get; set; }

    public double Longitude { get; set; }

    public double Altitude { get; set; }

    public GeoCoordinate ToGeoCoordinate()
    {
      return Math.Abs(this.Latitude) > double.Epsilon
                || Math.Abs(this.Longitude) > double.Epsilon || Math.Abs(this.Altitude) > double.Epsilon
                ? new GeoCoordinate(this.Latitude, this.Longitude, this.Altitude)
                : GeoCoordinate.Unknown;
    }

    public static GeoLocation FromGeoCoordinate(GeoCoordinate coordinate)
    {
      if (coordinate == (GeoCoordinate) null || coordinate == GeoCoordinate.Unknown)
        return (GeoLocation) null;
      return new GeoLocation()
      {
        Latitude = coordinate.Latitude,
        Longitude = coordinate.Longitude,
        Altitude = coordinate.Altitude
      };
    }
  }
}
