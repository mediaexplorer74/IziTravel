// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Client.Entities.Sponsor
// Assembly: Izi.Travel.Client.WindowsPhone, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: C73077B8-157D-49B0-834E-CAFCC993728A
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Client.WindowsPhone.dll

using Newtonsoft.Json;
using System.Collections.Generic;

#nullable disable
namespace Izi.Travel.Client.Entities
{
  [JsonObject]
  public class Sponsor
  {
    [JsonProperty(PropertyName = "name")]
    public string Name { get; set; }

    [JsonProperty(PropertyName = "website")]
    public string Website { get; set; }

    [JsonProperty(PropertyName = "order")]
    public int Order { get; set; }

    [JsonProperty(PropertyName = "images")]
    public List<Media> Images { get; set; }
  }
}
