// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Tar.TarArchive
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51802, Culture=neutral, PublicKeyToken=null
// MVID: 6C361F8B-A5CA-49B8-ABC1-A50EE493C003
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.xml

using ICSharpCode.SharpZipLib.VirtualFileSystem;
using System;
using System.IO;

#nullable disable
namespace ICSharpCode.SharpZipLib.Tar
{
  /// <summary>
  /// The TarArchive class implements the concept of a
  /// 'Tape Archive'. A tar archive is a series of entries, each of
  /// which represents a file system object. Each entry in
  /// the archive consists of a header block followed by 0 or more data blocks.
  /// Directory entries consist only of the header block, and are followed by entries
  /// for the directory's contents. File entries consist of a
  /// header followed by the number of blocks needed to
  /// contain the file's contents. All entries are written on
  /// block boundaries. Blocks are 512 bytes long.
  /// 
  /// TarArchives are instantiated in either read or write mode,
  /// based upon whether they are instantiated with an InputStream
  /// or an OutputStream. Once instantiated TarArchives read/write
  /// mode can not be changed.
  /// 
  /// There is currently no support for random access to tar archives.
  /// However, it seems that subclassing TarArchive, and using the
  /// TarBuffer.CurrentRecord and TarBuffer.CurrentBlock
  /// properties, this would be rather trivial.
  /// </summary>
  public class TarArchive : IDisposable
  {
    private bool keepOldFiles;
    private bool asciiTranslate;
    private int userId;
    private string userName = string.Empty;
    private int groupId;
    private string groupName = string.Empty;
    private string rootPath;
    private string pathPrefix;
    private bool applyUserInfoOverrides;
    private TarInputStream tarIn;
    private TarOutputStream tarOut;
    private bool isDisposed;

    /// <summary>
    /// Client hook allowing detailed information to be reported during processing
    /// </summary>
    public event ProgressMessageHandler ProgressMessageEvent;

    /// <summary>Raises the ProgressMessage event</summary>
    /// <param name="entry">The <see cref="T:ICSharpCode.SharpZipLib.Tar.TarEntry">TarEntry</see> for this event</param>
    /// <param name="message">message for this event.  Null is no message</param>
    protected virtual void OnProgressMessageEvent(TarEntry entry, string message)
    {
      ProgressMessageHandler progressMessageEvent = this.ProgressMessageEvent;
      if (progressMessageEvent == null)
        return;
      progressMessageEvent(this, entry, message);
    }

    /// <summary>
    /// Constructor for a default <see cref="T:ICSharpCode.SharpZipLib.Tar.TarArchive" />.
    /// </summary>
    protected TarArchive()
    {
    }

    /// <summary>Initalise a TarArchive for input.</summary>
    /// <param name="stream">The <see cref="T:ICSharpCode.SharpZipLib.Tar.TarInputStream" /> to use for input.</param>
    protected TarArchive(TarInputStream stream)
    {
      this.tarIn = stream != null ? stream : throw new ArgumentNullException(nameof (stream));
    }

    /// <summary>Initialise a TarArchive for output.</summary>
    /// <param name="stream">The <see cref="T:ICSharpCode.SharpZipLib.Tar.TarOutputStream" /> to use for output.</param>
    protected TarArchive(TarOutputStream stream)
    {
      this.tarOut = stream != null ? stream : throw new ArgumentNullException(nameof (stream));
    }

    /// <summary>
    /// The InputStream based constructors create a TarArchive for the
    /// purposes of extracting or listing a tar archive. Thus, use
    /// these constructors when you wish to extract files from or list
    /// the contents of an existing tar archive.
    /// </summary>
    /// <param name="inputStream">The stream to retrieve archive data from.</param>
    /// <returns>Returns a new <see cref="T:ICSharpCode.SharpZipLib.Tar.TarArchive" /> suitable for reading from.</returns>
    public static TarArchive CreateInputTarArchive(Stream inputStream)
    {
      if (inputStream == null)
        throw new ArgumentNullException(nameof (inputStream));
      return !(inputStream is TarInputStream stream) ? TarArchive.CreateInputTarArchive(inputStream, 20) : new TarArchive(stream);
    }

