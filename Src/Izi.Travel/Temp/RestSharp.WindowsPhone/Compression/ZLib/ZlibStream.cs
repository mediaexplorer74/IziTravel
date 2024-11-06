// Decompiled with JetBrains decompiler
// Type: RestSharp.Compression.ZLib.ZlibStream
// Assembly: RestSharp.WindowsPhone, Version=105.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EBEB4E14-2430-4192-BEF2-49D8C49DBCCE
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.xml

using System;
using System.IO;
using System.Text;

#nullable disable
namespace RestSharp.Compression.ZLib
{
  /// <summary>
  /// Represents a Zlib stream for compression or decompression.
  /// </summary>
  /// <remarks>
  /// 
  /// <para>
  /// The ZlibStream is a <see href="http://en.wikipedia.org/wiki/Decorator_pattern">Decorator</see> on a <see cref="T:System.IO.Stream" />.  It adds ZLIB compression or decompression to any
  /// stream.
  /// </para>
  /// 
  /// <para> Using this stream, applications can compress or decompress data via
  /// stream <c>Read</c> and <c>Write</c> operations.  Either compresssion or
  /// decompression can occur through either reading or writing. The compression
  /// format used is ZLIB, which is documented in <see href="http://www.ietf.org/rfc/rfc1950.txt">IETF RFC 1950</see>, "ZLIB Compressed
  /// Data Format Specification version 3.3". This implementation of ZLIB always uses
  /// DEFLATE as the compression method.  (see <see href="http://www.ietf.org/rfc/rfc1951.txt">IETF RFC 1951</see>, "DEFLATE
  /// Compressed Data Format Specification version 1.3.") </para>
  /// 
  /// <para>
  /// The ZLIB format allows for varying compression methods, window sizes, and dictionaries.
  /// This implementation always uses the DEFLATE compression method, a preset dictionary,
  /// and 15 window bits by default.
  /// </para>
  /// 
  /// <para>
  /// This class is similar to <see cref="!:DeflateStream" />, except that it adds the
  /// RFC1950 header and trailer bytes to a compressed stream when compressing, or expects
  /// the RFC1950 header and trailer bytes when decompressing.  It is also similar to the
  /// <see cref="T:RestSharp.Compression.ZLib.GZipStream" />.
  /// </para>
  /// </remarks>
  /// <seealso cref="!:DeflateStream" />
  /// <seealso cref="T:RestSharp.Compression.ZLib.GZipStream" />
  internal class ZlibStream : Stream
  {
    internal ZlibBaseStream _baseStream;
    private bool _disposed;

    public ZlibStream(Stream stream)
    {
      this._baseStream = new ZlibBaseStream(stream, ZlibStreamFlavor.ZLIB, false);
    }

    /// <summary>
    /// This property sets the flush behavior on the stream.
    /// Sorry, though, not sure exactly how to describe all the various settings.
    /// </summary>
    public virtual FlushType FlushMode
    {
      get => this._baseStream._flushMode;
      set
      {
        if (this._disposed)
          throw new ObjectDisposedException(nameof (ZlibStream));
        this._baseStream._flushMode = value;
      }
    }

    /// <summary>
    /// The size of the working buffer for the compression codec.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The working buffer is used for all stream operations.  The default size is 1024 bytes.
    /// The minimum size is 128 bytes. You may get better performance with a larger buffer.
    /// Then again, you might not.  You would have to test it.
    /// </para>
    /// 
    /// <para>
    /// Set this before the first call to Read()  or Write() on the stream. If you try to set it
    /// afterwards, it will throw.
    /// </para>
    /// </remarks>
    public int BufferSize
    {
      get => this._baseStream._bufferSize;
      set
      {
        if (this._disposed)
          throw new ObjectDisposedException(nameof (ZlibStream));
        if (this._baseStream._workingBuffer != null)
          throw new ZlibException("The working buffer is already set.");
        this._baseStream._bufferSize = value >= 128 ? value : throw new ZlibException(string.Format("Don't be silly. {0} bytes?? Use a bigger buffer.", (object) value));
      }
    }

    /// <summary> Returns the total number of bytes input so far.</summary>
    public virtual long TotalIn => this._baseStream._z.TotalBytesIn;

    /// <summary> Returns the total number of bytes output so far.</summary>
    public virtual long TotalOut => this._baseStream._z.TotalBytesOut;

    /// <summary>Dispose the stream.</summary>
    /// <remarks>
    /// This may or may not result in a Close() call on the captive stream.
    /// See the constructors that have a leaveOpen parameter for more information.
    /// </remarks>
    protected override void Dispose(bool disposing)
    {
      try
      {
        if (this._disposed)
          return;
        if (disposing && this._baseStream != null)
          this._baseStream.Close();
        this._disposed = true;
      }
      finally
      {
        base.Dispose(disposing);
      }
    }

    /// <summary>Indicates whether the stream can be read.</summary>
    /// <remarks>
    /// The return value depends on whether the captive stream supports reading.
    /// </remarks>
    public override bool CanRead
    {
      get
      {
        if (this._disposed)
          throw new ObjectDisposedException(nameof (ZlibStream));
        return this._baseStream._stream.CanRead;
      }
    }

    /// <summary>Indicates whether the stream supports Seek operations.</summary>
    /// <remarks>Always returns false.</remarks>
    public override bool CanSeek => false;

    /// <summary>Indicates whether the stream can be written.</summary>
    /// <remarks>
    /// The return value depends on whether the captive stream supports writing.
    /// </remarks>
    public override bool CanWrite
    {
      get
      {
        if (this._disposed)
          throw new ObjectDisposedException(nameof (ZlibStream));
        return this._baseStream._stream.CanWrite;
      }
    }

