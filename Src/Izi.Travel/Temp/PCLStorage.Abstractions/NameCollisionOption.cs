// Decompiled with JetBrains decompiler
// Type: PCLStorage.NameCollisionOption
// Assembly: PCLStorage.Abstractions, Version=1.0.2.0, Culture=neutral, PublicKeyToken=286fe515a2c35b64
// MVID: 1BC1AC1B-67B7-41CB-A202-40E4FD631624
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\PCLStorage.Abstractions.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\PCLStorage.Abstractions.xml

#nullable disable
namespace PCLStorage
{
  /// <summary>
  /// Specifies what should happen when trying to create/rename a file or folder to a name that already exists.
  /// </summary>
  public enum NameCollisionOption
  {
    /// <summary>
    /// Automatically generate a unique name by appending a number to the name of
    /// the file or folder.
    /// </summary>
    GenerateUniqueName,
    /// <summary>
    /// Replace the existing file or folder. Your app must have permission to access
    /// the location that contains the existing file or folder.
    /// </summary>
    ReplaceExisting,
    /// <summary>
    /// Return an error if another file or folder exists with the same name and abort
    /// the operation.
    /// </summary>
    FailIfExists,
  }
}
