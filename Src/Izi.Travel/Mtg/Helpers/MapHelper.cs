// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.Helpers.MapHelper
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Microsoft.Phone.Maps.Controls;
using Microsoft.Phone.Maps.Services;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Threading.Tasks;
using System.Windows.Media;

#nullable disable
namespace Izi.Travel.Shell.Mtg.Helpers
{
  public static class MapHelper
  {
    public static Task<QueryCompletedEventArgs<Route>> GetRoute(
      TravelMode travelMode,
      GeoCoordinate from,
      GeoCoordinate to)
    {
      TaskCompletionSource<QueryCompletedEventArgs<Route>> taskCompletionSource = new TaskCompletionSource<QueryCompletedEventArgs<Route>>();
      RouteQuery routeQuery = new RouteQuery();
      routeQuery.RouteOptimization = RouteOptimization.MinimizeDistance;
      routeQuery.TravelMode = travelMode;
      routeQuery.Waypoints = (IEnumerable<GeoCoordinate>) new List<GeoCoordinate>()
      {
        from,
        to
      };
      routeQuery.QueryCompleted += (EventHandler<QueryCompletedEventArgs<Route>>) ((s, e) => taskCompletionSource.SetResult(e));
      routeQuery.QueryAsync();
      return taskCompletionSource.Task;
    }

    public static MapPolyline CreatePolyline(
      IEnumerable<GeoCoordinate> path,
      Color color,
      double thickness)
    {
      MapPolyline polyline = new MapPolyline();
      polyline.StrokeColor = color;
      polyline.StrokeThickness = thickness;
      polyline.Path.AddRange(path);
      return polyline;
    }

    public static MapPolygon CreatePolygon(
      IEnumerable<GeoCoordinate> path,
      Color fillColor,
      Color strokeColor,
      double strokeThickness)
    {
      MapPolygon polygon = new MapPolygon();
      polygon.FillColor = fillColor;
      polygon.StrokeColor = strokeColor;
      polygon.StrokeThickness = strokeThickness;
      polygon.Path.AddRange(path);
      return polygon;
    }

    public static void AddRange(
      this GeoCoordinateCollection collection,
      IEnumerable<GeoCoordinate> path)
    {
      if (path == null)
        return;
      foreach (GeoCoordinate geoCoordinate in path)
        collection.Add(geoCoordinate);
    }
  }
}
