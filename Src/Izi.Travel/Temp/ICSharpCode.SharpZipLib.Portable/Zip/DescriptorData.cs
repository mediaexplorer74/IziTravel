// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.DescriptorData
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51802, Culture=neutral, PublicKeyToken=null
// MVID: 6C361F8B-A5CA-49B8-ABC1-A50EE493C003
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.xml

#nullable disable
namespace ICSharpCode.SharpZipLib.Zip
{
  /// <summary>Holds data pertinent to a data descriptor.</summary>
  public class DescriptorData
  {
    private long size;
    private long compressedSize;
    private long crc;

    /// <summary>Get /set the compressed size of data.</summary>
    public long CompressedSize
    {
      get => this.compressedSize;
      set => this.compressedSize = value;
    }

    /// <summary>Get / set the uncompressed size of data</summary>
    public long Size
    {
      get => this.size;
      set => this.size = value;
    }

    /// <summary>Get /set the crc value.</summary>
    public long Crc
    {
      get => this.crc;
      set => this.crc = value & (long) uint.MaxValue;
    }
  }
}
