// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Tar.TarEntry
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51802, Culture=neutral, PublicKeyToken=null
// MVID: 6C361F8B-A5CA-49B8-ABC1-A50EE493C003
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.xml

using ICSharpCode.SharpZipLib.VirtualFileSystem;
using System;
using System.Linq;

#nullable disable
namespace ICSharpCode.SharpZipLib.Tar
{
  /// <summary>
  /// This class represents an entry in a Tar archive. It consists
  /// of the entry's header, as well as the entry's File. Entries
  /// can be instantiated in one of three ways, depending on how
  /// they are to be used.
  /// <p>
  /// TarEntries that are created from the header bytes read from
  /// an archive are instantiated with the TarEntry( byte[] )
  /// constructor. These entries will be used when extracting from
  /// or listing the contents of an archive. These entries have their
  /// header filled in using the header bytes. They also set the File
  /// to null, since they reference an archive entry not a file.</p>
  /// <p>
  /// TarEntries that are created from files that are to be written
  /// into an archive are instantiated with the CreateEntryFromFile(string)
  /// pseudo constructor. These entries have their header filled in using
  /// the File's information. They also keep a reference to the File
  /// for convenience when writing entries.</p>
  /// <p>
  /// Finally, TarEntries can be constructed from nothing but a name.
  /// This allows the programmer to construct the entry by hand, for
  /// instance when only an InputStream is available for writing to
  /// the archive, and the header information is constructed from
  /// other information. In this case the header fields are set to
  /// defaults and the File is set to null.</p>
  /// <see cref="P:ICSharpCode.SharpZipLib.Tar.TarEntry.TarHeader" />
  /// </summary>
  public class TarEntry : ICloneable
  {
    /// <summary>
    /// The name of the file this entry represents or null if the entry is not based on a file.
    /// </summary>
    private string file;
    /// <summary>The entry's header information.</summary>
    private TarHeader header;

    /// <summary>
    /// Initialise a default instance of <see cref="T:ICSharpCode.SharpZipLib.Tar.TarEntry" />.
    /// </summary>
    private TarEntry() => this.header = new TarHeader();

    /// <summary>
    /// Construct an entry from an archive's header bytes. File is set
    /// to null.
    /// </summary>
    /// <param name="headerBuffer">
    /// The header bytes from a tar archive entry.
    /// </param>
    public TarEntry(byte[] headerBuffer)
    {
      this.header = new TarHeader();
      this.header.ParseBuffer(headerBuffer);
    }

    /// <summary>
    /// Construct a TarEntry using the <paramref name="header">header</paramref> provided
    /// </summary>
    /// <param name="header">Header details for entry</param>
    public TarEntry(TarHeader header)
    {
      this.header = header != null ? (TarHeader) header.Clone() : throw new ArgumentNullException(nameof (header));
    }

    /// <summary>Clone this tar entry.</summary>
    /// <returns>Returns a clone of this entry.</returns>
    public object Clone()
    {
      return (object) new TarEntry()
      {
        file = this.file,
        header = (TarHeader) this.header.Clone(),
        Name = this.Name
      };
    }

    /// <summary>
    /// Construct an entry with only a <paramref name="name">name</paramref>.
    /// This allows the programmer to construct the entry's header "by hand".
    /// </summary>
    /// <param name="name">The name to use for the entry</param>
    /// <returns>Returns the newly created <see cref="T:ICSharpCode.SharpZipLib.Tar.TarEntry" /></returns>
    public static TarEntry CreateTarEntry(string name)
    {
      TarEntry tarEntry = new TarEntry();
      TarEntry.NameTarHeader(tarEntry.header, name);
      return tarEntry;
    }

    /// <summary>
    /// Construct an entry for a file. File is set to file, and the
    /// header is constructed from information from the file.
    /// </summary>
    /// <param name="fileName">The file name that the entry represents.</param>
    /// <returns>Returns the newly created <see cref="T:ICSharpCode.SharpZipLib.Tar.TarEntry" /></returns>
    public static TarEntry CreateEntryFromFile(string fileName)
    {
      TarEntry entryFromFile = new TarEntry();
      entryFromFile.GetFileTarHeader(entryFromFile.header, fileName);
      return entryFromFile;
    }

