// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.ZipEntry
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51802, Culture=neutral, PublicKeyToken=null
// MVID: 6C361F8B-A5CA-49B8-ABC1-A50EE493C003
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.xml

using System;
using System.IO;

#nullable disable
namespace ICSharpCode.SharpZipLib.Zip
{
  /// <summary>
  /// This class represents an entry in a zip archive.  This can be a file
  /// or a directory
  /// ZipFile and ZipInputStream will give you instances of this class as
  /// information about the members in an archive.  ZipOutputStream
  /// uses an instance of this class when creating an entry in a Zip file.
  /// <br />
  /// <br />Author of the original java version : Jochen Hoenicke
  /// </summary>
  public class ZipEntry : ICloneable
  {
    /// <summary>AES unsupported prior to .NET 2.0</summary>
    internal int AESKeySize;
    private ZipEntry.Known known;
    private int externalFileAttributes = -1;
    private ushort versionMadeBy;
    private string name;
    private ulong size;
    private ulong compressedSize;
    private ushort versionToExtract;
    private uint crc;
    private uint dosTime;
    private CompressionMethod method = CompressionMethod.Deflated;
    private byte[] extra;
    private string comment;
    private int flags;
    private long zipFileIndex = -1;
    private long offset;
    private bool forceZip64_;
    private byte cryptoCheckValue_;

    /// <summary>Creates a zip entry with the given name.</summary>
    /// <param name="name">
    /// The name for this entry. Can include directory components.
    /// The convention for names is 'unix' style paths with relative names only.
    /// There are with no device names and path elements are separated by '/' characters.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    /// The name passed is null
    /// </exception>
    public ZipEntry(string name)
      : this(name, 0, 51, CompressionMethod.Deflated)
    {
    }

    /// <summary>
    /// Creates a zip entry with the given name and version required to extract
    /// </summary>
    /// <param name="name">
    /// The name for this entry. Can include directory components.
    /// The convention for names is 'unix'  style paths with no device names and
    /// path elements separated by '/' characters.  This is not enforced see <see cref="M:ICSharpCode.SharpZipLib.Zip.ZipEntry.CleanName(System.String)">CleanName</see>
    /// on how to ensure names are valid if this is desired.
    /// </param>
    /// <param name="versionRequiredToExtract">
    /// The minimum 'feature version' required this entry
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    /// The name passed is null
    /// </exception>
    internal ZipEntry(string name, int versionRequiredToExtract)
      : this(name, versionRequiredToExtract, 51, CompressionMethod.Deflated)
    {
    }

    /// <summary>
    /// Initializes an entry with the given name and made by information
    /// </summary>
    /// <param name="name">Name for this entry</param>
    /// <param name="madeByInfo">Version and HostSystem Information</param>
    /// <param name="versionRequiredToExtract">Minimum required zip feature version required to extract this entry</param>
    /// <param name="method">Compression method for this entry.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// The name passed is null
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// versionRequiredToExtract should be 0 (auto-calculate) or &gt; 10
    /// </exception>
    /// <remarks>
    /// This constructor is used by the ZipFile class when reading from the central header
    /// It is not generally useful, use the constructor specifying the name only.
    /// </remarks>
    internal ZipEntry(
      string name,
      int versionRequiredToExtract,
      int madeByInfo,
      CompressionMethod method)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      if (name.Length > (int) ushort.MaxValue)
        throw new ArgumentException("Name is too long", nameof (name));
      if (versionRequiredToExtract != 0 && versionRequiredToExtract < 10)
        throw new ArgumentOutOfRangeException(nameof (versionRequiredToExtract));
      this.DateTime = DateTime.Now;
      this.name = ZipEntry.CleanName(name);
      this.versionMadeBy = (ushort) madeByInfo;
      this.versionToExtract = (ushort) versionRequiredToExtract;
      this.method = method;
    }