    /// <summary>Create TarArchive for reading setting block factor</summary>
    /// <param name="inputStream">A stream containing the tar archive contents</param>
    /// <param name="blockFactor">The blocking factor to apply</param>
    /// <returns>Returns a <see cref="T:ICSharpCode.SharpZipLib.Tar.TarArchive" /> suitable for reading.</returns>
    public static TarArchive CreateInputTarArchive(Stream inputStream, int blockFactor)
    {
      if (inputStream == null)
        throw new ArgumentNullException(nameof (inputStream));
      return !(inputStream is TarInputStream) ? new TarArchive(new TarInputStream(inputStream, blockFactor)) : throw new ArgumentException("TarInputStream not valid");
    }

    /// <summary>
    /// Create a TarArchive for writing to, using the default blocking factor
    /// </summary>
    /// <param name="outputStream">The <see cref="T:System.IO.Stream" /> to write to</param>
    /// <returns>Returns a <see cref="T:ICSharpCode.SharpZipLib.Tar.TarArchive" /> suitable for writing.</returns>
    public static TarArchive CreateOutputTarArchive(Stream outputStream)
    {
      if (outputStream == null)
        throw new ArgumentNullException(nameof (outputStream));
      return !(outputStream is TarOutputStream stream) ? TarArchive.CreateOutputTarArchive(outputStream, 20) : new TarArchive(stream);
    }

    /// <summary>
    /// Create a <see cref="T:ICSharpCode.SharpZipLib.Tar.TarArchive">tar archive</see> for writing.
    /// </summary>
    /// <param name="outputStream">The stream to write to</param>
    /// <param name="blockFactor">The blocking factor to use for buffering.</param>
    /// <returns>Returns a <see cref="T:ICSharpCode.SharpZipLib.Tar.TarArchive" /> suitable for writing.</returns>
    public static TarArchive CreateOutputTarArchive(Stream outputStream, int blockFactor)
    {
      if (outputStream == null)
        throw new ArgumentNullException(nameof (outputStream));
      return !(outputStream is TarOutputStream) ? new TarArchive(new TarOutputStream(outputStream, blockFactor)) : throw new ArgumentException("TarOutputStream is not valid");
    }

    /// <summary>
    /// Set the flag that determines whether existing files are
    /// kept, or overwritten during extraction.
    /// </summary>
    /// <param name="keepExistingFiles">
    /// If true, do not overwrite existing files.
    /// </param>
    public void SetKeepOldFiles(bool keepExistingFiles)
    {
      if (this.isDisposed)
        throw new ObjectDisposedException(nameof (TarArchive));
      this.keepOldFiles = keepExistingFiles;
    }

    /// <summary>
    /// Get/set the ascii file translation flag. If ascii file translation
    /// is true, then the file is checked to see if it a binary file or not.
    /// If the flag is true and the test indicates it is ascii text
    /// file, it will be translated. The translation converts the local
    /// operating system's concept of line ends into the UNIX line end,
    /// '\n', which is the defacto standard for a TAR archive. This makes
    /// text files compatible with UNIX.
    /// </summary>
    public bool AsciiTranslate
    {
      get
      {
        if (this.isDisposed)
          throw new ObjectDisposedException(nameof (TarArchive));
        return this.asciiTranslate;
      }
      set
      {
        if (this.isDisposed)
          throw new ObjectDisposedException(nameof (TarArchive));
        this.asciiTranslate = value;
      }
    }

    /// <summary>Set the ascii file translation flag.</summary>
    /// <param name="translateAsciiFiles">
    /// If true, translate ascii text files.
    /// </param>
    [Obsolete("Use the AsciiTranslate property")]
    public void SetAsciiTranslation(bool translateAsciiFiles)
    {
      if (this.isDisposed)
        throw new ObjectDisposedException(nameof (TarArchive));
      this.asciiTranslate = translateAsciiFiles;
    }

