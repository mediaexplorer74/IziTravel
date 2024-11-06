// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Tar.ProgressMessageHandler
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51802, Culture=neutral, PublicKeyToken=null
// MVID: 6C361F8B-A5CA-49B8-ABC1-A50EE493C003
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.xml

#nullable disable
namespace ICSharpCode.SharpZipLib.Tar
{
  /// <summary>
  /// Used to advise clients of 'events' while processing archives
  /// </summary>
  public delegate void ProgressMessageHandler(TarArchive archive, TarEntry entry, string message);
}
