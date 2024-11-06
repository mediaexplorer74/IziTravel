// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Client.Entities.Schedule
// Assembly: Izi.Travel.Client.WindowsPhone, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: C73077B8-157D-49B0-834E-CAFCC993728A
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Client.WindowsPhone.dll

using Newtonsoft.Json;
using System.Collections.Generic;

#nullable disable
namespace Izi.Travel.Client.Entities
{
  [JsonObject]
  public class Schedule
  {
    [JsonProperty(PropertyName = "mon")]
    public List<string> Mon { get; set; }

    [JsonProperty(PropertyName = "tue")]
    public List<string> Tue { get; set; }

    [JsonProperty(PropertyName = "wed")]
    public List<string> Wed { get; set; }

    [JsonProperty(PropertyName = "thu")]
    public List<string> Thu { get; set; }

    [JsonProperty(PropertyName = "fri")]
    public List<string> Fri { get; set; }

    [JsonProperty(PropertyName = "sat")]
    public List<string> Sat { get; set; }

    [JsonProperty(PropertyName = "sun")]
    public List<string> Sun { get; set; }
  }
}
