// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Tar.TarHeader
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51802, Culture=neutral, PublicKeyToken=null
// MVID: 6C361F8B-A5CA-49B8-ABC1-A50EE493C003
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.xml

using System;
using System.Text;

#nullable disable
namespace ICSharpCode.SharpZipLib.Tar
{
  /// <summary>
  /// This class encapsulates the Tar Entry Header used in Tar Archives.
  /// The class also holds a number of tar constants, used mostly in headers.
  /// </summary>
  public class TarHeader : ICloneable
  {
    /// <summary>The length of the name field in a header buffer.</summary>
    public const int NAMELEN = 100;
    /// <summary>The length of the mode field in a header buffer.</summary>
    public const int MODELEN = 8;
    /// <summary>The length of the user id field in a header buffer.</summary>
    public const int UIDLEN = 8;
    /// <summary>The length of the group id field in a header buffer.</summary>
    public const int GIDLEN = 8;
    /// <summary>The length of the checksum field in a header buffer.</summary>
    public const int CHKSUMLEN = 8;
    /// <summary>Offset of checksum in a header buffer.</summary>
    public const int CHKSUMOFS = 148;
    /// <summary>The length of the size field in a header buffer.</summary>
    public const int SIZELEN = 12;
    /// <summary>The length of the magic field in a header buffer.</summary>
    public const int MAGICLEN = 6;
    /// <summary>The length of the version field in a header buffer.</summary>
    public const int VERSIONLEN = 2;
    /// <summary>
    /// The length of the modification time field in a header buffer.
    /// </summary>
    public const int MODTIMELEN = 12;
    /// <summary>The length of the user name field in a header buffer.</summary>
    public const int UNAMELEN = 32;
    /// <summary>The length of the group name field in a header buffer.</summary>
    public const int GNAMELEN = 32;
    /// <summary>The length of the devices field in a header buffer.</summary>
    public const int DEVLEN = 8;
    /// <summary>The "old way" of indicating a normal file.</summary>
    public const byte LF_OLDNORM = 0;
    /// <summary>Normal file type.</summary>
    public const byte LF_NORMAL = 48;
    /// <summary>Link file type.</summary>
    public const byte LF_LINK = 49;
    /// <summary>Symbolic link file type.</summary>
    public const byte LF_SYMLINK = 50;
    /// <summary>Character device file type.</summary>
    public const byte LF_CHR = 51;
    /// <summary>Block device file type.</summary>
    public const byte LF_BLK = 52;
    /// <summary>Directory file type.</summary>
    public const byte LF_DIR = 53;
    /// <summary>FIFO (pipe) file type.</summary>
    public const byte LF_FIFO = 54;
    /// <summary>Contiguous file type.</summary>
    public const byte LF_CONTIG = 55;
    /// <summary>Posix.1 2001 global extended header</summary>
    public const byte LF_GHDR = 103;
    /// <summary>Posix.1 2001 extended header</summary>
    public const byte LF_XHDR = 120;
    /// <summary>Solaris access control list file type</summary>
    public const byte LF_ACL = 65;
    /// <summary>
    /// GNU dir dump file type
    /// This is a dir entry that contains the names of files that were in the
    /// dir at the time the dump was made
    /// </summary>
    public const byte LF_GNU_DUMPDIR = 68;
    /// <summary>Solaris Extended Attribute File</summary>
    public const byte LF_EXTATTR = 69;
    /// <summary>Inode (metadata only) no file content</summary>
    public const byte LF_META = 73;
    /// <summary>
    /// Identifies the next file on the tape as having a long link name
    /// </summary>
    public const byte LF_GNU_LONGLINK = 75;
    /// <summary>
    /// Identifies the next file on the tape as having a long name
    /// </summary>
    public const byte LF_GNU_LONGNAME = 76;
    /// <summary>Continuation of a file that began on another volume</summary>
    public const byte LF_GNU_MULTIVOL = 77;
    /// <summary>
    /// For storing filenames that dont fit in the main header (old GNU)
    /// </summary>
    public const byte LF_GNU_NAMES = 78;
    /// <summary>GNU Sparse file</summary>
    public const byte LF_GNU_SPARSE = 83;
    /// <summary>GNU Tape/volume header ignore on extraction</summary>
    public const byte LF_GNU_VOLHDR = 86;
    /// <summary>
    /// The magic tag representing a POSIX tar archive.  (includes trailing NULL)
    /// </summary>
    public const string TMAGIC = "ustar ";
    /// <summary>
    /// The magic tag representing an old GNU tar archive where version is included in magic and overwrites it
    /// </summary>
    public const string GNU_TMAGIC = "ustar  ";
    private const long timeConversionFactor = 10000000;
    private static readonly DateTime dateTime1970 = new DateTime(1970, 1, 1, 0, 0, 0, 0);
    private string name;
    private int mode;
    private int userId;
    private int groupId;
    private long size;
    private DateTime modTime;
    private int checksum;
    private bool isChecksumValid;
    private byte typeFlag;
    private string linkName;
    private string magic;
    private string version;
    private string userName;
    private string groupName;
    private int devMajor;
    private int devMinor;
    internal static int userIdAsSet;
    internal static int groupIdAsSet;
    internal static string userNameAsSet;
    internal static string groupNameAsSet = "None";
    internal static int defaultUserId;
    internal static int defaultGroupId;
    internal static string defaultGroupName = "None";
    internal static string defaultUser;

