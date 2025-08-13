// ********************************************************************
// Type: Izi.Travel.Settings.Model.LicenseInfo
// Assembly: Izi.Travel, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.dll

using System.Collections.Generic;
using System.Xml.Serialization;

#nullable disable
namespace Izi.Travel.Settings.Model
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
