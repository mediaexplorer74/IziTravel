// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Toolkit.Controls.Maps.GeocoordinateExtensions
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using System.Device.Location;
using Windows.Devices.Geolocation;

#nullable disable
namespace Izi.Travel.Shell.Toolkit.Controls.Maps
{
  public static class GeocoordinateExtensions
  {
    public static GeoCoordinate ToGeoCoordinate(this Geocoordinate geocoordinate)
    {
      if (geocoordinate == null)
        return (GeoCoordinate) null;
      GeoCoordinate geoCoordinate = new GeoCoordinate();
      double? nullable = geocoordinate.Altitude;
      geoCoordinate.Altitude = nullable ?? double.NaN;
      nullable = geocoordinate.Heading;
      geoCoordinate.Course = nullable ?? double.NaN;
      geoCoordinate.HorizontalAccuracy = geocoordinate.Accuracy;
      geoCoordinate.Latitude = geocoordinate.Latitude;
      geoCoordinate.Longitude = geocoordinate.Longitude;
      nullable = geocoordinate.Speed;
      geoCoordinate.Speed = nullable ?? double.NaN;
      nullable = geocoordinate.AltitudeAccuracy;
      geoCoordinate.VerticalAccuracy = nullable ?? double.NaN;
      return geoCoordinate;
    }
  }
}
