// Decompiled with JetBrains decompiler
// Type: RestSharp.Serializers.JsonSerializer
// Assembly: RestSharp.WindowsPhone, Version=105.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EBEB4E14-2430-4192-BEF2-49D8C49DBCCE
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.xml

#nullable disable
namespace RestSharp.Serializers
{
  /// <summary>
  /// Default JSON serializer for request bodies
  /// Doesn't currently use the SerializeAs attribute, defers to Newtonsoft's attributes
  /// </summary>
  public class JsonSerializer : ISerializer
  {
    /// <summary>Default serializer</summary>
    public JsonSerializer() => this.ContentType = "application/json";

    /// <summary>Serialize the object as JSON</summary>
    /// <param name="obj">Object to serialize</param>
    /// <returns>JSON as String</returns>
    public string Serialize(object obj) => SimpleJson.SerializeObject(obj);

    /// <summary>Unused for JSON Serialization</summary>
    public string DateFormat { get; set; }

    /// <summary>Unused for JSON Serialization</summary>
    public string RootElement { get; set; }

    /// <summary>Unused for JSON Serialization</summary>
    public string Namespace { get; set; }

    /// <summary>Content type for serialized content</summary>
    public string ContentType { get; set; }
  }
}
