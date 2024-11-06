// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.GeneralBitFlags
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51802, Culture=neutral, PublicKeyToken=null
// MVID: 6C361F8B-A5CA-49B8-ABC1-A50EE493C003
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.xml

using System;

#nullable disable
namespace ICSharpCode.SharpZipLib.Zip
{
  /// <summary>
  /// Defines the contents of the general bit flags field for an archive entry.
  /// </summary>
  [Flags]
  public enum GeneralBitFlags
  {
    /// <summary>Bit 0 if set indicates that the file is encrypted</summary>
    Encrypted = 1,
    /// <summary>
    /// Bits 1 and 2 - Two bits defining the compression method (only for Method 6 Imploding and 8,9 Deflating)
    /// </summary>
    Method = 6,
    /// <summary>
    /// Bit 3 if set indicates a trailing data desciptor is appended to the entry data
    /// </summary>
    Descriptor = 8,
    /// <summary>
    /// Bit 4 is reserved for use with method 8 for enhanced deflation
    /// </summary>
    ReservedPKware4 = 16, // 0x00000010
    /// <summary>
    /// Bit 5 if set indicates the file contains Pkzip compressed patched data.
    /// Requires version 2.7 or greater.
    /// </summary>
    Patched = 32, // 0x00000020
    /// <summary>
    /// Bit 6 if set indicates strong encryption has been used for this entry.
    /// </summary>
    StrongEncryption = 64, // 0x00000040
    /// <summary>Bit 7 is currently unused</summary>
    Unused7 = 128, // 0x00000080
    /// <summary>Bit 8 is currently unused</summary>
    Unused8 = 256, // 0x00000100
    /// <summary>Bit 9 is currently unused</summary>
    Unused9 = 512, // 0x00000200
    /// <summary>Bit 10 is currently unused</summary>
    Unused10 = 1024, // 0x00000400
    /// <summary>
    /// Bit 11 if set indicates the filename and
    /// comment fields for this file must be encoded using UTF-8.
    /// </summary>
    UnicodeText = 2048, // 0x00000800
    /// <summary>
    /// Bit 12 is documented as being reserved by PKware for enhanced compression.
    /// </summary>
    EnhancedCompress = 4096, // 0x00001000
    /// <summary>
    /// Bit 13 if set indicates that values in the local header are masked to hide
    /// their actual values, and the central directory is encrypted.
    /// </summary>
    /// <remarks>Used when encrypting the central directory contents.</remarks>
    HeaderMasked = 8192, // 0x00002000
    /// <summary>
    /// Bit 14 is documented as being reserved for use by PKware
    /// </summary>
    ReservedPkware14 = 16384, // 0x00004000
    /// <summary>
    /// Bit 15 is documented as being reserved for use by PKware
    /// </summary>
    ReservedPkware15 = 32768, // 0x00008000
  }
}
