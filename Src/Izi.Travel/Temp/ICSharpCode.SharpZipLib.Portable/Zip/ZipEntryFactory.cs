// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.ZipEntryFactory
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51802, Culture=neutral, PublicKeyToken=null
// MVID: 6C361F8B-A5CA-49B8-ABC1-A50EE493C003
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.xml

using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.VirtualFileSystem;
using System;

#nullable disable
namespace ICSharpCode.SharpZipLib.Zip
{
  /// <summary>
  /// Basic implementation of <see cref="T:ICSharpCode.SharpZipLib.Zip.IEntryFactory"></see>
  /// </summary>
  public class ZipEntryFactory : IEntryFactory
  {
    private INameTransform nameTransform_;
    private DateTime fixedDateTime_ = DateTime.Now;
    private ZipEntryFactory.TimeSetting timeSetting_;
    private bool isUnicodeText_;
    private int getAttributes_ = -1;
    private int setAttributes_;

    /// <summary>
    /// Initialise a new instance of the <see cref="T:ICSharpCode.SharpZipLib.Zip.ZipEntryFactory" /> class.
    /// </summary>
    /// <remarks>A default <see cref="T:ICSharpCode.SharpZipLib.Core.INameTransform" />, and the LastWriteTime for files is used.</remarks>
    public ZipEntryFactory() => this.nameTransform_ = (INameTransform) new ZipNameTransform();

    /// <summary>
    /// Initialise a new instance of <see cref="T:ICSharpCode.SharpZipLib.Zip.ZipEntryFactory" /> using the specified <see cref="T:ICSharpCode.SharpZipLib.Zip.ZipEntryFactory.TimeSetting" />
    /// </summary>
    /// <param name="timeSetting">The <see cref="T:ICSharpCode.SharpZipLib.Zip.ZipEntryFactory.TimeSetting">time setting</see> to use when creating <see cref="T:ICSharpCode.SharpZipLib.Zip.ZipEntry">Zip entries</see>.</param>
    public ZipEntryFactory(ZipEntryFactory.TimeSetting timeSetting)
    {
      this.timeSetting_ = timeSetting;
      this.nameTransform_ = (INameTransform) new ZipNameTransform();
    }

    /// <summary>
    /// Initialise a new instance of <see cref="T:ICSharpCode.SharpZipLib.Zip.ZipEntryFactory" /> using the specified <see cref="T:System.DateTime" />
    /// </summary>
    /// <param name="time">The time to set all <see cref="P:ICSharpCode.SharpZipLib.Zip.ZipEntry.DateTime" /> values to.</param>
    public ZipEntryFactory(DateTime time)
    {
      this.timeSetting_ = ZipEntryFactory.TimeSetting.Fixed;
      this.FixedDateTime = time;
      this.nameTransform_ = (INameTransform) new ZipNameTransform();
    }

    /// <summary>
    /// Get / set the <see cref="T:ICSharpCode.SharpZipLib.Core.INameTransform" /> to be used when creating new <see cref="T:ICSharpCode.SharpZipLib.Zip.ZipEntry" /> values.
    /// </summary>
    /// <remarks>
    /// Setting this property to null will cause a default <see cref="T:ICSharpCode.SharpZipLib.Zip.ZipNameTransform">name transform</see> to be used.
    /// </remarks>
    public INameTransform NameTransform
    {
      get => this.nameTransform_;
      set
      {
        if (value == null)
          this.nameTransform_ = (INameTransform) new ZipNameTransform();
        else
          this.nameTransform_ = value;
      }
    }

    /// <summary>
    /// Get / set the <see cref="T:ICSharpCode.SharpZipLib.Zip.ZipEntryFactory.TimeSetting" /> in use.
    /// </summary>
    public ZipEntryFactory.TimeSetting Setting
    {
      get => this.timeSetting_;
      set => this.timeSetting_ = value;
    }

    /// <summary>
    /// Get / set the <see cref="T:System.DateTime" /> value to use when <see cref="P:ICSharpCode.SharpZipLib.Zip.ZipEntryFactory.Setting" /> is set to <see cref="F:ICSharpCode.SharpZipLib.Zip.ZipEntryFactory.TimeSetting.Fixed" />
    /// </summary>
    public DateTime FixedDateTime
    {
      get => this.fixedDateTime_;
      set
      {
        this.fixedDateTime_ = value.Year >= 1970 ? value : throw new ArgumentException("Value is too old to be valid", nameof (value));
      }
    }

    /// <summary>
    /// A bitmask defining the attributes to be retrieved from the actual file.
    /// </summary>
    /// <remarks>The default is to get all possible attributes from the actual file.</remarks>
    public int GetAttributes
    {
      get => this.getAttributes_;
      set => this.getAttributes_ = value;
    }

