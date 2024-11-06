// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.FileUpdateMode
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51802, Culture=neutral, PublicKeyToken=null
// MVID: 6C361F8B-A5CA-49B8-ABC1-A50EE493C003
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.xml

#nullable disable
namespace ICSharpCode.SharpZipLib.Zip
{
  /// <summary>
  /// The possible ways of <see cref="M:ICSharpCode.SharpZipLib.Zip.ZipFile.CommitUpdate">applying updates</see> to an archive.
  /// </summary>
  public enum FileUpdateMode
  {
    /// <summary>
    /// Perform all updates on temporary files ensuring that the original file is saved.
    /// </summary>
    Safe,
    /// <summary>
    /// Update the archive directly, which is faster but less safe.
    /// </summary>
    Direct,
  }
}
