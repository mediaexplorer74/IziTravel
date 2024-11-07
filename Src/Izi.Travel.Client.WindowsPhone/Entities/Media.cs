// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Client.Entities.Media
// Assembly: Izi.Travel.Client.WindowsPhone, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: C73077B8-157D-49B0-834E-CAFCC993728A
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Client.WindowsPhone.dll

using Izi.Travel.Client.Converters;
using Newtonsoft.Json;

#nullable disable
namespace Izi.Travel.Client.Entities
{
  [JsonObject]
  public class Media
  {
    [JsonProperty(PropertyName = "uuid")]
    public string Uid { get; set; }

    [JsonProperty(PropertyName = "type")]
    [JsonConverter(typeof (JsonMediaTypeEnumConverter))]
    public MediaType Type { get; set; }

    [JsonProperty(PropertyName = "order")]
    public int Order { get; set; }

    [JsonProperty(PropertyName = "duration")]
    public int Duration { get; set; }

    [JsonProperty(PropertyName = "url")]
    public string Url { get; set; }

    [JsonProperty(PropertyName = "title")]
    public string Title { get; set; }

    [JsonProperty(PropertyName = "hash")]
    public string Hash { get; set; }

    [JsonProperty(PropertyName = "size")]
    public int Size { get; set; }
  }
}