    /// <summary>Initialise a default TarHeader instance</summary>
    public TarHeader()
    {
      this.Magic = "ustar ";
      this.Version = " ";
      this.Name = "";
      this.LinkName = "";
      this.UserId = TarHeader.defaultUserId;
      this.GroupId = TarHeader.defaultGroupId;
      this.UserName = TarHeader.defaultUser;
      this.GroupName = TarHeader.defaultGroupName;
      this.Size = 0L;
    }

    /// <summary>Get/set the name for this tar entry.</summary>
    /// <exception cref="T:System.ArgumentNullException">Thrown when attempting to set the property to null.</exception>
    public string Name
    {
      get => this.name;
      set => this.name = value != null ? value : throw new ArgumentNullException(nameof (value));
    }

    /// <summary>Get the name of this entry.</summary>
    /// <returns>The entry's name.</returns>
    [Obsolete("Use the Name property instead", true)]
    public string GetName() => this.name;

    /// <summary>Get/set the entry's Unix style permission mode.</summary>
    public int Mode
    {
      get => this.mode;
      set => this.mode = value;
    }

    /// <summary>The entry's user id.</summary>
    /// <remarks>
    /// This is only directly relevant to unix systems.
    /// The default is zero.
    /// </remarks>
    public int UserId
    {
      get => this.userId;
      set => this.userId = value;
    }

    /// <summary>Get/set the entry's group id.</summary>
    /// <remarks>
    /// This is only directly relevant to linux/unix systems.
    /// The default value is zero.
    /// </remarks>
    public int GroupId
    {
      get => this.groupId;
      set => this.groupId = value;
    }

    /// <summary>Get/set the entry's size.</summary>
    /// <exception cref="T:System.ArgumentOutOfRangeException">Thrown when setting the size to less than zero.</exception>
    public long Size
    {
      get => this.size;
      set
      {
        this.size = value >= 0L ? value : throw new ArgumentOutOfRangeException(nameof (value), "Cannot be less than zero");
      }
    }

    /// <summary>Get/set the entry's modification time.</summary>
    /// <remarks>
    /// The modification time is only accurate to within a second.
    /// </remarks>
    /// <exception cref="T:System.ArgumentOutOfRangeException">Thrown when setting the date time to less than 1/1/1970.</exception>
    public DateTime ModTime
    {
      get => this.modTime;
      set
      {
        if (value < TarHeader.dateTime1970)
          throw new ArgumentOutOfRangeException(nameof (value), "ModTime cannot be before Jan 1st 1970");
        this.modTime = new DateTime(value.Year, value.Month, value.Day, value.Hour, value.Minute, value.Second);
      }
    }

    /// <summary>
    /// Get the entry's checksum.  This is only valid/updated after writing or reading an entry.
    /// </summary>
    public int Checksum => this.checksum;

