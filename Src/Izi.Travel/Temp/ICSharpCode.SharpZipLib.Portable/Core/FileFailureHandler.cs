// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Core.FileFailureHandler
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51802, Culture=neutral, PublicKeyToken=null
// MVID: 6C361F8B-A5CA-49B8-ABC1-A50EE493C003
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.xml

#nullable disable
namespace ICSharpCode.SharpZipLib.Core
{
  /// <summary>Delegate invoked when a file failure is detected.</summary>
  /// <param name="sender">The source of the event</param>
  /// <param name="e">The event arguments.</param>
  public delegate void FileFailureHandler(object sender, ScanFailureEventArgs e);
}
