// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.StaticDiskDataSource
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51802, Culture=neutral, PublicKeyToken=null
// MVID: 6C361F8B-A5CA-49B8-ABC1-A50EE493C003
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.xml

using System.IO;

#nullable disable
namespace ICSharpCode.SharpZipLib.Zip
{
  /// <summary>
  /// Default implementation of a <see cref="T:ICSharpCode.SharpZipLib.Zip.IStaticDataSource" /> for use with files stored on disk.
  /// </summary>
  public class StaticDiskDataSource : IStaticDataSource
  {
    private string fileName_;

    /// <summary>
    /// Initialise a new instnace of <see cref="T:ICSharpCode.SharpZipLib.Zip.StaticDiskDataSource" />
    /// </summary>
    /// <param name="fileName">The name of the file to obtain data from.</param>
    public StaticDiskDataSource(string fileName) => this.fileName_ = fileName;

    /// <summary>
    /// Get a <see cref="T:System.IO.Stream" /> providing data.
    /// </summary>
    /// <returns>Returns a <see cref="T:System.IO.Stream" /> provising data.</returns>
    public Stream GetSource() => (Stream) VFS.Current.OpenReadFile(this.fileName_);
  }
}