    /// <summary>
    /// Get value of true if the header checksum is valid, false otherwise.
    /// </summary>
    public bool IsChecksumValid => this.isChecksumValid;

    /// <summary>Get/set the entry's type flag.</summary>
    public byte TypeFlag
    {
      get => this.typeFlag;
      set => this.typeFlag = value;
    }

    /// <summary>The entry's link name.</summary>
    /// <exception cref="T:System.ArgumentNullException">Thrown when attempting to set LinkName to null.</exception>
    public string LinkName
    {
      get => this.linkName;
      set
      {
        this.linkName = value != null ? value : throw new ArgumentNullException(nameof (value));
      }
    }

    /// <summary>Get/set the entry's magic tag.</summary>
    /// <exception cref="T:System.ArgumentNullException">Thrown when attempting to set Magic to null.</exception>
    public string Magic
    {
      get => this.magic;
      set => this.magic = value != null ? value : throw new ArgumentNullException(nameof (value));
    }

    /// <summary>The entry's version.</summary>
    /// <exception cref="T:System.ArgumentNullException">Thrown when attempting to set Version to null.</exception>
    public string Version
    {
      get => this.version;
      set => this.version = value != null ? value : throw new ArgumentNullException(nameof (value));
    }

    /// <summary>The entry's user name.</summary>
    public string UserName
    {
      get => this.userName;
      set
      {
        if (value != null)
        {
          this.userName = value.Substring(0, Math.Min(32, value.Length));
        }
        else
        {
          string str = "PCL";
          if (str.Length > 32)
            str = str.Substring(0, 32);
          this.userName = str;
        }
      }
    }

    /// <summary>Get/set the entry's group name.</summary>
    /// <remarks>This is only directly relevant to unix systems.</remarks>
    public string GroupName
    {
      get => this.groupName;
      set
      {
        if (value == null)
          this.groupName = "None";
        else
          this.groupName = value;
      }
    }

    /// <summary>Get/set the entry's major device number.</summary>
    public int DevMajor
    {
      get => this.devMajor;
      set => this.devMajor = value;
    }

    /// <summary>Get/set the entry's minor device number.</summary>
    public int DevMinor
    {
      get => this.devMinor;
      set => this.devMinor = value;
    }

    /// <summary>
    /// Create a new <see cref="T:ICSharpCode.SharpZipLib.Tar.TarHeader" /> that is a copy of the current instance.
    /// </summary>
    /// <returns>A new <see cref="T:System.Object" /> that is a copy of the current instance.</returns>
    public object Clone() => this.MemberwiseClone();

    /// <summary>Parse TarHeader information from a header buffer.</summary>
    /// <param name="header">
    /// The tar entry header buffer to get information from.
    /// </param>
    public void ParseBuffer(byte[] header)
    {
      if (header == null)
        throw new ArgumentNullException(nameof (header));
      int offset1 = 0;
      this.name = TarHeader.ParseName(header, offset1, 100).ToString();
      int offset2 = offset1 + 100;
      this.mode = (int) TarHeader.ParseOctal(header, offset2, 8);
      int offset3 = offset2 + 8;
      this.UserId = (int) TarHeader.ParseOctal(header, offset3, 8);
      int offset4 = offset3 + 8;
      this.GroupId = (int) TarHeader.ParseOctal(header, offset4, 8);
      int offset5 = offset4 + 8;
      this.Size = TarHeader.ParseBinaryOrOctal(header, offset5, 12);
      int offset6 = offset5 + 12;
      this.ModTime = TarHeader.GetDateTimeFromCTime(TarHeader.ParseOctal(header, offset6, 12));
      int offset7 = offset6 + 12;
      this.checksum = (int) TarHeader.ParseOctal(header, offset7, 8);
      int num = offset7 + 8;
      byte[] numArray = header;
      int index = num;
      int offset8 = index + 1;
      this.TypeFlag = numArray[index];
      this.LinkName = TarHeader.ParseName(header, offset8, 100).ToString();
      int offset9 = offset8 + 100;
      this.Magic = TarHeader.ParseName(header, offset9, 6).ToString();
      int offset10 = offset9 + 6;
      this.Version = TarHeader.ParseName(header, offset10, 2).ToString();
      int offset11 = offset10 + 2;
      this.UserName = TarHeader.ParseName(header, offset11, 32).ToString();
      int offset12 = offset11 + 32;
      this.GroupName = TarHeader.ParseName(header, offset12, 32).ToString();
      int offset13 = offset12 + 32;
      this.DevMajor = (int) TarHeader.ParseOctal(header, offset13, 8);
      int offset14 = offset13 + 8;
      this.DevMinor = (int) TarHeader.ParseOctal(header, offset14, 8);
      this.isChecksumValid = this.Checksum == TarHeader.MakeCheckSum(header);
    }

