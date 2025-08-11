// ********************************************************************
// Type: Izi.Travel.Shell.Mtg.Helpers.MapHelper
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Windows.UI.Xaml.Controls.Maps;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.UI;
using Windows.Devices.Geolocation;

#nullable disable
namespace Izi.Travel.Shell.Mtg.Helpers
{
  public static class MapHelper
  {
    public static MapPolyline CreatePolyline(
      IEnumerable<BasicGeoposition> path,
      Color color,
      double thickness)
    {
      MapPolyline polyline = new MapPolyline();
      polyline.StrokeColor = color;
      polyline.StrokeThickness = thickness;
      polyline.Path = new Geopath(path);
      return polyline;
    }

    public static MapPolygon CreatePolygon(
      IEnumerable<BasicGeoposition> path,
      Color fillColor,
      Color strokeColor,
      double strokeThickness)
    {
      MapPolygon polygon = new MapPolygon();
      polygon.FillColor = fillColor;
      polygon.StrokeColor = strokeColor;
      polygon.StrokeThickness = strokeThickness;
      polygon.Paths.Add(new Geopath(path));
      return polygon;
    }

    public static void AddRange(
      this IList<BasicGeoposition> collection,
      IEnumerable<BasicGeoposition> path)
    {
      if (path == null)
        return;
      foreach (var position in path)
        collection.Add(position);
    }
  }
}
