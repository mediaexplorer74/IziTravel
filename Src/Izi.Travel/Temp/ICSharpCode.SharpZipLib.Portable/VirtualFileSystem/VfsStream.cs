// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.VirtualFileSystem.VfsStream
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51802, Culture=neutral, PublicKeyToken=null
// MVID: 6C361F8B-A5CA-49B8-ABC1-A50EE493C003
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.xml

using System.IO;

#nullable disable
namespace ICSharpCode.SharpZipLib.VirtualFileSystem
{
  /// <summary>Virtual File System Stream</summary>
  public abstract class VfsStream : Stream
  {
    /// <summary>Name of file name</summary>
    public abstract string Name { get; }
  }
}
