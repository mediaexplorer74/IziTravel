// Decompiled with JetBrains decompiler
// Type: GoogleAnalytics.Helpers
// Assembly: GoogleAnalytics, Version=1.2.11.25892, Culture=neutral, PublicKeyToken=null
// MVID: ABC239A9-7B01-4013-916D-8F4A2BC96BC0
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\GoogleAnalytics.dll

using System;
using System.Xml;

#nullable disable
namespace GoogleAnalytics
{
  internal static class Helpers
  {
    public static string GetAppAttribute(string attributeName)
    {
      try
      {
        using (XmlReader xmlReader = XmlReader.Create("WMAppManifest.xml", new XmlReaderSettings()
        {
          XmlResolver = (XmlResolver) new XmlXapResolver()
        }))
        {
          xmlReader.ReadToDescendant("App");
          return xmlReader.IsStartElement() ? xmlReader.GetAttribute(attributeName) : throw new FormatException("WMAppManifest.xml is missing");
        }
      }
      catch
      {
        return (string) null;
      }
    }
  }
}
