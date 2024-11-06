// Decompiled with JetBrains decompiler
// Type: RestSharp.HttpParameter
// Assembly: RestSharp.WindowsPhone, Version=105.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EBEB4E14-2430-4192-BEF2-49D8C49DBCCE
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.xml

#nullable disable
namespace RestSharp
{
  /// <summary>
  /// Representation of an HTTP parameter (QueryString or Form value)
  /// </summary>
  public class HttpParameter
  {
    /// <summary>Name of the parameter</summary>
    public string Name { get; set; }

    /// <summary>Value of the parameter</summary>
    public string Value { get; set; }
  }
}
