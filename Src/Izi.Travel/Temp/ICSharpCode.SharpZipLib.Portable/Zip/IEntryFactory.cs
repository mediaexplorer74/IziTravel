// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.IEntryFactory
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51802, Culture=neutral, PublicKeyToken=null
// MVID: 6C361F8B-A5CA-49B8-ABC1-A50EE493C003
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.xml

using ICSharpCode.SharpZipLib.Core;

#nullable disable
namespace ICSharpCode.SharpZipLib.Zip
{
  /// <summary>
  /// Defines factory methods for creating new <see cref="T:ICSharpCode.SharpZipLib.Zip.ZipEntry"></see> values.
  /// </summary>
  public interface IEntryFactory
  {
    /// <summary>
    /// Create a <see cref="T:ICSharpCode.SharpZipLib.Zip.ZipEntry" /> for a file given its name
    /// </summary>
    /// <param name="fileName">The name of the file to create an entry for.</param>
    /// <returns>Returns a <see cref="T:ICSharpCode.SharpZipLib.Zip.ZipEntry">file entry</see> based on the <paramref name="fileName" /> passed.</returns>
    ZipEntry MakeFileEntry(string fileName);

    /// <summary>
    /// Create a <see cref="T:ICSharpCode.SharpZipLib.Zip.ZipEntry" /> for a file given its name
    /// </summary>
    /// <param name="fileName">The name of the file to create an entry for.</param>
    /// <param name="useFileSystem">If true get details from the file system if the file exists.</param>
    /// <returns>Returns a <see cref="T:ICSharpCode.SharpZipLib.Zip.ZipEntry">file entry</see> based on the <paramref name="fileName" /> passed.</returns>
    ZipEntry MakeFileEntry(string fileName, bool useFileSystem);

    /// <summary>
    /// Create a <see cref="T:ICSharpCode.SharpZipLib.Zip.ZipEntry" /> for a file given its actual name and optional override name
    /// </summary>
    /// <param name="fileName">The name of the file to create an entry for.</param>
    /// <param name="entryName">An alternative name to be used for the new entry. Null if not applicable.</param>
    /// <param name="useFileSystem">If true get details from the file system if the file exists.</param>
    /// <returns>Returns a <see cref="T:ICSharpCode.SharpZipLib.Zip.ZipEntry">file entry</see> based on the <paramref name="fileName" /> passed.</returns>
    ZipEntry MakeFileEntry(string fileName, string entryName, bool useFileSystem);

    /// <summary>
    /// Create a <see cref="T:ICSharpCode.SharpZipLib.Zip.ZipEntry" /> for a directory given its name
    /// </summary>
    /// <param name="directoryName">The name of the directory to create an entry for.</param>
    /// <returns>Returns a <see cref="T:ICSharpCode.SharpZipLib.Zip.ZipEntry">directory entry</see> based on the <paramref name="directoryName" /> passed.</returns>
    ZipEntry MakeDirectoryEntry(string directoryName);

    /// <summary>
    /// Create a <see cref="T:ICSharpCode.SharpZipLib.Zip.ZipEntry" /> for a directory given its name
    /// </summary>
    /// <param name="directoryName">The name of the directory to create an entry for.</param>
    /// <param name="useFileSystem">If true get details from the file system for this directory if it exists.</param>
    /// <returns>Returns a <see cref="T:ICSharpCode.SharpZipLib.Zip.ZipEntry">directory entry</see> based on the <paramref name="directoryName" /> passed.</returns>
    ZipEntry MakeDirectoryEntry(string directoryName, bool useFileSystem);

    /// <summary>
    /// Get/set the <see cref="T:ICSharpCode.SharpZipLib.Core.INameTransform"></see> applicable.
    /// </summary>
    INameTransform NameTransform { get; set; }
  }
}