    /// <summary>
    /// 'Write' header information to buffer provided, updating the <see cref="P:ICSharpCode.SharpZipLib.Tar.TarHeader.Checksum">check sum</see>.
    /// </summary>
    /// <param name="outBuffer">output buffer for header information</param>
    public void WriteHeader(byte[] outBuffer)
    {
      if (outBuffer == null)
        throw new ArgumentNullException(nameof (outBuffer));
      int offset1 = 0;
      int nameBytes1 = TarHeader.GetNameBytes(this.Name, outBuffer, offset1, 100);
      int octalBytes1 = TarHeader.GetOctalBytes((long) this.mode, outBuffer, nameBytes1, 8);
      int octalBytes2 = TarHeader.GetOctalBytes((long) this.UserId, outBuffer, octalBytes1, 8);
      int octalBytes3 = TarHeader.GetOctalBytes((long) this.GroupId, outBuffer, octalBytes2, 8);
      int binaryOrOctalBytes = TarHeader.GetBinaryOrOctalBytes(this.Size, outBuffer, octalBytes3, 12);
      int octalBytes4 = TarHeader.GetOctalBytes((long) TarHeader.GetCTime(this.ModTime), outBuffer, binaryOrOctalBytes, 12);
      int offset2 = octalBytes4;
      for (int index = 0; index < 8; ++index)
        outBuffer[octalBytes4++] = (byte) 32;
      byte[] numArray = outBuffer;
      int index1 = octalBytes4;
      int offset3 = index1 + 1;
      int typeFlag = (int) this.TypeFlag;
      numArray[index1] = (byte) typeFlag;
      int nameBytes2 = TarHeader.GetNameBytes(this.LinkName, outBuffer, offset3, 100);
      int asciiBytes = TarHeader.GetAsciiBytes(this.Magic, 0, outBuffer, nameBytes2, 6);
      int nameBytes3 = TarHeader.GetNameBytes(this.Version, outBuffer, asciiBytes, 2);
      int nameBytes4 = TarHeader.GetNameBytes(this.UserName, outBuffer, nameBytes3, 32);
      int offset4 = TarHeader.GetNameBytes(this.GroupName, outBuffer, nameBytes4, 32);
      if (this.TypeFlag == (byte) 51 || this.TypeFlag == (byte) 52)
      {
        int octalBytes5 = TarHeader.GetOctalBytes((long) this.DevMajor, outBuffer, offset4, 8);
        offset4 = TarHeader.GetOctalBytes((long) this.DevMinor, outBuffer, octalBytes5, 8);
      }
      while (offset4 < outBuffer.Length)
        outBuffer[offset4++] = (byte) 0;
      this.checksum = TarHeader.ComputeCheckSum(outBuffer);
      TarHeader.GetCheckSumOctalBytes((long) this.checksum, outBuffer, offset2, 8);
      this.isChecksumValid = true;
    }

    /// <summary>Get a hash code for the current object.</summary>
    /// <returns>A hash code for the current object.</returns>
    public override int GetHashCode() => this.Name.GetHashCode();

