// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Core.INameTransform
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51802, Culture=neutral, PublicKeyToken=null
// MVID: 6C361F8B-A5CA-49B8-ABC1-A50EE493C003
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.xml

#nullable disable
namespace ICSharpCode.SharpZipLib.Core
{
  /// <summary>
  /// INameTransform defines how file system names are transformed for use with archives, or vice versa.
  /// </summary>
  public interface INameTransform
  {
    /// <summary>Given a file name determine the transformed value.</summary>
    /// <param name="name">The name to transform.</param>
    /// <returns>The transformed file name.</returns>
    string TransformFile(string name);

    /// <summary>Given a directory name determine the transformed value.</summary>
    /// <param name="name">The name to transform.</param>
    /// <returns>The transformed directory name</returns>
    string TransformDirectory(string name);
  }
}
