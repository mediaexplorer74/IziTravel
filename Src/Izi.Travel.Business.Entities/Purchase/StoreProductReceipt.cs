// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Entities.Purchase.StoreProductReceipt
// Assembly: Izi.Travel.Business.Entities, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: DDED5915-8B3A-4C03-AAF5-BE6B16E9CC4A
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.Entities.dll

using System.Xml.Serialization;

#nullable disable
namespace Izi.Travel.Business.Entities.Purchase
{
  [XmlType(TypeName = "ProductReceipt")]
  public class StoreProductReceipt
  {
    [XmlAttribute("Id")]
    public string Id { get; set; }

    [XmlAttribute("PurchasePrice")]
    public string PurchasePrice { get; set; }

    [XmlAttribute("PurchaseDate")]
    public string PurchaseDate { get; set; }

    [XmlAttribute("ExpirationDate")]
    public string ExpirationDate { get; set; }

    [XmlAttribute("ProductId")]
    public string ProductId { get; set; }

    [XmlAttribute("ProductType")]
    public string ProductType { get; set; }

    [XmlAttribute("AppId")]
    public string AppId { get; set; }

    [XmlAttribute("PublisherDeviceId")]
    public string PublisherDeviceId { get; set; }

    [XmlAttribute("PublisherUserId")]
    public string PublisherUserId { get; set; }

    [XmlAttribute("MicrosoftProductId")]
    public string MicrosoftProductId { get; set; }

    [XmlAttribute("MicrosoftAppId")]
    public string MicrosoftAppId { get; set; }
  }
}
