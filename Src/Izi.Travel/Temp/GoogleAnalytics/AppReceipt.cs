// Decompiled with JetBrains decompiler
// Type: GoogleAnalytics.AppReceipt
// Assembly: GoogleAnalytics, Version=1.2.11.25892, Culture=neutral, PublicKeyToken=null
// MVID: ABC239A9-7B01-4013-916D-8F4A2BC96BC0
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\GoogleAnalytics.dll

using System.IO;
using System.Xml;

#nullable disable
namespace GoogleAnalytics
{
  public sealed class AppReceipt
  {
    public string Id { get; set; }

    public string AppId { get; set; }

    public string LicenseType { get; set; }

    public static AppReceipt Load(string receipt)
    {
      using (XmlReader xmlReader = XmlReader.Create((TextReader) new StringReader(receipt)))
      {
        if (xmlReader.ReadToFollowing(nameof (AppReceipt)))
          return new AppReceipt()
          {
            Id = xmlReader.GetAttribute("Id"),
            AppId = xmlReader.GetAttribute("AppId"),
            LicenseType = xmlReader.GetAttribute("LicenseType")
          };
      }
      return (AppReceipt) null;
    }
  }
}
