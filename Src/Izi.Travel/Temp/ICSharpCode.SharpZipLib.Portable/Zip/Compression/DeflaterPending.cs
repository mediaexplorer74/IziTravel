// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.Compression.DeflaterPending
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51802, Culture=neutral, PublicKeyToken=null
// MVID: 6C361F8B-A5CA-49B8-ABC1-A50EE493C003
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.xml

#nullable disable
namespace ICSharpCode.SharpZipLib.Zip.Compression
{
  /// <summary>
  /// This class stores the pending output of the Deflater.
  /// 
  /// author of the original java version : Jochen Hoenicke
  /// </summary>
  public class DeflaterPending : PendingBuffer
  {
    /// <summary>Construct instance with default buffer size</summary>
    public DeflaterPending()
      : base(65536)
    {
    }
  }
}
