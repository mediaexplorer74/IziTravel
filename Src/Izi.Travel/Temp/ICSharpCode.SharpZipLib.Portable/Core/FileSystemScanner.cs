// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Core.FileSystemScanner
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51802, Culture=neutral, PublicKeyToken=null
// MVID: 6C361F8B-A5CA-49B8-ABC1-A50EE493C003
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.xml

using System;
using System.Linq;

#nullable disable
namespace ICSharpCode.SharpZipLib.Core
{
  /// <summary>
  /// FileSystemScanner provides facilities scanning of files and directories.
  /// </summary>
  public class FileSystemScanner
  {
    /// <summary>Delegate to invoke when a directory is processed.</summary>
    public ProcessDirectoryHandler ProcessDirectory;
    /// <summary>Delegate to invoke when a file is processed.</summary>
    public ProcessFileHandler ProcessFile;
    /// <summary>
    /// Delegate to invoke when processing for a file has finished.
    /// </summary>
    public CompletedFileHandler CompletedFile;
    /// <summary>
    /// Delegate to invoke when a directory failure is detected.
    /// </summary>
    public DirectoryFailureHandler DirectoryFailure;
    /// <summary>Delegate to invoke when a file failure is detected.</summary>
    public FileFailureHandler FileFailure;
    /// <summary>The file filter currently in use.</summary>
    private IScanFilter fileFilter_;
    /// <summary>The directory filter currently in use.</summary>
    private IScanFilter directoryFilter_;
    /// <summary>Flag indicating if scanning should continue running.</summary>
    private bool alive_;

    /// <summary>
    /// Initialise a new instance of <see cref="T:ICSharpCode.SharpZipLib.Core.FileSystemScanner"></see>
    /// </summary>
    /// <param name="filter">The <see cref="T:ICSharpCode.SharpZipLib.Core.PathFilter">file filter</see> to apply when scanning.</param>
    public FileSystemScanner(string filter)
    {
      this.fileFilter_ = (IScanFilter) new PathFilter(filter);
    }

    /// <summary>
    /// Initialise a new instance of <see cref="T:ICSharpCode.SharpZipLib.Core.FileSystemScanner"></see>
    /// </summary>
    /// <param name="fileFilter">The <see cref="T:ICSharpCode.SharpZipLib.Core.PathFilter">file filter</see> to apply.</param>
    /// <param name="directoryFilter">The <see cref="T:ICSharpCode.SharpZipLib.Core.PathFilter"> directory filter</see> to apply.</param>
    public FileSystemScanner(string fileFilter, string directoryFilter)
    {
      this.fileFilter_ = (IScanFilter) new PathFilter(fileFilter);
      this.directoryFilter_ = (IScanFilter) new PathFilter(directoryFilter);
    }

    /// <summary>
    /// Initialise a new instance of <see cref="T:ICSharpCode.SharpZipLib.Core.FileSystemScanner"></see>
    /// </summary>
    /// <param name="fileFilter">The file <see cref="T:ICSharpCode.SharpZipLib.Core.IScanFilter">filter</see> to apply.</param>
    public FileSystemScanner(IScanFilter fileFilter) => this.fileFilter_ = fileFilter;

    /// <summary>
    /// Initialise a new instance of <see cref="T:ICSharpCode.SharpZipLib.Core.FileSystemScanner"></see>
    /// </summary>
    /// <param name="fileFilter">The file <see cref="T:ICSharpCode.SharpZipLib.Core.IScanFilter">filter</see>  to apply.</param>
    /// <param name="directoryFilter">The directory <see cref="T:ICSharpCode.SharpZipLib.Core.IScanFilter">filter</see>  to apply.</param>
    public FileSystemScanner(IScanFilter fileFilter, IScanFilter directoryFilter)
    {
      this.fileFilter_ = fileFilter;
      this.directoryFilter_ = directoryFilter;
    }

