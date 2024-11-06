// Decompiled with JetBrains decompiler
// Type: RestSharp.Serializers.SerializeAsAttribute
// Assembly: RestSharp.WindowsPhone, Version=105.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EBEB4E14-2430-4192-BEF2-49D8C49DBCCE
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.xml

using RestSharp.Extensions;
using System;
using System.Globalization;

#nullable disable
namespace RestSharp.Serializers
{
  /// <summary>
  /// Allows control how class and property names and values are serialized by XmlSerializer
  /// Currently not supported with the JsonSerializer
  /// When specified at the property level the class-level specification is overridden
  /// </summary>
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
  public sealed class SerializeAsAttribute : System.Attribute
  {
    public SerializeAsAttribute()
    {
      this.NameStyle = NameStyle.AsIs;
      this.Index = int.MaxValue;
      this.Culture = CultureInfo.InvariantCulture;
    }

    /// <summary>The name to use for the serialized element</summary>
    public string Name { get; set; }

    /// <summary>
    /// Sets the value to be serialized as an Attribute instead of an Element
    /// </summary>
    public bool Attribute { get; set; }

    /// <summary>The culture to use when serializing</summary>
    public CultureInfo Culture { get; set; }

    /// <summary>
    /// Transforms the casing of the name based on the selected value.
    /// </summary>
    public NameStyle NameStyle { get; set; }

    /// <summary>
    /// The order to serialize the element. Default is int.MaxValue.
    /// </summary>
    public int Index { get; set; }

    /// <summary>Called by the attribute when NameStyle is speficied</summary>
    /// <param name="input">The string to transform</param>
    /// <returns>String</returns>
    public string TransformName(string input)
    {
      string lowercaseAndUnderscoredWord = this.Name ?? input;
      switch (this.NameStyle)
      {
        case NameStyle.CamelCase:
          return lowercaseAndUnderscoredWord.ToCamelCase(this.Culture);
        case NameStyle.LowerCase:
          return lowercaseAndUnderscoredWord.ToLower();
        case NameStyle.PascalCase:
          return lowercaseAndUnderscoredWord.ToPascalCase(this.Culture);
        default:
          return input;
      }
    }
  }
}
