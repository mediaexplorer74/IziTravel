// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Core.ExtendedPathFilter
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51802, Culture=neutral, PublicKeyToken=null
// MVID: 6C361F8B-A5CA-49B8-ABC1-A50EE493C003
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.xml

using ICSharpCode.SharpZipLib.VirtualFileSystem;
using System;

#nullable disable
namespace ICSharpCode.SharpZipLib.Core
{
  /// <summary>
  /// ExtendedPathFilter filters based on name, file size, and the last write time of the file.
  /// </summary>
  /// <remarks>Provides an example of how to customise filtering.</remarks>
  public class ExtendedPathFilter : PathFilter
  {
    private long minSize_;
    private long maxSize_ = long.MaxValue;
    private DateTime minDate_ = DateTime.MinValue;
    private DateTime maxDate_ = DateTime.MaxValue;

    /// <summary>Initialise a new instance of ExtendedPathFilter.</summary>
    /// <param name="filter">The filter to apply.</param>
    /// <param name="minSize">The minimum file size to include.</param>
    /// <param name="maxSize">The maximum file size to include.</param>
    public ExtendedPathFilter(string filter, long minSize, long maxSize)
      : base(filter)
    {
      this.MinSize = minSize;
      this.MaxSize = maxSize;
    }

    /// <summary>Initialise a new instance of ExtendedPathFilter.</summary>
    /// <param name="filter">The filter to apply.</param>
    /// <param name="minDate">The minimum <see cref="T:System.DateTime" /> to include.</param>
    /// <param name="maxDate">The maximum <see cref="T:System.DateTime" /> to include.</param>
    public ExtendedPathFilter(string filter, DateTime minDate, DateTime maxDate)
      : base(filter)
    {
      this.MinDate = minDate;
      this.MaxDate = maxDate;
    }

    /// <summary>Initialise a new instance of ExtendedPathFilter.</summary>
    /// <param name="filter">The filter to apply.</param>
    /// <param name="minSize">The minimum file size to include.</param>
    /// <param name="maxSize">The maximum file size to include.</param>
    /// <param name="minDate">The minimum <see cref="T:System.DateTime" /> to include.</param>
    /// <param name="maxDate">The maximum <see cref="T:System.DateTime" /> to include.</param>
    public ExtendedPathFilter(
      string filter,
      long minSize,
      long maxSize,
      DateTime minDate,
      DateTime maxDate)
      : base(filter)
    {
      this.MinSize = minSize;
      this.MaxSize = maxSize;
      this.MinDate = minDate;
      this.MaxDate = maxDate;
    }

    /// <summary>Test a filename to see if it matches the filter.</summary>
    /// <param name="name">The filename to test.</param>
    /// <returns>True if the filter matches, false otherwise.</returns>
    /// <exception cref="T:System.IO.FileNotFoundException">The <see paramref="fileName" /> doesnt exist</exception>
    public override bool IsMatch(string name)
    {
      bool flag = base.IsMatch(name);
      if (flag)
      {
        IFileInfo fileInfo = VFS.Current.GetFileInfo(name);
        flag = this.MinSize <= fileInfo.Length && this.MaxSize >= fileInfo.Length && this.MinDate <= fileInfo.LastWriteTime && this.MaxDate >= fileInfo.LastWriteTime;
      }
      return flag;
    }

    /// <summary>
    /// Get/set the minimum size/length for a file that will match this filter.
    /// </summary>
    /// <remarks>The default value is zero.</remarks>
    /// <exception cref="T:System.ArgumentOutOfRangeException">value is less than zero; greater than <see cref="P:ICSharpCode.SharpZipLib.Core.ExtendedPathFilter.MaxSize" /></exception>
    public long MinSize
    {
      get => this.minSize_;
      set
      {
        this.minSize_ = value >= 0L && this.maxSize_ >= value ? value : throw new ArgumentOutOfRangeException(nameof (value));
      }
    }

    /// <summary>
    /// Get/set the maximum size/length for a file that will match this filter.
    /// </summary>
    /// <remarks>The default value is <see cref="F:System.Int64.MaxValue" /></remarks>
    /// <exception cref="T:System.ArgumentOutOfRangeException">value is less than zero or less than <see cref="P:ICSharpCode.SharpZipLib.Core.ExtendedPathFilter.MinSize" /></exception>
    public long MaxSize
    {
      get => this.maxSize_;
      set
      {
        this.maxSize_ = value >= 0L && this.minSize_ <= value ? value : throw new ArgumentOutOfRangeException(nameof (value));
      }
    }

    /// <summary>
    /// Get/set the minimum <see cref="T:System.DateTime" /> value that will match for this filter.
    /// </summary>
    /// <remarks>Files with a LastWrite time less than this value are excluded by the filter.</remarks>
    public DateTime MinDate
    {
      get => this.minDate_;
      set
      {
        this.minDate_ = !(value > this.maxDate_) ? value : throw new ArgumentOutOfRangeException(nameof (value), "Exceeds MaxDate");
      }
    }

    /// <summary>
    /// Get/set the maximum <see cref="T:System.DateTime" /> value that will match for this filter.
    /// </summary>
    /// <remarks>Files with a LastWrite time greater than this value are excluded by the filter.</remarks>
    public DateTime MaxDate
    {
      get => this.maxDate_;
      set
      {
        this.maxDate_ = !(this.minDate_ > value) ? value : throw new ArgumentOutOfRangeException(nameof (value), "Exceeds MinDate");
      }
    }
  }
}
