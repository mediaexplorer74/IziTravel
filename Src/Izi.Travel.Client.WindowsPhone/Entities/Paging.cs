// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Client.Entities.Paging
// Assembly: Izi.Travel.Client.WindowsPhone, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: C73077B8-157D-49B0-834E-CAFCC993728A
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Client.WindowsPhone.dll

using Newtonsoft.Json;

#nullable disable
namespace Izi.Travel.Client.Entities
{
  [JsonObject]
  public class Paging
  {
    [JsonProperty(PropertyName = "limit")]
    public int Limit { get; set; }

    [JsonProperty(PropertyName = "returned_count")]
    public int ReturnedCount { get; set; }

    [JsonProperty(PropertyName = "total_count")]
    public int TotalCount { get; set; }

    [JsonProperty(PropertyName = "next")]
    public string Next { get; set; }

    [JsonProperty(PropertyName = "previous")]
    public string Previous { get; set; }
  }
}
