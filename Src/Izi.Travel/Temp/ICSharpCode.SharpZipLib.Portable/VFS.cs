// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.VFS
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51802, Culture=neutral, PublicKeyToken=null
// MVID: 6C361F8B-A5CA-49B8-ABC1-A50EE493C003
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.xml

using ICSharpCode.SharpZipLib.VirtualFileSystem;

#nullable disable
namespace ICSharpCode.SharpZipLib
{
  /// <summary>Virtual File System singleton</summary>
  public static class VFS
  {
    private static IVirtualFileSystem _Current;

    /// <summary>Define the current VFS</summary>
    public static void SetCurrent(IVirtualFileSystem vfs) => VFS._Current = vfs;

    /// <summary>Current VFS</summary>
    public static IVirtualFileSystem Current
    {
      get => VFS._Current ?? (VFS._Current = (IVirtualFileSystem) new DefaultFileSystem());
    }
  }
}
