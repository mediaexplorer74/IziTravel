// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.Compression.Streams.InflaterInputStream
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
  /// This filter stream is used to decompress data compressed using the "deflate"
  /// format. The "deflate" format is described in RFC 1951.
  /// 
  /// This stream may form the basis for other decompression filters, such
  /// as the <see cref="T:ICSharpCode.SharpZipLib.GZip.GZipInputStream">GZipInputStream</see>.
  /// 
  /// Author of the original java version : John Leuner.
  /// </summary>
  public class InflaterInputStream : Stream
  {
    /// <summary>Decompressor for this stream</summary>
    protected Inflater inf;
    /// <summary>
    /// <see cref="T:ICSharpCode.SharpZipLib.Zip.Compression.Streams.InflaterInputBuffer">Input buffer</see> for this stream.
    /// </summary>
    protected InflaterInputBuffer inputBuffer;
    /// <summary>Base stream the inflater reads from.</summary>
    private Stream baseInputStream;
    /// <summary>The compressed size</summary>
    protected long csize;
    /// <summary>
    /// Flag indicating wether this instance has been closed or not.
    /// </summary>
    private bool isClosed;
    /// <summary>
    /// Flag indicating wether this instance is designated the stream owner.
    /// When closing if this flag is true the underlying stream is closed.
    /// </summary>
    private bool isStreamOwner = true;

    /// <summary>
    /// Create an InflaterInputStream with the default decompressor
    /// and a default buffer size of 4KB.
    /// </summary>
    /// <param name="baseInputStream">The InputStream to read bytes from</param>
    public InflaterInputStream(Stream baseInputStream)
      : this(baseInputStream, new Inflater(), 4096)
    {
    }

    /// <summary>
    /// Create an InflaterInputStream with the specified decompressor
    /// and a default buffer size of 4KB.
    /// </summary>
    /// <param name="baseInputStream">The source of input data</param>
    /// <param name="inf">
    /// The decompressor used to decompress data read from baseInputStream
    /// </param>
    public InflaterInputStream(Stream baseInputStream, Inflater inf)
      : this(baseInputStream, inf, 4096)
    {
    }

    /// <summary>
    /// Create an InflaterInputStream with the specified decompressor
    /// and the specified buffer size.
    /// </summary>
    /// <param name="baseInputStream">The InputStream to read bytes from</param>
    /// <param name="inflater">The decompressor to use</param>
    /// <param name="bufferSize">Size of the buffer to use</param>
    public InflaterInputStream(Stream baseInputStream, Inflater inflater, int bufferSize)
    {
      if (baseInputStream == null)
        throw new ArgumentNullException(nameof (baseInputStream));
      if (inflater == null)
        throw new ArgumentNullException(nameof (inflater));
      if (bufferSize <= 0)
        throw new ArgumentOutOfRangeException(nameof (bufferSize));
      this.baseInputStream = baseInputStream;
      this.inf = inflater;
      this.inputBuffer = new InflaterInputBuffer(baseInputStream, bufferSize);
    }

    /// <summary>
    /// Get/set flag indicating ownership of underlying stream.
    /// When the flag is true <see cref="M:ICSharpCode.SharpZipLib.Zip.Compression.Streams.InflaterInputStream.Dispose(System.Boolean)" /> will close the underlying stream also.
    /// </summary>
    /// <remarks>The default value is true.</remarks>
    public bool IsStreamOwner
    {
      get => this.isStreamOwner;
      set => this.isStreamOwner = value;
    }

    /// <summary>Skip specified number of bytes of uncompressed data</summary>
    /// <param name="count">Number of bytes to skip</param>
    /// <returns>
    /// The number of bytes skipped, zero if the end of
    /// stream has been reached
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="count">The number of bytes</paramref> to skip is less than or equal to zero.
    /// </exception>
    public long Skip(long count)
    {
      if (count <= 0L)
        throw new ArgumentOutOfRangeException(nameof (count));
      if (this.baseInputStream.CanSeek)
      {
        this.baseInputStream.Seek(count, SeekOrigin.Current);
        return count;
      }
      int count1 = 2048;
      if (count < (long) count1)
        count1 = (int) count;
      byte[] buffer = new byte[count1];
      int num1 = 1;
      long num2;
      for (num2 = count; num2 > 0L && num1 > 0; num2 -= (long) num1)
      {
        if (num2 < (long) count1)
          count1 = (int) num2;
        num1 = this.baseInputStream.Read(buffer, 0, count1);
      }
      return count - num2;
    }

    /// <summary>Clear any cryptographic state.</summary>
    protected void StopDecrypting()
    {
    }

    /// <summary>
    /// Returns 0 once the end of the stream (EOF) has been reached.
    /// Otherwise returns 1.
    /// </summary>
    public virtual int Available => !this.inf.IsFinished ? 1 : 0;

    /// <summary>Fills the buffer with more data to decompress.</summary>
    /// <exception cref="T:ICSharpCode.SharpZipLib.SharpZipBaseException">
    /// Stream ends early
    /// </exception>
    protected void Fill()
    {
      if (this.inputBuffer.Available <= 0)
      {
        this.inputBuffer.Fill();
        if (this.inputBuffer.Available <= 0)
          throw new SharpZipBaseException("Unexpected EOF");
      }
      this.inputBuffer.SetInflaterInput(this.inf);
    }

    /// <summary>
    /// Gets a value indicating whether the current stream supports reading
    /// </summary>
    public override bool CanRead => this.baseInputStream.CanRead;

    /// <summary>
    /// Gets a value of false indicating seeking is not supported for this stream.
    /// </summary>
    public override bool CanSeek => false;

    /// <summary>
    /// Gets a value of false indicating that this stream is not writeable.
    /// </summary>
    public override bool CanWrite => false;

    /// <summary>A value representing the length of the stream in bytes.</summary>
    public override long Length => (long) this.inputBuffer.RawLength;

    /// <summary>
    /// The current position within the stream.
    /// Throws a NotSupportedException when attempting to set the position
    /// </summary>
    /// <exception cref="T:System.NotSupportedException">Attempting to set the position</exception>
    public override long Position
    {
      get => this.baseInputStream.Position;
      set => throw new NotSupportedException("InflaterInputStream Position not supported");
    }

    /// <summary>Flushes the baseInputStream</summary>
    public override void Flush() => this.baseInputStream.Flush();

    /// <summary>
    /// Sets the position within the current stream
    /// Always throws a NotSupportedException
    /// </summary>
    /// <param name="offset">The relative offset to seek to.</param>
    /// <param name="origin">The <see cref="T:System.IO.SeekOrigin" /> defining where to seek from.</param>
    /// <returns>The new position in the stream.</returns>
    /// <exception cref="T:System.NotSupportedException">Any access</exception>
    public override long Seek(long offset, SeekOrigin origin)
    {
      throw new NotSupportedException("Seek not supported");
    }

    /// <summary>
    /// Set the length of the current stream
    /// Always throws a NotSupportedException
    /// </summary>
    /// <param name="value">The new length value for the stream.</param>
    /// <exception cref="T:System.NotSupportedException">Any access</exception>
    public override void SetLength(long value)
    {
      throw new NotSupportedException("InflaterInputStream SetLength not supported");
    }

    /// <summary>
    /// Writes a sequence of bytes to stream and advances the current position
    /// This method always throws a NotSupportedException
    /// </summary>
    /// <param name="buffer">Thew buffer containing data to write.</param>
    /// <param name="offset">The offset of the first byte to write.</param>
    /// <param name="count">The number of bytes to write.</param>
    /// <exception cref="T:System.NotSupportedException">Any access</exception>
    public override void Write(byte[] buffer, int offset, int count)
    {
      throw new NotSupportedException("InflaterInputStream Write not supported");
    }

    /// <summary>
    /// Writes one byte to the current stream and advances the current position
    /// Always throws a NotSupportedException
    /// </summary>
    /// <param name="value">The byte to write.</param>
    /// <exception cref="T:System.NotSupportedException">Any access</exception>
    public override void WriteByte(byte value)
    {
      throw new NotSupportedException("InflaterInputStream WriteByte not supported");
    }

    /// <summary>
    /// Closes the input stream.  When <see cref="P:ICSharpCode.SharpZipLib.Zip.Compression.Streams.InflaterInputStream.IsStreamOwner"></see>
    /// is true the underlying stream is also closed.
    /// </summary>
    protected override void Dispose(bool disposing)
    {
      base.Dispose(disposing);
      if (!disposing || this.isClosed)
        return;
      this.isClosed = true;
      if (!this.isStreamOwner)
        return;
      this.baseInputStream.Dispose();
    }

    /// <summary>
    /// Reads decompressed data into the provided buffer byte array
    /// </summary>
    /// <param name="buffer">The array to read and decompress data into</param>
    /// <param name="offset">
    /// The offset indicating where the data should be placed
    /// </param>
    /// <param name="count">The number of bytes to decompress</param>
    /// <returns>The number of bytes read.  Zero signals the end of stream</returns>
    /// <exception cref="T:ICSharpCode.SharpZipLib.SharpZipBaseException">
    /// Inflater needs a dictionary
    /// </exception>
    public override int Read(byte[] buffer, int offset, int count)
    {
      if (this.inf.IsNeedingDictionary)
        throw new SharpZipBaseException("Need a dictionary");
      int count1 = count;
      int num;
      do
      {
        num = this.inf.Inflate(buffer, offset, count1);
        offset += num;
        count1 -= num;
        if (count1 != 0 && !this.inf.IsFinished)
        {
          if (this.inf.IsNeedingInput)
            this.Fill();
        }
        else
          goto label_8;
      }
      while (num != 0);
      throw new ZipException("Dont know what to do");
label_8:
      return count - count1;
    }
  }
}
