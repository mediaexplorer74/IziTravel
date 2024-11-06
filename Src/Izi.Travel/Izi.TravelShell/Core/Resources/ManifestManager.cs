// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Core.Resources.ManifestManager
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using System.Linq;
using System.Xml.Linq;

#nullable disable
namespace Izi.Travel.Shell.Core.Resources
{
  public class ManifestManager
  {
    private readonly XDocument _xDocumentManifest;

    public ManifestManager()
      : this("WMAppManifest.xml")
    {
    }

    public ManifestManager(string manifest) => this._xDocumentManifest = XDocument.Load(manifest);

    public string GetAppAttributeValue(string attributeName)
    {
      if (this._xDocumentManifest == null)
        return (string) null;
      return this._xDocumentManifest.Descendants((XName) "App").SingleOrDefault<XElement>()?.Attribute((XName) attributeName)?.Value;
    }
  }
}
