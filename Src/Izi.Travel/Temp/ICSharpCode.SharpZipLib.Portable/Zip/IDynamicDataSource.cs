// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.IDynamicDataSource
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51802, Culture=neutral, PublicKeyToken=null
// MVID: 6C361F8B-A5CA-49B8-ABC1-A50EE493C003
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.xml

using System.IO;

#nullable disable
namespace ICSharpCode.SharpZipLib.Zip
{
  /// <summary>
  /// Represents a source of data that can dynamically provide
  /// multiple <see cref="T:System.IO.Stream">data sources</see> based on the parameters passed.
  /// </summary>
  public interface IDynamicDataSource
  {
    /// <summary>Get a data source.</summary>
    /// <param name="entry">The <see cref="T:ICSharpCode.SharpZipLib.Zip.ZipEntry" /> to get a source for.</param>
    /// <param name="name">The name for data if known.</param>
    /// <returns>Returns a <see cref="T:System.IO.Stream" /> to use for compression input.</returns>
    /// <remarks>Ideally a new stream is created and opened to achieve this, to avoid locking problems.</remarks>
    Stream GetSource(ZipEntry entry, string name);
  }
}
