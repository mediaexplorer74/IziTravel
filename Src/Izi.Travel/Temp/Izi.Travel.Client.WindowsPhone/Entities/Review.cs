// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Client.Entities.Review
// Assembly: Izi.Travel.Client.WindowsPhone, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: C73077B8-157D-49B0-834E-CAFCC993728A
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Client.WindowsPhone.dll

using Newtonsoft.Json;
using System;

#nullable disable
namespace Izi.Travel.Client.Entities
{
  [JsonObject]
  public class Review
  {
    [JsonProperty(PropertyName = "id")]
    public int Id { get; set; }

    [JsonProperty(PropertyName = "lang")]
    public string Language { get; set; }

    [JsonProperty(PropertyName = "rating")]
    public int Rating { get; set; }

    [JsonProperty(PropertyName = "review")]
    public string Text { get; set; }

    [JsonProperty(PropertyName = "reviewer_name")]
    public string ReviewerName { get; set; }

    [JsonProperty(PropertyName = "date")]
    public DateTime Date { get; set; }
  }
}
