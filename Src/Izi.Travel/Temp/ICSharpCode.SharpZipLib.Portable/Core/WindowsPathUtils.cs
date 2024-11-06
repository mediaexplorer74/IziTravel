// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Core.WindowsPathUtils
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51802, Culture=neutral, PublicKeyToken=null
// MVID: 6C361F8B-A5CA-49B8-ABC1-A50EE493C003
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.xml

#nullable disable
namespace ICSharpCode.SharpZipLib.Core
{
  /// <summary>
  /// WindowsPathUtils provides simple utilities for handling windows paths.
  /// </summary>
  public abstract class WindowsPathUtils
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:ICSharpCode.SharpZipLib.Core.WindowsPathUtils" /> class.
    /// </summary>
    internal WindowsPathUtils()
    {
    }

    /// <summary>Remove any path root present in the path</summary>
    /// <param name="path">A <see cref="T:System.String" /> containing path information.</param>
    /// <returns>The path with the root removed if it was present; path otherwise.</returns>
    /// <remarks>Unlike the <see cref="T:System.IO.Path" /> class the path isnt otherwise checked for validity.</remarks>
    public static string DropPathRoot(string path)
    {
      string str = path;
      if (path != null && path.Length > 0)
      {
        if (path[0] == '\\' || path[0] == '/')
        {
          if (path.Length > 1 && (path[1] == '\\' || path[1] == '/'))
          {
            int index = 2;
            int num = 2;
            while (index <= path.Length && (path[index] != '\\' && path[index] != '/' || --num > 0))
              ++index;
            int startIndex = index + 1;
            str = startIndex >= path.Length ? "" : path.Substring(startIndex);
          }
        }
        else if (path.Length > 1 && path[1] == ':')
        {
          int count = 2;
          if (path.Length > 2 && (path[2] == '\\' || path[2] == '/'))
            count = 3;
          str = str.Remove(0, count);
        }
      }
      return str;
    }
  }
}
