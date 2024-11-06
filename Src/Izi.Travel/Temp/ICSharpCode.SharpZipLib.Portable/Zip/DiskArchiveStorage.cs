// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.DiskArchiveStorage
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51802, Culture=neutral, PublicKeyToken=null
// MVID: 6C361F8B-A5CA-49B8-ABC1-A50EE493C003
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.xml

using ICSharpCode.SharpZipLib.VirtualFileSystem;
using System;
using System.IO;

#nullable disable
namespace ICSharpCode.SharpZipLib.Zip
{
  /// <summary>
  /// An <see cref="T:ICSharpCode.SharpZipLib.Zip.IArchiveStorage" /> implementation suitable for hard disks.
  /// </summary>
  public class DiskArchiveStorage : BaseArchiveStorage
  {
    private Stream temporaryStream_;
    private string fileName_;
    private string temporaryName_;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:ICSharpCode.SharpZipLib.Zip.DiskArchiveStorage" /> class.
    /// </summary>
    /// <param name="file">The file.</param>
    /// <param name="updateMode">The update mode.</param>
    public DiskArchiveStorage(ZipFile file, FileUpdateMode updateMode)
      : base(updateMode)
    {
      this.fileName_ = file.Name != null ? file.Name : throw new ZipException("Cant handle non file archives");
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:ICSharpCode.SharpZipLib.Zip.DiskArchiveStorage" /> class.
    /// </summary>
    /// <param name="file">The file.</param>
    public DiskArchiveStorage(ZipFile file)
      : this(file, FileUpdateMode.Safe)
    {
    }

    /// <summary>
    /// Gets a temporary output <see cref="T:System.IO.Stream" /> for performing updates on.
    /// </summary>
    /// <returns>Returns the temporary output stream.</returns>
    public override Stream GetTemporaryOutput()
    {
      if (this.temporaryName_ != null)
      {
        this.temporaryName_ = DiskArchiveStorage.GetTempFileName(this.temporaryName_, true);
        this.temporaryStream_ = (Stream) VFS.Current.CreateFile(this.temporaryName_);
      }
      else
      {
        this.temporaryName_ = VFS.Current.GetTempFileName();
        this.temporaryStream_ = (Stream) VFS.Current.CreateFile(this.temporaryName_);
      }
      return this.temporaryStream_;
    }

    /// <summary>
    /// Converts a temporary <see cref="T:System.IO.Stream" /> to its final form.
    /// </summary>
    /// <returns>Returns a <see cref="T:System.IO.Stream" /> that can be used to read
    /// the final storage for the archive.</returns>
    public override Stream ConvertTemporaryToFinal()
    {
      if (this.temporaryStream_ == null)
        throw new ZipException("No temporary stream has been created");
      Stream stream = (Stream) null;
      string tempFileName = DiskArchiveStorage.GetTempFileName(this.fileName_, false);
      bool flag = false;
      try
      {
        this.temporaryStream_.Dispose();
        VFS.Current.MoveFile(this.fileName_, tempFileName);
        VFS.Current.MoveFile(this.temporaryName_, this.fileName_);
        flag = true;
        VFS.Current.DeleteFile(tempFileName);
        return (Stream) VFS.Current.OpenReadFile(this.fileName_);
      }
      catch (Exception ex)
      {
        stream = (Stream) null;
        if (!flag)
        {
          VFS.Current.MoveFile(tempFileName, this.fileName_);
          VFS.Current.DeleteFile(this.temporaryName_);
        }
        throw;
      }
    }

    /// <summary>Make a temporary copy of a stream.</summary>
    /// <param name="stream">The <see cref="T:System.IO.Stream" /> to copy.</param>
    /// <returns>Returns a temporary output <see cref="T:System.IO.Stream" /> that is a copy of the input.</returns>
    public override Stream MakeTemporaryCopy(Stream stream)
    {
      stream.Dispose();
      this.temporaryName_ = DiskArchiveStorage.GetTempFileName(this.fileName_, true);
      VFS.Current.CopyFile(this.fileName_, this.temporaryName_, true);
      this.temporaryStream_ = (Stream) VFS.Current.OpenReadFile(this.temporaryName_);
      return this.temporaryStream_;
    }

    /// <summary>
    /// Return a stream suitable for performing direct updates on the original source.
    /// </summary>
    /// <param name="stream">The current stream.</param>
    /// <returns>Returns a stream suitable for direct updating.</returns>
    /// <remarks>If the <paramref name="stream" /> stream is not null this is used as is.</remarks>
    public override Stream OpenForDirectUpdate(Stream stream)
    {
      Stream stream1;
      if (stream == null || !stream.CanWrite)
      {
        stream?.Dispose();
        stream1 = (Stream) VFS.Current.OpenReadFile(this.fileName_);
      }
      else
        stream1 = stream;
      return stream1;
    }

    /// <summary>Disposes this instance.</summary>
    public override void Dispose()
    {
      if (this.temporaryStream_ == null)
        return;
      this.temporaryStream_.Dispose();
    }

    private static string GetTempFileName(string original, bool makeTempFile)
    {
      string tempFileName = (string) null;
      if (original == null)
      {
        tempFileName = VFS.Current.GetTempFileName();
      }
      else
      {
        int num = 0;
        int second = DateTime.Now.Second;
        while (tempFileName == null)
        {
          ++num;
          string str = string.Format("{0}.{1}{2}.tmp", (object) original, (object) second, (object) num);
          if (!VFS.Current.FileExists(str))
          {
            if (makeTempFile)
            {
              try
              {
                using ((Stream) VFS.Current.CreateFile(str))
                  ;
                tempFileName = str;
              }
              catch
              {
                second = DateTime.Now.Second;
              }
            }
            else
              tempFileName = str;
          }
        }
      }
      return tempFileName;
    }
  }
}