    /// <summary>Creates a deep copy of the given zip entry.</summary>
    /// <param name="entry">The entry to copy.</param>
    [Obsolete("Use Clone instead")]
    public ZipEntry(ZipEntry entry)
    {
      this.known = entry != null ? entry.known : throw new ArgumentNullException(nameof (entry));
      this.name = entry.name;
      this.size = entry.size;
      this.compressedSize = entry.compressedSize;
      this.crc = entry.crc;
      this.dosTime = entry.dosTime;
      this.method = entry.method;
      this.comment = entry.comment;
      this.versionToExtract = entry.versionToExtract;
      this.versionMadeBy = entry.versionMadeBy;
      this.externalFileAttributes = entry.externalFileAttributes;
      this.flags = entry.flags;
      this.zipFileIndex = entry.zipFileIndex;
      this.offset = entry.offset;
      this.forceZip64_ = entry.forceZip64_;
      if (entry.extra == null)
        return;
      this.extra = new byte[entry.extra.Length];
      Array.Copy((Array) entry.extra, 0, (Array) this.extra, 0, entry.extra.Length);
    }

    /// <summary>
    /// Get a value indicating wether the entry has a CRC value available.
    /// </summary>
    public bool HasCrc => (this.known & ZipEntry.Known.Crc) != ZipEntry.Known.None;

    /// <summary>
    /// Get/Set flag indicating if entry is encrypted.
    /// A simple helper routine to aid interpretation of <see cref="P:ICSharpCode.SharpZipLib.Zip.ZipEntry.Flags">flags</see>
    /// </summary>
    /// <remarks>This is an assistant that interprets the <see cref="P:ICSharpCode.SharpZipLib.Zip.ZipEntry.Flags">flags</see> property.</remarks>
    public bool IsCrypted
    {
      get => (this.flags & 1) != 0;
      set
      {
        if (value)
          this.flags |= 1;
        else
          this.flags &= -2;
      }
    }

    /// <summary>
    /// Get / set a flag indicating wether entry name and comment text are
    /// encoded in <a href="http://www.unicode.org">unicode UTF8</a>.
    /// </summary>
    /// <remarks>This is an assistant that interprets the <see cref="P:ICSharpCode.SharpZipLib.Zip.ZipEntry.Flags">flags</see> property.</remarks>
    public bool IsUnicodeText
    {
      get => (this.flags & 2048) != 0;
      set
      {
        if (value)
          this.flags |= 2048;
        else
          this.flags &= -2049;
      }
    }

    /// <summary>
    /// Value used during password checking for PKZIP 2.0 / 'classic' encryption.
    /// </summary>
    internal byte CryptoCheckValue
    {
      get => this.cryptoCheckValue_;
      set => this.cryptoCheckValue_ = value;
    }

    /// <summary>Get/Set general purpose bit flag for entry</summary>
    /// <remarks>
    /// General purpose bit flag<br />
    /// <br />
    /// Bit 0: If set, indicates the file is encrypted<br />
    /// Bit 1-2 Only used for compression type 6 Imploding, and 8, 9 deflating<br />
    /// Imploding:<br />
    /// Bit 1 if set indicates an 8K sliding dictionary was used.  If clear a 4k dictionary was used<br />
    /// Bit 2 if set indicates 3 Shannon-Fanno trees were used to encode the sliding dictionary, 2 otherwise<br />
    /// <br />
    /// Deflating:<br />
    ///   Bit 2    Bit 1<br />
    ///     0        0       Normal compression was used<br />
    ///     0        1       Maximum compression was used<br />
    ///     1        0       Fast compression was used<br />
    ///     1        1       Super fast compression was used<br />
    /// <br />
    /// Bit 3: If set, the fields crc-32, compressed size
    /// and uncompressed size are were not able to be written during zip file creation
    /// The correct values are held in a data descriptor immediately following the compressed data. <br />
    /// Bit 4: Reserved for use by PKZIP for enhanced deflating<br />
    /// Bit 5: If set indicates the file contains compressed patch data<br />
    /// Bit 6: If set indicates strong encryption was used.<br />
    /// Bit 7-10: Unused or reserved<br />
    /// Bit 11: If set the name and comments for this entry are in <a href="http://www.unicode.org">unicode</a>.<br />
    /// Bit 12-15: Unused or reserved<br />
    /// </remarks>
    /// <seealso cref="P:ICSharpCode.SharpZipLib.Zip.ZipEntry.IsUnicodeText"></seealso>
    /// <seealso cref="P:ICSharpCode.SharpZipLib.Zip.ZipEntry.IsCrypted"></seealso>
    public int Flags
    {
      get => this.flags;
      set => this.flags = value;
    }

