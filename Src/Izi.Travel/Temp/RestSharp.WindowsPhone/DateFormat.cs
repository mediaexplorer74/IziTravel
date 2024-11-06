// Decompiled with JetBrains decompiler
// Type: RestSharp.DateFormat
// Assembly: RestSharp.WindowsPhone, Version=105.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EBEB4E14-2430-4192-BEF2-49D8C49DBCCE
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.xml

using System.Runtime.InteropServices;

#nullable disable
namespace RestSharp
{
  /// <summary>Format strings for commonly-used date formats</summary>
  [StructLayout(LayoutKind.Sequential, Size = 1)]
  public struct DateFormat
  {
    /// <summary>.NET format string for ISO 8601 date format</summary>
    public const string Iso8601 = "s";
    /// <summary>.NET format string for roundtrip date format</summary>
    public const string RoundTrip = "u";
  }
}
