// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.StringStaticDataSource
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51802, Culture=neutral, PublicKeyToken=null
// MVID: 6C361F8B-A5CA-49B8-ABC1-A50EE493C003
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.xml

using System.IO;
using System.Text;

#nullable disable
namespace ICSharpCode.SharpZipLib.Zip
{
  /// <summary>String static data source</summary>
  public class StringStaticDataSource : IStaticDataSource
  {
    private string _StringBuffer;

    /// <summary>Create a new data source</summary>
    /// <param name="str">String content</param>
    public StringStaticDataSource(string str) => this._StringBuffer = str ?? string.Empty;

    /// <summary>Get then stream based on the string content</summary>
    /// <returns></returns>
    public Stream GetSource()
    {
      return (Stream) new MemoryStream(Encoding.UTF8.GetBytes(this._StringBuffer));
    }
  }
}
