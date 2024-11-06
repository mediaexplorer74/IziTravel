// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.ZipConstants
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51802, Culture=neutral, PublicKeyToken=null
// MVID: 6C361F8B-A5CA-49B8-ABC1-A50EE493C003
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.xml

using System;
using System.Text;

#nullable disable
namespace ICSharpCode.SharpZipLib.Zip
{
  /// <summary>This class contains constants used for Zip format files</summary>
  public sealed class ZipConstants
  {
    /// <summary>
    /// The version made by field for entries in the central header when created by this library
    /// </summary>
    /// <remarks>
    /// This is also the Zip version for the library when comparing against the version required to extract
    /// for an entry.  See <see cref="P:ICSharpCode.SharpZipLib.Zip.ZipEntry.CanDecompress" />.
    /// </remarks>
    public const int VersionMadeBy = 51;
    /// <summary>
    /// The version made by field for entries in the central header when created by this library
    /// </summary>
    /// <remarks>
    /// This is also the Zip version for the library when comparing against the version required to extract
    /// for an entry.  See <see cref="P:ICSharpCode.SharpZipLib.Zip.ZipInputStream.CanDecompressEntry">ZipInputStream.CanDecompressEntry</see>.
    /// </remarks>
    [Obsolete("Use VersionMadeBy instead")]
    public const int VERSION_MADE_BY = 51;
    /// <summary>
    /// The minimum version required to support strong encryption
    /// </summary>
    public const int VersionStrongEncryption = 50;
    /// <summary>
    /// The minimum version required to support strong encryption
    /// </summary>
    [Obsolete("Use VersionStrongEncryption instead")]
    public const int VERSION_STRONG_ENCRYPTION = 50;
    /// <summary>Version indicating AES encryption</summary>
    public const int VERSION_AES = 51;
    /// <summary>
    /// The version required for Zip64 extensions (4.5 or higher)
    /// </summary>
    public const int VersionZip64 = 45;
    /// <summary>
    /// Size of local entry header (excluding variable length fields at end)
    /// </summary>
    public const int LocalHeaderBaseSize = 30;
    /// <summary>
    /// Size of local entry header (excluding variable length fields at end)
    /// </summary>
    [Obsolete("Use LocalHeaderBaseSize instead")]
    public const int LOCHDR = 30;
    /// <summary>Size of Zip64 data descriptor</summary>
    public const int Zip64DataDescriptorSize = 24;
    /// <summary>Size of data descriptor</summary>
    public const int DataDescriptorSize = 16;
    /// <summary>Size of data descriptor</summary>
    [Obsolete("Use DataDescriptorSize instead")]
    public const int EXTHDR = 16;
    /// <summary>
    /// Size of central header entry (excluding variable fields)
    /// </summary>
    public const int CentralHeaderBaseSize = 46;
    /// <summary>Size of central header entry</summary>
    [Obsolete("Use CentralHeaderBaseSize instead")]
    public const int CENHDR = 46;
    /// <summary>
    /// Size of end of central record (excluding variable fields)
    /// </summary>
    public const int EndOfCentralRecordBaseSize = 22;
    /// <summary>
    /// Size of end of central record (excluding variable fields)
    /// </summary>
    [Obsolete("Use EndOfCentralRecordBaseSize instead")]
    public const int ENDHDR = 22;
    /// <summary>
    /// Size of 'classic' cryptographic header stored before any entry data
    /// </summary>
    public const int CryptoHeaderSize = 12;
    /// <summary>Size of cryptographic header stored before entry data</summary>
    [Obsolete("Use CryptoHeaderSize instead")]
    public const int CRYPTO_HEADER_SIZE = 12;
    /// <summary>Signature for local entry header</summary>
    public const int LocalHeaderSignature = 67324752;
    /// <summary>Signature for local entry header</summary>
    [Obsolete("Use LocalHeaderSignature instead")]
    public const int LOCSIG = 67324752;
    /// <summary>Signature for spanning entry</summary>
    public const int SpanningSignature = 134695760;
    /// <summary>Signature for spanning entry</summary>
    [Obsolete("Use SpanningSignature instead")]
    public const int SPANNINGSIG = 134695760;
    /// <summary>Signature for temporary spanning entry</summary>
    public const int SpanningTempSignature = 808471376;
    /// <summary>Signature for temporary spanning entry</summary>
    [Obsolete("Use SpanningTempSignature instead")]
    public const int SPANTEMPSIG = 808471376;
    /// <summary>Signature for data descriptor</summary>
    /// <remarks>
    /// This is only used where the length, Crc, or compressed size isnt known when the
    /// entry is created and the output stream doesnt support seeking.
    /// The local entry cannot be 'patched' with the correct values in this case
    /// so the values are recorded after the data prefixed by this header, as well as in the central directory.
    /// </remarks>
    public const int DataDescriptorSignature = 134695760;
    /// <summary>Signature for data descriptor</summary>
    /// <remarks>
    /// This is only used where the length, Crc, or compressed size isnt known when the
    /// entry is created and the output stream doesnt support seeking.
    /// The local entry cannot be 'patched' with the correct values in this case
    /// so the values are recorded after the data prefixed by this header, as well as in the central directory.
    /// </remarks>
    [Obsolete("Use DataDescriptorSignature instead")]
    public const int EXTSIG = 134695760;
    /// <summary>Signature for central header</summary>
    [Obsolete("Use CentralHeaderSignature instead")]
    public const int CENSIG = 33639248;
    /// <summary>Signature for central header</summary>
    public const int CentralHeaderSignature = 33639248;
    /// <summary>Signature for Zip64 central file header</summary>
    public const int Zip64CentralFileHeaderSignature = 101075792;
    /// <summary>Signature for Zip64 central file header</summary>
    [Obsolete("Use Zip64CentralFileHeaderSignature instead")]
    public const int CENSIG64 = 101075792;
    /// <summary>Signature for Zip64 central directory locator</summary>
    public const int Zip64CentralDirLocatorSignature = 117853008;
    /// <summary>
    /// Signature for archive extra data signature (were headers are encrypted).
    /// </summary>
    public const int ArchiveExtraDataSignature = 117853008;
    /// <summary>Central header digitial signature</summary>
    public const int CentralHeaderDigitalSignature = 84233040;
    /// <summary>Central header digitial signature</summary>
    [Obsolete("Use CentralHeaderDigitalSignaure instead")]
    public const int CENDIGITALSIG = 84233040;
    /// <summary>End of central directory record signature</summary>
    public const int EndOfCentralDirectorySignature = 101010256;
    /// <summary>End of central directory record signature</summary>
    [Obsolete("Use EndOfCentralDirectorySignature instead")]
    public const int ENDSIG = 101010256;
    private static Encoding defaultEncoding = Encoding.UTF8;

