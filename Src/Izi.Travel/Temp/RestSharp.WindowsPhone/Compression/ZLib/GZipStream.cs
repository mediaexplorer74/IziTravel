// Decompiled with JetBrains decompiler
// Type: RestSharp.Compression.ZLib.GZipStream
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
  /// A class for compressing and decompressing GZIP streams.
  /// </summary>
  /// <remarks>
  /// 
  /// <para>
  /// The GZipStream is a <see href="http://en.wikipedia.org/wiki/Decorator_pattern">Decorator</see> on a <see cref="T:System.IO.Stream" />.  It adds GZIP compression or decompression to any stream.
  /// </para>
  /// 
  /// <para> Like the <c>Compression.GZipStream</c> in the .NET Base
  /// Class Library, the Ionic.Zlib.GZipStream can compress while writing, or decompress
  /// while reading, but not vice versa.  The compression method used is GZIP, which is
  /// documented in <see href="http://www.ietf.org/rfc/rfc1952.txt">IETF RFC 1952</see>,
  /// "GZIP file format specification version 4.3".</para>
  /// 
  /// <para> A GZipStream can be used to decompress data (through Read()) or to compress
  /// data (through Write()), but not both.  </para>
  /// 
  /// <para> If you wish to use the GZipStream to compress data, you must wrap it around a
  /// write-able stream. As you call Write() on the GZipStream, the data will be
  /// compressed into the GZIP format.  If you want to decompress data, you must wrap the
  /// GZipStream around a readable stream that contains an IETF RFC 1952-compliant stream.
  /// The data will be decompressed as you call Read() on the GZipStream.  </para>
  /// 
  /// <para> Though the GZIP format allows data from multiple files to be concatenated
  /// together, this stream handles only a single segment of GZIP format, typically
  /// representing a single file.  </para>
  /// 
  /// <para>
  /// This class is similar to <see cref="T:RestSharp.Compression.ZLib.ZlibStream" /> and <see cref="!:DeflateStream" />.
  /// <c>ZlibStream</c> handles RFC1950-compliant streams.  <see cref="!:DeflateStream" />
  /// handles RFC1951-compliant streams. This class handles RFC1952-compliant streams.
  /// </para>
  /// 
  /// </remarks>
  /// <seealso cref="!:DeflateStream" />
  /// <seealso cref="T:RestSharp.Compression.ZLib.ZlibStream" />
  internal class GZipStream : Stream
  {
    /// <summary>The last modified time for the GZIP stream.</summary>
    /// <remarks> GZIP allows the storage of a last modified time with each GZIP entry.
    /// When compressing data, you can set this before the first call to Write().  When
    /// decompressing, you can retrieve this value any time after the first call to
    /// Read().  </remarks>
    public DateTime? LastModified;
    internal ZlibBaseStream _baseStream;
    private bool _disposed;
    private bool _firstReadDone;
    private string _FileName;
    private string _Comment;
    private int _Crc32;
    internal static DateTime _unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    internal static Encoding iso8859dash1 = Encoding.GetEncoding("iso-8859-1");

    /// <summary>The Comment on the GZIP stream.</summary>
    /// <remarks>
    /// <para>
    /// The GZIP format allows for each file to optionally have an associated comment stored with the
    /// file.  The comment is encoded with the ISO-8859-1 code page.  To include a comment in
    /// a GZIP stream you create, set this property before calling Write() for the first time
    /// on the GZipStream.
    /// </para>
    /// 
    /// <para>
    /// When using GZipStream to decompress, you can retrieve this property after the first
    /// call to Read().  If no comment has been set in the GZIP bytestream, the Comment
    /// property will return null (Nothing in VB).
    /// </para>
    /// </remarks>
    public string Comment
    {
      get => this._Comment;
      set
      {
        if (this._disposed)
          throw new ObjectDisposedException(nameof (GZipStream));
        this._Comment = value;
      }
    }

    /// <summary>The FileName for the GZIP stream.</summary>
    /// <remarks>
    /// <para>
    /// The GZIP format optionally allows each file to have an associated filename.  When
    /// compressing data (through Write()), set this FileName before calling Write() the first
    /// time on the GZipStream.  The actual filename is encoded into the GZIP bytestream with
    /// the ISO-8859-1 code page, according to RFC 1952. It is the application's responsibility to
    /// insure that the FileName can be encoded and decoded correctly with this code page.
    /// </para>
    /// <para>
    /// When decompressing (through Read()), you can retrieve this value any time after the
    /// first Read().  In the case where there was no filename encoded into the GZIP
    /// bytestream, the property will return null (Nothing in VB).
    /// </para>
    /// </remarks>
    public string FileName
    {
      get => this._FileName;
      set
      {
        if (this._disposed)
          throw new ObjectDisposedException(nameof (GZipStream));
        this._FileName = value;
        if (this._FileName == null)
          return;
        if (this._FileName.IndexOf("/") != -1)
          this._FileName = this._FileName.Replace("/", "\\");
        if (this._FileName.EndsWith("\\"))
          throw new Exception("Illegal filename");
        if (this._FileName.IndexOf("\\") == -1)
          return;
        this._FileName = Path.GetFileName(this._FileName);
      }
    }

    /// <summary>The CRC on the GZIP stream.</summary>
    /// <remarks>
    /// This is used for internal error checking. You probably don't need to look at this property.
    /// </remarks>
    public int Crc32 => this._Crc32;

    /// <summary>
    /// Create a GZipStream using the specified CompressionMode and the specified CompressionLevel,
    /// and explicitly specify whether the stream should be left open after Deflation or Inflation.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This constructor allows the application to request that the captive stream remain open after
    /// the deflation or inflation occurs.  By default, after Close() is called on the stream, the
    /// captive stream is also closed. In some cases this is not desired, for example if the stream
    /// is a memory stream that will be re-read after compressed data has been written to it.  Specify true for the
    /// leaveOpen parameter to leave the stream open.
    /// </para>
    /// <para>
    /// As noted in the class documentation,
    /// the CompressionMode (Compress or Decompress) also establishes the "direction" of the stream.
    /// A GZipStream with CompressionMode.Compress works only through Write().  A GZipStream with
    /// CompressionMode.Decompress works only through Read().
    /// </para>
    /// </remarks>
    /// <example>
    /// This example shows how to use a DeflateStream to compress data.
    /// <code>
    /// using (System.IO.Stream input = System.IO.File.OpenRead(fileToCompress))
    /// {
    ///     using (var raw = System.IO.File.Create(outputFile))
    ///     {
    ///         using (Stream compressor = new GZipStream(raw, CompressionMode.Compress, CompressionLevel.BestCompression, true))
    ///         {
    ///             byte[] buffer = new byte[WORKING_BUFFER_SIZE];
    ///             int n;
    ///             while ((n= input.Read(buffer, 0, buffer.Length)) != 0)
    ///             {
    ///                 compressor.Write(buffer, 0, n);
    ///             }
    ///         }
    ///     }
    /// }
    /// </code>
    /// <code lang="VB">
    /// Dim outputFile As String = (fileToCompress &amp; ".compressed")
    /// Using input As Stream = File.OpenRead(fileToCompress)
    ///     Using raw As FileStream = File.Create(outputFile)
    ///     Using compressor As Stream = New GZipStream(raw, CompressionMode.Compress, CompressionLevel.BestCompression, True)
    ///         Dim buffer As Byte() = New Byte(4096) {}
    ///         Dim n As Integer = -1
    ///         Do While (n &lt;&gt; 0)
    ///             If (n &gt; 0) Then
    ///                 compressor.Write(buffer, 0, n)
    ///             End If
    ///             n = input.Read(buffer, 0, buffer.Length)
    ///         Loop
    ///     End Using
    ///     End Using
    /// End Using
    /// </code>
    /// </example>
    /// <param name="stream">The stream which will be read or written.</param>
    /// <param name="mode">Indicates whether the GZipStream will compress or decompress.</param>
    /// <param name="leaveOpen">true if the application would like the stream to remain open after inflation/deflation.</param>
    /// <param name="level">A tuning knob to trade speed for effectiveness.</param>
    public GZipStream(Stream stream)
    {
      this._baseStream = new ZlibBaseStream(stream, ZlibStreamFlavor.GZIP, false);
    }

    /// <summary>This property sets the flush behavior on the stream.</summary>
    public virtual FlushType FlushMode
    {
      get => this._baseStream._flushMode;
      set
      {
        if (this._disposed)
          throw new ObjectDisposedException(nameof (GZipStream));
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
          throw new ObjectDisposedException(nameof (GZipStream));
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
    /// See the ctor's with leaveOpen parameters for more information.
    /// </remarks>
    protected override void Dispose(bool disposing)
    {
      try
      {
        if (this._disposed)
          return;
        if (disposing && this._baseStream != null)
        {
          this._baseStream.Close();
          this._Crc32 = this._baseStream.Crc32;
        }
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
          throw new ObjectDisposedException(nameof (GZipStream));
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
          throw new ObjectDisposedException(nameof (GZipStream));
        return this._baseStream._stream.CanWrite;
      }
    }

    /// <summary>Flush the stream.</summary>
    public override void Flush()
    {
      if (this._disposed)
        throw new ObjectDisposedException(nameof (GZipStream));
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
        return this._baseStream._streamMode == ZlibBaseStream.StreamMode.Reader ? this._baseStream._z.TotalBytesIn + (long) this._baseStream._gzipHeaderByteCount : 0L;
      }
      set => throw new NotImplementedException();
    }

    /// <summary>Read and decompress data from the source stream.</summary>
    /// <remarks>
    /// With a GZipStream, decompression is done through reading.
    /// </remarks>
    /// <example>
    /// <code>
    /// byte[] working = new byte[WORKING_BUFFER_SIZE];
    /// using (System.IO.Stream input = System.IO.File.OpenRead(_CompressedFile))
    /// {
    ///     using (Stream decompressor= new Ionic.Zlib.GZipStream(input, CompressionMode.Decompress, true))
    ///     {
    ///         using (var output = System.IO.File.Create(_DecompressedFile))
    ///         {
    ///             int n;
    ///             while ((n= decompressor.Read(working, 0, working.Length)) !=0)
    ///             {
    ///                 output.Write(working, 0, n);
    ///             }
    ///         }
    ///     }
    /// }
    /// </code>
    /// </example>
    /// <param name="buffer">The buffer into which the decompressed data should be placed.</param>
    /// <param name="offset">the offset within that data array to put the first byte read.</param>
    /// <param name="count">the number of bytes to read.</param>
    /// <returns>the number of bytes actually read</returns>
    public override int Read(byte[] buffer, int offset, int count)
    {
      if (this._disposed)
        throw new ObjectDisposedException(nameof (GZipStream));
      int num = this._baseStream.Read(buffer, offset, count);
      if (!this._firstReadDone)
      {
        this._firstReadDone = true;
        this.FileName = this._baseStream._GzipFileName;
        this.Comment = this._baseStream._GzipComment;
      }
      return num;
    }

    /// <summary>
    /// Calling this method always throws a <see cref="T:System.NotImplementedException" />.
    /// </summary>
    /// <param name="offset">irrelevant; it will always throw!</param>
    /// <param name="origin">irrelevant; it will always throw!</param>
    /// <returns>irrelevant!</returns>
    public override long Seek(long offset, SeekOrigin origin)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// Calling this method always throws a NotImplementedException.
    /// </summary>
    /// <param name="value">irrelevant; this method will always throw!</param>
    public override void SetLength(long value) => throw new NotImplementedException();

    private int EmitHeader()
    {
      byte[] bytes1 = this.Comment == null ? (byte[]) null : GZipStream.iso8859dash1.GetBytes(this.Comment);
      byte[] bytes2 = this.FileName == null ? (byte[]) null : GZipStream.iso8859dash1.GetBytes(this.FileName);
      int num1 = this.Comment == null ? 0 : bytes1.Length + 1;
      int num2 = this.FileName == null ? 0 : bytes2.Length + 1;
      byte[] numArray1 = new byte[10 + num1 + num2];
      int num3 = 0;
      byte[] numArray2 = numArray1;
      int index1 = num3;
      int num4 = index1 + 1;
      numArray2[index1] = (byte) 31;
      byte[] numArray3 = numArray1;
      int index2 = num4;
      int num5 = index2 + 1;
      numArray3[index2] = (byte) 139;
      byte[] numArray4 = numArray1;
      int index3 = num5;
      int num6 = index3 + 1;
      numArray4[index3] = (byte) 8;
      byte num7 = 0;
      if (this.Comment != null)
        num7 ^= (byte) 16;
      if (this.FileName != null)
        num7 ^= (byte) 8;
      byte[] numArray5 = numArray1;
      int index4 = num6;
      int destinationIndex1 = index4 + 1;
      int num8 = (int) num7;
      numArray5[index4] = (byte) num8;
      if (!this.LastModified.HasValue)
        this.LastModified = new DateTime?(DateTime.Now);
      Array.Copy((Array) BitConverter.GetBytes((int) (this.LastModified.Value - GZipStream._unixEpoch).TotalSeconds), 0, (Array) numArray1, destinationIndex1, 4);
      int num9 = destinationIndex1 + 4;
      byte[] numArray6 = numArray1;
      int index5 = num9;
      int num10 = index5 + 1;
      numArray6[index5] = (byte) 0;
      byte[] numArray7 = numArray1;
      int index6 = num10;
      int destinationIndex2 = index6 + 1;
      numArray7[index6] = byte.MaxValue;
      if (num2 != 0)
      {
        Array.Copy((Array) bytes2, 0, (Array) numArray1, destinationIndex2, num2 - 1);
        int num11 = destinationIndex2 + (num2 - 1);
        byte[] numArray8 = numArray1;
        int index7 = num11;
        destinationIndex2 = index7 + 1;
        numArray8[index7] = (byte) 0;
      }
      if (num1 != 0)
      {
        Array.Copy((Array) bytes1, 0, (Array) numArray1, destinationIndex2, num1 - 1);
        int num12 = destinationIndex2 + (num1 - 1);
        byte[] numArray9 = numArray1;
        int index8 = num12;
        int num13 = index8 + 1;
        numArray9[index8] = (byte) 0;
      }
      this._baseStream._stream.Write(numArray1, 0, numArray1.Length);
      return numArray1.Length;
    }

    public override void Write(byte[] buffer, int offset, int count)
    {
      throw new NotImplementedException();
    }
  }
}