    /// <summary>Get/Set index of this entry in Zip file</summary>
    /// <remarks>This is only valid when the entry is part of a <see cref="T:ICSharpCode.SharpZipLib.Zip.ZipFile"></see></remarks>
    public long ZipFileIndex
    {
      get => this.zipFileIndex;
      set => this.zipFileIndex = value;
    }

    /// <summary>Get/set offset for use in central header</summary>
    public long Offset
    {
      get => this.offset;
      set => this.offset = value;
    }

    /// <summary>
    /// Get/Set external file attributes as an integer.
    /// The values of this are operating system dependant see
    /// <see cref="P:ICSharpCode.SharpZipLib.Zip.ZipEntry.HostSystem">HostSystem</see> for details
    /// </summary>
    public int ExternalFileAttributes
    {
      get
      {
        return (this.known & ZipEntry.Known.ExternalAttributes) == ZipEntry.Known.None ? -1 : this.externalFileAttributes;
      }
      set
      {
        this.externalFileAttributes = value;
        this.known |= ZipEntry.Known.ExternalAttributes;
      }
    }

    /// <summary>
    /// Get the version made by for this entry or zero if unknown.
    /// The value / 10 indicates the major version number, and
    /// the value mod 10 is the minor version number
    /// </summary>
    public int VersionMadeBy => (int) this.versionMadeBy & (int) byte.MaxValue;

    /// <summary>
    /// Get a value indicating this entry is for a DOS/Windows system.
    /// </summary>
    public bool IsDOSEntry => this.HostSystem == 0 || this.HostSystem == 10;

    /// <summary>
    /// Test the external attributes for this <see cref="T:ICSharpCode.SharpZipLib.Zip.ZipEntry" /> to
    /// see if the external attributes are Dos based (including WINNT and variants)
    /// and match the values
    /// </summary>
    /// <param name="attributes">The attributes to test.</param>
    /// <returns>Returns true if the external attributes are known to be DOS/Windows
    /// based and have the same attributes set as the value passed.</returns>
    private bool HasDosAttributes(int attributes)
    {
      bool flag = false;
      if ((this.known & ZipEntry.Known.ExternalAttributes) != ZipEntry.Known.None && (this.HostSystem == 0 || this.HostSystem == 10) && (this.ExternalFileAttributes & attributes) == attributes)
        flag = true;
      return flag;
    }

