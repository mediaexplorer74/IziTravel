// Decompiled with JetBrains decompiler
// Type: RestSharp.Extensions.XmlExtensions
// Assembly: RestSharp.WindowsPhone, Version=105.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EBEB4E14-2430-4192-BEF2-49D8C49DBCCE
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.xml

using System.Xml.Linq;

#nullable disable
namespace RestSharp.Extensions
{
  /// <summary>XML Extension Methods</summary>
  public static class XmlExtensions
  {
    /// <summary>
    /// Returns the name of an element with the namespace if specified
    /// </summary>
    /// <param name="name">Element name</param>
    /// <param name="namespace">XML Namespace</param>
    /// <returns></returns>
    public static XName AsNamespaced(this string name, string @namespace)
    {
      XName xname = (XName) name;
      if (@namespace.HasValue())
        xname = XName.Get(name, @namespace);
      return xname;
    }
  }
}
