// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Client.Entities.MtgObjectChildrenListMetadata
// Assembly: Izi.Travel.Client.WindowsPhone, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: C73077B8-157D-49B0-834E-CAFCC993728A
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Client.WindowsPhone.dll

using Newtonsoft.Json;

#nullable disable
namespace Izi.Travel.Client.Entities
{
  [JsonObject]
  public class MtgObjectChildrenListMetadata
  {
    [JsonProperty(PropertyName = "offset")]
    public int Offset { get; set; }

    [JsonProperty(PropertyName = "limit")]
    public int Limit { get; set; }

    [JsonProperty(PropertyName = "total_count")]
    public int TotalCount { get; set; }

    [JsonProperty(PropertyName = "returned_count")]
    public int ReturnedCount { get; set; }

    [JsonProperty(PropertyName = "spent")]
    public double Spent { get; set; }

    [JsonProperty(PropertyName = "pages_total")]
    public int PageTotal { get; set; }

    [JsonProperty(PropertyName = "page_current")]
    public int PageCurrent { get; set; }

    [JsonProperty(PropertyName = "pages_left")]
    public bool PageLeft { get; set; }

    [JsonProperty(PropertyName = "pages_right")]
    public bool PageRight { get; set; }

    [JsonProperty(PropertyName = "page_out_of_bounds")]
    public bool PageOutOfBounds { get; set; }
  }
}