    /// <summary>
    /// PCL don't support CodePage so we used Encoding instead of
    /// </summary>
    public static Encoding DefaultEncoding
    {
      get => ZipConstants.defaultEncoding;
      set => ZipConstants.defaultEncoding = value;
    }

    /// <summary>Convert a portion of a byte array to a string.</summary>
    /// <param name="data">Data to convert to string</param>
    /// <param name="count">
    /// Number of bytes to convert starting from index 0
    /// </param>
    /// <returns>data[0]..data[count - 1] converted to a string</returns>
    public static string ConvertToString(byte[] data, int count)
    {
      return data == null ? string.Empty : ZipConstants.DefaultEncoding.GetString(data, 0, count);
    }

    /// <summary>Convert a byte array to string</summary>
    /// <param name="data">Byte array to convert</param>
    /// <returns>
    /// <paramref name="data">data</paramref>converted to a string
    /// </returns>
    public static string ConvertToString(byte[] data)
    {
      return data == null ? string.Empty : ZipConstants.ConvertToString(data, data.Length);
    }

    /// <summary>Convert a byte array to string</summary>
    /// <param name="flags">The applicable general purpose bits flags</param>
    /// <param name="data">Byte array to convert</param>
    /// <param name="count">The number of bytes to convert.</param>
    /// <returns>
    /// <paramref name="data">data</paramref>converted to a string
    /// </returns>
    public static string ConvertToStringExt(int flags, byte[] data, int count)
    {
      if (data == null)
        return string.Empty;
      return (flags & 2048) != 0 ? Encoding.UTF8.GetString(data, 0, count) : ZipConstants.ConvertToString(data, count);
    }

    /// <summary>Convert a byte array to string</summary>
    /// <param name="data">Byte array to convert</param>
    /// <param name="flags">The applicable general purpose bits flags</param>
    /// <returns>
    /// <paramref name="data">data</paramref>converted to a string
    /// </returns>
    public static string ConvertToStringExt(int flags, byte[] data)
    {
      if (data == null)
        return string.Empty;
      return (flags & 2048) != 0 ? Encoding.UTF8.GetString(data, 0, data.Length) : ZipConstants.ConvertToString(data, data.Length);
    }

    /// <summary>Convert a string to a byte array</summary>
    /// <param name="str">String to convert to an array</param>
    /// <returns>Converted array</returns>
    public static byte[] ConvertToArray(string str)
    {
      return str == null ? new byte[0] : ZipConstants.DefaultEncoding.GetBytes(str);
    }

    /// <summary>Convert a string to a byte array</summary>
    /// <param name="flags">The applicable <see cref="T:ICSharpCode.SharpZipLib.Zip.GeneralBitFlags">general purpose bits flags</see></param>
    /// <param name="str">String to convert to an array</param>
    /// <returns>Converted array</returns>
    public static byte[] ConvertToArray(int flags, string str)
    {
      if (str == null)
        return new byte[0];
      return (flags & 2048) != 0 ? Encoding.UTF8.GetBytes(str) : ZipConstants.ConvertToArray(str);
    }

    /// <summary>
    /// Initialise default instance of <see cref="T:ICSharpCode.SharpZipLib.Zip.ZipConstants">ZipConstants</see>
    /// </summary>
    /// <remarks>Private to prevent instances being created.</remarks>
    private ZipConstants()
    {
    }
  }
}
