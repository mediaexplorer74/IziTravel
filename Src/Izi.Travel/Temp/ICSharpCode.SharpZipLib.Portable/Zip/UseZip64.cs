// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.UseZip64
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51802, Culture=neutral, PublicKeyToken=null
// MVID: 6C361F8B-A5CA-49B8-ABC1-A50EE493C003
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.xml

#nullable disable
namespace ICSharpCode.SharpZipLib.Zip
{
  /// <summary>
  /// Determines how entries are tested to see if they should use Zip64 extensions or not.
  /// </summary>
  public enum UseZip64
  {
    /// <summary>Zip64 will not be forced on entries during processing.</summary>
    /// <remarks>An entry can have this overridden if required <see cref="M:ICSharpCode.SharpZipLib.Zip.ZipEntry.ForceZip64"></see></remarks>
    Off,
    /// <summary>Zip64 should always be used.</summary>
    On,
    /// <summary>
    /// #ZipLib will determine use based on entry values when added to archive.
    /// </summary>
    Dynamic,
  }
}