    /// <summary>
    /// Determines if this instance is equal to the specified object.
    /// </summary>
    /// <param name="obj">The object to compare with.</param>
    /// <returns>true if the objects are equal, false otherwise.</returns>
    public override bool Equals(object obj)
    {
      return obj is TarHeader tarHeader && this.name == tarHeader.name && this.mode == tarHeader.mode && this.UserId == tarHeader.UserId && this.GroupId == tarHeader.GroupId && this.Size == tarHeader.Size && this.ModTime == tarHeader.ModTime && this.Checksum == tarHeader.Checksum && (int) this.TypeFlag == (int) tarHeader.TypeFlag && this.LinkName == tarHeader.LinkName && this.Magic == tarHeader.Magic && this.Version == tarHeader.Version && this.UserName == tarHeader.UserName && this.GroupName == tarHeader.GroupName && this.DevMajor == tarHeader.DevMajor && this.DevMinor == tarHeader.DevMinor;
    }

    /// <summary>
    /// Set defaults for values used when constructing a TarHeader instance.
    /// </summary>
    /// <param name="userId">Value to apply as a default for userId.</param>
    /// <param name="userName">Value to apply as a default for userName.</param>
    /// <param name="groupId">Value to apply as a default for groupId.</param>
    /// <param name="groupName">Value to apply as a default for groupName.</param>
    internal static void SetValueDefaults(
      int userId,
      string userName,
      int groupId,
      string groupName)
    {
      TarHeader.defaultUserId = TarHeader.userIdAsSet = userId;
      TarHeader.defaultUser = TarHeader.userNameAsSet = userName;
      TarHeader.defaultGroupId = TarHeader.groupIdAsSet = groupId;
      TarHeader.defaultGroupName = TarHeader.groupNameAsSet = groupName;
    }

    internal static void RestoreSetValues()
    {
      TarHeader.defaultUserId = TarHeader.userIdAsSet;
      TarHeader.defaultUser = TarHeader.userNameAsSet;
      TarHeader.defaultGroupId = TarHeader.groupIdAsSet;
      TarHeader.defaultGroupName = TarHeader.groupNameAsSet;
    }

    private static long ParseBinaryOrOctal(byte[] header, int offset, int length)
    {
      if (header[offset] < (byte) 128)
        return TarHeader.ParseOctal(header, offset, length);
      long binaryOrOctal = 0;
      for (int index = length - 8; index < length; ++index)
        binaryOrOctal = binaryOrOctal << 8 | (long) header[offset + index];
      return binaryOrOctal;
    }

    /// <summary>Parse an octal string from a header buffer.</summary>
    /// <param name="header">The header buffer from which to parse.</param>
    /// <param name="offset">The offset into the buffer from which to parse.</param>
    /// <param name="length">The number of header bytes to parse.</param>
    /// <returns>The long equivalent of the octal string.</returns>
    public static long ParseOctal(byte[] header, int offset, int length)
    {
      if (header == null)
        throw new ArgumentNullException(nameof (header));
      long octal = 0;
      bool flag = true;
      int num = offset + length;
      for (int index = offset; index < num && header[index] != (byte) 0; ++index)
      {
        if (header[index] == (byte) 32 || header[index] == (byte) 48)
        {
          if (!flag)
          {
            if (header[index] == (byte) 32)
              break;
          }
          else
            continue;
        }
        flag = false;
        octal = (octal << 3) + (long) ((int) header[index] - 48);
      }
      return octal;
    }

    /// <summary>Parse a name from a header buffer.</summary>
    /// <param name="header">The header buffer from which to parse.</param>
    /// <param name="offset">
    /// The offset into the buffer from which to parse.
    /// </param>
    /// <param name="length">The number of header bytes to parse.</param>
    /// <returns>The name parsed.</returns>
    public static StringBuilder ParseName(byte[] header, int offset, int length)
    {
      if (header == null)
        throw new ArgumentNullException(nameof (header));
      if (offset < 0)
        throw new ArgumentOutOfRangeException(nameof (offset), "Cannot be less than zero");
      if (length < 0)
        throw new ArgumentOutOfRangeException(nameof (length), "Cannot be less than zero");
      if (offset + length > header.Length)
        throw new ArgumentException("Exceeds header size", nameof (length));
      StringBuilder name = new StringBuilder(length);
      for (int index = offset; index < offset + length && header[index] != (byte) 0; ++index)
        name.Append((char) header[index]);
      return name;
    }

