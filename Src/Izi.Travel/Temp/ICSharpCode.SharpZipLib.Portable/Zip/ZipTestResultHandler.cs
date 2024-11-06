// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.ZipTestResultHandler
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51802, Culture=neutral, PublicKeyToken=null
// MVID: 6C361F8B-A5CA-49B8-ABC1-A50EE493C003
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.xml

#nullable disable
namespace ICSharpCode.SharpZipLib.Zip
{
  /// <summary>
  /// Delegate invoked during <see cref="M:ICSharpCode.SharpZipLib.Zip.ZipFile.TestArchive(System.Boolean,ICSharpCode.SharpZipLib.Zip.TestStrategy,ICSharpCode.SharpZipLib.Zip.ZipTestResultHandler)">testing</see> if supplied indicating current progress and status.
  /// </summary>
  /// <remarks>If the message is non-null an error has occured.  If the message is null
  /// the operation as found in <see cref="T:ICSharpCode.SharpZipLib.Zip.TestStatus">status</see> has started.</remarks>
  public delegate void ZipTestResultHandler(TestStatus status, string message);
}
