// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Entities.Purchase.StoreReceipt
// Assembly: Izi.Travel.Business.Entities, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: DDED5915-8B3A-4C03-AAF5-BE6B16E9CC4A
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.Entities.dll

using System.Xml.Serialization;

#nullable disable
namespace Izi.Travel.Business.Entities.Purchase
{
  [XmlRoot(ElementName = "Receipt", Namespace = "http://schemas.microsoft.com/windows/2012/store/receipt")]
  public class StoreReceipt
  {
    [XmlAttribute(AttributeName = "Version")]
    public string Version { get; set; }

    [XmlAttribute(AttributeName = "CertificateId")]
    public string CertificateId { get; set; }

    [XmlElement(ElementName = "ProductReceipt")]
    public StoreProductReceipt ProductReceipt { get; set; }
  }
}