    /// <summary>
    /// Add <paramref name="name">name</paramref> to the buffer as a collection of bytes
    /// </summary>
    /// <param name="name">The name to add</param>
    /// <param name="nameOffset">The offset of the first character</param>
    /// <param name="buffer">The buffer to add to</param>
    /// <param name="bufferOffset">The index of the first byte to add</param>
    /// <param name="length">The number of characters/bytes to add</param>
    /// <returns>The next free index in the <paramref name="buffer" /></returns>
    public static int GetNameBytes(
      StringBuilder name,
      int nameOffset,
      byte[] buffer,
      int bufferOffset,
      int length)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      if (buffer == null)
        throw new ArgumentNullException(nameof (buffer));
      return TarHeader.GetNameBytes(name.ToString(), nameOffset, buffer, bufferOffset, length);
    }

    /// <summary>
    /// Add <paramref name="name">name</paramref> to the buffer as a collection of bytes
    /// </summary>
    /// <param name="name">The name to add</param>
    /// <param name="nameOffset">The offset of the first character</param>
    /// <param name="buffer">The buffer to add to</param>
    /// <param name="bufferOffset">The index of the first byte to add</param>
    /// <param name="length">The number of characters/bytes to add</param>
    /// <returns>The next free index in the <paramref name="buffer" /></returns>
    public static int GetNameBytes(
      string name,
      int nameOffset,
      byte[] buffer,
      int bufferOffset,
      int length)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      if (buffer == null)
        throw new ArgumentNullException(nameof (buffer));
      int num;
      for (num = 0; num < length - 1 && nameOffset + num < name.Length; ++num)
        buffer[bufferOffset + num] = (byte) name[nameOffset + num];
      for (; num < length; ++num)
        buffer[bufferOffset + num] = (byte) 0;
      return bufferOffset + length;
    }

    /// <summary>Add an entry name to the buffer</summary>
    /// <param name="name">The name to add</param>
    /// <param name="buffer">The buffer to add to</param>
    /// <param name="offset">
    /// The offset into the buffer from which to start adding
    /// </param>
    /// <param name="length">The number of header bytes to add</param>
    /// <returns>The index of the next free byte in the buffer</returns>
    public static int GetNameBytes(StringBuilder name, byte[] buffer, int offset, int length)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      if (buffer == null)
        throw new ArgumentNullException(nameof (buffer));
      return TarHeader.GetNameBytes(name.ToString(), 0, buffer, offset, length);
    }

    /// <summary>Add an entry name to the buffer</summary>
    /// <param name="name">The name to add</param>
    /// <param name="buffer">The buffer to add to</param>
    /// <param name="offset">The offset into the buffer from which to start adding</param>
    /// <param name="length">The number of header bytes to add</param>
    /// <returns>The index of the next free byte in the buffer</returns>
    public static int GetNameBytes(string name, byte[] buffer, int offset, int length)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      if (buffer == null)
        throw new ArgumentNullException(nameof (buffer));
      return TarHeader.GetNameBytes(name, 0, buffer, offset, length);
    }

    /// <summary>
    /// Add a string to a buffer as a collection of ascii bytes.
    /// </summary>
    /// <param name="toAdd">The string to add</param>
    /// <param name="nameOffset">The offset of the first character to add.</param>
    /// <param name="buffer">The buffer to add to.</param>
    /// <param name="bufferOffset">The offset to start adding at.</param>
    /// <param name="length">The number of ascii characters to add.</param>
    /// <returns>The next free index in the buffer.</returns>
    public static int GetAsciiBytes(
      string toAdd,
      int nameOffset,
      byte[] buffer,
      int bufferOffset,
      int length)
    {
      if (toAdd == null)
        throw new ArgumentNullException(nameof (toAdd));
      if (buffer == null)
        throw new ArgumentNullException(nameof (buffer));
      for (int index = 0; index < length && nameOffset + index < toAdd.Length; ++index)
        buffer[bufferOffset + index] = (byte) toAdd[nameOffset + index];
      return bufferOffset + length;
    }

    /// <summary>Put an octal representation of a value into a buffer</summary>
    /// <param name="value">the value to be converted to octal</param>
    /// <param name="buffer">buffer to store the octal string</param>
    /// <param name="offset">
    /// The offset into the buffer where the value starts
    /// </param>
    /// <param name="length">The length of the octal string to create</param>
    /// <returns>
    /// The offset of the character next byte after the octal string
    /// </returns>
    public static int GetOctalBytes(long value, byte[] buffer, int offset, int length)
    {
      if (buffer == null)
        throw new ArgumentNullException(nameof (buffer));
      int num1 = length - 1;
      buffer[offset + num1] = (byte) 0;
      int num2 = num1 - 1;
      if (value > 0L)
      {
        for (long index = value; num2 >= 0 && index > 0L; --num2)
        {
          buffer[offset + num2] = (byte) (48U + (uint) (byte) ((ulong) index & 7UL));
          index >>= 3;
        }
      }
      for (; num2 >= 0; --num2)
        buffer[offset + num2] = (byte) 48;
      return offset + length;
    }

    /// <summary>
    /// Put an octal or binary representation of a value into a buffer
    /// </summary>
    /// <param name="value">Value to be convert to octal</param>
    /// <param name="buffer">The buffer to update</param>
    /// <param name="offset">The offset into the buffer to store the value</param>
    /// <param name="length">The length of the octal string. Must be 12.</param>
    /// <returns>Index of next byte</returns>
    private static int GetBinaryOrOctalBytes(long value, byte[] buffer, int offset, int length)
    {
      if (value <= 8589934591L)
        return TarHeader.GetOctalBytes(value, buffer, offset, length);
      for (int index = length - 1; index > 0; --index)
      {
        buffer[offset + index] = (byte) value;
        value >>= 8;
      }
      buffer[offset] = (byte) 128;
      return offset + length;
    }

    /// <summary>Add the checksum integer to header buffer.</summary>
    /// <param name="value"></param>
    /// <param name="buffer">The header buffer to set the checksum for</param>
    /// <param name="offset">The offset into the buffer for the checksum</param>
    /// <param name="length">The number of header bytes to update.
    /// It's formatted differently from the other fields: it has 6 digits, a
    /// null, then a space -- rather than digits, a space, then a null.
    /// The final space is already there, from checksumming
    /// </param>
    /// <returns>The modified buffer offset</returns>
    private static void GetCheckSumOctalBytes(long value, byte[] buffer, int offset, int length)
    {
      TarHeader.GetOctalBytes(value, buffer, offset, length - 1);
    }

    /// <summary>
    /// Compute the checksum for a tar entry header.
    /// The checksum field must be all spaces prior to this happening
    /// </summary>
    /// <param name="buffer">The tar entry's header buffer.</param>
    /// <returns>The computed checksum.</returns>
    private static int ComputeCheckSum(byte[] buffer)
    {
      int checkSum = 0;
      for (int index = 0; index < buffer.Length; ++index)
        checkSum += (int) buffer[index];
      return checkSum;
    }

    /// <summary>
    /// Make a checksum for a tar entry ignoring the checksum contents.
    /// </summary>
    /// <param name="buffer">The tar entry's header buffer.</param>
    /// <returns>The checksum for the buffer</returns>
    private static int MakeCheckSum(byte[] buffer)
    {
      int num = 0;
      for (int index = 0; index < 148; ++index)
        num += (int) buffer[index];
      for (int index = 0; index < 8; ++index)
        num += 32;
      for (int index = 156; index < buffer.Length; ++index)
        num += (int) buffer[index];
      return num;
    }

    private static int GetCTime(DateTime dateTime)
    {
      return (int) ((dateTime.Ticks - TarHeader.dateTime1970.Ticks) / 10000000L);
    }

    private static DateTime GetDateTimeFromCTime(long ticks)
    {
      try
      {
        return new DateTime(TarHeader.dateTime1970.Ticks + ticks * 10000000L);
      }
      catch (ArgumentOutOfRangeException ex)
      {
        return TarHeader.dateTime1970;
      }
    }
  }
}
