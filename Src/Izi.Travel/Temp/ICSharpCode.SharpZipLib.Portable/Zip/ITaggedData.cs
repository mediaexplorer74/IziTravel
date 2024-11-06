// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.ITaggedData
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51802, Culture=neutral, PublicKeyToken=null
// MVID: 6C361F8B-A5CA-49B8-ABC1-A50EE493C003
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.xml

#nullable disable
namespace ICSharpCode.SharpZipLib.Zip
{
  /// <summary>ExtraData tagged value interface.</summary>
  public interface ITaggedData
  {
    /// <summary>Get the ID for this tagged data value.</summary>
    short TagID { get; }

    /// <summary>Set the contents of this instance from the data passed.</summary>
    /// <param name="data">The data to extract contents from.</param>
    /// <param name="offset">The offset to begin extracting data from.</param>
    /// <param name="count">The number of bytes to extract.</param>
    void SetData(byte[] data, int offset, int count);

    /// <summary>Get the data representing this instance.</summary>
    /// <returns>Returns the data for this instance.</returns>
    byte[] GetData();
  }
}
