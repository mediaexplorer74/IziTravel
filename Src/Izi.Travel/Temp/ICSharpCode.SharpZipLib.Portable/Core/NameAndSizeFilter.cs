// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Core.NameAndSizeFilter
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51802, Culture=neutral, PublicKeyToken=null
// MVID: 6C361F8B-A5CA-49B8-ABC1-A50EE493C003
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.xml

using System;

#nullable disable
namespace ICSharpCode.SharpZipLib.Core
{
  /// <summary>NameAndSizeFilter filters based on name and file size.</summary>
  /// <remarks>A sample showing how filters might be extended.</remarks>
  [Obsolete("Use ExtendedPathFilter instead")]
  public class NameAndSizeFilter : PathFilter
  {
    private long minSize_;
    private long maxSize_ = long.MaxValue;

    /// <summary>Initialise a new instance of NameAndSizeFilter.</summary>
    /// <param name="filter">The filter to apply.</param>
    /// <param name="minSize">The minimum file size to include.</param>
    /// <param name="maxSize">The maximum file size to include.</param>
    public NameAndSizeFilter(string filter, long minSize, long maxSize)
      : base(filter)
    {
      this.MinSize = minSize;
      this.MaxSize = maxSize;
    }

    /// <summary>Test a filename to see if it matches the filter.</summary>
    /// <param name="name">The filename to test.</param>
    /// <returns>True if the filter matches, false otherwise.</returns>
    public override bool IsMatch(string name)
    {
      bool flag = base.IsMatch(name);
      if (flag)
      {
        long length = VFS.Current.GetFileInfo(name).Length;
        flag = this.MinSize <= length && this.MaxSize >= length;
      }
      return flag;
    }

    /// <summary>
    /// Get/set the minimum size for a file that will match this filter.
    /// </summary>
    public long MinSize
    {
      get => this.minSize_;
      set
      {
        this.minSize_ = value >= 0L && this.maxSize_ >= value ? value : throw new ArgumentOutOfRangeException(nameof (value));
      }
    }

    /// <summary>
    /// Get/set the maximum size for a file that will match this filter.
    /// </summary>
    public long MaxSize
    {
      get => this.maxSize_;
      set
      {
        this.maxSize_ = value >= 0L && this.minSize_ <= value ? value : throw new ArgumentOutOfRangeException(nameof (value));
      }
    }
  }
}
