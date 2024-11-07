// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Client.Queries.ReviewPostQuery
// Assembly: Izi.Travel.Client.WindowsPhone, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: C73077B8-157D-49B0-834E-CAFCC993728A
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Client.WindowsPhone.dll

using Newtonsoft.Json;

#nullable disable
namespace Izi.Travel.Client.Queries
{
  [JsonObject]
  public class ReviewPostQuery
  {
    [JsonIgnore]
    public string Uid { get; set; }

    [JsonProperty(PropertyName = "lang")]
    public string Language { get; set; }

    [JsonProperty(PropertyName = "hash")]
    public string Hash { get; set; }

    [JsonProperty(PropertyName = "rating")]
    public int Rating { get; set; }

    [JsonProperty(PropertyName = "review")]
    public string Review { get; set; }

    [JsonProperty(PropertyName = "reviewer_name")]
    public string ReviewerName { get; set; }
  }
}
