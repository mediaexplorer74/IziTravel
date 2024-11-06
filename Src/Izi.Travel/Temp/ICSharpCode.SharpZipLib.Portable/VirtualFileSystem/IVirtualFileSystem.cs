// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.VirtualFileSystem.IVirtualFileSystem
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51802, Culture=neutral, PublicKeyToken=null
// MVID: 6C361F8B-A5CA-49B8-ABC1-A50EE493C003
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace ICSharpCode.SharpZipLib.VirtualFileSystem
{
  /// <summary>Interface providing Virtual File System access</summary>
  public interface IVirtualFileSystem
  {
    /// <summary>List directory files</summary>
    IEnumerable<string> GetFiles(string directory);

    /// <summary>List directories</summary>
    IEnumerable<string> GetDirectories(string directory);

    /// <summary>Get the full path for a file</summary>
    string GetFullPath(string path);

    /// <summary>Get the informations of a directory</summary>
    IDirectoryInfo GetDirectoryInfo(string directoryName);

    /// <summary>Get the informations of a file</summary>
    IFileInfo GetFileInfo(string filename);

    /// <summary>Define the last write time</summary>
    void SetLastWriteTime(string name, DateTime dateTime);

    /// <summary>Define the attributes</summary>
    /// <param name="name"></param>
    /// <param name="attributes"></param>
    void SetAttributes(string name, FileAttributes attributes);

    /// <summary>Create a new directory</summary>
    void CreateDirectory(string directory);

    /// <summary>Create a temporary file name</summary>
    string GetTempFileName();

    /// <summary>Copy a file</summary>
    void CopyFile(string fromFileName, string toFileName, bool overwrite);

    /// <summary>Move a file</summary>
    void MoveFile(string fromFileName, string toFileName);

    /// <summary>Delete a file</summary>
    void DeleteFile(string fileName);

    /// <summary>Create an new file</summary>
    VfsStream CreateFile(string filename);

    /// <summary>Open file for read</summary>
    VfsStream OpenReadFile(string filename);

    /// <summary>Open existing file for write</summary>
    VfsStream OpenWriteFile(string filename);

    /// <summary>Current directory</summary>
    string CurrentDirectory { get; }

    /// <summary>Directory separator</summary>
    char DirectorySeparatorChar { get; }
  }
}
