// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Client.Entities.MtgObjectContacts
// Assembly: Izi.Travel.Client.WindowsPhone, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: C73077B8-157D-49B0-834E-CAFCC993728A
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Client.WindowsPhone.dll

using Newtonsoft.Json;

#nullable disable
namespace Izi.Travel.Client.Entities
{
  [JsonObject]
  public class MtgObjectContacts
  {
    [JsonProperty(PropertyName = "phone_number")]
    public string PhoneNumber { get; set; }

    [JsonProperty(PropertyName = "website")]
    public string WebSite { get; set; }

    [JsonProperty(PropertyName = "country")]
    public string Country { get; set; }

    [JsonProperty(PropertyName = "city")]
    public string City { get; set; }

    [JsonProperty(PropertyName = "address")]
    public string Address { get; set; }

    [JsonProperty(PropertyName = "postcode")]
    public string PostCode { get; set; }

    [JsonProperty(PropertyName = "state")]
    public string State { get; set; }
  }
}
