// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Settings.Model.LicenseInfo
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using System.Collections.Generic;
using System.Xml.Serialization;

#nullable disable
namespace Izi.Travel.Shell.Settings.Model
{
  [XmlRoot("licenseInfo")]
  public class LicenseInfo
  {
    [XmlArray("licenses")]
    [XmlArrayItem(typeof (License))]
    public List<License> Licenses { get; set; }

    [XmlArray("packages")]
    [XmlArrayItem(typeof (Package))]
    public List<Package> Packages { get; set; }
  }
}
