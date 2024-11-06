// Decompiled with JetBrains decompiler
// Type: RestSharp.IRestResponse`1
// Assembly: RestSharp.WindowsPhone, Version=105.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EBEB4E14-2430-4192-BEF2-49D8C49DBCCE
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.xml

#nullable disable
namespace RestSharp
{
  /// <summary>
  /// Container for data sent back from API including deserialized data
  /// </summary>
  /// <typeparam name="T">Type of data to deserialize to</typeparam>
  public interface IRestResponse<T> : IRestResponse
  {
    /// <summary>Deserialized entity data</summary>
    T Data { get; set; }
  }
}
