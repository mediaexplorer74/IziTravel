// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.VirtualFileSystem.IVfsElement
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51802, Culture=neutral, PublicKeyToken=null
// MVID: 6C361F8B-A5CA-49B8-ABC1-A50EE493C003
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.xml

using System;

#nullable disable
namespace ICSharpCode.SharpZipLib.VirtualFileSystem
{
  /// <summary>VFS Element informations</summary>
  public interface IVfsElement
  {
    /// <summary>Name</summary>
    string Name { get; }

    /// <summary>Element exists</summary>
    bool Exists { get; }

    /// <summary>Attributes</summary>
    FileAttributes Attributes { get; }

    /// <summary>Creation time</summary>
    DateTime CreationTime { get; }

    /// <summary>Last access time</summary>
    DateTime LastAccessTime { get; }

    /// <summary>Last write time</summary>
    DateTime LastWriteTime { get; }
  }
}
