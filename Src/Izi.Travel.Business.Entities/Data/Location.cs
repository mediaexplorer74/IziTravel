// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Entities.Data.Location
// Assembly: Izi.Travel.Business.Entities, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: DDED5915-8B3A-4C03-AAF5-BE6B16E9CC4A
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.Entities.dll

#nullable disable
namespace Izi.Travel.Business.Entities.Data
{
  public class Location : GeoLocation
  {
    public string Ip { get; set; }

    public string Number { get; set; }

    public string CountryCode { get; set; }

    public string CountryUid { get; set; }

    public string CityUid { get; set; }

    public Location Clone()
    {
      Location location = new Location();
      location.Latitude = this.Latitude;
      location.Longitude = this.Longitude;
      location.Altitude = this.Altitude;
      location.Ip = this.Ip;
      location.Number = this.Number;
      location.CountryCode = this.CountryCode;
      location.CountryUid = this.CountryUid;
      location.CityUid = this.CityUid;
      return location;
    }
  }
}
