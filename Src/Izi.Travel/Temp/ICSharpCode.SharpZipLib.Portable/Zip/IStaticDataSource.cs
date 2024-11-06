// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.IStaticDataSource
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51802, Culture=neutral, PublicKeyToken=null
// MVID: 6C361F8B-A5CA-49B8-ABC1-A50EE493C003
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.xml

using System.IO;

#nullable disable
namespace ICSharpCode.SharpZipLib.Zip
{
  /// <summary>
  /// Provides a static way to obtain a source of data for an entry.
  /// </summary>
  public interface IStaticDataSource
  {
    /// <summary>Get a source of data by creating a new stream.</summary>
    /// <returns>Returns a <see cref="T:System.IO.Stream" /> to use for compression input.</returns>
    /// <remarks>Ideally a new stream is created and opened to achieve this, to avoid locking problems.</remarks>
    Stream GetSource();
  }
}
