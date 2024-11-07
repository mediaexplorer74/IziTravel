// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Client.Entities.ContentBase
// Assembly: Izi.Travel.Client.WindowsPhone, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: C73077B8-157D-49B0-834E-CAFCC993728A
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Client.WindowsPhone.dll

using Newtonsoft.Json;

#nullable disable
namespace Izi.Travel.Client.Entities
{
  [JsonObject]
  public abstract class ContentBase
  {
    [JsonProperty(PropertyName = "language")]
    public string Language { get; set; }

    [JsonProperty(PropertyName = "title")]
    public string Title { get; set; }

    [JsonProperty(PropertyName = "summary")]
    public string Summary { get; set; }

    [JsonProperty(PropertyName = "desc")]
    public string Description { get; set; }
  }
}
