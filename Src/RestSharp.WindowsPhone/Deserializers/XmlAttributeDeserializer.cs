// Decompiled with JetBrains decompiler
// Type: RestSharp.Deserializers.XmlAttributeDeserializer
// Assembly: RestSharp.WindowsPhone, Version=105.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EBEB4E14-2430-4192-BEF2-49D8C49DBCCE
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.xml

using RestSharp.Extensions;
using System.Reflection;
using System.Xml.Linq;

#nullable disable
namespace RestSharp.Deserializers
{
  public class XmlAttributeDeserializer : XmlDeserializer
  {
    protected override object GetValueFromXml(XElement root, XName name, PropertyInfo prop)
    {
      bool flag = false;
      DeserializeAsAttribute attribute = prop.GetAttribute<DeserializeAsAttribute>();
      if (attribute != null)
      {
        string name1 = attribute.Name;
        name = name1 != null ? (XName) name1 : name;
        flag = attribute.Attribute;
      }
      if (flag)
      {
        XAttribute attributeByName = this.GetAttributeByName(root, name);
        if (attributeByName != null)
          return (object) attributeByName.Value;
      }
      return base.GetValueFromXml(root, name, prop);
    }
  }
}
