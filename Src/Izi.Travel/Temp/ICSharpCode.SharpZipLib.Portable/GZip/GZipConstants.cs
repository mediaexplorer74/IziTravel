// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.GZip.GZipConstants
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51802, Culture=neutral, PublicKeyToken=null
// MVID: 6C361F8B-A5CA-49B8-ABC1-A50EE493C003
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.xml

#nullable disable
namespace ICSharpCode.SharpZipLib.GZip
{
  /// <summary>This class contains constants used for gzip.</summary>
  public sealed class GZipConstants
  {
    /// <summary>Magic number found at start of GZIP header</summary>
    public const int GZIP_MAGIC = 8075;
    /// <summary>Flag bit mask for text</summary>
    public const int FTEXT = 1;
    /// <summary>Flag bitmask for Crc</summary>
    public const int FHCRC = 2;
    /// <summary>Flag bit mask for extra</summary>
    public const int FEXTRA = 4;
    /// <summary>flag bitmask for name</summary>
    public const int FNAME = 8;
    /// <summary>flag bit mask indicating comment is present</summary>
    public const int FCOMMENT = 16;

    /// <summary>Initialise default instance.</summary>
    /// <remarks>Constructor is private to prevent instances being created.</remarks>
    private GZipConstants()
    {
    }
  }
}
