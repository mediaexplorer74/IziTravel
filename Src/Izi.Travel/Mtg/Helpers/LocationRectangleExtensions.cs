// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.Helpers.LocationRectangleExtensions
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Microsoft.Phone.Maps.Controls;
using System.Device.Location;

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
      return new LocationRectangle(new GeoCoordinate(northwest.Latitude + num1, northwest.Longitude - num2), new GeoCoordinate(southeast.Latitude - num1, southeast.Longitude + num2));
    }
  }
}