    /// <summary>
    /// PathPrefix is added to entry names as they are written if the value is not null.
    /// A slash character is appended after PathPrefix
    /// </summary>
    public string PathPrefix
    {
      get
      {
        if (this.isDisposed)
          throw new ObjectDisposedException(nameof (TarArchive));
        return this.pathPrefix;
      }
      set
      {
        if (this.isDisposed)
          throw new ObjectDisposedException(nameof (TarArchive));
        this.pathPrefix = value;
      }
    }

    /// <summary>
    /// RootPath is removed from entry names if it is found at the
    /// beginning of the name.
    /// </summary>
    public string RootPath
    {
      get
      {
        if (this.isDisposed)
          throw new ObjectDisposedException(nameof (TarArchive));
        return this.rootPath;
      }
      set
      {
        if (this.isDisposed)
          throw new ObjectDisposedException(nameof (TarArchive));
        this.rootPath = value.Replace('\\', '/').TrimEnd('/');
      }
    }

    /// <summary>
    /// Set user and group information that will be used to fill in the
    /// tar archive's entry headers. This information is based on that available
    /// for the linux operating system, which is not always available on other
    /// operating systems.  TarArchive allows the programmer to specify values
    /// to be used in their place.
    /// <see cref="P:ICSharpCode.SharpZipLib.Tar.TarArchive.ApplyUserInfoOverrides" /> is set to true by this call.
    /// </summary>
    /// <param name="userId">The user id to use in the headers.</param>
    /// <param name="userName">The user name to use in the headers.</param>
    /// <param name="groupId">The group id to use in the headers.</param>
    /// <param name="groupName">The group name to use in the headers.</param>
    public void SetUserInfo(int userId, string userName, int groupId, string groupName)
    {
      if (this.isDisposed)
        throw new ObjectDisposedException(nameof (TarArchive));
      this.userId = userId;
      this.userName = userName;
      this.groupId = groupId;
      this.groupName = groupName;
      this.applyUserInfoOverrides = true;
    }

    /// <summary>
    /// Get or set a value indicating if overrides defined by <see cref="M:ICSharpCode.SharpZipLib.Tar.TarArchive.SetUserInfo(System.Int32,System.String,System.Int32,System.String)">SetUserInfo</see> should be applied.
    /// </summary>
    /// <remarks>If overrides are not applied then the values as set in each header will be used.</remarks>
    public bool ApplyUserInfoOverrides
    {
      get
      {
        if (this.isDisposed)
          throw new ObjectDisposedException(nameof (TarArchive));
        return this.applyUserInfoOverrides;
      }
      set
      {
        if (this.isDisposed)
          throw new ObjectDisposedException(nameof (TarArchive));
        this.applyUserInfoOverrides = value;
      }
    }

    /// <summary>
    /// Get the archive user id.
    /// See <see cref="P:ICSharpCode.SharpZipLib.Tar.TarArchive.ApplyUserInfoOverrides">ApplyUserInfoOverrides</see> for detail
    /// on how to allow setting values on a per entry basis.
    /// </summary>
    /// <returns>The current user id.</returns>
    public int UserId
    {
      get
      {
        if (this.isDisposed)
          throw new ObjectDisposedException(nameof (TarArchive));
        return this.userId;
      }
    }

    /// <summary>
    /// Get the archive user name.
    /// See <see cref="P:ICSharpCode.SharpZipLib.Tar.TarArchive.ApplyUserInfoOverrides">ApplyUserInfoOverrides</see> for detail
    /// on how to allow setting values on a per entry basis.
    /// </summary>
    /// <returns>The current user name.</returns>
    public string UserName
    {
      get
      {
        if (this.isDisposed)
          throw new ObjectDisposedException(nameof (TarArchive));
        return this.userName;
      }
    }

