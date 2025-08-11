// ********************************************************************
// Type: Izi.Travel.Shell.Mtg.Helpers.TourHelper
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Geofencing.Helpers;
using Izi.Travel.Geofencing.Primitives;
using Izi.Travel.Helpers;
using Izi.Travel.Shell.Core.Resources;
using Izi.Travel.Shell.Core.Themes;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using Windows.Devices.Geolocation;
using Windows.UI;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Media;

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
      if (tour?.Map?.Route == null)
        return null;
        
      //var coordinates = tour.Map.Route.Select(loc => new GeoCoordinate(loc.Position.Latitude, loc.Position.Longitude));
      var color = ThemeHelper.GetThemeColor("IziTravelBlueColor");
      return MapHelper.CreatePolyline(/*coordinates*/default, color, 4.0);
    }

    public static MapElement CreateTriggerZoneMapElement(TriggerZone triggerZone)
    {
      if (triggerZone == null)
        return null;
        
      IEnumerable<GeoCoordinate> path = null;
      
      switch (triggerZone.Type)
      {
        case TriggerZoneType.Polygon:
          path = triggerZone.PolygonPath?.Select(loc => new GeoCoordinate(loc.Latitude, loc.Longitude));
          break;
          
        case TriggerZoneType.Circle:
            var center = new Izi.Travel.Geofencing.Primitives.Geolocation(triggerZone.CircleCenter.Latitude, triggerZone.CircleCenter.Longitude);
            var polygon = Izi.Travel.Geofencing.Helpers.GeoHelper.ConvertCircleToPolygon(center, triggerZone.CircleRadius, 36);
            path = polygon?.Select(p => new GeoCoordinate(p.Latitude, p.Longitude));
          break;
      }
      
      if (path == null || !path.Any())
        return null;
        
      var fillColor = ThemeHelper.GetThemeColor("IziTravelBlueColor", (byte)40);
      return MapHelper.CreatePolygon(path.Select(p => new BasicGeoposition { Latitude = p.Latitude, Longitude = p.Longitude }), fillColor, Colors.Transparent, 1.0);
    }

    public static IEnumerable<MapElement> CreateTriggerZoneMapElements(IEnumerable<MtgObject> items)
    {
      if (items == null)
        return Enumerable.Empty<MapElement>();
        
      return items
        .Where(x => x.TriggerZones != null)
        .SelectMany(x => x.TriggerZones)
        .Select(CreateTriggerZoneMapElement)
        .Where(mapElement => mapElement != null);
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
