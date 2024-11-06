// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.ITaggedDataFactory
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51802, Culture=neutral, PublicKeyToken=null
// MVID: 6C361F8B-A5CA-49B8-ABC1-A50EE493C003
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.xml

#nullable disable
namespace ICSharpCode.SharpZipLib.Zip
{
  /// <summary>
  /// A factory that creates <see cref="T:ICSharpCode.SharpZipLib.Zip.ITaggedData">tagged data</see> instances.
  /// </summary>
  internal interface ITaggedDataFactory
  {
    /// <summary>Get data for a specific tag value.</summary>
    /// <param name="tag">The tag ID to find.</param>
    /// <param name="data">The data to search.</param>
    /// <param name="offset">The offset to begin extracting data from.</param>
    /// <param name="count">The number of bytes to extract.</param>
    /// <returns>The located <see cref="T:ICSharpCode.SharpZipLib.Zip.ITaggedData">value found</see>, or null if not found.</returns>
    ITaggedData Create(short tag, byte[] data, int offset, int count);
  }
}