    /// <summary>Flush the stream.</summary>
    public override void Flush()
    {
      if (this._disposed)
        throw new ObjectDisposedException(nameof (ZlibStream));
      this._baseStream.Flush();
    }

    /// <summary>
    /// Reading this property always throws a NotImplementedException.
    /// </summary>
    public override long Length => throw new NotImplementedException();

    /// <summary>The position of the stream pointer.</summary>
    /// <remarks>
    /// Writing this property always throws a NotImplementedException. Reading will
    /// return the total bytes written out, if used in writing, or the total bytes
    /// read in, if used in reading.   The count may refer to compressed bytes or
    /// uncompressed bytes, depending on how you've used the stream.
    /// </remarks>
    public override long Position
    {
      get
      {
        if (this._baseStream._streamMode == ZlibBaseStream.StreamMode.Writer)
          return this._baseStream._z.TotalBytesOut;
        return this._baseStream._streamMode == ZlibBaseStream.StreamMode.Reader ? this._baseStream._z.TotalBytesIn : 0L;
      }
      set => throw new NotImplementedException();
    }

    /// <summary>Read data from the stream.</summary>
    /// <remarks>
    /// 
    /// <para>
    /// If you wish to use the ZlibStream to compress data while reading, you can create a
    /// ZlibStream with CompressionMode.Compress, providing an uncompressed data stream.  Then
    /// call Read() on that ZlibStream, and the data read will be compressed.  If you wish to
    /// use the ZlibStream to decompress data while reading, you can create a ZlibStream with
    /// CompressionMode.Decompress, providing a readable compressed data stream.  Then call
    /// Read() on that ZlibStream, and the data will be decompressed as it is read.
    /// </para>
    /// 
    /// <para>
    /// A ZlibStream can be used for Read() or Write(), but not both.
    /// </para>
    /// </remarks>
    /// <param name="buffer">The buffer into which the read data should be placed.</param>
    /// <param name="offset">the offset within that data array to put the first byte read.</param>
    /// <param name="count">the number of bytes to read.</param>
    public override int Read(byte[] buffer, int offset, int count)
    {
      if (this._disposed)
        throw new ObjectDisposedException(nameof (ZlibStream));
      return this._baseStream.Read(buffer, offset, count);
    }

    /// <summary>
    /// Calling this method always throws a NotImplementedException.
    /// </summary>
    public override long Seek(long offset, SeekOrigin origin)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// Calling this method always throws a NotImplementedException.
    /// </summary>
    public override void SetLength(long value) => throw new NotImplementedException();

    /// <summary>Write data to the stream.</summary>
    /// <remarks>
    /// 
    /// <para>
    /// If you wish to use the ZlibStream to compress data while writing, you can create a
    /// ZlibStream with CompressionMode.Compress, and a writable output stream.  Then call
    /// Write() on that ZlibStream, providing uncompressed data as input.  The data sent to
    /// the output stream will be the compressed form of the data written.  If you wish to use
    /// the ZlibStream to decompress data while writing, you can create a ZlibStream with
    /// CompressionMode.Decompress, and a writable output stream.  Then call Write() on that
    /// stream, providing previously compressed data. The data sent to the output stream will
    /// be the decompressed form of the data written.
    /// </para>
    /// 
    /// <para>
    /// A ZlibStream can be used for Read() or Write(), but not both.
    /// </para>
    /// </remarks>
    /// <param name="buffer">The buffer holding data to write to the stream.</param>
    /// <param name="offset">the offset within that data array to find the first byte to write.</param>
    /// <param name="count">the number of bytes to write.</param>
    public override void Write(byte[] buffer, int offset, int count)
    {
      if (this._disposed)
        throw new ObjectDisposedException(nameof (ZlibStream));
      this._baseStream.Write(buffer, offset, count);
    }

    /// <summary>Uncompress a byte array into a single string.</summary>
    /// <seealso cref="!:ZlibStream.CompressString(String)" />
    /// <param name="compressed">
    /// A buffer containing ZLIB-compressed data.
    /// </param>
    public static string UncompressString(byte[] compressed)
    {
      byte[] buffer = new byte[1024];
      Encoding utF8 = Encoding.UTF8;
      using (MemoryStream memoryStream1 = new MemoryStream())
      {
        using (MemoryStream memoryStream2 = new MemoryStream(compressed))
        {
          using (Stream stream = (Stream) new ZlibStream((Stream) memoryStream2))
          {
            int count;
            while ((count = stream.Read(buffer, 0, buffer.Length)) != 0)
              memoryStream1.Write(buffer, 0, count);
          }
          memoryStream1.Seek(0L, SeekOrigin.Begin);
          return new StreamReader((Stream) memoryStream1, utF8).ReadToEnd();
        }
      }
    }

    /// <summary>Uncompress a byte array into a byte array.</summary>
    /// <seealso cref="!:ZlibStream.CompressBuffer(byte[])" />
    /// <seealso cref="M:RestSharp.Compression.ZLib.ZlibStream.UncompressString(System.Byte[])" />
    /// <param name="compressed">
    /// A buffer containing ZLIB-compressed data.
    /// </param>
    public static byte[] UncompressBuffer(byte[] compressed)
    {
      byte[] buffer = new byte[1024];
      using (MemoryStream memoryStream1 = new MemoryStream())
      {
        using (MemoryStream memoryStream2 = new MemoryStream(compressed))
        {
          using (Stream stream = (Stream) new ZlibStream((Stream) memoryStream2))
          {
            int count;
            while ((count = stream.Read(buffer, 0, buffer.Length)) != 0)
              memoryStream1.Write(buffer, 0, count);
          }
          return memoryStream1.ToArray();
        }
      }
    }
  }
}
