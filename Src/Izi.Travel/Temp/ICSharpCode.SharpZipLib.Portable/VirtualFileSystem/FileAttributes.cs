// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.VirtualFileSystem.FileAttributes
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51802, Culture=neutral, PublicKeyToken=null
// MVID: 6C361F8B-A5CA-49B8-ABC1-A50EE493C003
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.xml

using System;

#nullable disable
namespace ICSharpCode.SharpZipLib.VirtualFileSystem
{
  /// <summary>File attributes</summary>
  [Flags]
  public enum FileAttributes
  {
    /// <summary>File is read only</summary>
    ReadOnly = 1,
    /// <summary>File is hidden</summary>
    Hidden = 2,
    /// <summary>File is directory</summary>
    Directory = 16, // 0x00000010
    /// <summary>File is for archive</summary>
    Archive = 32, // 0x00000020
    /// <summary>File is normal</summary>
    Normal = 128, // 0x00000080
  }
}
