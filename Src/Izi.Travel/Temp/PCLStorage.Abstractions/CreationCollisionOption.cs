// Decompiled with JetBrains decompiler
// Type: PCLStorage.CreationCollisionOption
// Assembly: PCLStorage.Abstractions, Version=1.0.2.0, Culture=neutral, PublicKeyToken=286fe515a2c35b64
// MVID: 1BC1AC1B-67B7-41CB-A202-40E4FD631624
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\PCLStorage.Abstractions.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\PCLStorage.Abstractions.xml

#nullable disable
namespace PCLStorage
{
  /// <summary>
  /// Specifies what should happen when trying to create a file or folder that already exists.
  /// </summary>
  public enum CreationCollisionOption
  {
    /// <summary>
    /// Creates a new file with a unique name of the form "name (2).txt"
    /// </summary>
    GenerateUniqueName,
    /// <summary>Replaces any existing file with a new (empty) one</summary>
    ReplaceExisting,
    /// <summary>Throws an exception if the file exists</summary>
    FailIfExists,
    /// <summary>Opens the existing file, if any</summary>
    OpenIfExists,
  }
}
