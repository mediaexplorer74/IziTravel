// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.EncryptionAlgorithm
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51802, Culture=neutral, PublicKeyToken=null
// MVID: 6C361F8B-A5CA-49B8-ABC1-A50EE493C003
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.xml

#nullable disable
namespace ICSharpCode.SharpZipLib.Zip
{
  /// <summary>Identifies the encryption algorithm used for an entry</summary>
  public enum EncryptionAlgorithm
  {
    /// <summary>No encryption has been used.</summary>
    None = 0,
    /// <summary>Encrypted using PKZIP 2.0 or 'classic' encryption.</summary>
    PkzipClassic = 1,
    /// <summary>DES encryption has been used.</summary>
    Des = 26113, // 0x00006601
    /// <summary>RC2 encryption has been used for encryption.</summary>
    RC2 = 26114, // 0x00006602
    /// <summary>
    /// Triple DES encryption with 168 bit keys has been used for this entry.
    /// </summary>
    TripleDes168 = 26115, // 0x00006603
    /// <summary>
    /// Triple DES with 112 bit keys has been used for this entry.
    /// </summary>
    TripleDes112 = 26121, // 0x00006609
    /// <summary>AES 128 has been used for encryption.</summary>
    Aes128 = 26126, // 0x0000660E
    /// <summary>AES 192 has been used for encryption.</summary>
    Aes192 = 26127, // 0x0000660F
    /// <summary>AES 256 has been used for encryption.</summary>
    Aes256 = 26128, // 0x00006610
    /// <summary>RC2 corrected has been used for encryption.</summary>
    RC2Corrected = 26370, // 0x00006702
    /// <summary>Blowfish has been used for encryption.</summary>
    Blowfish = 26400, // 0x00006720
    /// <summary>Twofish has been used for encryption.</summary>
    Twofish = 26401, // 0x00006721
    /// <summary>RC4 has been used for encryption.</summary>
    RC4 = 26625, // 0x00006801
    /// <summary>An unknown algorithm has been used for encryption.</summary>
    Unknown = 65535, // 0x0000FFFF
  }
}