    /// <summary>
    /// Determine if the two entries are equal. Equality is determined
    /// by the header names being equal.
    /// </summary>
    /// <param name="obj">The <see cref="T:System.Object" /> to compare with the current Object.</param>
    /// <returns>True if the entries are equal; false if not.</returns>
    public override bool Equals(object obj)
    {
      return obj is TarEntry tarEntry && this.Name.Equals(tarEntry.Name);
    }

    /// <summary>
    /// Derive a Hash value for the current <see cref="T:System.Object" />
    /// </summary>
    /// <returns>A Hash code for the current <see cref="T:System.Object" /></returns>
    public override int GetHashCode() => this.Name.GetHashCode();

    /// <summary>
    /// Determine if the given entry is a descendant of this entry.
    /// Descendancy is determined by the name of the descendant
    /// starting with this entry's name.
    /// </summary>
    /// <param name="toTest">Entry to be checked as a descendent of this.</param>
    /// <returns>True if entry is a descendant of this.</returns>
    public bool IsDescendent(TarEntry toTest)
    {
      return toTest != null ? toTest.Name.StartsWith(this.Name) : throw new ArgumentNullException(nameof (toTest));
    }

    /// <summary>Get this entry's header.</summary>
    /// <returns>This entry's TarHeader.</returns>
    public TarHeader TarHeader => this.header;

    /// <summary>Get/Set this entry's name.</summary>
    public string Name
    {
      get => this.header.Name;
      set => this.header.Name = value;
    }

    /// <summary>Get/set this entry's user id.</summary>
    public int UserId
    {
      get => this.header.UserId;
      set => this.header.UserId = value;
    }

    /// <summary>Get/set this entry's group id.</summary>
    public int GroupId
    {
      get => this.header.GroupId;
      set => this.header.GroupId = value;
    }

    /// <summary>Get/set this entry's user name.</summary>
    public string UserName
    {
      get => this.header.UserName;
      set => this.header.UserName = value;
    }

    /// <summary>Get/set this entry's group name.</summary>
    public string GroupName
    {
      get => this.header.GroupName;
      set => this.header.GroupName = value;
    }

    /// <summary>
    /// Convenience method to set this entry's group and user ids.
    /// </summary>
    /// <param name="userId">This entry's new user id.</param>
    /// <param name="groupId">This entry's new group id.</param>
    public void SetIds(int userId, int groupId)
    {
      this.UserId = userId;
      this.GroupId = groupId;
    }

    /// <summary>
    /// Convenience method to set this entry's group and user names.
    /// </summary>
    /// <param name="userName">This entry's new user name.</param>
    /// <param name="groupName">This entry's new group name.</param>
    public void SetNames(string userName, string groupName)
    {
      this.UserName = userName;
      this.GroupName = groupName;
    }

    /// <summary>Get/Set the modification time for this entry</summary>
    public DateTime ModTime
    {
      get => this.header.ModTime;
      set => this.header.ModTime = value;
    }

    /// <summary>Get this entry's file.</summary>
    /// <returns>This entry's file.</returns>
    public string File => this.file;

    /// <summary>Get/set this entry's recorded file size.</summary>
    public long Size
    {
      get => this.header.Size;
      set => this.header.Size = value;
    }

    /// <summary>
    /// Return true if this entry represents a directory, false otherwise
    /// </summary>
    /// <returns>True if this entry is a directory.</returns>
    public bool IsDirectory
    {
      get
      {
        if (this.file != null)
          return VFS.Current.DirectoryExists(this.file);
        return this.header != null && (this.header.TypeFlag == (byte) 53 || this.Name.EndsWith("/"));
      }
    }

