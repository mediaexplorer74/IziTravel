// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.EmptyDynamicDataSource
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51802, Culture=neutral, PublicKeyToken=null
// MVID: 6C361F8B-A5CA-49B8-ABC1-A50EE493C003
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.xml

using System.IO;

#nullable disable
namespace ICSharpCode.SharpZipLib.Zip
{
  /// <summary>Empty data source</summary>
  public class EmptyDynamicDataSource : IDynamicDataSource
  {
    /// <summary>
    /// Get a <see cref="T:System.IO.Stream" /> providing data for an entry.
    /// </summary>
    /// <param name="entry"></param>
    /// <param name="name"></param>
    /// <returns>Returns always null.</returns>
    public Stream GetSource(ZipEntry entry, string name) => (Stream) null;
  }
}
