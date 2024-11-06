// Decompiled with JetBrains decompiler
// Type: RestSharp.Deserializers.DotNetXmlDeserializer
// Assembly: RestSharp.WindowsPhone, Version=105.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EBEB4E14-2430-4192-BEF2-49D8C49DBCCE
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.xml

using System.IO;
using System.Text;
using System.Xml.Serialization;

#nullable disable
namespace RestSharp.Deserializers
{
  /// <summary>Wrapper for System.Xml.Serialization.XmlSerializer.</summary>
  public class DotNetXmlDeserializer : IDeserializer
  {
    public string DateFormat { get; set; }

    public string Namespace { get; set; }

    public string RootElement { get; set; }

    public T Deserialize<T>(IRestResponse response)
    {
      if (string.IsNullOrEmpty(response.Content))
        return default (T);
      using (MemoryStream memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(response.Content)))
        return (T) new XmlSerializer(typeof (T)).Deserialize((Stream) memoryStream);
    }
  }
}
