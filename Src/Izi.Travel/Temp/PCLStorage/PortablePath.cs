// Decompiled with JetBrains decompiler
// Type: PCLStorage.PortablePath
// Assembly: PCLStorage, Version=1.0.2.0, Culture=neutral, PublicKeyToken=286fe515a2c35b64
// MVID: C962FBF1-A378-45AB-97C6-C265F8F0F86C
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\PCLStorage.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\PCLStorage.xml

using System.Collections.Generic;
using System.IO;
using System.Linq;

#nullable disable
namespace PCLStorage
{
  /// <summary>Provides portable versions of APIs such as Path.Combine</summary>
  public static class PortablePath
  {
    /// <summary>
    /// The character used to separate elements in a file system path
    /// </summary>
    public static char DirectorySeparatorChar => Path.DirectorySeparatorChar;

    /// <summary>Combines multiple strings into a path</summary>
    /// <param name="paths">Path elements to combine</param>
    /// <returns>A combined path</returns>
    public static string Combine(params string[] paths)
    {
      if (paths.Length == 0)
        return string.Empty;
      if (paths.Length == 1)
        return paths[0];
      string path1 = Path.Combine(paths[0], paths[1]);
      foreach (string path2 in ((IEnumerable<string>) paths).Skip<string>(2))
        path1 = Path.Combine(path1, path2);
      return path1;
    }
  }
}
