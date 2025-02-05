// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Settings.Model.License
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

#nullable disable
namespace Izi.Travel.Shell.Settings.Model
{
  [XmlType("license")]
  public class License
  {
    [XmlAttribute("id")]
    public string Id { get; set; }

    [XmlAttribute("name")]
    public string Name { get; set; }

    [XmlText]
    public string Content { get; set; }

    [XmlIgnore]
    public string[] ContentParts
    {
      get
      {
        if (this.Content == null)
          return (string[]) null;
        return ((IEnumerable<string>) this.Content.Split('\n', '\r')).Select<string, string>((Func<string, string>) (x => x.Trim())).Where<string>((Func<string, bool>) (x => !string.IsNullOrWhiteSpace(x))).ToArray<string>();
      }
    }
  }
}