    /// <summary>
    /// Gets the compatability information for the <see cref="P:ICSharpCode.SharpZipLib.Zip.ZipEntry.ExternalFileAttributes">external file attribute</see>
    /// If the external file attributes are compatible with MS-DOS and can be read
    /// by PKZIP for DOS version 2.04g then this value will be zero.  Otherwise the value
    /// will be non-zero and identify the host system on which the attributes are compatible.
    /// </summary>
    /// <remarks>
    /// The values for this as defined in the Zip File format and by others are shown below.  The values are somewhat
    /// misleading in some cases as they are not all used as shown.  You should consult the relevant documentation
    /// to obtain up to date and correct information.  The modified appnote by the infozip group is
    /// particularly helpful as it documents a lot of peculiarities.  The document is however a little dated.
    /// <list type="table">
    /// <item>0 - MS-DOS and OS/2 (FAT / VFAT / FAT32 file systems)</item>
    /// <item>1 - Amiga</item>
    /// <item>2 - OpenVMS</item>
    /// <item>3 - Unix</item>
    /// <item>4 - VM/CMS</item>
    /// <item>5 - Atari ST</item>
    /// <item>6 - OS/2 HPFS</item>
    /// <item>7 - Macintosh</item>
    /// <item>8 - Z-System</item>
    /// <item>9 - CP/M</item>
    /// <item>10 - Windows NTFS</item>
    /// <item>11 - MVS (OS/390 - Z/OS)</item>
    /// <item>12 - VSE</item>
    /// <item>13 - Acorn Risc</item>
    /// <item>14 - VFAT</item>
    /// <item>15 - Alternate MVS</item>
    /// <item>16 - BeOS</item>
    /// <item>17 - Tandem</item>
    /// <item>18 - OS/400</item>
    /// <item>19 - OS/X (Darwin)</item>
    /// <item>99 - WinZip AES</item>
    /// <item>remainder - unused</item>
    /// </list>
    /// </remarks>
    public int HostSystem
    {
      get => (int) this.versionMadeBy >> 8 & (int) byte.MaxValue;
      set
      {
        this.versionMadeBy &= (ushort) byte.MaxValue;
        this.versionMadeBy |= (ushort) ((value & (int) byte.MaxValue) << 8);
      }
    }

    /// <summary>
    /// Get minimum Zip feature version required to extract this entry
    /// </summary>
    /// <remarks>
    /// Minimum features are defined as:<br />
    /// 1.0 - Default value<br />
    /// 1.1 - File is a volume label<br />
    /// 2.0 - File is a folder/directory<br />
    /// 2.0 - File is compressed using Deflate compression<br />
    /// 2.0 - File is encrypted using traditional encryption<br />
    /// 2.1 - File is compressed using Deflate64<br />
    /// 2.5 - File is compressed using PKWARE DCL Implode<br />
    /// 2.7 - File is a patch data set<br />
    /// 4.5 - File uses Zip64 format extensions<br />
    /// 4.6 - File is compressed using BZIP2 compression<br />
    /// 5.0 - File is encrypted using DES<br />
    /// 5.0 - File is encrypted using 3DES<br />
    /// 5.0 - File is encrypted using original RC2 encryption<br />
    /// 5.0 - File is encrypted using RC4 encryption<br />
    /// 5.1 - File is encrypted using AES encryption<br />
    /// 5.1 - File is encrypted using corrected RC2 encryption<br />
    /// 5.1 - File is encrypted using corrected RC2-64 encryption<br />
    /// 6.1 - File is encrypted using non-OAEP key wrapping<br />
    /// 6.2 - Central directory encryption (not confirmed yet)<br />
    /// 6.3 - File is compressed using LZMA<br />
    /// 6.3 - File is compressed using PPMD+<br />
    /// 6.3 - File is encrypted using Blowfish<br />
    /// 6.3 - File is encrypted using Twofish<br />
    /// </remarks>
    /// <seealso cref="P:ICSharpCode.SharpZipLib.Zip.ZipEntry.CanDecompress"></seealso>
    public int Version
    {
      get
      {
        if (this.versionToExtract != (ushort) 0)
          return (int) this.versionToExtract & (int) byte.MaxValue;
        int version = 10;
        if (this.AESKeySize > 0)
          version = 51;
        else if (this.CentralHeaderRequiresZip64)
          version = 45;
        else if (CompressionMethod.Deflated == this.method)
          version = 20;
        else if (this.IsDirectory)
          version = 20;
        else if (this.IsCrypted)
          version = 20;
        else if (this.HasDosAttributes(8))
          version = 11;
        return version;
      }
    }

