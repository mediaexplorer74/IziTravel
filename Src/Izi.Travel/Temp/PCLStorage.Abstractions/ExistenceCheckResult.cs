// Decompiled with JetBrains decompiler
// Type: PCLStorage.ExistenceCheckResult
// Assembly: PCLStorage.Abstractions, Version=1.0.2.0, Culture=neutral, PublicKeyToken=286fe515a2c35b64
// MVID: 1BC1AC1B-67B7-41CB-A202-40E4FD631624
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\PCLStorage.Abstractions.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\PCLStorage.Abstractions.xml

#nullable disable
namespace PCLStorage
{
  /// <summary>
  /// Describes the result of a file or folder existence check.
  /// </summary>
  public enum ExistenceCheckResult
  {
    /// <summary>No file system entity was found at the given path.</summary>
    NotFound,
    /// <summary>A file was found at the given path.</summary>
    FileExists,
    /// <summary>A folder was found at the given path.</summary>
    FolderExists,
  }
}
