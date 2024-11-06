// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Core.ProgressEventArgs
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51802, Culture=neutral, PublicKeyToken=null
// MVID: 6C361F8B-A5CA-49B8-ABC1-A50EE493C003
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.xml

using System;

#nullable disable
namespace ICSharpCode.SharpZipLib.Core
{
  /// <summary>
  /// Event arguments during processing of a single file or directory.
  /// </summary>
  public class ProgressEventArgs : EventArgs
  {
    private string name_;
    private long processed_;
    private long target_;
    private bool continueRunning_ = true;

    /// <summary>
    /// Initialise a new instance of <see cref="T:ICSharpCode.SharpZipLib.Core.ScanEventArgs" />
    /// </summary>
    /// <param name="name">The file or directory name if known.</param>
    /// <param name="processed">The number of bytes processed so far</param>
    /// <param name="target">The total number of bytes to process, 0 if not known</param>
    public ProgressEventArgs(string name, long processed, long target)
    {
      this.name_ = name;
      this.processed_ = processed;
      this.target_ = target;
    }

    /// <summary>The name for this event if known.</summary>
    public string Name => this.name_;

    /// <summary>
    /// Get set a value indicating wether scanning should continue or not.
    /// </summary>
    public bool ContinueRunning
    {
      get => this.continueRunning_;
      set => this.continueRunning_ = value;
    }

    /// <summary>
    /// Get a percentage representing how much of the <see cref="P:ICSharpCode.SharpZipLib.Core.ProgressEventArgs.Target"></see> has been processed
    /// </summary>
    /// <value>0.0 to 100.0 percent; 0 if target is not known.</value>
    public float PercentComplete
    {
      get
      {
        return this.target_ > 0L ? (float) ((double) this.processed_ / (double) this.target_ * 100.0) : 0.0f;
      }
    }

    /// <summary>The number of bytes processed so far</summary>
    public long Processed => this.processed_;

    /// <summary>The number of bytes to process.</summary>
    /// <remarks>Target may be 0 or negative if the value isnt known.</remarks>
    public long Target => this.target_;
  }
}