    /// <summary>
    /// Get a value indicating whether this entry can be decompressed by the library.
    /// </summary>
    /// <remarks>This is based on the <see cref="P:ICSharpCode.SharpZipLib.Zip.ZipEntry.Version"></see> and
    /// wether the <see cref="M:ICSharpCode.SharpZipLib.Zip.ZipEntry.IsCompressionMethodSupported">compression method</see> is supported.</remarks>
    public bool CanDecompress
    {
      get
      {
        return this.Version <= 51 && (this.Version == 10 || this.Version == 11 || this.Version == 20 || this.Version == 45 || this.Version == 51) && this.IsCompressionMethodSupported();
      }
    }

    /// <summary>Force this entry to be recorded using Zip64 extensions.</summary>
    public void ForceZip64() => this.forceZip64_ = true;

    /// <summary>
    /// Get a value indicating wether Zip64 extensions were forced.
    /// </summary>
    /// <returns>A <see cref="T:System.Boolean" /> value of true if Zip64 extensions have been forced on; false if not.</returns>
    public bool IsZip64Forced() => this.forceZip64_;

    /// <summary>
    /// Gets a value indicating if the entry requires Zip64 extensions
    /// to store the full entry values.
    /// </summary>
    /// <value>A <see cref="T:System.Boolean" /> value of true if a local header requires Zip64 extensions; false if not.</value>
    public bool LocalHeaderRequiresZip64
    {
      get
      {
        bool headerRequiresZip64 = this.forceZip64_;
        if (!headerRequiresZip64)
        {
          ulong compressedSize = this.compressedSize;
          if (this.versionToExtract == (ushort) 0 && this.IsCrypted)
            compressedSize += 12UL;
          headerRequiresZip64 = (this.size >= (ulong) uint.MaxValue || compressedSize >= (ulong) uint.MaxValue) && (this.versionToExtract == (ushort) 0 || this.versionToExtract >= (ushort) 45);
        }
        return headerRequiresZip64;
      }
    }

    /// <summary>
    /// Get a value indicating wether the central directory entry requires Zip64 extensions to be stored.
    /// </summary>
    public bool CentralHeaderRequiresZip64
    {
      get => this.LocalHeaderRequiresZip64 || this.offset >= (long) uint.MaxValue;
    }

    /// <summary>Get/Set DosTime value.</summary>
    /// <remarks>
    /// The MS-DOS date format can only represent dates between 1/1/1980 and 12/31/2107.
    /// </remarks>
    public long DosTime
    {
      get => (this.known & ZipEntry.Known.Time) == ZipEntry.Known.None ? 0L : (long) this.dosTime;
      set
      {
        this.dosTime = (uint) value;
        this.known |= ZipEntry.Known.Time;
      }
    }

    /// <summary>Gets/Sets the time of last modification of the entry.</summary>
    /// <remarks>
    /// The <see cref="P:ICSharpCode.SharpZipLib.Zip.ZipEntry.DosTime"></see> property is updated to match this as far as possible.
    /// </remarks>
    public DateTime DateTime
    {
      get
      {
        uint second = Math.Min(59U, (uint) (2 * ((int) this.dosTime & 31)));
        uint minute = Math.Min(59U, this.dosTime >> 5 & 63U);
        uint hour = Math.Min(23U, this.dosTime >> 11 & 31U);
        uint month = Math.Max(1U, Math.Min(12U, this.dosTime >> 21 & 15U));
        uint year = (uint) (((int) (this.dosTime >> 25) & (int) sbyte.MaxValue) + 1980);
        int day = Math.Max(1, Math.Min(DateTime.DaysInMonth((int) year, (int) month), (int) (this.dosTime >> 16) & 31));
        return new DateTime((int) year, (int) month, day, (int) hour, (int) minute, (int) second);
      }
      set
      {
        uint num1 = (uint) value.Year;
        uint num2 = (uint) value.Month;
        uint num3 = (uint) value.Day;
        uint num4 = (uint) value.Hour;
        uint num5 = (uint) value.Minute;
        uint num6 = (uint) value.Second;
        if (num1 < 1980U)
        {
          num1 = 1980U;
          num2 = 1U;
          num3 = 1U;
          num4 = 0U;
          num5 = 0U;
          num6 = 0U;
        }
        else if (num1 > 2107U)
        {
          num1 = 2107U;
          num2 = 12U;
          num3 = 31U;
          num4 = 23U;
          num5 = 59U;
          num6 = 59U;
        }
        this.DosTime = (long) ((uint) (((int) num1 - 1980 & (int) sbyte.MaxValue) << 25 | (int) num2 << 21 | (int) num3 << 16 | (int) num4 << 11 | (int) num5 << 5) | num6 >> 1);
      }
    }