    /// <summary>Fill in a TarHeader with information from a File.</summary>
    /// <param name="header">The TarHeader to fill in.</param>
    /// <param name="file">
    /// The file from which to get the header information.
    /// </param>
    public void GetFileTarHeader(TarHeader header, string file)
    {
      if (header == null)
        throw new ArgumentNullException(nameof (header));
      this.file = file != null ? file : throw new ArgumentNullException(nameof (file));
      string str1 = file;
      if (str1.IndexOf(VFS.Current.CurrentDirectory) == 0)
        str1 = str1.Substring(VFS.Current.CurrentDirectory.Length);
      string str2 = str1.Replace(VFS.Current.DirectorySeparatorChar, '/');
      while (str2.StartsWith("/"))
        str2 = str2.Substring(1);
      header.LinkName = string.Empty;
      header.Name = str2;
      if (VFS.Current.DirectoryExists(file))
      {
        header.Mode = 1003;
        header.TypeFlag = (byte) 53;
        if (header.Name.Length == 0 || header.Name[header.Name.Length - 1] != '/')
          header.Name += "/";
        header.Size = 0L;
      }
      else
      {
        header.Mode = 33216;
        header.TypeFlag = (byte) 48;
        header.Size = VFS.Current.GetFileInfo(file.Replace('/', VFS.Current.DirectorySeparatorChar)).Length;
      }
      header.ModTime = VFS.Current.GetFileInfo(file.Replace('/', VFS.Current.DirectorySeparatorChar)).LastWriteTime.ToUniversalTime();
      header.DevMajor = 0;
      header.DevMinor = 0;
    }

    /// <summary>
    /// Get entries for all files present in this entries directory.
    /// If this entry doesnt represent a directory zero entries are returned.
    /// </summary>
    /// <returns>An array of TarEntry's for this entry's children.</returns>
    public TarEntry[] GetDirectoryEntries()
    {
      if (this.file == null || !VFS.Current.DirectoryExists(this.file))
        return new TarEntry[0];
      string[] array = VFS.Current.GetDirectoriesAndFiles(this.file).ToArray<string>();
      TarEntry[] directoryEntries = new TarEntry[array.Length];
      for (int index = 0; index < array.Length; ++index)
        directoryEntries[index] = TarEntry.CreateEntryFromFile(array[index]);
      return directoryEntries;
    }

    /// <summary>Write an entry's header information to a header buffer.</summary>
    /// <param name="outBuffer">The tar entry header buffer to fill in.</param>
    public void WriteEntryHeader(byte[] outBuffer) => this.header.WriteHeader(outBuffer);

    /// <summary>
    /// Convenience method that will modify an entry's name directly
    /// in place in an entry header buffer byte array.
    /// </summary>
    /// <param name="buffer">
    /// The buffer containing the entry header to modify.
    /// </param>
    /// <param name="newName">
    /// The new name to place into the header buffer.
    /// </param>
    public static void AdjustEntryName(byte[] buffer, string newName)
    {
      TarHeader.GetNameBytes(newName, buffer, 0, 100);
    }

    /// <summary>Fill in a TarHeader given only the entry's name.</summary>
    /// <param name="header">The TarHeader to fill in.</param>
    /// <param name="name">The tar entry name.</param>
    public static void NameTarHeader(TarHeader header, string name)
    {
      if (header == null)
        throw new ArgumentNullException(nameof (header));
      bool flag = name != null ? name.EndsWith("/") : throw new ArgumentNullException(nameof (name));
      header.Name = name;
      header.Mode = flag ? 1003 : 33216;
      header.UserId = 0;
      header.GroupId = 0;
      header.Size = 0L;
      header.ModTime = DateTime.UtcNow;
      header.TypeFlag = flag ? (byte) 53 : (byte) 48;
      header.LinkName = string.Empty;
      header.UserName = string.Empty;
      header.GroupName = string.Empty;
      header.DevMajor = 0;
      header.DevMinor = 0;
    }
  }
}
