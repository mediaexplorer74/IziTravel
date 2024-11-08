// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Client.Entities.PublisherContacts
// Assembly: Izi.Travel.Client.WindowsPhone, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: C73077B8-157D-49B0-834E-CAFCC993728A
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Client.WindowsPhone.dll

using Newtonsoft.Json;

#nullable disable
namespace Izi.Travel.Client.Entities
{
  [JsonObject]
  public class PublisherContacts
  {
    [JsonProperty(PropertyName = "website")]
    public string WebSite { get; set; }

    [JsonProperty(PropertyName = "facebook")]
    public string Facebook { get; set; }

    [JsonProperty(PropertyName = "twitter")]
    public string Twitter { get; set; }

    [JsonProperty(PropertyName = "instagram")]
    public string Instagram { get; set; }

    [JsonProperty(PropertyName = "googleplus")]
    public string GooglePlus { get; set; }

    [JsonProperty(PropertyName = "vk")]
    public string Vk { get; set; }

    [JsonProperty(PropertyName = "youtube")]
    public string YouTube { get; set; }
  }
}