    /// <summary>
    /// Get the archive group id.
    /// See <see cref="P:ICSharpCode.SharpZipLib.Tar.TarArchive.ApplyUserInfoOverrides">ApplyUserInfoOverrides</see> for detail
    /// on how to allow setting values on a per entry basis.
    /// </summary>
    /// <returns>The current group id.</returns>
    public int GroupId
    {
      get
      {
        if (this.isDisposed)
          throw new ObjectDisposedException(nameof (TarArchive));
        return this.groupId;
      }
    }

    /// <summary>
    /// Get the archive group name.
    /// See <see cref="P:ICSharpCode.SharpZipLib.Tar.TarArchive.ApplyUserInfoOverrides">ApplyUserInfoOverrides</see> for detail
    /// on how to allow setting values on a per entry basis.
    /// </summary>
    /// <returns>The current group name.</returns>
    public string GroupName
    {
      get
      {
        if (this.isDisposed)
          throw new ObjectDisposedException(nameof (TarArchive));
        return this.groupName;
      }
    }

    /// <summary>
    /// Get the archive's record size. Tar archives are composed of
    /// a series of RECORDS each containing a number of BLOCKS.
    /// This allowed tar archives to match the IO characteristics of
    /// the physical device being used. Archives are expected
    /// to be properly "blocked".
    /// </summary>
    /// <returns>The record size this archive is using.</returns>
    public int RecordSize
    {
      get
      {
        if (this.isDisposed)
          throw new ObjectDisposedException(nameof (TarArchive));
        if (this.tarIn != null)
          return this.tarIn.RecordSize;
        return this.tarOut != null ? this.tarOut.RecordSize : 10240;
      }
    }

    /// <summary>
    /// Sets the IsStreamOwner property on the underlying stream.
    /// Set this to false to prevent the Close of the TarArchive from closing the stream.
    /// </summary>
    public bool IsStreamOwner
    {
      set
      {
        if (this.tarIn != null)
          this.tarIn.IsStreamOwner = value;
        else
          this.tarOut.IsStreamOwner = value;
      }
    }

    /// <summary>Close the archive.</summary>
    [Obsolete("Use Close instead")]
    public void CloseArchive() => this.Close();

    /// <summary>
    /// Perform the "list" command for the archive contents.
    /// 
    /// NOTE That this method uses the <see cref="E:ICSharpCode.SharpZipLib.Tar.TarArchive.ProgressMessageEvent"> progress event</see> to actually list
    /// the contents. If the progress display event is not set, nothing will be listed!
    /// </summary>
    public void ListContents()
    {
      if (this.isDisposed)
        throw new ObjectDisposedException(nameof (TarArchive));
      while (true)
      {
        TarEntry nextEntry = this.tarIn.GetNextEntry();
        if (nextEntry != null)
          this.OnProgressMessageEvent(nextEntry, (string) null);
        else
          break;
      }
    }

    /// <summary>
    /// Perform the "extract" command and extract the contents of the archive.
    /// </summary>
    /// <param name="destinationDirectory">
    /// The destination directory into which to extract.
    /// </param>
    public void ExtractContents(string destinationDirectory)
    {
      if (this.isDisposed)
        throw new ObjectDisposedException(nameof (TarArchive));
      while (true)
      {
        TarEntry nextEntry = this.tarIn.GetNextEntry();
        if (nextEntry != null)
          this.ExtractEntry(destinationDirectory, nextEntry);
        else
          break;
      }
    }

