// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Client.Entities.MtgObjectContent
// Assembly: Izi.Travel.Client.WindowsPhone, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: C73077B8-157D-49B0-834E-CAFCC993728A
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Client.WindowsPhone.dll

using Newtonsoft.Json;
using System.Collections.Generic;

#nullable disable
namespace Izi.Travel.Client.Entities
{
  [JsonObject]
  public class MtgObjectContent : ContentBase
  {
    [JsonProperty(PropertyName = "playback")]
    public Playback Playback { get; set; }

    [JsonProperty(PropertyName = "images")]
    public List<Media> Images { get; set; }

    [JsonProperty(PropertyName = "audio")]
    public List<Media> Audio { get; set; }

    [JsonProperty(PropertyName = "video")]
    public List<Media> Video { get; set; }

    [JsonProperty(PropertyName = "children")]
    public List<MtgObjectCompact> Children { get; set; }

    [JsonProperty(PropertyName = "collections")]
    public List<MtgObjectCompact> Collections { get; set; }

    [JsonProperty(PropertyName = "references")]
    public List<MtgObjectCompact> References { get; set; }

    [JsonProperty(PropertyName = "quiz")]
    public Quiz Quiz { get; set; }

    [JsonProperty(PropertyName = "news")]
    public string News { get; set; }

    [JsonProperty(PropertyName = "children_count")]
    public int ChildrenCount { get; set; }

    [JsonProperty(PropertyName = "audio_duration")]
    public int AudioDuration { get; set; }
  }
}
