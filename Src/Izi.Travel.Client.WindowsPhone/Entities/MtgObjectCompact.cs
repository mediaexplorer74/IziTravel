// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Client.Entities.MtgObjectCompact
// Assembly: Izi.Travel.Client.WindowsPhone, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: C73077B8-157D-49B0-834E-CAFCC993728A
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Client.WindowsPhone.dll

using Newtonsoft.Json;
using System.Collections.Generic;

#nullable disable
namespace Izi.Travel.Client.Entities
{
  [JsonObject]
  public class MtgObjectCompact : MtgObjectBase
  {
    [JsonProperty(PropertyName = "language")]
    public string Language { get; set; }

    [JsonProperty(PropertyName = "route")]
    public string Route { get; set; }

    [JsonProperty(PropertyName = "title")]
    public string Title { get; set; }

    [JsonProperty(PropertyName = "images")]
    public List<Media> Images { get; set; }

    [JsonProperty(PropertyName = "children_count")]
    public int ChildrenCount { get; set; }
  }
}