    /// <summary>
    /// Extract an entry from the archive. This method assumes that the
    /// tarIn stream has been properly set with a call to GetNextEntry().
    /// </summary>
    /// <param name="destDir">
    /// The destination directory into which to extract.
    /// </param>
    /// <param name="entry">The TarEntry returned by tarIn.GetNextEntry().</param>
    private void ExtractEntry(string destDir, TarEntry entry)
    {
      this.OnProgressMessageEvent(entry, (string) null);
      string path = entry.Name;
      if (Path.IsPathRooted(path))
        path = path.Substring(Path.GetPathRoot(path).Length);
      string str1 = path.Replace('/', VFS.Current.DirectorySeparatorChar);
      string str2 = Path.Combine(destDir, str1);
      if (entry.IsDirectory)
      {
        TarArchive.EnsureDirectoryExists(str2);
      }
      else
      {
        TarArchive.EnsureDirectoryExists(Path.GetDirectoryName(str2));
        bool flag1 = true;
        IFileInfo fileInfo = VFS.Current.GetFileInfo(str2);
        if (fileInfo.Exists)
        {
          if (this.keepOldFiles)
          {
            this.OnProgressMessageEvent(entry, "Destination file already exists");
            flag1 = false;
          }
          else if ((fileInfo.Attributes & FileAttributes.ReadOnly) != (FileAttributes) 0)
          {
            this.OnProgressMessageEvent(entry, "Destination file already exists, and is read-only");
            flag1 = false;
          }
        }
        if (!flag1)
          return;
        bool flag2 = false;
        Stream file = (Stream) VFS.Current.CreateFile(str2);
        if (this.asciiTranslate)
          flag2 = !TarArchive.IsBinary(str2);
        StreamWriter streamWriter = (StreamWriter) null;
        if (flag2)
          streamWriter = new StreamWriter(file);
        byte[] numArray = new byte[32768];
label_15:
        int count;
        while (true)
        {
          count = this.tarIn.Read(numArray, 0, numArray.Length);
          if (count > 0)
          {
            if (!flag2)
              file.Write(numArray, 0, count);
            else
              break;
          }
          else
            goto label_24;
        }
        int index1 = 0;
        for (int index2 = 0; index2 < count; ++index2)
        {
          if (numArray[index2] == (byte) 10)
          {
            string str3 = AsciiEncoding.Default.GetString(numArray, index1, index2 - index1);
            streamWriter.WriteLine(str3);
            index1 = index2 + 1;
          }
        }
        goto label_15;
label_24:
        if (flag2)
          streamWriter.Dispose();
        else
          file.Dispose();
      }
    }

    /// <summary>
    /// Write an entry to the archive. This method will call the putNextEntry
    /// and then write the contents of the entry, and finally call closeEntry()
    /// for entries that are files. For directories, it will call putNextEntry(),
    /// and then, if the recurse flag is true, process each entry that is a
    /// child of the directory.
    /// </summary>
    /// <param name="sourceEntry">
    /// The TarEntry representing the entry to write to the archive.
    /// </param>
    /// <param name="recurse">
    /// If true, process the children of directory entries.
    /// </param>
    public void WriteEntry(TarEntry sourceEntry, bool recurse)
    {
      if (sourceEntry == null)
        throw new ArgumentNullException(nameof (sourceEntry));
      if (this.isDisposed)
        throw new ObjectDisposedException(nameof (TarArchive));
      try
      {
        if (recurse)
          TarHeader.SetValueDefaults(sourceEntry.UserId, sourceEntry.UserName, sourceEntry.GroupId, sourceEntry.GroupName);
        this.WriteEntryCore(sourceEntry, recurse);
      }
      finally
      {
        if (recurse)
          TarHeader.RestoreSetValues();
      }
    }

