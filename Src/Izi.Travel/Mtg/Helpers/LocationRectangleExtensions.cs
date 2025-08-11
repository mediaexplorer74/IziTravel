// ********************************************************************
// Type: Izi.Travel.Shell.Mtg.Helpers.LocationRectangleExtensions
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Data.Entities.Common;
using Windows.Devices.Geolocation;

#nullable disable
namespace Izi.Travel.Shell.Mtg.Helpers
{
  public static class LocationRectangleExtensions
  {
    public static LocationRectangle Expand(this LocationRectangle rectangle, double amount)
    {
      GeoCoordinate northwest = rectangle.Northwest;
      GeoCoordinate southeast = rectangle.Southeast;
      double num1 = (northwest.Latitude - southeast.Latitude) * amount;
      double num2 = (southeast.Longitude - northwest.Longitude) * amount;

      return new LocationRectangle(
        new Geopoint(new BasicGeoposition 
        { 
            Latitude = northwest.Latitude + num1, Longitude = northwest.Longitude - num2 }),
            0.0f, 
            0.0f);        
        }
  }
}
