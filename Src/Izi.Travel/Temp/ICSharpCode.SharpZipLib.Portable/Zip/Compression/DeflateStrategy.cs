// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.Compression.DeflateStrategy
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51802, Culture=neutral, PublicKeyToken=null
// MVID: 6C361F8B-A5CA-49B8-ABC1-A50EE493C003
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.xml

#nullable disable
namespace ICSharpCode.SharpZipLib.Zip.Compression
{
  /// <summary>Strategies for deflater</summary>
  public enum DeflateStrategy
  {
    /// <summary>The default strategy</summary>
    Default,
    /// <summary>
    /// This strategy will only allow longer string repetitions.  It is
    /// useful for random data with a small character set.
    /// </summary>
    Filtered,
    /// <summary>
    /// This strategy will not look for string repetitions at all.  It
    /// only encodes with Huffman trees (which means, that more common
    /// characters get a smaller encoding.
    /// </summary>
    HuffmanOnly,
  }
}
