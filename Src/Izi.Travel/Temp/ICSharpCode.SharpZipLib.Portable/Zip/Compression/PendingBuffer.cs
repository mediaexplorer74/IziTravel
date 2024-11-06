// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.Compression.PendingBuffer
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51802, Culture=neutral, PublicKeyToken=null
// MVID: 6C361F8B-A5CA-49B8-ABC1-A50EE493C003
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.xml

using System;

#nullable disable
namespace ICSharpCode.SharpZipLib.Zip.Compression
{
  /// <summary>
  /// This class is general purpose class for writing data to a buffer.
  /// 
  /// It allows you to write bits as well as bytes
  /// Based on DeflaterPending.java
  /// 
  /// author of the original java version : Jochen Hoenicke
  /// </summary>
  public class PendingBuffer
  {
    /// <summary>Internal work buffer</summary>
    private byte[] buffer_;
    private int start;
    private int end;
    private uint bits;
    private int bitCount;

    /// <summary>construct instance using default buffer size of 4096</summary>
    public PendingBuffer()
      : this(4096)
    {
    }

    /// <summary>construct instance using specified buffer size</summary>
    /// <param name="bufferSize">size to use for internal buffer</param>
    public PendingBuffer(int bufferSize) => this.buffer_ = new byte[bufferSize];

    /// <summary>Clear internal state/buffers</summary>
    public void Reset() => this.start = this.end = this.bitCount = 0;

    /// <summary>Write a byte to buffer</summary>
    /// <param name="value">The value to write</param>
    public void WriteByte(int value) => this.buffer_[this.end++] = (byte) value;

    /// <summary>Write a short value to buffer LSB first</summary>
    /// <param name="value">The value to write.</param>
    public void WriteShort(int value)
    {
      this.buffer_[this.end++] = (byte) value;
      this.buffer_[this.end++] = (byte) (value >> 8);
    }

    /// <summary>write an integer LSB first</summary>
    /// <param name="value">The value to write.</param>
    public void WriteInt(int value)
    {
      this.buffer_[this.end++] = (byte) value;
      this.buffer_[this.end++] = (byte) (value >> 8);
      this.buffer_[this.end++] = (byte) (value >> 16);
      this.buffer_[this.end++] = (byte) (value >> 24);
    }

    /// <summary>Write a block of data to buffer</summary>
    /// <param name="block">data to write</param>
    /// <param name="offset">offset of first byte to write</param>
    /// <param name="length">number of bytes to write</param>
    public void WriteBlock(byte[] block, int offset, int length)
    {
      Array.Copy((Array) block, offset, (Array) this.buffer_, this.end, length);
      this.end += length;
    }

    /// <summary>The number of bits written to the buffer</summary>
    public int BitCount => this.bitCount;

    /// <summary>Align internal buffer on a byte boundary</summary>
    public void AlignToByte()
    {
      if (this.bitCount > 0)
      {
        this.buffer_[this.end++] = (byte) this.bits;
        if (this.bitCount > 8)
          this.buffer_[this.end++] = (byte) (this.bits >> 8);
      }
      this.bits = 0U;
      this.bitCount = 0;
    }

    /// <summary>Write bits to internal buffer</summary>
    /// <param name="b">source of bits</param>
    /// <param name="count">number of bits to write</param>
    public void WriteBits(int b, int count)
    {
      this.bits |= (uint) (b << this.bitCount);
      this.bitCount += count;
      if (this.bitCount < 16)
        return;
      this.buffer_[this.end++] = (byte) this.bits;
      this.buffer_[this.end++] = (byte) (this.bits >> 8);
      this.bits >>= 16;
      this.bitCount -= 16;
    }

    /// <summary>
    /// Write a short value to internal buffer most significant byte first
    /// </summary>
    /// <param name="s">value to write</param>
    public void WriteShortMSB(int s)
    {
      this.buffer_[this.end++] = (byte) (s >> 8);
      this.buffer_[this.end++] = (byte) s;
    }

    /// <summary>Indicates if buffer has been flushed</summary>
    public bool IsFlushed => this.end == 0;

    /// <summary>
    /// Flushes the pending buffer into the given output array.  If the
    /// output array is to small, only a partial flush is done.
    /// </summary>
    /// <param name="output">The output array.</param>
    /// <param name="offset">The offset into output array.</param>
    /// <param name="length">The maximum number of bytes to store.</param>
    /// <returns>The number of bytes flushed.</returns>
    public int Flush(byte[] output, int offset, int length)
    {
      if (this.bitCount >= 8)
      {
        this.buffer_[this.end++] = (byte) this.bits;
        this.bits >>= 8;
        this.bitCount -= 8;
      }
      if (length > this.end - this.start)
      {
        length = this.end - this.start;
        Array.Copy((Array) this.buffer_, this.start, (Array) output, offset, length);
        this.start = 0;
        this.end = 0;
      }
      else
      {
        Array.Copy((Array) this.buffer_, this.start, (Array) output, offset, length);
        this.start += length;
      }
      return length;
    }

    /// <summary>
    /// Convert internal buffer to byte array.
    /// Buffer is empty on completion
    /// </summary>
    /// <returns>The internal buffer contents converted to a byte array.</returns>
    public byte[] ToByteArray()
    {
      byte[] destinationArray = new byte[this.end - this.start];
      Array.Copy((Array) this.buffer_, this.start, (Array) destinationArray, 0, destinationArray.Length);
      this.start = 0;
      this.end = 0;
      return destinationArray;
    }
  }
}
