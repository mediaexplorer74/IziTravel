// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.VirtualFileSystem.VfsExtensions
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51802, Culture=neutral, PublicKeyToken=null
// MVID: 6C361F8B-A5CA-49B8-ABC1-A50EE493C003
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.xml

using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace ICSharpCode.SharpZipLib.VirtualFileSystem
{
  /// <summary>Extensions for VFS</summary>
  public static class VfsExtensions
  {
    /// <summary>Check if a directory exists</summary>
    public static bool DirectoryExists(this IVirtualFileSystem vfs, string directoryName)
    {
      try
      {
        return vfs.GetDirectoryInfo(directoryName).Exists;
      }
      catch
      {
        return false;
      }
    }

    /// <summary>Check if a file exists</summary>
    public static bool FileExists(this IVirtualFileSystem vfs, string fileName)
    {
      try
      {
        return vfs.GetFileInfo(fileName).Exists;
      }
      catch
      {
        return false;
      }
    }

    /// <summary>List directorires and files</summary>
    public static IEnumerable<string> GetDirectoriesAndFiles(
      this IVirtualFileSystem vfs,
      string directoryName)
    {
      return vfs.GetDirectories(directoryName).Concat<string>(vfs.GetFiles(directoryName));
    }
  }
}
