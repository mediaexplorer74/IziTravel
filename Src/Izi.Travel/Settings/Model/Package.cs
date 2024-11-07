// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Settings.Model.Package
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using System.Xml.Serialization;

#nullable disable
namespace Izi.Travel.Shell.Settings.Model
{
  [XmlType("package")]
  public class Package
  {
    [XmlAttribute("name")]
    public string Name { get; set; }

    [XmlAttribute("version")]
    public string Version { get; set; }

    [XmlAttribute("copyright")]
    public string Copyright { get; set; }

    [XmlAttribute("license")]
    public string LicenseId { get; set; }

    [XmlIgnore]
    public License License { get; set; }
  }
}
