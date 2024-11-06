// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Geofencing.Helpers.GeoHelper
// Assembly: Izi.Travel.Geofencing, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: 67B57F63-A085-4500-9D6D-5D3E58E5548F
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Geofencing.dll

using Izi.Travel.Geofencing.Primitives;
using System;
using System.Collections.Generic;

#nullable disable
namespace Izi.Travel.Geofencing.Helpers
{
  public static class GeoHelper
  {
    public const double EarthRadiusMeters = 6378137.0;
    public const double PiDiv180 = 0.017453292519943295;
    public const double PiDivRevert180 = 57.295779513082323;

    public static IEnumerable<Geolocation> ConvertCircleToPolygon(
      Geolocation center,
      double radius,
      int segments = 360)
    {
      double radian1 = GeoHelper.ToRadian(center.Latitude);
      double radian2 = GeoHelper.ToRadian(center.Longitude);
      double num1 = radius / 6378137.0;
      List<Geolocation> polygon = new List<Geolocation>();
      int num2 = segments <= 0 || segments > 360 ? 1 : 360 / segments;
      for (int degrees = 0; degrees <= 360; degrees += num2)
      {
        double radian3 = GeoHelper.ToRadian((double) degrees);
        double num3 = Math.Asin(Math.Sin(radian1) * Math.Cos(num1) + Math.Cos(radian1) * Math.Sin(num1) * Math.Cos(radian3));
        double radians = radian2 + Math.Atan2(Math.Sin(radian3) * Math.Sin(num1) * Math.Cos(radian1), Math.Cos(num1) - Math.Sin(radian1) * Math.Sin(num3));
        polygon.Add(new Geolocation(GeoHelper.ToDegrees(num3), GeoHelper.ToDegrees(radians)));
      }
      return (IEnumerable<Geolocation>) polygon;
    }

    public static double CalculateDistance(Geolocation[] points)
    {
      if (points == null || points.Length < 2)
        return 0.0;
      double distance = 0.0;
      for (int index = 1; index < points.Length; ++index)
        distance += GeoHelper.CalculateDistance(points[index - 1], points[index]);
      return distance;
    }

    public static double CalculateDistance(Geolocation source, Geolocation target)
    {
      double radian1 = GeoHelper.ToRadian(target.Latitude - source.Latitude);
      double radian2 = GeoHelper.ToRadian(target.Longitude - source.Longitude);
      double radian3 = GeoHelper.ToRadian(source.Latitude);
      double radian4 = GeoHelper.ToRadian(target.Latitude);
      double d = Math.Sin(radian1 / 2.0) * Math.Sin(radian1 / 2.0) + Math.Sin(radian2 / 2.0) * Math.Sin(radian2 / 2.0) * Math.Cos(radian3) * Math.Cos(radian4);
      return 6378137.0 * (2.0 * Math.Atan2(Math.Sqrt(d), Math.Sqrt(1.0 - d)));
    }

    public static double MetersToPixels(double meters, double latitude, double zoomLevel)
    {
      return meters / GeoHelper.MetersPerPixels(latitude, zoomLevel);
    }

    public static double MetersPerPixels(double latitude, double zoomLevel)
    {
      return Math.Cos(GeoHelper.ToRadian(latitude)) * 2.0 * Math.PI * 6378137.0 / (256.0 * Math.Pow(2.0, zoomLevel));
    }

    public static double ToRadian(double degrees) => degrees * (Math.PI / 180.0);

    public static double ToDegrees(double radians) => radians * (180.0 / Math.PI);
  }
}