    /// <summary>Returns the entry name.</summary>
    /// <remarks>
    ///  The unix naming convention is followed.
    ///  Path components in the entry should always separated by forward slashes ('/').
    ///  Dos device names like C: should also be removed.
    ///  See the <see cref="T:ICSharpCode.SharpZipLib.Zip.ZipNameTransform" /> class, or <see cref="M:ICSharpCode.SharpZipLib.Zip.ZipEntry.CleanName(System.String)" />
    /// </remarks>
    public string Name => this.name;

    /// <summary>Gets/Sets the size of the uncompressed data.</summary>
    /// <returns>The size or -1 if unknown.</returns>
    /// <remarks>Setting the size before adding an entry to an archive can help
    /// avoid compatability problems with some archivers which dont understand Zip64 extensions.</remarks>
    public long Size
    {
      get => (this.known & ZipEntry.Known.Size) == ZipEntry.Known.None ? -1L : (long) this.size;
      set
      {
        this.size = (ulong) value;
        this.known |= ZipEntry.Known.Size;
      }
    }

    /// <summary>Gets/Sets the size of the compressed data.</summary>
    /// <returns>The compressed entry size or -1 if unknown.</returns>
    public long CompressedSize
    {
      get
      {
        return (this.known & ZipEntry.Known.CompressedSize) == ZipEntry.Known.None ? -1L : (long) this.compressedSize;
      }
      set
      {
        this.compressedSize = (ulong) value;
        this.known |= ZipEntry.Known.CompressedSize;
      }
    }

    /// <summary>Gets/Sets the crc of the uncompressed data.</summary>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// Crc is not in the range 0..0xffffffffL
    /// </exception>
    /// <returns>The crc value or -1 if unknown.</returns>
    public long Crc
    {
      get
      {
        return (this.known & ZipEntry.Known.Crc) == ZipEntry.Known.None ? -1L : (long) this.crc & (long) uint.MaxValue;
      }
      set
      {
        this.crc = ((long) this.crc & -4294967296L) == 0L ? (uint) value : throw new ArgumentOutOfRangeException(nameof (value));
        this.known |= ZipEntry.Known.Crc;
      }
    }

    /// <summary>
    /// Gets/Sets the compression method. Only Deflated and Stored are supported.
    /// </summary>
    /// <returns>The compression method for this entry</returns>
    /// <see cref="F:ICSharpCode.SharpZipLib.Zip.CompressionMethod.Deflated" />
    /// <see cref="F:ICSharpCode.SharpZipLib.Zip.CompressionMethod.Stored" />
    public CompressionMethod CompressionMethod
    {
      get => this.method;
      set
      {
        this.method = ZipEntry.IsCompressionMethodSupported(value) ? value : throw new NotSupportedException("Compression method not supported");
      }
    }

    /// <summary>
    /// Gets the compression method for outputting to the local or central header.
    /// Returns same value as CompressionMethod except when AES encrypting, which
    /// places 99 in the method and places the real method in the extra data.
    /// </summary>
    internal CompressionMethod CompressionMethodForHeader
    {
      get => this.AESKeySize <= 0 ? this.method : CompressionMethod.WinZipAES;
    }

