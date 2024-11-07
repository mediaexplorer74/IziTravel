// Decompiled with JetBrains decompiler
// Type: RestSharp.Serializers.DotNetXmlSerializer
// Assembly: RestSharp.WindowsPhone, Version=105.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EBEB4E14-2430-4192-BEF2-49D8C49DBCCE
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.xml

using System.IO;
using System.Text;
using System.Xml.Serialization;

#nullable disable
namespace RestSharp.Serializers
{
  /// <summary>Wrapper for System.Xml.Serialization.XmlSerializer.</summary>
  public class DotNetXmlSerializer : ISerializer
  {
    /// <summary>Default constructor, does not specify namespace</summary>
    public DotNetXmlSerializer()
    {
      this.ContentType = "application/xml";
      this.Encoding = Encoding.UTF8;
    }

    /// <summary>Specify the namespaced to be used when serializing</summary>
    /// <param name="namespace">XML namespace</param>
    public DotNetXmlSerializer(string @namespace)
      : this()
    {
      this.Namespace = @namespace;
    }

    /// <summary>Serialize the object as XML</summary>
    /// <param name="obj">Object to serialize</param>
    /// <returns>XML as string</returns>
    public string Serialize(object obj)
    {
      XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
      namespaces.Add(string.Empty, this.Namespace);
      System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(obj.GetType());
      DotNetXmlSerializer.EncodingStringWriter encodingStringWriter = new DotNetXmlSerializer.EncodingStringWriter(this.Encoding);
      xmlSerializer.Serialize((TextWriter) encodingStringWriter, obj, namespaces);
      return encodingStringWriter.ToString();
    }

    /// <summary>Name of the root element to use when serializing</summary>
    public string RootElement { get; set; }

    /// <summary>XML namespace to use when serializing</summary>
    public string Namespace { get; set; }

    /// <summary>Format string to use when serializing dates</summary>
    public string DateFormat { get; set; }

    /// <summary>Content type for serialized content</summary>
    public string ContentType { get; set; }

    /// <summary>Encoding for serialized content</summary>
    public Encoding Encoding { get; set; }

    /// <summary>
    /// Need to subclass StringWriter in order to override Encoding
    /// </summary>
    private class EncodingStringWriter : StringWriter
    {
      private readonly Encoding encoding;

      public EncodingStringWriter(Encoding encoding) => this.encoding = encoding;

      public override Encoding Encoding => this.encoding;
    }
  }
}
