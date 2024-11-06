// Decompiled with JetBrains decompiler
// Type: GoogleAnalytics.ProductReceipt
// Assembly: GoogleAnalytics, Version=1.2.11.25892, Culture=neutral, PublicKeyToken=null
// MVID: ABC239A9-7B01-4013-916D-8F4A2BC96BC0
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\GoogleAnalytics.dll

using System.IO;
using System.Xml;

#nullable disable
namespace GoogleAnalytics
{
  public sealed class ProductReceipt
  {
    public string Id { get; set; }

    public string ProductId { get; set; }

    public string ProductType { get; set; }

    public static ProductReceipt Load(string receipt)
    {
      using (XmlReader xmlReader = XmlReader.Create((TextReader) new StringReader(receipt)))
      {
        if (xmlReader.ReadToFollowing(nameof (ProductReceipt)))
          return new ProductReceipt()
          {
            Id = xmlReader.GetAttribute("Id"),
            ProductId = xmlReader.GetAttribute("ProductId"),
            ProductType = xmlReader.GetAttribute("ProductType")
          };
      }
      return (ProductReceipt) null;
    }
  }
}
