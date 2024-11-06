// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.Compression.Streams.InflaterInputBuffer
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51802, Culture=neutral, PublicKeyToken=null
// MVID: 6C361F8B-A5CA-49B8-ABC1-A50EE493C003
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.xml

using System;
using System.IO;

#nullable disable
namespace ICSharpCode.SharpZipLib.Zip.Compression.Streams
{
  /// <summary>
  /// An input buffer customised for use by <see cref="T:ICSharpCode.SharpZipLib.Zip.Compression.Streams.InflaterInputStream" />
  /// </summary>
  /// <remarks>The buffer supports decryption of incoming data.</remarks>
  public class InflaterInputBuffer
  {
    private int rawLength;
    private byte[] rawData;
    private int clearTextLength;
    private byte[] clearText;
    private int available;
    private Stream inputStream;

    /// <summary>
    /// Initialise a new instance of <see cref="T:ICSharpCode.SharpZipLib.Zip.Compression.Streams.InflaterInputBuffer" /> with a default buffer size
    /// </summary>
    /// <param name="stream">The stream to buffer.</param>
    public InflaterInputBuffer(Stream stream)
      : this(stream, 4096)
    {
    }

    /// <summary>
    /// Initialise a new instance of <see cref="T:ICSharpCode.SharpZipLib.Zip.Compression.Streams.InflaterInputBuffer" />
    /// </summary>
    /// <param name="stream">The stream to buffer.</param>
    /// <param name="bufferSize">The size to use for the buffer</param>
    /// <remarks>A minimum buffer size of 1KB is permitted.  Lower sizes are treated as 1KB.</remarks>
    public InflaterInputBuffer(Stream stream, int bufferSize)
    {
      this.inputStream = stream;
      if (bufferSize < 1024)
        bufferSize = 1024;
      this.rawData = new byte[bufferSize];
      this.clearText = this.rawData;
    }

    /// <summary>
    /// Get the length of bytes bytes in the <see cref="P:ICSharpCode.SharpZipLib.Zip.Compression.Streams.InflaterInputBuffer.RawData" />
    /// </summary>
    public int RawLength => this.rawLength;

    /// <summary>Get the contents of the raw data buffer.</summary>
    /// <remarks>This may contain encrypted data.</remarks>
    public byte[] RawData => this.rawData;

    /// <summary>
    /// Get the number of useable bytes in <see cref="P:ICSharpCode.SharpZipLib.Zip.Compression.Streams.InflaterInputBuffer.ClearText" />
    /// </summary>
    public int ClearTextLength => this.clearTextLength;

    /// <summary>Get the contents of the clear text buffer.</summary>
    public byte[] ClearText => this.clearText;

    /// <summary>Get/set the number of bytes available</summary>
    public int Available
    {
      get => this.available;
      set => this.available = value;
    }

    /// <summary>
    /// Call <see cref="M:ICSharpCode.SharpZipLib.Zip.Compression.Inflater.SetInput(System.Byte[],System.Int32,System.Int32)" /> passing the current clear text buffer contents.
    /// </summary>
    /// <param name="inflater">The inflater to set input for.</param>
    public void SetInflaterInput(Inflater inflater)
    {
      if (this.available <= 0)
        return;
      inflater.SetInput(this.clearText, this.clearTextLength - this.available, this.available);
      this.available = 0;
    }

    /// <summary>Fill the buffer from the underlying input stream.</summary>
    public void Fill()
    {
      this.rawLength = 0;
      int num;
      for (int length = this.rawData.Length; length > 0; length -= num)
      {
        num = this.inputStream.Read(this.rawData, this.rawLength, length);
        if (num > 0)
          this.rawLength += num;
        else
          break;
      }
      this.clearTextLength = this.rawLength;
      this.available = this.clearTextLength;
    }

    /// <summary>Read a buffer directly from the input stream</summary>
    /// <param name="buffer">The buffer to fill</param>
    /// <returns>Returns the number of bytes read.</returns>
    public int ReadRawBuffer(byte[] buffer) => this.ReadRawBuffer(buffer, 0, buffer.Length);

    /// <summary>Read a buffer directly from the input stream</summary>
    /// <param name="outBuffer">The buffer to read into</param>
    /// <param name="offset">The offset to start reading data into.</param>
    /// <param name="length">The number of bytes to read.</param>
    /// <returns>Returns the number of bytes read.</returns>
    public int ReadRawBuffer(byte[] outBuffer, int offset, int length)
    {
      if (length < 0)
        throw new ArgumentOutOfRangeException(nameof (length));
      int destinationIndex = offset;
      int val1 = length;
      while (val1 > 0)
      {
        if (this.available <= 0)
        {
          this.Fill();
          if (this.available <= 0)
            return 0;
        }
        int length1 = Math.Min(val1, this.available);
        Array.Copy((Array) this.rawData, this.rawLength - this.available, (Array) outBuffer, destinationIndex, length1);
        destinationIndex += length1;
        val1 -= length1;
        this.available -= length1;
      }
      return length;
    }

    /// <summary>Read clear text data from the input stream.</summary>
    /// <param name="outBuffer">The buffer to add data to.</param>
    /// <param name="offset">The offset to start adding data at.</param>
    /// <param name="length">The number of bytes to read.</param>
    /// <returns>Returns the number of bytes actually read.</returns>
    public int ReadClearTextBuffer(byte[] outBuffer, int offset, int length)
    {
      if (length < 0)
        throw new ArgumentOutOfRangeException(nameof (length));
      int destinationIndex = offset;
      int val1 = length;
      while (val1 > 0)
      {
        if (this.available <= 0)
        {
          this.Fill();
          if (this.available <= 0)
            return 0;
        }
        int length1 = Math.Min(val1, this.available);
        Array.Copy((Array) this.clearText, this.clearTextLength - this.available, (Array) outBuffer, destinationIndex, length1);
        destinationIndex += length1;
        val1 -= length1;
        this.available -= length1;
      }
      return length;
    }

    /// <summary>
    /// Read a <see cref="T:System.Byte" /> from the input stream.
    /// </summary>
    /// <returns>Returns the byte read.</returns>
    public int ReadLeByte()
    {
      if (this.available <= 0)
      {
        this.Fill();
        if (this.available <= 0)
          throw new ZipException("EOF in header");
      }
      byte num = this.rawData[this.rawLength - this.available];
      --this.available;
      return (int) num;
    }

    /// <summary>
    /// Read an <see cref="T:System.Int16" /> in little endian byte order.
    /// </summary>
    /// <returns>The short value read case to an int.</returns>
    public int ReadLeShort() => this.ReadLeByte() | this.ReadLeByte() << 8;

    /// <summary>
    /// Read an <see cref="T:System.Int32" /> in little endian byte order.
    /// </summary>
    /// <returns>The int value read.</returns>
    public int ReadLeInt() => this.ReadLeShort() | this.ReadLeShort() << 16;

    /// <summary>
    /// Read a <see cref="T:System.Int64" /> in little endian byte order.
    /// </summary>
    /// <returns>The long value read.</returns>
    public long ReadLeLong() => (long) (uint) this.ReadLeInt() | (long) this.ReadLeInt() << 32;
  }
}
