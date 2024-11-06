// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Client.Entities.FeaturedContentImage
// Assembly: Izi.Travel.Client.WindowsPhone, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: C73077B8-157D-49B0-834E-CAFCC993728A
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Client.WindowsPhone.dll

using Izi.Travel.Client.Converters;
using Newtonsoft.Json;

#nullable disable
namespace Izi.Travel.Client.Entities
{
  [JsonObject]
  public class FeaturedContentImage
  {
    [JsonProperty(PropertyName = "uuid")]
    public string Uid { get; set; }

    [JsonProperty(PropertyName = "type")]
    [JsonConverter(typeof (JsonMediaTypeEnumConverter))]
    public MediaType Type { get; set; }
  }
}