    /// <summary>A bitmask defining which attributes are to be set on.</summary>
    /// <remarks>By default no attributes are set on.</remarks>
    public int SetAttributes
    {
      get => this.setAttributes_;
      set => this.setAttributes_ = value;
    }

    /// <summary>
    /// Get set a value indicating wether unidoce text should be set on.
    /// </summary>
    public bool IsUnicodeText
    {
      get => this.isUnicodeText_;
      set => this.isUnicodeText_ = value;
    }

    /// <summary>
    /// Make a new <see cref="T:ICSharpCode.SharpZipLib.Zip.ZipEntry" /> for a file.
    /// </summary>
    /// <param name="fileName">The name of the file to create a new entry for.</param>
    /// <returns>Returns a new <see cref="T:ICSharpCode.SharpZipLib.Zip.ZipEntry" /> based on the <paramref name="fileName" />.</returns>
    public ZipEntry MakeFileEntry(string fileName)
    {
      return this.MakeFileEntry(fileName, (string) null, true);
    }

    /// <summary>
    /// Make a new <see cref="T:ICSharpCode.SharpZipLib.Zip.ZipEntry" /> for a file.
    /// </summary>
    /// <param name="fileName">The name of the file to create a new entry for.</param>
    /// <param name="useFileSystem">If true entry detail is retrieved from the file system if the file exists.</param>
    /// <returns>Returns a new <see cref="T:ICSharpCode.SharpZipLib.Zip.ZipEntry" /> based on the <paramref name="fileName" />.</returns>
    public ZipEntry MakeFileEntry(string fileName, bool useFileSystem)
    {
      return this.MakeFileEntry(fileName, (string) null, useFileSystem);
    }

    /// <summary>
    /// Make a new <see cref="T:ICSharpCode.SharpZipLib.Zip.ZipEntry" /> from a name.
    /// </summary>
    /// <param name="fileName">The name of the file to create a new entry for.</param>
    /// <param name="entryName">An alternative name to be used for the new entry. Null if not applicable.</param>
    /// <param name="useFileSystem">If true entry detail is retrieved from the file system if the file exists.</param>
    /// <returns>Returns a new <see cref="T:ICSharpCode.SharpZipLib.Zip.ZipEntry" /> based on the <paramref name="fileName" />.</returns>
    public ZipEntry MakeFileEntry(string fileName, string entryName, bool useFileSystem)
    {
      ZipEntry zipEntry = new ZipEntry(this.nameTransform_.TransformFile(entryName == null || entryName.Length <= 0 ? fileName : entryName));
      zipEntry.IsUnicodeText = this.isUnicodeText_;
      int num1 = 0;
      bool flag = this.setAttributes_ != 0;
      IFileInfo fileInfo = (IFileInfo) null;
      if (useFileSystem)
        fileInfo = VFS.Current.GetFileInfo(fileName);
      if (fileInfo != null && fileInfo.Exists)
      {
        switch (this.timeSetting_)
        {
          case ZipEntryFactory.TimeSetting.LastWriteTime:
            zipEntry.DateTime = fileInfo.LastWriteTime;
            break;
          case ZipEntryFactory.TimeSetting.LastWriteTimeUtc:
            zipEntry.DateTime = fileInfo.LastWriteTime.ToUniversalTime();
            break;
          case ZipEntryFactory.TimeSetting.CreateTime:
            zipEntry.DateTime = fileInfo.CreationTime;
            break;
          case ZipEntryFactory.TimeSetting.CreateTimeUtc:
            zipEntry.DateTime = fileInfo.CreationTime.ToUniversalTime();
            break;
          case ZipEntryFactory.TimeSetting.LastAccessTime:
            zipEntry.DateTime = fileInfo.LastAccessTime;
            break;
          case ZipEntryFactory.TimeSetting.LastAccessTimeUtc:
            zipEntry.DateTime = fileInfo.LastAccessTime.ToUniversalTime();
            break;
          case ZipEntryFactory.TimeSetting.Fixed:
            zipEntry.DateTime = this.fixedDateTime_;
            break;
          default:
            throw new ZipException("Unhandled time setting in MakeFileEntry");
        }
        zipEntry.Size = fileInfo.Length;
        flag = true;
        num1 = (int) (fileInfo.Attributes & (FileAttributes) this.getAttributes_);
      }
      else if (this.timeSetting_ == ZipEntryFactory.TimeSetting.Fixed)
        zipEntry.DateTime = this.fixedDateTime_;
      if (flag)
      {
        int num2 = num1 | this.setAttributes_;
        zipEntry.ExternalFileAttributes = num2;
      }
      return zipEntry;
    }

