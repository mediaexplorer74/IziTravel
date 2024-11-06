// Decompiled with JetBrains decompiler
// Type: RestSharp.Parameter
// Assembly: RestSharp.WindowsPhone, Version=105.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EBEB4E14-2430-4192-BEF2-49D8C49DBCCE
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.xml

#nullable disable
namespace RestSharp
{
  /// <summary>Parameter container for REST requests</summary>
  public class Parameter
  {
    /// <summary>Name of the parameter</summary>
    public string Name { get; set; }

    /// <summary>Value of the parameter</summary>
    public object Value { get; set; }

    /// <summary>Type of the parameter</summary>
    public ParameterType Type { get; set; }

    /// <summary>
    /// Return a human-readable representation of this parameter
    /// </summary>
    /// <returns>String</returns>
    public override string ToString() => string.Format("{0}={1}", (object) this.Name, this.Value);
  }
}
