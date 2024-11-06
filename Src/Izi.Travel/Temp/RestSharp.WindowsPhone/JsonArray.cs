// Decompiled with JetBrains decompiler
// Type: RestSharp.JsonArray
// Assembly: RestSharp.WindowsPhone, Version=105.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EBEB4E14-2430-4192-BEF2-49D8C49DBCCE
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.xml

using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable
namespace RestSharp
{
  /// <summary>Represents the json array.</summary>
  [EditorBrowsable(EditorBrowsableState.Never)]
  [GeneratedCode("simple-json", "1.0.0")]
  public class JsonArray : List<object>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:RestSharp.JsonArray" /> class.
    /// </summary>
    public JsonArray()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:RestSharp.JsonArray" /> class.
    /// </summary>
    /// <param name="capacity">The capacity of the json array.</param>
    public JsonArray(int capacity)
      : base(capacity)
    {
    }

    /// <summary>The json representation of the array.</summary>
    /// <returns>The json representation of the array.</returns>
    public override string ToString() => SimpleJson.SerializeObject((object) this) ?? string.Empty;
  }
}