    /// <summary>
    /// Write an entry to the archive. This method will call the putNextEntry
    /// and then write the contents of the entry, and finally call closeEntry()
    /// for entries that are files. For directories, it will call putNextEntry(),
    /// and then, if the recurse flag is true, process each entry that is a
    /// child of the directory.
    /// </summary>
    /// <param name="sourceEntry">
    /// The TarEntry representing the entry to write to the archive.
    /// </param>
    /// <param name="recurse">
    /// If true, process the children of directory entries.
    /// </param>
    private void WriteEntryCore(TarEntry sourceEntry, bool recurse)
    {
      string str1 = (string) null;
      string filename = sourceEntry.File;
      TarEntry entry = (TarEntry) sourceEntry.Clone();
      if (this.applyUserInfoOverrides)
      {
        entry.GroupId = this.groupId;
        entry.GroupName = this.groupName;
        entry.UserId = this.userId;
        entry.UserName = this.userName;
      }
      this.OnProgressMessageEvent(entry, (string) null);
      if (this.asciiTranslate && !entry.IsDirectory && !TarArchive.IsBinary(filename))
      {
        str1 = VFS.Current.GetTempFileName();
        using (StreamReader streamReader = new StreamReader((Stream) VFS.Current.OpenReadFile(filename)))
        {
          using (Stream file = (Stream) VFS.Current.CreateFile(str1))
          {
            while (true)
            {
              string s = streamReader.ReadLine();
              if (s != null)
              {
                byte[] bytes = AsciiEncoding.Default.GetBytes(s);
                file.Write(bytes, 0, bytes.Length);
                file.WriteByte((byte) 10);
              }
              else
                break;
            }
            file.Flush();
          }
        }
        entry.Size = VFS.Current.GetFileInfo(str1).Length;
        filename = str1;
      }
      string str2 = (string) null;
      if (this.rootPath != null && entry.Name.StartsWith(this.rootPath, StringComparison.OrdinalIgnoreCase))
        str2 = entry.Name.Substring(this.rootPath.Length + 1);
      if (this.pathPrefix != null)
        str2 = str2 == null ? this.pathPrefix + "/" + entry.Name : this.pathPrefix + "/" + str2;
      if (str2 != null)
        entry.Name = str2;
      this.tarOut.PutNextEntry(entry);
      if (entry.IsDirectory)
      {
        if (!recurse)
          return;
        foreach (TarEntry directoryEntry in entry.GetDirectoryEntries())
          this.WriteEntryCore(directoryEntry, recurse);
      }
      else
      {
        using (Stream stream = (Stream) VFS.Current.OpenReadFile(filename))
        {
          byte[] buffer = new byte[32768];
          while (true)
          {
            int count = stream.Read(buffer, 0, buffer.Length);
            if (count > 0)
              this.tarOut.Write(buffer, 0, count);
            else
              break;
          }
        }
        if (str1 != null && str1.Length > 0)
          VFS.Current.DeleteFile(str1);
        this.tarOut.CloseEntry();
      }
    }

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    /// <summary>
    /// Releases the unmanaged resources used by the FileStream and optionally releases the managed resources.
    /// </summary>
    /// <param name="disposing">true to release both managed and unmanaged resources;
    /// false to release only unmanaged resources.</param>
    protected virtual void Dispose(bool disposing)
    {
      if (this.isDisposed)
        return;
      this.isDisposed = true;
      if (!disposing)
        return;
      if (this.tarOut != null)
      {
        this.tarOut.Flush();
        this.tarOut.Dispose();
      }
      if (this.tarIn == null)
        return;
      this.tarIn.Dispose();
    }

    /// <summary>
    /// Closes the archive and releases any associated resources.
    /// </summary>
    public virtual void Close() => this.Dispose(true);

    /// <summary>
    /// Ensures that resources are freed and other cleanup operations are performed
    /// when the garbage collector reclaims the <see cref="T:ICSharpCode.SharpZipLib.Tar.TarArchive" />.
    /// </summary>
    ~TarArchive() => this.Dispose(false);

    private static void EnsureDirectoryExists(string directoryName)
    {
      if (VFS.Current.DirectoryExists(directoryName))
        return;
      try
      {
        VFS.Current.CreateDirectory(directoryName);
      }
      catch (Exception ex)
      {
        throw new TarException("Exception creating directory '" + directoryName + "', " + ex.Message, ex);
      }
    }

    private static bool IsBinary(string filename)
    {
      using (Stream stream = (Stream) VFS.Current.OpenReadFile(filename))
      {
        int count = Math.Min(4096, (int) stream.Length);
        byte[] buffer = new byte[count];
        int num1 = stream.Read(buffer, 0, count);
        for (int index = 0; index < num1; ++index)
        {
          byte num2 = buffer[index];
          if (num2 < (byte) 8 || num2 > (byte) 13 && num2 < (byte) 32 || num2 == byte.MaxValue)
            return true;
        }
      }
      return false;
    }
  }
}
