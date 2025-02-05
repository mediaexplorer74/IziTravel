// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.Helpers.TourHelper
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Geofencing.Helpers;
using Izi.Travel.Geofencing.Primitives;
using Izi.Travel.Shell.Core.Resources;
using Izi.Travel.Shell.Core.Themes;
using Microsoft.Phone.Maps.Controls;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Windows.Media;

#nullable disable
namespace Izi.Travel.Shell.Mtg.Helpers
{
  public static class TourHelper
  {
    public const double TourAvarageSpeedWalk = 4.0;
    public const double TourAvarageSpeedBike = 7.0;
    public const double TourAvarageSpeedBus = 25.0;
    public const double TourAvarageSpeedCar = 50.0;
    public const double TourAvarageSpeedBoat = 50.0;

    public static double GetAvarageSpeedByCategory(MtgObjectCategory category)
    {
      switch (category)
      {
        case MtgObjectCategory.Walk:
          return 4.0;
        case MtgObjectCategory.Bike:
          return 7.0;
        case MtgObjectCategory.Bus:
          return 25.0;
        case MtgObjectCategory.Car:
          return 50.0;
        case MtgObjectCategory.Boat:
          return 50.0;
        default:
          return 4.0;
      }
    }

    public static MapElement CreateRouteMapElement(MtgObject tour)
    {
      return tour == null || tour.Map == null || tour.Map.Route == null ? (MapElement) null : (MapElement) MapHelper.CreatePolyline(((IEnumerable<GeoLocation>) tour.Map.Route).Select<GeoLocation, GeoCoordinate>((Func<GeoLocation, GeoCoordinate>) (x => x.ToGeoCoordinate())), ThemeHelper.GetThemeColor("IziTravelBlueColor"), 4.0);
    }

    public static MapElement CreateTriggerZoneMapElement(TriggerZone triggerZone)
    {
      if (triggerZone == null)
        return (MapElement) null;
      IEnumerable<GeoCoordinate> path = (IEnumerable<GeoCoordinate>) null;
      switch (triggerZone.Type)
      {
        case TriggerZoneType.Polygon:
          path = ((IEnumerable<GeoLocation>) triggerZone.PolygonPath).Select<GeoLocation, GeoCoordinate>((Func<GeoLocation, GeoCoordinate>) (x => x.ToGeoCoordinate()));
          break;
        case TriggerZoneType.Circle:
          IEnumerable<Geolocation> polygon = GeoHelper.ConvertCircleToPolygon(new Geolocation(triggerZone.CircleCenter.Latitude, triggerZone.CircleCenter.Longitude), triggerZone.CircleRadius, 36);
          if (polygon != null)
          {
            path = polygon.Select<Geolocation, GeoCoordinate>((Func<Geolocation, GeoCoordinate>) (x => new GeoCoordinate(x.Latitude, x.Longitude)));
            break;
          }
          break;
      }
      return path == null ? (MapElement) null : (MapElement) MapHelper.CreatePolygon(path, ThemeHelper.GetThemeColor("IziTravelBlueColor", (byte) 40), Colors.Transparent, 1.0);
    }

    public static IEnumerable<MapElement> CreateTriggerZoneMapElements(IEnumerable<MtgObject> items)
    {
      List<MapElement> triggerZoneMapElements = new List<MapElement>();
      if (items != null)
        triggerZoneMapElements.AddRange(items.Where<MtgObject>((Func<MtgObject, bool>) (x => x.TriggerZones != null)).SelectMany<MtgObject, TriggerZone>((Func<MtgObject, IEnumerable<TriggerZone>>) (x => (IEnumerable<TriggerZone>) x.TriggerZones)).Select<TriggerZone, MapElement>(new Func<TriggerZone, MapElement>(TourHelper.CreateTriggerZoneMapElement)).Where<MapElement>((Func<MapElement, bool>) (mapElement => mapElement != null)));
      return (IEnumerable<MapElement>) triggerZoneMapElements;
    }

    public static string GetCategoryName(MtgObjectCategory category)
    {
      switch (category)
      {
        case MtgObjectCategory.Walk:
          return AppResources.EnumCategoryWalk;
        case MtgObjectCategory.Bike:
          return AppResources.EnumCategoryBike;
        case MtgObjectCategory.Bus:
          return AppResources.EnumCategoryBus;
        case MtgObjectCategory.Car:
          return AppResources.EnumCategoryCar;
        case MtgObjectCategory.Boat:
          return AppResources.EnumCategoryBoat;
        default:
          return AppResources.LabelTour;
      }
    }
  }
}
