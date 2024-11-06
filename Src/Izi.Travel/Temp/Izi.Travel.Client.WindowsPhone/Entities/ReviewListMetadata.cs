// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Client.Entities.ReviewListMetadata
// Assembly: Izi.Travel.Client.WindowsPhone, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: C73077B8-157D-49B0-834E-CAFCC993728A
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Client.WindowsPhone.dll

using Newtonsoft.Json;
using System;

#nullable disable
namespace Izi.Travel.Client.Entities
{
  [JsonObject]
  public class ReviewListMetadata
  {
    [JsonProperty(PropertyName = "uuid")]
    public string Uid { get; set; }

    [JsonProperty(PropertyName = "rating_average")]
    public double RatingAverage { get; set; }

    [JsonProperty(PropertyName = "ratings_count")]
    public int RatingsCount { get; set; }

    [JsonProperty(PropertyName = "reviews_count")]
    public int ReviewsCount { get; set; }

    [JsonProperty(PropertyName = "date")]
    public DateTime DateTime { get; set; }
  }
}
