// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Client.Entities.TriggerZone
// Assembly: Izi.Travel.Client.WindowsPhone, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: C73077B8-157D-49B0-834E-CAFCC993728A
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Client.WindowsPhone.dll

using Newtonsoft.Json;

#nullable disable
namespace Izi.Travel.Client.Entities
{
  [JsonObject]
  public class TriggerZone
  {
    [JsonProperty(PropertyName = "type")]
    public TriggerZoneType Type { get; set; }

    [JsonProperty(PropertyName = "polygon_corners")]
    public string PolygonCorners { get; set; }

    [JsonProperty(PropertyName = "circle_altitude")]
    public double CircleAltitude { get; set; }

    [JsonProperty(PropertyName = "circle_latitude")]
    public double CircleLatitude { get; set; }

    [JsonProperty(PropertyName = "circle_longitude")]
    public double CircleLongitude { get; set; }

    [JsonProperty(PropertyName = "circle_radius")]
    public double CircleRadius { get; set; }
  }
}