    /// <summary>Gets/Sets the extra data.</summary>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// Extra data is longer than 64KB (0xffff) bytes.
    /// </exception>
    /// <returns>Extra data or null if not set.</returns>
    public byte[] ExtraData
    {
      get => this.extra;
      set
      {
        if (value == null)
        {
          this.extra = (byte[]) null;
        }
        else
        {
          this.extra = value.Length <= (int) ushort.MaxValue ? new byte[value.Length] : throw new ArgumentOutOfRangeException(nameof (value));
          Array.Copy((Array) value, 0, (Array) this.extra, 0, value.Length);
        }
      }
    }

    /// <summary>Returns the length of the salt, in bytes</summary>
    internal int AESSaltLen => this.AESKeySize / 16;

    /// <summary>
    /// Number of extra bytes required to hold the AES Header fields (Salt, Pwd verify, AuthCode)
    /// </summary>
    internal int AESOverheadSize => 12 + this.AESSaltLen;

    /// <summary>
    /// Process extra data fields updating the entry based on the contents.
    /// </summary>
    /// <param name="localHeader">True if the extra data fields should be handled
    /// for a local header, rather than for a central header.
    /// </param>
    internal void ProcessExtraData(bool localHeader)
    {
      ZipExtraData extraData = new ZipExtraData(this.extra);
      if (extraData.Find(1))
      {
        this.forceZip64_ = true;
        if (extraData.ValueLength < 4)
          throw new ZipException("Extra data extended Zip64 information length is invalid");
        if (localHeader || this.size == (ulong) uint.MaxValue)
          this.size = (ulong) extraData.ReadLong();
        if (localHeader || this.compressedSize == (ulong) uint.MaxValue)
          this.compressedSize = (ulong) extraData.ReadLong();
        if (!localHeader && this.offset == (long) uint.MaxValue)
          this.offset = extraData.ReadLong();
      }
      else if (((int) this.versionToExtract & (int) byte.MaxValue) >= 45 && (this.size == (ulong) uint.MaxValue || this.compressedSize == (ulong) uint.MaxValue))
        throw new ZipException("Zip64 Extended information required but is missing.");
      if (extraData.Find(10))
      {
        if (extraData.ValueLength < 4)
          throw new ZipException("NTFS Extra data invalid");
        extraData.ReadInt();
        while (extraData.UnreadCount >= 4)
        {
          int num = extraData.ReadShort();
          int amount = extraData.ReadShort();
          if (num == 1)
          {
            if (amount >= 24)
            {
              long fileTime = extraData.ReadLong();
              extraData.ReadLong();
              extraData.ReadLong();
              this.DateTime = DateTime.FromFileTime(fileTime);
              break;
            }
            break;
          }
          extraData.Skip(amount);
        }
      }
      else if (extraData.Find(21589))
      {
        int valueLength = extraData.ValueLength;
        if ((extraData.ReadByte() & 1) != 0 && valueLength >= 5)
        {
          int seconds = extraData.ReadInt();
          this.DateTime = (new DateTime(1970, 1, 1, 0, 0, 0).ToUniversalTime() + new TimeSpan(0, 0, 0, seconds, 0)).ToLocalTime();
        }
      }
      if (this.method != CompressionMethod.WinZipAES)
        return;
      this.ProcessAESExtraData(extraData);
    }

    private void ProcessAESExtraData(ZipExtraData extraData)
    {
      throw new ZipException("AES unsupported");
    }

    /// <summary>Gets/Sets the entry comment.</summary>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// If comment is longer than 0xffff.
    /// </exception>
    /// <returns>The comment or null if not set.</returns>
    /// <remarks>
    /// A comment is only available for entries when read via the <see cref="T:ICSharpCode.SharpZipLib.Zip.ZipFile" /> class.
    /// The <see cref="T:ICSharpCode.SharpZipLib.Zip.ZipInputStream" /> class doesnt have the comment data available.
    /// </remarks>
    public string Comment
    {
      get => this.comment;
      set
      {
        this.comment = value == null || value.Length <= (int) ushort.MaxValue ? value : throw new ArgumentOutOfRangeException(nameof (value), "cannot exceed 65535");
      }
    }

