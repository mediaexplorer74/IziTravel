// Decompiled with JetBrains decompiler
// Type: RestSharp.Authenticators.OAuth.HttpPostParameter
// Assembly: RestSharp.WindowsPhone, Version=105.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EBEB4E14-2430-4192-BEF2-49D8C49DBCCE
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.xml

using System.IO;

#nullable disable
namespace RestSharp.Authenticators.OAuth
{
  internal class HttpPostParameter : WebParameter
  {
    public HttpPostParameter(string name, string value)
      : base(name, value)
    {
    }

    public virtual HttpPostParameterType Type { get; private set; }

    public virtual string FileName { get; private set; }

    public virtual string FilePath { get; private set; }

    public virtual Stream FileStream { get; set; }

    public virtual string ContentType { get; private set; }

    public static HttpPostParameter CreateFile(
      string name,
      string fileName,
      string filePath,
      string contentType)
    {
      return new HttpPostParameter(name, string.Empty)
      {
        Type = HttpPostParameterType.File,
        FileName = fileName,
        FilePath = filePath,
        ContentType = contentType
      };
    }

    public static HttpPostParameter CreateFile(
      string name,
      string fileName,
      Stream fileStream,
      string contentType)
    {
      return new HttpPostParameter(name, string.Empty)
      {
        Type = HttpPostParameterType.File,
        FileName = fileName,
        FileStream = fileStream,
        ContentType = contentType
      };
    }
  }
}
