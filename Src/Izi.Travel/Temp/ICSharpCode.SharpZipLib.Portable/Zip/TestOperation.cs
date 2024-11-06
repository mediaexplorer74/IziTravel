// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.TestOperation
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51802, Culture=neutral, PublicKeyToken=null
// MVID: 6C361F8B-A5CA-49B8-ABC1-A50EE493C003
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.xml

#nullable disable
namespace ICSharpCode.SharpZipLib.Zip
{
  /// <summary>
  /// The operation in progress reported by a <see cref="T:ICSharpCode.SharpZipLib.Zip.ZipTestResultHandler" /> during testing.
  /// </summary>
  /// <seealso cref="M:ICSharpCode.SharpZipLib.Zip.ZipFile.TestArchive(System.Boolean)">TestArchive</seealso>
  public enum TestOperation
  {
    /// <summary>Setting up testing.</summary>
    Initialising,
    /// <summary>Testing an individual entries header</summary>
    EntryHeader,
    /// <summary>Testing an individual entries data</summary>
    EntryData,
    /// <summary>Testing an individual entry has completed.</summary>
    EntryComplete,
    /// <summary>Running miscellaneous tests</summary>
    MiscellaneousTests,
    /// <summary>Testing is complete</summary>
    Complete,
  }
}
