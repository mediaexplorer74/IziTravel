// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.BufferStaticDataSource
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51802, Culture=neutral, PublicKeyToken=null
// MVID: 6C361F8B-A5CA-49B8-ABC1-A50EE493C003
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.xml

using System;
using System.IO;

#nullable disable
namespace ICSharpCode.SharpZipLib.Zip
{
  /// <summary>Bytes array static data source</summary>
  public class BufferStaticDataSource : IStaticDataSource
  {
    private byte[] _Buffer;

    /// <summary>Create a new buffer data source</summary>
    /// <param name="buffer"></param>
    public BufferStaticDataSource(byte[] buffer)
    {
      this._Buffer = buffer != null ? buffer : throw new ArgumentNullException(nameof (buffer));
    }

    /// <summary>Get a stream based on the buffer</summary>
    /// <returns></returns>
    public Stream GetSource() => (Stream) new MemoryStream(this._Buffer);
  }
}
