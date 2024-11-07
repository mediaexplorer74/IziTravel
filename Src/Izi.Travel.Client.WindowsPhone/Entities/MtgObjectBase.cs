// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Client.Entities.MtgObjectBase
// Assembly: Izi.Travel.Client.WindowsPhone, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: C73077B8-157D-49B0-834E-CAFCC993728A
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Client.WindowsPhone.dll

using Izi.Travel.Client.Converters;
using Newtonsoft.Json;
using System.Collections.Generic;

#nullable disable
namespace Izi.Travel.Client.Entities
{
  [JsonObject]
  public abstract class MtgObjectBase
  {
    [JsonProperty(PropertyName = "uuid")]
    public string Uid { get; set; }

    [JsonProperty(PropertyName = "type")]
    [JsonConverter(typeof (JsonMtgObjectTypeEnumConverter))]
    public MtgObjectType Type { get; set; }

    [JsonProperty(PropertyName = "languages")]
    public List<string> Languages { get; set; }

    [JsonProperty(PropertyName = "category")]
    public MtgObjectCategory Category { get; set; }

    [JsonProperty(PropertyName = "country")]
    public CountryCompact Country { get; set; }

    [JsonProperty(PropertyName = "city")]
    public CityCompact City { get; set; }

    [JsonProperty(PropertyName = "status")]
    public PublicationStatus Status { get; set; }

    [JsonProperty(PropertyName = "content_provider")]
    public ContentProvider ContentProvider { get; set; }

    [JsonProperty(PropertyName = "publisher")]
    public PublisherCompact Publisher { get; set; }

    [JsonProperty(PropertyName = "duration")]
    public int Duration { get; set; }

    [JsonProperty(PropertyName = "distance")]
    public int Distance { get; set; }

    [JsonProperty(PropertyName = "purchase")]
    public Purchase Purchase { get; set; }

    [JsonProperty(PropertyName = "location")]
    public Location Location { get; set; }

    [JsonProperty(PropertyName = "map")]
    public Map Map { get; set; }

    [JsonProperty(PropertyName = "trigger_zones")]
    public List<TriggerZone> TriggerZones { get; set; }

    [JsonProperty(PropertyName = "hidden")]
    public bool Hidden { get; set; }

    [JsonProperty(PropertyName = "hash")]
    public string Hash { get; set; }

    [JsonProperty(PropertyName = "reviews")]
    public Rating Rating { get; set; }
  }
}
