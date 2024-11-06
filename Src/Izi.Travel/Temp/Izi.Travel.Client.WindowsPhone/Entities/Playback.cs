// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Client.Entities.Playback
// Assembly: Izi.Travel.Client.WindowsPhone, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: C73077B8-157D-49B0-834E-CAFCC993728A
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Client.WindowsPhone.dll

using Newtonsoft.Json;
using System.Collections.Generic;

#nullable disable
namespace Izi.Travel.Client.Entities
{
  [JsonObject]
  public class Playback
  {
    [JsonProperty(PropertyName = "type")]
    public PlaybackType Type { get; set; }

    [JsonProperty(PropertyName = "order")]
    public List<string> Order { get; set; }
  }
}
