// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.TestStatus
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51802, Culture=neutral, PublicKeyToken=null
// MVID: 6C361F8B-A5CA-49B8-ABC1-A50EE493C003
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.xml

#nullable disable
namespace ICSharpCode.SharpZipLib.Zip
{
  /// <summary>
  /// Status returned returned by <see cref="T:ICSharpCode.SharpZipLib.Zip.ZipTestResultHandler" /> during testing.
  /// </summary>
  /// <seealso cref="M:ICSharpCode.SharpZipLib.Zip.ZipFile.TestArchive(System.Boolean)">TestArchive</seealso>
  public class TestStatus
  {
    private ZipFile file_;
    private ZipEntry entry_;
    private bool entryValid_;
    private int errorCount_;
    private long bytesTested_;
    private TestOperation operation_;

    /// <summary>
    /// Initialise a new instance of <see cref="T:ICSharpCode.SharpZipLib.Zip.TestStatus" />
    /// </summary>
    /// <param name="file">The <see cref="T:ICSharpCode.SharpZipLib.Zip.ZipFile" /> this status applies to.</param>
    public TestStatus(ZipFile file) => this.file_ = file;

    /// <summary>
    /// Get the current <see cref="T:ICSharpCode.SharpZipLib.Zip.TestOperation" /> in progress.
    /// </summary>
    public TestOperation Operation => this.operation_;

    /// <summary>
    /// Get the <see cref="T:ICSharpCode.SharpZipLib.Zip.ZipFile" /> this status is applicable to.
    /// </summary>
    public ZipFile File => this.file_;

    /// <summary>Get the current/last entry tested.</summary>
    public ZipEntry Entry => this.entry_;

    /// <summary>Get the number of errors detected so far.</summary>
    public int ErrorCount => this.errorCount_;

    /// <summary>
    /// Get the number of bytes tested so far for the current entry.
    /// </summary>
    public long BytesTested => this.bytesTested_;

    /// <summary>
    /// Get a value indicating wether the last entry test was valid.
    /// </summary>
    public bool EntryValid => this.entryValid_;

    internal void AddError()
    {
      ++this.errorCount_;
      this.entryValid_ = false;
    }

    internal void SetOperation(TestOperation operation) => this.operation_ = operation;

    internal void SetEntry(ZipEntry entry)
    {
      this.entry_ = entry;
      this.entryValid_ = true;
      this.bytesTested_ = 0L;
    }

    internal void SetBytesTested(long value) => this.bytesTested_ = value;
  }
}
