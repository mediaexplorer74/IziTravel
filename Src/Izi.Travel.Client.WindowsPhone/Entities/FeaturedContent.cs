// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Client.Entities.FeaturedContent
// Assembly: Izi.Travel.Client.WindowsPhone, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: C73077B8-157D-49B0-834E-CAFCC993728A
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Client.WindowsPhone.dll

using Newtonsoft.Json;

#nullable disable
namespace Izi.Travel.Client.Entities
{
  [JsonObject]
  public class FeaturedContent
  {
    [JsonProperty(PropertyName = "uuid")]
    public string Uid { get; set; }

    [JsonProperty(PropertyName = "type")]
    public MtgObjectType Type { get; set; }

    [JsonProperty(PropertyName = "category")]
    public MtgObjectCategory Category { get; set; }

    [JsonProperty(PropertyName = "title")]
    public string Title { get; set; }

    [JsonProperty(PropertyName = "sub_title")]
    public string SubTitle { get; set; }

    [JsonProperty(PropertyName = "position")]
    public int Position { get; set; }

    [JsonProperty(PropertyName = "images")]
    public FeaturedContentImage[] Images { get; set; }
  }
}