    /// <summary>
    /// Gets a value indicating if the entry is a directory.
    /// however.
    /// </summary>
    /// <remarks>
    /// A directory is determined by an entry name with a trailing slash '/'.
    /// The external file attributes can also indicate an entry is for a directory.
    /// Currently only dos/windows attributes are tested in this manner.
    /// The trailing slash convention should always be followed.
    /// </remarks>
    public bool IsDirectory
    {
      get
      {
        int length = this.name.Length;
        return length > 0 && (this.name[length - 1] == '/' || this.name[length - 1] == '\\') || this.HasDosAttributes(16);
      }
    }

    /// <summary>
    /// Get a value of true if the entry appears to be a file; false otherwise
    /// </summary>
    /// <remarks>
    /// This only takes account of DOS/Windows attributes.  Other operating systems are ignored.
    /// For linux and others the result may be incorrect.
    /// </remarks>
    public bool IsFile => !this.IsDirectory && !this.HasDosAttributes(8);

    /// <summary>Test entry to see if data can be extracted.</summary>
    /// <returns>Returns true if data can be extracted for this entry; false otherwise.</returns>
    public bool IsCompressionMethodSupported()
    {
      return ZipEntry.IsCompressionMethodSupported(this.CompressionMethod);
    }

    /// <summary>Creates a copy of this zip entry.</summary>
    /// <returns>An <see cref="T:System.Object" /> that is a copy of the current instance.</returns>
    public object Clone()
    {
      ZipEntry zipEntry = (ZipEntry) this.MemberwiseClone();
      if (this.extra != null)
      {
        zipEntry.extra = new byte[this.extra.Length];
        Array.Copy((Array) this.extra, 0, (Array) zipEntry.extra, 0, this.extra.Length);
      }
      return (object) zipEntry;
    }

    /// <summary>Gets a string representation of this ZipEntry.</summary>
    /// <returns>A readable textual representation of this <see cref="T:ICSharpCode.SharpZipLib.Zip.ZipEntry" /></returns>
    public override string ToString() => this.name;

    /// <summary>
    /// Test a <see cref="P:ICSharpCode.SharpZipLib.Zip.ZipEntry.CompressionMethod">compression method</see> to see if this library
    /// supports extracting data compressed with that method
    /// </summary>
    /// <param name="method">The compression method to test.</param>
    /// <returns>Returns true if the compression method is supported; false otherwise</returns>
    public static bool IsCompressionMethodSupported(CompressionMethod method)
    {
      return method == CompressionMethod.Deflated || method == CompressionMethod.Stored;
    }

    /// <summary>
    /// Cleans a name making it conform to Zip file conventions.
    /// Devices names ('c:\') and UNC share names ('\\server\share') are removed
    /// and forward slashes ('\') are converted to back slashes ('/').
    /// Names are made relative by trimming leading slashes which is compatible
    /// with the ZIP naming convention.
    /// </summary>
    /// <param name="name">The name to clean</param>
    /// <returns>The 'cleaned' name.</returns>
    /// <remarks>
    /// The <seealso cref="T:ICSharpCode.SharpZipLib.Zip.ZipNameTransform">Zip name transform</seealso> class is more flexible.
    /// </remarks>
    public static string CleanName(string name)
    {
      if (name == null)
        return string.Empty;
      if (Path.IsPathRooted(name))
        name = name.Substring(Path.GetPathRoot(name).Length);
      name = name.Replace("\\", "/");
      while (name.Length > 0 && name[0] == '/')
        name = name.Remove(0, 1);
      return name;
    }

    [System.Flags]
    private enum Known : byte
    {
      None = 0,
      Size = 1,
      CompressedSize = 2,
      Crc = 4,
      Time = 8,
      ExternalAttributes = 16, // 0x10
    }
  }
}
