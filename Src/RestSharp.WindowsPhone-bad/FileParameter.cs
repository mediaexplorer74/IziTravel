// Decompiled with JetBrains decompiler
// Type: RestSharp.FileParameter
// Assembly: RestSharp.WindowsPhone, Version=105.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EBEB4E14-2430-4192-BEF2-49D8C49DBCCE
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.xml

using System;
using System.IO;

#nullable disable
namespace RestSharp
{
  /// <summary>Container for files to be uploaded with requests</summary>
  public class FileParameter
  {
    /// <summary>Creates a file parameter from an array of bytes.</summary>
    /// <param name="name">The parameter name to use in the request.</param>
    /// <param name="data">The data to use as the file's contents.</param>
    /// <param name="filename">The filename to use in the request.</param>
    /// <param name="contentType">The content type to use in the request.</param>
    /// <returns>The <see cref="T:RestSharp.FileParameter" /></returns>
    public static FileParameter Create(
      string name,
      byte[] data,
      string filename,
      string contentType)
    {
      long length = (long) data.Length;
      return new FileParameter()
      {
        Writer = (Action<Stream>) (s => s.Write(data, 0, data.Length)),
        FileName = filename,
        ContentType = contentType,
        ContentLength = length,
        Name = name
      };
    }

    /// <summary>Creates a file parameter from an array of bytes.</summary>
    /// <param name="name">The parameter name to use in the request.</param>
    /// <param name="data">The data to use as the file's contents.</param>
    /// <param name="filename">The filename to use in the request.</param>
    /// <returns>The <see cref="T:RestSharp.FileParameter" /> using the default content type.</returns>
    public static FileParameter Create(string name, byte[] data, string filename)
    {
      return FileParameter.Create(name, data, filename, (string) null);
    }

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
