// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.RawTaggedData
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51802, Culture=neutral, PublicKeyToken=null
// MVID: 6C361F8B-A5CA-49B8-ABC1-A50EE493C003
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.xml

using System;

#nullable disable
namespace ICSharpCode.SharpZipLib.Zip
{
  /// <summary>A raw binary tagged value</summary>
  public class RawTaggedData : ITaggedData
  {
    /// <summary>The tag ID for this instance.</summary>
    private short _tag;
    private byte[] _data;

    /// <summary>Initialise a new instance.</summary>
    /// <param name="tag">The tag ID.</param>
    public RawTaggedData(short tag) => this._tag = tag;

    /// <summary>Get the ID for this tagged data value.</summary>
    public short TagID
    {
      get => this._tag;
      set => this._tag = value;
    }

    /// <summary>Set the data from the raw values provided.</summary>
    /// <param name="data">The raw data to extract values from.</param>
    /// <param name="offset">The index to start extracting values from.</param>
    /// <param name="count">The number of bytes available.</param>
    public void SetData(byte[] data, int offset, int count)
    {
      if (data == null)
        throw new ArgumentNullException(nameof (data));
      this._data = new byte[count];
      Array.Copy((Array) data, offset, (Array) this._data, 0, count);
    }

    /// <summary>Get the binary data representing this instance.</summary>
    /// <returns>The raw binary data representing this instance.</returns>
    public byte[] GetData() => this._data;

    /// <summary>Get /set the binary data representing this instance.</summary>
    /// <returns>The raw binary data representing this instance.</returns>
    public byte[] Data
    {
      get => this._data;
      set => this._data = value;
    }
  }
}
