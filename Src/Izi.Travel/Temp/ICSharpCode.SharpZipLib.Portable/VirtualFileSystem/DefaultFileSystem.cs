// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.VirtualFileSystem.DefaultFileSystem
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51802, Culture=neutral, PublicKeyToken=null
// MVID: 6C361F8B-A5CA-49B8-ABC1-A50EE493C003
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace ICSharpCode.SharpZipLib.VirtualFileSystem
{
  /// <summary>Default file system</summary>
  public class DefaultFileSystem : IVirtualFileSystem
  {
    private const string InvalidOperationMessage = "The default file system is not implemented in the Portable Class Library. Implement IVirtualFileSystem and defined it with VFS.SetCurrent().";

    /// <summary>List directory files</summary>
    public virtual IEnumerable<string> GetFiles(string directory)
    {
      throw new InvalidOperationException("The default file system is not implemented in the Portable Class Library. Implement IVirtualFileSystem and defined it with VFS.SetCurrent().");
    }

    /// <summary>List directories</summary>
    public virtual IEnumerable<string> GetDirectories(string directory)
    {
      throw new InvalidOperationException("The default file system is not implemented in the Portable Class Library. Implement IVirtualFileSystem and defined it with VFS.SetCurrent().");
    }

    /// <summary>Create a temporary file name</summary>
    public virtual string GetTempFileName()
    {
      throw new InvalidOperationException("The default file system is not implemented in the Portable Class Library. Implement IVirtualFileSystem and defined it with VFS.SetCurrent().");
    }

    /// <summary>Copy a file</summary>
    public virtual void CopyFile(string fromFileName, string toFileName, bool overwrite)
    {
      throw new InvalidOperationException("The default file system is not implemented in the Portable Class Library. Implement IVirtualFileSystem and defined it with VFS.SetCurrent().");
    }

    /// <summary>Move a file</summary>
    public virtual void MoveFile(string fromFileName, string toFileName)
    {
      throw new InvalidOperationException("The default file system is not implemented in the Portable Class Library. Implement IVirtualFileSystem and defined it with VFS.SetCurrent().");
    }

    /// <summary>Delete a file</summary>
    public virtual void DeleteFile(string fileName)
    {
      throw new InvalidOperationException("The default file system is not implemented in the Portable Class Library. Implement IVirtualFileSystem and defined it with VFS.SetCurrent().");
    }

    /// <summary>Get the full path for a file</summary>
    public virtual string GetFullPath(string path) => path;

    /// <summary>Get the informations of a file</summary>
    public virtual IFileInfo GetFileInfo(string filename)
    {
      throw new InvalidOperationException("The default file system is not implemented in the Portable Class Library. Implement IVirtualFileSystem and defined it with VFS.SetCurrent().");
    }

    /// <summary>Get the informations of a directory</summary>
    public virtual IDirectoryInfo GetDirectoryInfo(string directoryName)
    {
      throw new InvalidOperationException("The default file system is not implemented in the Portable Class Library. Implement IVirtualFileSystem and defined it with VFS.SetCurrent().");
    }

    /// <summary>Define the last write time</summary>
    public virtual void SetLastWriteTime(string name, DateTime dateTime)
    {
      throw new InvalidOperationException("The default file system is not implemented in the Portable Class Library. Implement IVirtualFileSystem and defined it with VFS.SetCurrent().");
    }

    /// <summary>Define the attributes</summary>
    /// <param name="name"></param>
    /// <param name="attributes"></param>
    public virtual void SetAttributes(string name, FileAttributes attributes)
    {
      throw new InvalidOperationException("The default file system is not implemented in the Portable Class Library. Implement IVirtualFileSystem and defined it with VFS.SetCurrent().");
    }

    /// <summary>Create a new directory</summary>
    public virtual void CreateDirectory(string directory)
    {
      throw new InvalidOperationException("The default file system is not implemented in the Portable Class Library. Implement IVirtualFileSystem and defined it with VFS.SetCurrent().");
    }

    /// <summary>Create a new file</summary>
    public virtual VfsStream CreateFile(string filename)
    {
      throw new InvalidOperationException("The default file system is not implemented in the Portable Class Library. Implement IVirtualFileSystem and defined it with VFS.SetCurrent().");
    }

    /// <summary>Open a file for read</summary>
    public virtual VfsStream OpenReadFile(string filename)
    {
      throw new InvalidOperationException("The default file system is not implemented in the Portable Class Library. Implement IVirtualFileSystem and defined it with VFS.SetCurrent().");
    }

    /// <summary>Open an existing file for writing</summary>
    public virtual VfsStream OpenWriteFile(string filename)
    {
      throw new InvalidOperationException("The default file system is not implemented in the Portable Class Library. Implement IVirtualFileSystem and defined it with VFS.SetCurrent().");
    }

    /// <summary>Current directory</summary>
    public virtual string CurrentDirectory
    {
      get
      {
        throw new InvalidOperationException("The default file system is not implemented in the Portable Class Library. Implement IVirtualFileSystem and defined it with VFS.SetCurrent().");
      }
    }

    /// <summary>Directory separator</summary>
    public virtual char DirectorySeparatorChar
    {
      get
      {
        throw new InvalidOperationException("The default file system is not implemented in the Portable Class Library. Implement IVirtualFileSystem and defined it with VFS.SetCurrent().");
      }
    }
  }
}
