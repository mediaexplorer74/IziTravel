// Decompiled with JetBrains decompiler
// Type: Weakly.MemoryTributary
// Assembly: Weakly, Version=2.1.0.0, Culture=neutral, PublicKeyToken=3e9c206b2200b970
// MVID: 59987104-5B29-48EC-89B5-2E7347C0D910
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Weakly.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Weakly.xml

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

#nullable disable
namespace Weakly
{
  /// <summary>
  /// MemoryTributary is a re-implementation of <see cref="T:System.IO.MemoryStream" /> that uses a dynamic list of byte arrays as a backing store,
  /// instead of a single byte array, the allocation of which will fail for relatively small streams as it requires contiguous memory.
  /// </summary>
  /// <remarks>Based on http://memorytributary.codeplex.com/ by Sebastian Friston.</remarks>
  public sealed class MemoryTributary : Stream
  {
    private const long BlockSize = 65536;
    private long _length;
    private readonly List<byte[]> _blocks = new List<byte[]>();

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Weakly.MemoryTributary" /> class with an expandable capacity initialized to zero.
    /// </summary>
    public MemoryTributary() => this.Position = 0L;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Weakly.MemoryTributary" /> class based on the specified byte array.
    /// </summary>
    /// <param name="source">The array of unsigned bytes from which to create the current stream.</param>
    public MemoryTributary(byte[] source)
    {
      this.Write(source, 0, source.Length);
      this.Position = 0L;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Weakly.MemoryTributary" /> class with an expandable capacity initialized as specified.
    /// </summary>
    /// <param name="length">The initial size of the internal array in bytes.</param>
    public MemoryTributary(int length)
    {
      this.SetLength((long) length);
      this.Position = (long) length;
      byte[] block = this.Block;
      this.Position = 0L;
    }

    /// <summary>
    /// Gets a value indicating whether the current stream supports reading.
    /// </summary>
    /// <returns>true if the stream supports reading; otherwise, false.</returns>
    public override bool CanRead => true;

    /// <summary>
    /// Gets a value indicating whether the current stream supports seeking.
    /// </summary>
    /// <returns>true if the stream supports seeking; otherwise, false.</returns>
    public override bool CanSeek => true;

    /// <summary>
    /// When overridden in a derived class, gets a value indicating whether the current stream supports writing.
    /// </summary>
    /// <returns>true if the stream supports writing; otherwise, false.</returns>
    public override bool CanWrite => true;

    /// <summary>Gets the length in bytes of the stream.</summary>
    /// <returns>A long value representing the length of the stream in bytes.</returns>
    public override long Length => this._length;

    /// <summary>Gets or sets the position within the current stream.</summary>
    /// <returns>The current position within the stream.</returns>
    public override long Position { get; set; }

    /// <summary>The block of memory currently addressed by Position</summary>
    private byte[] Block
    {
      get
      {
        while ((long) this._blocks.Count <= this.BlockId)
          this._blocks.Add(new byte[new IntPtr(65536).ToInt32()]);
                
        return this._blocks[(int) this.BlockId];
      }
    }

    /// <summary>The id of the block currently addressed by Position</summary>
    private long BlockId => this.Position / 65536L;

    /// <summary>
    /// The offset of the byte currently addressed by Position, into the block that contains it
    /// </summary>
    private long BlockOffset => this.Position % 65536L;

    /// <summary>
    /// Cears all buffers for this stream and causes any buffered data to be written to the underlying device.
    /// </summary>
    public override void Flush()
    {
    }

    /// <summary>
    /// Reads a sequence of bytes from the current stream and advances the position within the stream by the number of bytes read.
    /// </summary>
    /// <param name="buffer">An array of bytes. When this method returns, the buffer contains the specified byte array with the values between <paramref name="offset" /> and (<paramref name="offset" /> + <paramref name="count" /> - 1) replaced by the bytes read from the current source.</param>
    /// <param name="offset">The zero-based byte offset in <paramref name="buffer" /> at which to begin storing the data read from the current stream.</param>
    /// <param name="count">The maximum number of bytes to be read from the current stream.</param>
    /// <returns>
    /// The total number of bytes read into the buffer. This can be less than the number of bytes requested if that many bytes are not currently available, or zero (0) if the end of the stream has been reached.
    /// </returns>
    public override int Read(byte[] buffer, int offset, int count)
    {
      long val1 = (long) count;
      if (val1 < 0L)
        throw new ArgumentOutOfRangeException(nameof (count), "Number of bytes to copy cannot be negative.");
      long num1 = this._length - this.Position;
      if (val1 > num1)
        val1 = num1;
      if (buffer == null)
        throw new ArgumentNullException(nameof (buffer), "Buffer cannot be null.");
      if (offset < 0)
        throw new ArgumentOutOfRangeException(nameof (offset), "Destination offset cannot be negative.");
      int num2 = 0;
      do
      {
        long count1 = Math.Min(val1, 65536L - this.BlockOffset);
        Buffer.BlockCopy((Array) this.Block, (int) this.BlockOffset, (Array) buffer, offset, (int) count1);
        val1 -= count1;
        offset += (int) count1;
        num2 += (int) count1;
        this.Position += count1;
      }
      while (val1 > 0L);
      return num2;
    }

    /// <summary>Sets the position within the current stream.</summary>
    /// <param name="offset">A byte offset relative to the <paramref name="origin" /> parameter.</param>
    /// <param name="origin">A value of type <see cref="T:System.IO.SeekOrigin" /> indicating the reference point used to obtain the new position.</param>
    /// <returns>The new position within the current stream.</returns>
    public override long Seek(long offset, SeekOrigin origin)
    {
      switch (origin)
      {
        case SeekOrigin.Begin:
          this.Position = offset;
          break;
        case SeekOrigin.Current:
          this.Position += offset;
          break;
        case SeekOrigin.End:
          this.Position = this.Length - offset;
          break;
      }
      return this.Position;
    }

    /// <summary>Sets the length of the current stream.</summary>
    /// <param name="value">The desired length of the current stream in bytes.</param>
    public override void SetLength(long value) => this._length = value;

    /// <summary>
    /// Writes a sequence of bytes to the current stream and advances the current position within this stream by the number of bytes written.
    /// </summary>
    /// <param name="buffer">An array of bytes. This method copies <paramref name="count" /> bytes from <paramref name="buffer" /> to the current stream.</param>
    /// <param name="offset">The zero-based byte offset in <paramref name="buffer" /> at which to begin copying bytes to the current stream.</param>
    /// <param name="count">The number of bytes to be written to the current stream.</param>
    public override void Write(byte[] buffer, int offset, int count)
    {
      long position = this.Position;
      try
      {
        do
        {
          int count1 = Math.Min(count, (int) (65536L - this.BlockOffset));
          this.EnsureCapacity(this.Position + (long) count1);
          Buffer.BlockCopy((Array) buffer, offset, (Array) this.Block, (int) this.BlockOffset, count1);
          count -= count1;
          offset += count1;
          this.Position += (long) count1;
        }
        while (count > 0);
      }
      catch //(Exception ex)
      {
        //Debug.WriteLine(ex.Message);
        this.Position = position;
        throw;
      }
    }

    /// <summary>
    /// Reads a byte from the stream and advances the position within the stream by one byte, or returns -1 if at the end of the stream.
    /// </summary>
    /// <returns>
    /// The unsigned byte cast to an Int32, or -1 if at the end of the stream.
    /// </returns>
    public override int ReadByte()
    {
      if (this.Position >= this._length)
        return -1;
      byte num = this.Block[this.BlockOffset];
      ++this.Position;
      return (int) num;
    }

    /// <summary>
    /// Writes a byte to the current position in the stream and advances the position within the stream by one byte.
    /// </summary>
    /// <param name="value">The byte to write to the stream.</param>
    public override void WriteByte(byte value)
    {
      this.EnsureCapacity(this.Position + 1L);
      this.Block[this.BlockOffset] = value;
      ++this.Position;
    }

    private void EnsureCapacity(long intendedLength)
    {
      if (intendedLength <= this._length)
        return;
      this._length = intendedLength;
    }

    /// <summary>
    /// Returns the entire content of the stream as a byte array. This is not safe because the call to new byte[] may
    /// fail if the stream is large enough. Where possible use methods which operate on streams directly instead.
    /// </summary>
    /// <returns>A byte[] containing the current data in the stream</returns>
    public byte[] ToArray()
    {
      long position = this.Position;
      this.Position = 0L;
      byte[] buffer = new byte[this.Length];
      this.Read(buffer, 0, (int) this.Length);
      this.Position = position;
      return buffer;
    }

    /// <summary>
    /// Reads length bytes from source into the this instance at the current position.
    /// </summary>
    /// <param name="source">The stream containing the data to copy</param>
    /// <param name="length">The number of bytes to copy</param>
    public void ReadFrom(Stream source, long length)
    {
      byte[] buffer = new byte[4096];
      do
      {
        int count = source.Read(buffer, 0, (int) Math.Min(4096L, length));
        length -= (long) count;
        this.Write(buffer, 0, count);
      }
      while (length > 0L);
    }

    /// <summary>
    /// Writes the entire stream into destination, regardless of Position, which remains unchanged.
    /// </summary>
    /// <param name="destination">The stream to write the content of this stream to</param>
    public void WriteTo(Stream destination)
    {
      long position = this.Position;
      this.Position = 0L;
      this.CopyTo(destination);
      this.Position = position;
    }
  }
}
