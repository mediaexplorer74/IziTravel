// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Core.DirectoryEventArgs
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51802, Culture=neutral, PublicKeyToken=null
// MVID: 6C361F8B-A5CA-49B8-ABC1-A50EE493C003
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.xml

#nullable disable
namespace ICSharpCode.SharpZipLib.Core
{
  /// <summary>Event arguments for directories.</summary>
  public class DirectoryEventArgs : ScanEventArgs
  {
    private bool hasMatchingFiles_;

    /// <summary>
    /// Initialize an instance of <see cref="T:ICSharpCode.SharpZipLib.Core.DirectoryEventArgs"></see>.
    /// </summary>
    /// <param name="name">The name for this directory.</param>
    /// <param name="hasMatchingFiles">Flag value indicating if any matching files are contained in this directory.</param>
    public DirectoryEventArgs(string name, bool hasMatchingFiles)
      : base(name)
    {
      this.hasMatchingFiles_ = hasMatchingFiles;
    }

    /// <summary>
    /// Get a value indicating if the directory contains any matching files or not.
    /// </summary>
    public bool HasMatchingFiles => this.hasMatchingFiles_;
  }
}
