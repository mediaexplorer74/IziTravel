// Decompiled with JetBrains decompiler
// Type: Coding4Fun.Toolkit.Controls.Common.PhoneHelper
// Assembly: Coding4Fun.Toolkit.Controls, Version=2.0.7.0, Culture=neutral, PublicKeyToken=c5fd7b72b1a17ce4
// MVID: FA82EF8B-B083-4BA3-8FA6-4342AD0FAB1C
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Coding4Fun.Toolkit.Controls.dll

using System;
using System.Xml;

#nullable disable
namespace Coding4Fun.Toolkit.Controls.Common
{
  public class PhoneHelper
  {
    private const string AppManifestName = "WMAppManifest.xml";
    private const string AppNodeName = "App";

    public static string GetAppAttribute(string attributeName)
    {
      if (ApplicationSpace.IsDesignMode)
        return "";
      try
      {
        using (XmlReader xmlReader = XmlReader.Create("WMAppManifest.xml", new XmlReaderSettings()
        {
          XmlResolver = (XmlResolver) new XmlXapResolver()
        }))
        {
          xmlReader.ReadToDescendant("App");
          return xmlReader.IsStartElement() ? xmlReader.GetAttribute(attributeName) : throw new FormatException("WMAppManifest.xml is missing App");
        }
      }
      catch (Exception ex)
      {
        return "";
      }
    }
  }
}
