// Decompiled with JetBrains decompiler
// Type: PCLStorage.FileAccess
// Assembly: PCLStorage.Abstractions, Version=1.0.2.0, Culture=neutral, PublicKeyToken=286fe515a2c35b64
// MVID: 1BC1AC1B-67B7-41CB-A202-40E4FD631624
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\PCLStorage.Abstractions.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\PCLStorage.Abstractions.xml

#nullable disable
namespace PCLStorage
{
  /// <summary>
  /// Specifies whether a file should be opened for write access or not
  /// </summary>
  public enum FileAccess
  {
    /// <summary>
    /// Specifies that a file should be opened for read-only access
    /// </summary>
    Read,
    /// <summary>
    /// Specifies that a file should be opened for read/write access
    /// </summary>
    ReadAndWrite,
  }
}
