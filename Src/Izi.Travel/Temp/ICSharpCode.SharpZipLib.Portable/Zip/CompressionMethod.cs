// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.CompressionMethod
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51802, Culture=neutral, PublicKeyToken=null
// MVID: 6C361F8B-A5CA-49B8-ABC1-A50EE493C003
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.xml

#nullable disable
namespace ICSharpCode.SharpZipLib.Zip
{
  /// <summary>The kind of compression used for an entry in an archive</summary>
  public enum CompressionMethod
  {
    /// <summary>
    /// A direct copy of the file contents is held in the archive
    /// </summary>
    Stored = 0,
    /// <summary>
    /// Common Zip compression method using a sliding dictionary
    /// of up to 32KB and secondary compression from Huffman/Shannon-Fano trees
    /// </summary>
    Deflated = 8,
    /// <summary>
    /// An extension to deflate with a 64KB window. Not supported by #Zip currently
    /// </summary>
    Deflate64 = 9,
    /// <summary>BZip2 compression. Not supported by #Zip.</summary>
    BZip2 = 11, // 0x0000000B
    /// <summary>
    /// WinZip special for AES encryption, Now supported by #Zip.
    /// </summary>
    WinZipAES = 99, // 0x00000063
  }
}
