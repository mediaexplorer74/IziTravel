// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Core.PathFilter
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51802, Culture=neutral, PublicKeyToken=null
// MVID: 6C361F8B-A5CA-49B8-ABC1-A50EE493C003
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.xml

#nullable disable
namespace ICSharpCode.SharpZipLib.Core
{
  /// <summary>
  /// PathFilter filters directories and files using a form of <see cref="T:System.Text.RegularExpressions.Regex">regular expressions</see>
  /// by full path name.
  /// See <see cref="T:ICSharpCode.SharpZipLib.Core.NameFilter">NameFilter</see> for more detail on filtering.
  /// </summary>
  public class PathFilter : IScanFilter
  {
    private NameFilter nameFilter_;

    /// <summary>
    /// Initialise a new instance of <see cref="T:ICSharpCode.SharpZipLib.Core.PathFilter"></see>.
    /// </summary>
    /// <param name="filter">The <see cref="T:ICSharpCode.SharpZipLib.Core.NameFilter">filter</see> expression to apply.</param>
    public PathFilter(string filter) => this.nameFilter_ = new NameFilter(filter);

    /// <summary>Test a name to see if it matches the filter.</summary>
    /// <param name="name">The name to test.</param>
    /// <returns>True if the name matches, false otherwise.</returns>
    public virtual bool IsMatch(string name)
    {
      bool flag = false;
      if (name != null)
        flag = this.nameFilter_.IsMatch(name.Length > 0 ? VFS.Current.GetFullPath(name) : "");
      return flag;
    }
  }
}
