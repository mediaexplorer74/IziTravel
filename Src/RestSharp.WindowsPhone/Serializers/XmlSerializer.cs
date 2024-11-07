// Decompiled with JetBrains decompiler
// Type: RestSharp.Serializers.XmlSerializer
// Assembly: RestSharp.WindowsPhone, Version=105.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EBEB4E14-2430-4192-BEF2-49D8C49DBCCE
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.xml

using RestSharp.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

#nullable disable
namespace RestSharp.Serializers
{
  /// <summary>Default XML Serializer</summary>
  public class XmlSerializer : ISerializer
  {
    /// <summary>Default constructor, does not specify namespace</summary>
    public XmlSerializer() => this.ContentType = "text/xml";

    /// <summary>Specify the namespaced to be used when serializing</summary>
    /// <param name="namespace">XML namespace</param>
    public XmlSerializer(string @namespace)
    {
      this.Namespace = @namespace;
      this.ContentType = "text/xml";
    }

    /// <summary>Serialize the object as XML</summary>
    /// <param name="obj">Object to serialize</param>
    /// <returns>XML as string</returns>
    public string Serialize(object obj)
    {
      XDocument xdocument = new XDocument();
      Type type1 = obj.GetType();
      string name1 = type1.Name;
      SerializeAsAttribute attribute1 = ReflectionExtensions.GetAttribute<SerializeAsAttribute>(type1);
      if (attribute1 != null)
        name1 = attribute1.TransformName(attribute1.Name ?? name1);
      XElement xelement1 = new XElement(name1.AsNamespaced(this.Namespace));
      if (obj is IList)
      {
        string name2 = "";
        foreach (object obj1 in (IEnumerable) obj)
        {
          Type type2 = obj1.GetType();
          SerializeAsAttribute attribute2 = ReflectionExtensions.GetAttribute<SerializeAsAttribute>(type2);
          if (attribute2 != null)
            name2 = attribute2.TransformName(attribute2.Name ?? name1);
          if (name2 == "")
            name2 = type2.Name;
          XElement xelement2 = new XElement(name2.AsNamespaced(this.Namespace));
          this.Map(xelement2, obj1);
          xelement1.Add((object) xelement2);
        }
      }
      else
        this.Map(xelement1, obj);
      if (this.RootElement.HasValue())
      {
        XElement content = new XElement(this.RootElement.AsNamespaced(this.Namespace), (object) xelement1);
        xdocument.Add((object) content);
      }
      else
        xdocument.Add((object) xelement1);
      return xdocument.ToString();
    }

    private void Map(XElement root, object obj)
    {
      Type type1 = obj.GetType();
      IEnumerable<PropertyInfo> propertyInfos = ((IEnumerable<PropertyInfo>) type1.GetProperties()).Select(p => new
      {
        p = p,
        indexAttribute = p.GetAttribute<SerializeAsAttribute>()
      }).Where(_param0 => _param0.p.CanRead && _param0.p.CanWrite).OrderBy(_param0 => _param0.indexAttribute != null ? _param0.indexAttribute.Index : int.MaxValue).Select(_param0 => _param0.p);
      SerializeAsAttribute attribute1 = ReflectionExtensions.GetAttribute<SerializeAsAttribute>(type1);
      foreach (PropertyInfo prop in propertyInfos)
      {
        string str = prop.Name;
        object obj1 = prop.GetValue(obj, (object[]) null);
        if (obj1 != null)
        {
          string serializedValue = this.GetSerializedValue(obj1);
          Type propertyType = prop.PropertyType;
          bool flag = false;
          SerializeAsAttribute attribute2 = prop.GetAttribute<SerializeAsAttribute>();
          if (attribute2 != null)
          {
            str = attribute2.Name.HasValue() ? attribute2.Name : str;
            flag = attribute2.Attribute;
          }
          SerializeAsAttribute attribute3 = prop.GetAttribute<SerializeAsAttribute>();
          if (attribute3 != null)
            str = attribute3.TransformName(str);
          else if (attribute1 != null)
            str = attribute1.TransformName(str);
          XElement xelement1 = new XElement(str.AsNamespaced(this.Namespace));
          if (propertyType.IsPrimitive || propertyType.IsValueType || propertyType == typeof (string))
          {
            if (flag)
            {
              root.Add((object) new XAttribute((XName) str, (object) serializedValue));
              continue;
            }
            xelement1.Value = serializedValue;
          }
          else if (obj1 is IList)
          {
            string name = "";
            foreach (object obj2 in (IEnumerable) obj1)
            {
              if (name == "")
              {
                Type type2 = obj2.GetType();
                SerializeAsAttribute attribute4 = ReflectionExtensions.GetAttribute<SerializeAsAttribute>(type2);
                name = attribute4 == null || !attribute4.Name.HasValue() ? type2.Name : attribute4.Name;
              }
              XElement xelement2 = new XElement(name.AsNamespaced(this.Namespace));
              this.Map(xelement2, obj2);
              xelement1.Add((object) xelement2);
            }
          }
          else
            this.Map(xelement1, obj1);
          root.Add((object) xelement1);
        }
      }
    }

    private string GetSerializedValue(object obj)
    {
      object obj1 = obj;
      if (obj is DateTime && this.DateFormat.HasValue())
        obj1 = (object) ((DateTime) obj).ToString(this.DateFormat, (IFormatProvider) CultureInfo.InvariantCulture);
      if (obj is bool flag)
        obj1 = (object) flag.ToString((IFormatProvider) CultureInfo.InvariantCulture).ToLower();
      return XmlSerializer.IsNumeric(obj) ? XmlSerializer.SerializeNumber(obj) : obj1.ToString();
    }

    private static string SerializeNumber(object number)
    {
      switch (number)
      {
        case long num1:
          return num1.ToString((IFormatProvider) CultureInfo.InvariantCulture);
        case ulong num2:
          return num2.ToString((IFormatProvider) CultureInfo.InvariantCulture);
        case int num3:
          return num3.ToString((IFormatProvider) CultureInfo.InvariantCulture);
        case uint num4:
          return num4.ToString((IFormatProvider) CultureInfo.InvariantCulture);
        case Decimal num5:
          return num5.ToString((IFormatProvider) CultureInfo.InvariantCulture);
        case float num6:
          return num6.ToString((IFormatProvider) CultureInfo.InvariantCulture);
        default:
          return Convert.ToDouble(number, (IFormatProvider) CultureInfo.InvariantCulture).ToString("r", (IFormatProvider) CultureInfo.InvariantCulture);
      }
    }

    /// <summary>
    /// Determines if a given object is numeric in any way
    /// (can be integer, double, null, etc).
    /// </summary>
    private static bool IsNumeric(object value)
    {
      switch (value)
      {
        case sbyte _:
          return true;
        case byte _:
          return true;
        case short _:
          return true;
        case ushort _:
          return true;
        case int _:
          return true;
        case uint _:
          return true;
        case long _:
          return true;
        case ulong _:
          return true;
        case float _:
          return true;
        case double _:
          return true;
        case Decimal _:
          return true;
        default:
          return false;
      }
    }

    /// <summary>Name of the root element to use when serializing</summary>
    public string RootElement { get; set; }

    /// <summary>XML namespace to use when serializing</summary>
    public string Namespace { get; set; }

    /// <summary>Format string to use when serializing dates</summary>
    public string DateFormat { get; set; }

    /// <summary>Content type for serialized content</summary>
    public string ContentType { get; set; }
  }
}
