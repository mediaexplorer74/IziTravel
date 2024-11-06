// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.DynamicDiskDataSource
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51802, Culture=neutral, PublicKeyToken=null
// MVID: 6C361F8B-A5CA-49B8-ABC1-A50EE493C003
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.xml

using System.IO;

#nullable disable
namespace ICSharpCode.SharpZipLib.Zip
{
  /// <summary>
  /// Default implementation of <see cref="T:ICSharpCode.SharpZipLib.Zip.IDynamicDataSource" /> for files stored on disk.
  /// </summary>
  public class DynamicDiskDataSource : IDynamicDataSource
  {
    /// <summary>
    /// Get a <see cref="T:System.IO.Stream" /> providing data for an entry.
    /// </summary>
    /// <param name="entry">The entry to provide data for.</param>
    /// <param name="name">The file name for data if known.</param>
    /// <returns>Returns a stream providing data; or null if not available</returns>
    public Stream GetSource(ZipEntry entry, string name)
    {
      Stream source = (Stream) null;
      if (name != null)
        source = (Stream) VFS.Current.OpenReadFile(name);
      return source;
    }
  }
}
