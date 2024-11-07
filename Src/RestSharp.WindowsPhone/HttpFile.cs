// Decompiled with JetBrains decompiler
// Type: RestSharp.HttpFile
// Assembly: RestSharp.WindowsPhone, Version=105.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EBEB4E14-2430-4192-BEF2-49D8C49DBCCE
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.xml

using System;
using System.IO;

#nullable disable
namespace RestSharp
{
  /// <summary>Container for HTTP file</summary>
  public class HttpFile
  {
    /// <summary>The length of data to be sent</summary>
    public long ContentLength { get; set; }

    /// <summary>Provides raw data for file</summary>
    public Action<Stream> Writer { get; set; }

    /// <summary>Name of the file to use when uploading</summary>
    public string FileName { get; set; }

    /// <summary>MIME content type of file</summary>
    public string ContentType { get; set; }

    /// <summary>Name of the parameter</summary>
    public string Name { get; set; }
  }
}
