// Decompiled with JetBrains decompiler
// Type: RestSharp.Deserializers.DeserializeAsAttribute
// Assembly: RestSharp.WindowsPhone, Version=105.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EBEB4E14-2430-4192-BEF2-49D8C49DBCCE
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.xml

using System;

#nullable disable
namespace RestSharp.Deserializers
{
  /// <summary>
  /// Allows control how class and property names and values are deserialized by XmlAttributeDeserializer
  /// </summary>
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
  public sealed class DeserializeAsAttribute : System.Attribute
  {
    /// <summary>The name to use for the serialized element</summary>
    public string Name { get; set; }

    /// <summary>
    /// Sets if the property to Deserialize is an Attribute or Element (Default: false)
    /// </summary>
    public bool Attribute { get; set; }
  }
}