    /// <summary>
    /// Make a new <see cref="T:ICSharpCode.SharpZipLib.Zip.ZipEntry"></see> for a directory.
    /// </summary>
    /// <param name="directoryName">The raw untransformed name for the new directory</param>
    /// <returns>Returns a new <see cref="T:ICSharpCode.SharpZipLib.Zip.ZipEntry"></see> representing a directory.</returns>
    public ZipEntry MakeDirectoryEntry(string directoryName)
    {
      return this.MakeDirectoryEntry(directoryName, true);
    }

    /// <summary>
    /// Make a new <see cref="T:ICSharpCode.SharpZipLib.Zip.ZipEntry"></see> for a directory.
    /// </summary>
    /// <param name="directoryName">The raw untransformed name for the new directory</param>
    /// <param name="useFileSystem">If true entry detail is retrieved from the file system if the file exists.</param>
    /// <returns>Returns a new <see cref="T:ICSharpCode.SharpZipLib.Zip.ZipEntry"></see> representing a directory.</returns>
    public ZipEntry MakeDirectoryEntry(string directoryName, bool useFileSystem)
    {
      ZipEntry zipEntry = new ZipEntry(this.nameTransform_.TransformDirectory(directoryName));
      zipEntry.IsUnicodeText = this.isUnicodeText_;
      zipEntry.Size = 0L;
      int num1 = 0;
      IDirectoryInfo directoryInfo = (IDirectoryInfo) null;
      if (useFileSystem)
        directoryInfo = VFS.Current.GetDirectoryInfo(directoryName);
      if (directoryInfo != null && directoryInfo.Exists)
      {
        switch (this.timeSetting_)
        {
          case ZipEntryFactory.TimeSetting.LastWriteTime:
            zipEntry.DateTime = directoryInfo.LastWriteTime;
            break;
          case ZipEntryFactory.TimeSetting.LastWriteTimeUtc:
            zipEntry.DateTime = directoryInfo.LastWriteTime.ToUniversalTime();
            break;
          case ZipEntryFactory.TimeSetting.CreateTime:
            zipEntry.DateTime = directoryInfo.CreationTime;
            break;
          case ZipEntryFactory.TimeSetting.CreateTimeUtc:
            zipEntry.DateTime = directoryInfo.CreationTime.ToUniversalTime();
            break;
          case ZipEntryFactory.TimeSetting.LastAccessTime:
            zipEntry.DateTime = directoryInfo.LastAccessTime;
            break;
          case ZipEntryFactory.TimeSetting.LastAccessTimeUtc:
            zipEntry.DateTime = directoryInfo.LastAccessTime.ToUniversalTime();
            break;
          case ZipEntryFactory.TimeSetting.Fixed:
            zipEntry.DateTime = this.fixedDateTime_;
            break;
          default:
            throw new ZipException("Unhandled time setting in MakeDirectoryEntry");
        }
        num1 = (int) (directoryInfo.Attributes & (FileAttributes) this.getAttributes_);
      }
      else if (this.timeSetting_ == ZipEntryFactory.TimeSetting.Fixed)
        zipEntry.DateTime = this.fixedDateTime_;
      int num2 = num1 | this.setAttributes_ | 16;
      zipEntry.ExternalFileAttributes = num2;
      return zipEntry;
    }

    /// <summary>
    /// Defines the possible values to be used for the <see cref="P:ICSharpCode.SharpZipLib.Zip.ZipEntry.DateTime" />.
    /// </summary>
    public enum TimeSetting
    {
      /// <summary>Use the recorded LastWriteTime value for the file.</summary>
      LastWriteTime,
      /// <summary>Use the recorded LastWriteTimeUtc value for the file</summary>
      LastWriteTimeUtc,
      /// <summary>Use the recorded CreateTime value for the file.</summary>
      CreateTime,
      /// <summary>Use the recorded CreateTimeUtc value for the file.</summary>
      CreateTimeUtc,
      /// <summary>Use the recorded LastAccessTime value for the file.</summary>
      LastAccessTime,
      /// <summary>Use the recorded LastAccessTimeUtc value for the file.</summary>
      LastAccessTimeUtc,
      /// <summary>Use a fixed value.</summary>
      /// <remarks>The actual <see cref="T:System.DateTime" /> value used can be
      /// specified via the <see cref="M:ICSharpCode.SharpZipLib.Zip.ZipEntryFactory.#ctor(System.DateTime)" /> constructor or
      /// using the <see cref="M:ICSharpCode.SharpZipLib.Zip.ZipEntryFactory.#ctor(ICSharpCode.SharpZipLib.Zip.ZipEntryFactory.TimeSetting)" /> with the setting set
      /// to <see cref="F:ICSharpCode.SharpZipLib.Zip.ZipEntryFactory.TimeSetting.Fixed" /> which will use the <see cref="T:System.DateTime" /> when this class was constructed.
      /// The <see cref="P:ICSharpCode.SharpZipLib.Zip.ZipEntryFactory.FixedDateTime" /> property can also be used to set this value.</remarks>
      Fixed,
    }
  }
}
