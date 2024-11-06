// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Client.Entities.CityBase
// Assembly: Izi.Travel.Client.WindowsPhone, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: C73077B8-157D-49B0-834E-CAFCC993728A
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Client.WindowsPhone.dll

using Newtonsoft.Json;
using System.Collections.Generic;

#nullable disable
namespace Izi.Travel.Client.Entities
{
  [JsonObject]
  public abstract class CityBase
  {
    [JsonProperty(PropertyName = "uuid")]
    public string Uid { get; set; }

    [JsonProperty(PropertyName = "languages")]
    public List<string> Languages { get; set; }

    public MtgObjectType Type => MtgObjectType.City;

    [JsonProperty(PropertyName = "status")]
    public PublicationStatus Status { get; set; }

    [JsonProperty(PropertyName = "visible")]
    public bool Visible { get; set; }

    [JsonProperty(PropertyName = "location")]
    public Location Location { get; set; }

    [JsonProperty(PropertyName = "map")]
    public Map Map { get; set; }

    [JsonProperty(PropertyName = "hash")]
    public string Hash { get; set; }

    [JsonProperty(PropertyName = "children_count")]
    public int ChildrenCount { get; set; }

    [JsonProperty(PropertyName = "translations")]
    public List<Translation> Translations { get; set; }
  }
}
