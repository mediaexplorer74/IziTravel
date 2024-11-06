// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.FastZipEvents
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51802, Culture=neutral, PublicKeyToken=null
// MVID: 6C361F8B-A5CA-49B8-ABC1-A50EE493C003
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.xml

using ICSharpCode.SharpZipLib.Core;
using System;

#nullable disable
namespace ICSharpCode.SharpZipLib.Zip
{
  /// <summary>
  /// FastZipEvents supports all events applicable to <see cref="T:ICSharpCode.SharpZipLib.Zip.FastZip">FastZip</see> operations.
  /// </summary>
  public class FastZipEvents
  {
    /// <summary>Delegate to invoke when processing directories.</summary>
    public ProcessDirectoryHandler ProcessDirectory;
    /// <summary>Delegate to invoke when processing files.</summary>
    public ProcessFileHandler ProcessFile;
    /// <summary>Delegate to invoke during processing of files.</summary>
    public ProgressHandler Progress;
    /// <summary>
    /// Delegate to invoke when processing for a file has been completed.
    /// </summary>
    public CompletedFileHandler CompletedFile;
    /// <summary>Delegate to invoke when processing directory failures.</summary>
    public DirectoryFailureHandler DirectoryFailure;
    /// <summary>Delegate to invoke when processing file failures.</summary>
    public FileFailureHandler FileFailure;
    private TimeSpan progressInterval_ = TimeSpan.FromSeconds(3.0);

    /// <summary>
    /// Raise the <see cref="F:ICSharpCode.SharpZipLib.Zip.FastZipEvents.DirectoryFailure">directory failure</see> event.
    /// </summary>
    /// <param name="directory">The directory causing the failure.</param>
    /// <param name="e">The exception for this event.</param>
    /// <returns>A boolean indicating if execution should continue or not.</returns>
    public bool OnDirectoryFailure(string directory, Exception e)
    {
      bool flag = false;
      DirectoryFailureHandler directoryFailure = this.DirectoryFailure;
      if (directoryFailure != null)
      {
        ScanFailureEventArgs e1 = new ScanFailureEventArgs(directory, e);
        directoryFailure((object) this, e1);
        flag = e1.ContinueRunning;
      }
      return flag;
    }

    /// <summary>
    /// Fires the <see cref="F:ICSharpCode.SharpZipLib.Zip.FastZipEvents.FileFailure"> file failure handler delegate</see>.
    /// </summary>
    /// <param name="file">The file causing the failure.</param>
    /// <param name="e">The exception for this failure.</param>
    /// <returns>A boolean indicating if execution should continue or not.</returns>
    public bool OnFileFailure(string file, Exception e)
    {
      FileFailureHandler fileFailure = this.FileFailure;
      bool flag = fileFailure != null;
      if (flag)
      {
        ScanFailureEventArgs e1 = new ScanFailureEventArgs(file, e);
        fileFailure((object) this, e1);
        flag = e1.ContinueRunning;
      }
      return flag;
    }

    /// <summary>
    /// Fires the <see cref="F:ICSharpCode.SharpZipLib.Zip.FastZipEvents.ProcessFile">ProcessFile delegate</see>.
    /// </summary>
    /// <param name="file">The file being processed.</param>
    /// <returns>A boolean indicating if execution should continue or not.</returns>
    public bool OnProcessFile(string file)
    {
      bool flag = true;
      ProcessFileHandler processFile = this.ProcessFile;
      if (processFile != null)
      {
        ScanEventArgs e = new ScanEventArgs(file);
        processFile((object) this, e);
        flag = e.ContinueRunning;
      }
      return flag;
    }

    /// <summary>
    /// Fires the <see cref="F:ICSharpCode.SharpZipLib.Zip.FastZipEvents.CompletedFile" /> delegate
    /// </summary>
    /// <param name="file">The file whose processing has been completed.</param>
    /// <returns>A boolean indicating if execution should continue or not.</returns>
    public bool OnCompletedFile(string file)
    {
      bool flag = true;
      CompletedFileHandler completedFile = this.CompletedFile;
      if (completedFile != null)
      {
        ScanEventArgs e = new ScanEventArgs(file);
        completedFile((object) this, e);
        flag = e.ContinueRunning;
      }
      return flag;
    }

    /// <summary>
    /// Fires the <see cref="F:ICSharpCode.SharpZipLib.Zip.FastZipEvents.ProcessDirectory">process directory</see> delegate.
    /// </summary>
    /// <param name="directory">The directory being processed.</param>
    /// <param name="hasMatchingFiles">Flag indicating if the directory has matching files as determined by the current filter.</param>
    /// <returns>A <see cref="T:System.Boolean" /> of true if the operation should continue; false otherwise.</returns>
    public bool OnProcessDirectory(string directory, bool hasMatchingFiles)
    {
      bool flag = true;
      ProcessDirectoryHandler processDirectory = this.ProcessDirectory;
      if (processDirectory != null)
      {
        DirectoryEventArgs e = new DirectoryEventArgs(directory, hasMatchingFiles);
        processDirectory((object) this, e);
        flag = e.ContinueRunning;
      }
      return flag;
    }

    /// <summary>
    /// The minimum timespan between <see cref="F:ICSharpCode.SharpZipLib.Zip.FastZipEvents.Progress" /> events.
    /// </summary>
    /// <value>The minimum period of time between <see cref="F:ICSharpCode.SharpZipLib.Zip.FastZipEvents.Progress" /> events.</value>
    /// <seealso cref="F:ICSharpCode.SharpZipLib.Zip.FastZipEvents.Progress" />
    /// <remarks>The default interval is three seconds.</remarks>
    public TimeSpan ProgressInterval
    {
      get => this.progressInterval_;
      set => this.progressInterval_ = value;
    }
  }
}
