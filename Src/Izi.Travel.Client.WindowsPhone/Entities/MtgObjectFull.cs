// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Client.Entities.MtgObjectFull
// Assembly: Izi.Travel.Client.WindowsPhone, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: C73077B8-157D-49B0-834E-CAFCC993728A
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Client.WindowsPhone.dll

using Newtonsoft.Json;
using System.Collections.Generic;

#nullable disable
namespace Izi.Travel.Client.Entities
{
  [JsonObject]
  public class MtgObjectFull : MtgObjectBase
  {
    [JsonProperty(PropertyName = "parent_uuid")]
    public string ParentUid { get; set; }

    [JsonProperty(PropertyName = "content")]
    public List<MtgObjectContent> Content { get; set; }

    [JsonProperty(PropertyName = "schedule")]
    public Schedule Schedule { get; set; }

    [JsonProperty(PropertyName = "contacts")]
    public MtgObjectContacts Contacts { get; set; }

    [JsonProperty(PropertyName = "size")]
    public int Size { get; set; }

    [JsonProperty(PropertyName = "sponsors")]
    public List<Sponsor> Sponsors { get; set; }
  }
}