    /// <summary>Raise the DirectoryFailure event.</summary>
    /// <param name="directory">The directory name.</param>
    /// <param name="e">The exception detected.</param>
    private bool OnDirectoryFailure(string directory, Exception e)
    {
      DirectoryFailureHandler directoryFailure = this.DirectoryFailure;
      bool flag = directoryFailure != null;
      if (flag)
      {
        ScanFailureEventArgs e1 = new ScanFailureEventArgs(directory, e);
        directoryFailure((object) this, e1);
        this.alive_ = e1.ContinueRunning;
      }
      return flag;
    }

    /// <summary>Raise the FileFailure event.</summary>
    /// <param name="file">The file name.</param>
    /// <param name="e">The exception detected.</param>
    private bool OnFileFailure(string file, Exception e)
    {
      bool flag = this.FileFailure != null;
      if (flag)
      {
        ScanFailureEventArgs e1 = new ScanFailureEventArgs(file, e);
        this.FileFailure((object) this, e1);
        this.alive_ = e1.ContinueRunning;
      }
      return flag;
    }

    /// <summary>Raise the ProcessFile event.</summary>
    /// <param name="file">The file name.</param>
    private void OnProcessFile(string file)
    {
      ProcessFileHandler processFile = this.ProcessFile;
      if (processFile == null)
        return;
      ScanEventArgs e = new ScanEventArgs(file);
      processFile((object) this, e);
      this.alive_ = e.ContinueRunning;
    }

    /// <summary>Raise the complete file event</summary>
    /// <param name="file">The file name</param>
    private void OnCompleteFile(string file)
    {
      CompletedFileHandler completedFile = this.CompletedFile;
      if (completedFile == null)
        return;
      ScanEventArgs e = new ScanEventArgs(file);
      completedFile((object) this, e);
      this.alive_ = e.ContinueRunning;
    }

    /// <summary>Raise the ProcessDirectory event.</summary>
    /// <param name="directory">The directory name.</param>
    /// <param name="hasMatchingFiles">Flag indicating if the directory has matching files.</param>
    private void OnProcessDirectory(string directory, bool hasMatchingFiles)
    {
      ProcessDirectoryHandler processDirectory = this.ProcessDirectory;
      if (processDirectory == null)
        return;
      DirectoryEventArgs e = new DirectoryEventArgs(directory, hasMatchingFiles);
      processDirectory((object) this, e);
      this.alive_ = e.ContinueRunning;
    }

    /// <summary>Scan a directory.</summary>
    /// <param name="directory">The base directory to scan.</param>
    /// <param name="recurse">True to recurse subdirectories, false to scan a single directory.</param>
    public void Scan(string directory, bool recurse)
    {
      this.alive_ = true;
      this.ScanDir(directory, recurse);
    }

    private void ScanDir(string directory, bool recurse)
    {
      try
      {
        string[] array = VFS.Current.GetFiles(directory).ToArray<string>();
        bool hasMatchingFiles = false;
        for (int index = 0; index < array.Length; ++index)
        {
          if (!this.fileFilter_.IsMatch(array[index]))
            array[index] = (string) null;
          else
            hasMatchingFiles = true;
        }
        this.OnProcessDirectory(directory, hasMatchingFiles);
        if (this.alive_)
        {
          if (hasMatchingFiles)
          {
            foreach (string file in array)
            {
              try
              {
                if (file != null)
                {
                  this.OnProcessFile(file);
                  if (!this.alive_)
                    break;
                }
              }
              catch (Exception ex)
              {
                if (!this.OnFileFailure(file, ex))
                  throw;
              }
            }
          }
        }
      }
      catch (Exception ex)
      {
        if (!this.OnDirectoryFailure(directory, ex))
          throw;
      }
      if (!this.alive_)
        return;
      if (!recurse)
        return;
      try
      {
        foreach (string str in VFS.Current.GetDirectories(directory).ToArray<string>())
        {
          if (this.directoryFilter_ == null || this.directoryFilter_.IsMatch(str))
          {
            this.ScanDir(str, true);
            if (!this.alive_)
              break;
          }
        }
      }
      catch (Exception ex)
      {
        if (this.OnDirectoryFailure(directory, ex))
          return;
        throw;
      }
    }
  }
}
