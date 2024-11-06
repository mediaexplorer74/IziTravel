// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Geofencing.Helpers.Extensions
// Assembly: Izi.Travel.Geofencing, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: 67B57F63-A085-4500-9D6D-5D3E58E5548F
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Geofencing.dll

using System.Device.Location;
using Windows.Devices.Geolocation;

#nullable disable
namespace Izi.Travel.Geofencing.Helpers
{
  public static class Extensions
  {
    public static Izi.Travel.Geofencing.Primitives.Geolocation ToGeolocation(
      this Geoposition geoposition)
    {
      if (geoposition == null)
        return (Izi.Travel.Geofencing.Primitives.Geolocation) null;
      return new Izi.Travel.Geofencing.Primitives.Geolocation()
      {
        Latitude = geoposition.Coordinate.Latitude,
        Longitude = geoposition.Coordinate.Longitude,
        Altitude = geoposition.Coordinate.Altitude,
        Accuracy = geoposition.Coordinate.Accuracy
      };
    }

    public static GeoCoordinate ToGeoCoordinate(this Izi.Travel.Geofencing.Primitives.Geolocation geolocation)
    {
      if (geolocation == null)
        return (GeoCoordinate) null;
      return new GeoCoordinate()
      {
        Latitude = geolocation.Latitude,
        Longitude = geolocation.Longitude,
        Altitude = geolocation.Altitude.GetValueOrDefault(),
        HorizontalAccuracy = geolocation.Accuracy,
        VerticalAccuracy = geolocation.Accuracy
      };
    }
  }
}
