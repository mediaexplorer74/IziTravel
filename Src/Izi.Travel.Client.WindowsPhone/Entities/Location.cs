// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Client.Entities.Location
// Assembly: Izi.Travel.Client.WindowsPhone, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: C73077B8-157D-49B0-834E-CAFCC993728A
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Client.WindowsPhone.dll

using Newtonsoft.Json;

#nullable disable
namespace Izi.Travel.Client.Entities
{
  [JsonObject]
  public class Location
  {
    [JsonProperty(PropertyName = "latitude")]
    public double Latitude { get; set; }

    [JsonProperty(PropertyName = "longitude")]
    public double Longitude { get; set; }

    [JsonProperty(PropertyName = "altitude")]
    public double Altitude { get; set; }

    [JsonProperty(PropertyName = "number")]
    public string Number { get; set; }

    [JsonProperty(PropertyName = "ip")]
    public string Ip { get; set; }

    [JsonProperty(PropertyName = "country_code")]
    public string CountryCode { get; set; }

    [JsonProperty(PropertyName = "country_uuid")]
    public string CountryUid { get; set; }

    [JsonProperty(PropertyName = "city_uuid")]
    public string CityUid { get; set; }
  }
}
