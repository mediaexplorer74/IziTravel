// Decompiled with JetBrains decompiler
// Type: RestSharp.Compression.ZLib.CrcCalculatorStream
// Assembly: RestSharp.WindowsPhone, Version=105.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EBEB4E14-2430-4192-BEF2-49D8C49DBCCE
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.xml

using System;
using System.IO;

#nullable disable
namespace RestSharp.Compression.ZLib
{
  /// <summary>
  /// A Stream that calculates a CRC32 (a checksum) on all bytes read,
  /// or on all bytes written.
  /// </summary>
  /// <remarks>
  /// <para>
  /// This class can be used to verify the CRC of a ZipEntry when
  /// reading from a stream, or to calculate a CRC when writing to a
  /// stream.  The stream should be used to either read, or write, but
  /// not both.  If you intermix reads and writes, the results are not
  /// defined.
  /// </para>
  /// 
  /// <para>
  /// This class is intended primarily for use internally by the
  /// DotNetZip library.
  /// </para>
  /// </remarks>
  internal class CrcCalculatorStream : Stream, IDisposable
  {
    private static readonly long UnsetLengthLimit = -99;
    private Stream _innerStream;
    private CRC32 _Crc32;
    private long _lengthLimit = -99;
    private bool _leaveOpen;

    /// <summary>
    /// Gets the total number of bytes run through the CRC32 calculator.
    /// </summary>
    /// <remarks>
    /// This is either the total number of bytes read, or the total number of bytes
    /// written, depending on the direction of this stream.
    /// </remarks>
    public long TotalBytesSlurped => this._Crc32.TotalBytesRead;

    /// <summary>The default constructor.</summary>
    /// <remarks>
    /// Instances returned from this constructor will leave the underlying stream
    /// open upon Close().
    /// </remarks>
    /// <param name="stream">The underlying stream</param>
    public CrcCalculatorStream(Stream stream)
      : this(true, CrcCalculatorStream.UnsetLengthLimit, stream)
    {
    }

    /// <summary>
    /// The constructor allows the caller to specify how to handle the underlying
    /// stream at close.
    /// </summary>
    /// <param name="stream">The underlying stream</param>
    /// <param name="leaveOpen">true to leave the underlying stream
    /// open upon close of the CrcCalculatorStream.; false otherwise.</param>
    public CrcCalculatorStream(Stream stream, bool leaveOpen)
      : this(leaveOpen, CrcCalculatorStream.UnsetLengthLimit, stream)
    {
    }

    /// <summary>
    /// A constructor allowing the specification of the length of the stream to read.
    /// </summary>
    /// <remarks>
    /// Instances returned from this constructor will leave the underlying stream open
    /// upon Close().
    /// </remarks>
    /// <param name="stream">The underlying stream</param>
    /// <param name="length">The length of the stream to slurp</param>
    public CrcCalculatorStream(Stream stream, long length)
      : this(true, length, stream)
    {
      if (length < 0L)
        throw new ArgumentException(nameof (length));
    }

    /// <summary>
    /// A constructor allowing the specification of the length of the stream to
    /// read, as well as whether to keep the underlying stream open upon Close().
    /// </summary>
    /// <param name="stream">The underlying stream</param>
    /// <param name="length">The length of the stream to slurp</param>
    /// <param name="leaveOpen">true to leave the underlying stream
    /// open upon close of the CrcCalculatorStream.; false otherwise.</param>
    public CrcCalculatorStream(Stream stream, long length, bool leaveOpen)
      : this(leaveOpen, length, stream)
    {
      if (length < 0L)
        throw new ArgumentException(nameof (length));
    }

    private CrcCalculatorStream(bool leaveOpen, long length, Stream stream)
    {
      this._innerStream = stream;
      this._Crc32 = new CRC32();
      this._lengthLimit = length;
      this._leaveOpen = leaveOpen;
    }

    /// <summary>Provides the current CRC for all blocks slurped in.</summary>
    public int Crc => this._Crc32.Crc32Result;

    /// <summary>
    /// Indicates whether the underlying stream will be left open when the
    /// CrcCalculatorStream is Closed.
    /// </summary>
    public bool LeaveOpen
    {
      get => this._leaveOpen;
      set => this._leaveOpen = value;
    }

    /// <summary>Read from the stream</summary>
    /// <param name="buffer">the buffer to read</param>
    /// <param name="offset">the offset at which to start</param>
    /// <param name="count">the number of bytes to read</param>
    /// <returns>the number of bytes actually read</returns>
    public override int Read(byte[] buffer, int offset, int count)
    {
      int count1 = count;
      if (this._lengthLimit != CrcCalculatorStream.UnsetLengthLimit)
      {
        if (this._Crc32.TotalBytesRead >= this._lengthLimit)
          return 0;
        long num = this._lengthLimit - this._Crc32.TotalBytesRead;
        if (num < (long) count)
          count1 = (int) num;
      }
      int count2 = this._innerStream.Read(buffer, offset, count1);
      if (count2 > 0)
        this._Crc32.SlurpBlock(buffer, offset, count2);
      return count2;
    }

    /// <summary>Write to the stream.</summary>
    /// <param name="buffer">the buffer from which to write</param>
    /// <param name="offset">the offset at which to start writing</param>
    /// <param name="count">the number of bytes to write</param>
    public override void Write(byte[] buffer, int offset, int count)
    {
      if (count > 0)
        this._Crc32.SlurpBlock(buffer, offset, count);
      this._innerStream.Write(buffer, offset, count);
    }

    /// <summary>Indicates whether the stream supports reading.</summary>
    public override bool CanRead => this._innerStream.CanRead;

    /// <summary>Indicates whether the stream supports seeking.</summary>
    public override bool CanSeek => this._innerStream.CanSeek;

    /// <summary>Indicates whether the stream supports writing.</summary>
    public override bool CanWrite => this._innerStream.CanWrite;

    /// <summary>Flush the stream.</summary>
    public override void Flush() => this._innerStream.Flush();

    /// <summary>Not implemented.</summary>
    public override long Length
    {
      get
      {
        return this._lengthLimit == CrcCalculatorStream.UnsetLengthLimit ? this._innerStream.Length : this._lengthLimit;
      }
    }

    /// <summary>Not implemented.</summary>
    public override long Position
    {
      get => this._Crc32.TotalBytesRead;
      set => throw new NotImplementedException();
    }

    /// <summary>Not implemented.</summary>
    /// <param name="offset">N/A</param>
    /// <param name="origin">N/A</param>
    /// <returns>N/A</returns>
    public override long Seek(long offset, SeekOrigin origin)
    {
      throw new NotImplementedException();
    }

    /// <summary>Not implemented.</summary>
    /// <param name="value">N/A</param>
    public override void SetLength(long value) => throw new NotImplementedException();

    void IDisposable.Dispose() => this.Close();

    /// <summary>Closes the stream.</summary>
    public override void Close()
    {
      base.Close();
      if (this._leaveOpen)
        return;
      this._innerStream.Close();
    }
  }
}
