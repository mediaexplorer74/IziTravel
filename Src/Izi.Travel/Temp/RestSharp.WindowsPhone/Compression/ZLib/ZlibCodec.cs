// Decompiled with JetBrains decompiler
// Type: RestSharp.Compression.ZLib.ZlibCodec
// Assembly: RestSharp.WindowsPhone, Version=105.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EBEB4E14-2430-4192-BEF2-49D8C49DBCCE
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.xml

#nullable disable
namespace RestSharp.Compression.ZLib
{
  /// <summary>
  /// Encoder and Decoder for ZLIB and DEFLATE (IETF RFC1950 and RFC1951).
  /// </summary>
  /// <remarks>
  /// This class compresses and decompresses data according to the Deflate algorithm
  /// and optionally, the ZLIB format, as documented in <see href="http://www.ietf.org/rfc/rfc1950.txt">RFC 1950 - ZLIB</see> and <see href="http://www.ietf.org/rfc/rfc1951.txt">RFC 1951 - DEFLATE</see>.
  /// </remarks>
  internal sealed class ZlibCodec
  {
    /// <summary>The buffer from which data is taken.</summary>
    public byte[] InputBuffer;
    /// <summary>
    /// An index into the InputBuffer array, indicating where to start reading.
    /// </summary>
    public int NextIn;
    /// <summary>
    /// The number of bytes available in the InputBuffer, starting at NextIn.
    /// </summary>
    /// <remarks>
    /// Generally you should set this to InputBuffer.Length before the first Inflate() or Deflate() call.
    /// The class will update this number as calls to Inflate/Deflate are made.
    /// </remarks>
    public int AvailableBytesIn;
    /// <summary>
    /// Total number of bytes read so far, through all calls to Inflate()/Deflate().
    /// </summary>
    public long TotalBytesIn;
    /// <summary>Buffer to store output data.</summary>
    public byte[] OutputBuffer;
    /// <summary>
    /// An index into the OutputBuffer array, indicating where to start writing.
    /// </summary>
    public int NextOut;
    /// <summary>
    /// The number of bytes available in the OutputBuffer, starting at NextOut.
    /// </summary>
    /// <remarks>
    /// Generally you should set this to OutputBuffer.Length before the first Inflate() or Deflate() call.
    /// The class will update this number as calls to Inflate/Deflate are made.
    /// </remarks>
    public int AvailableBytesOut;
    /// <summary>
    /// Total number of bytes written to the output so far, through all calls to Inflate()/Deflate().
    /// </summary>
    public long TotalBytesOut;
    /// <summary>used for diagnostics, when something goes wrong!</summary>
    public string Message;
    internal InflateManager istate;
    internal long _Adler32;
    /// <summary>The number of Window Bits to use.</summary>
    /// <remarks>
    /// This gauges the size of the sliding window, and hence the
    /// compression effectiveness as well as memory consumption. It's best to just leave this
    /// setting alone if you don't know what it is.  The maximum value is 15 bits, which implies
    /// a 32k window.
    /// </remarks>
    public int WindowBits = 15;

    /// <summary>
    /// The Adler32 checksum on the data transferred through the codec so far. You probably don't need to look at this.
    /// </summary>
    public long Adler32 => this._Adler32;

    /// <summary>Create a ZlibCodec that decompresses.</summary>
    public ZlibCodec()
    {
      if (this.InitializeInflate() != 0)
        throw new ZlibException("Cannot initialize for inflate.");
    }

    /// <summary>Initialize the inflation state.</summary>
    /// <remarks>
    /// It is not necessary to call this before using the ZlibCodec to inflate data;
    /// It is implicitly called when you call the constructor.
    /// </remarks>
    /// <returns>Z_OK if everything goes well.</returns>
    public int InitializeInflate() => this.InitializeInflate(this.WindowBits);

    /// <summary>
    /// Initialize the inflation state with an explicit flag to
    /// govern the handling of RFC1950 header bytes.
    /// </summary>
    /// <remarks>
    /// By default, the ZLIB header defined in <see href="http://www.ietf.org/rfc/rfc1950.txt">RFC 1950</see> is expected.  If
    /// you want to read a zlib stream you should specify true for
    /// expectRfc1950Header.  If you have a deflate stream, you will want to specify
    /// false. It is only necessary to invoke this initializer explicitly if you
    /// want to specify false.
    /// </remarks>
    /// <param name="expectRfc1950Header">whether to expect an RFC1950 header byte
    /// pair when reading the stream of data to be inflated.</param>
    /// <returns>Z_OK if everything goes well.</returns>
    public int InitializeInflate(bool expectRfc1950Header)
    {
      return this.InitializeInflate(this.WindowBits, expectRfc1950Header);
    }

    /// <summary>
    /// Initialize the ZlibCodec for inflation, with the specified number of window bits.
    /// </summary>
    /// <param name="windowBits">The number of window bits to use. If you need to ask what that is,
    /// then you shouldn't be calling this initializer.</param>
    /// <returns>Z_OK if all goes well.</returns>
    public int InitializeInflate(int windowBits)
    {
      this.WindowBits = windowBits;
      return this.InitializeInflate(windowBits, true);
    }

    /// <summary>
    /// Initialize the inflation state with an explicit flag to govern the handling of
    /// RFC1950 header bytes.
    /// </summary>
    /// <remarks>
    /// If you want to read a zlib stream you should specify true for
    /// expectRfc1950Header. In this case, the library will expect to find a ZLIB
    /// header, as defined in <see href="http://www.ietf.org/rfc/rfc1950.txt">RFC
    /// 1950</see>, in the compressed stream.  If you will be reading a DEFLATE or
    /// GZIP stream, which does not have such a header, you will want to specify
    /// false.
    /// </remarks>
    /// <param name="expectRfc1950Header">whether to expect an RFC1950 header byte pair when reading
    /// the stream of data to be inflated.</param>
    /// <param name="windowBits">The number of window bits to use. If you need to ask what that is,
    /// then you shouldn't be calling this initializer.</param>
    /// <returns>Z_OK if everything goes well.</returns>
    public int InitializeInflate(int windowBits, bool expectRfc1950Header)
    {
      this.WindowBits = windowBits;
      this.istate = new InflateManager(expectRfc1950Header);
      return this.istate.Initialize(this, windowBits);
    }

    /// <summary>
    /// Inflate the data in the InputBuffer, placing the result in the OutputBuffer.
    /// </summary>
    /// <remarks>
    /// You must have set InputBuffer and OutputBuffer, NextIn and NextOut, and AvailableBytesIn and
    /// AvailableBytesOut  before calling this method.
    /// </remarks>
    /// <example>
    /// <code>
    /// private void InflateBuffer()
    /// {
    ///     int bufferSize = 1024;
    ///     byte[] buffer = new byte[bufferSize];
    ///     ZlibCodec decompressor = new ZlibCodec();
    /// 
    ///     Console.WriteLine("\n============================================");
    ///     Console.WriteLine("Size of Buffer to Inflate: {0} bytes.", CompressedBytes.Length);
    ///     MemoryStream ms = new MemoryStream(DecompressedBytes);
    /// 
    ///     int rc = decompressor.InitializeInflate();
    /// 
    ///     decompressor.InputBuffer = CompressedBytes;
    ///     decompressor.NextIn = 0;
    ///     decompressor.AvailableBytesIn = CompressedBytes.Length;
    /// 
    ///     decompressor.OutputBuffer = buffer;
    /// 
    ///     // pass 1: inflate
    ///     do
    ///     {
    ///         decompressor.NextOut = 0;
    ///         decompressor.AvailableBytesOut = buffer.Length;
    ///         rc = decompressor.Inflate(ZlibConstants.Z_NO_FLUSH);
    /// 
    ///         if (rc != ZlibConstants.Z_OK &amp;&amp; rc != ZlibConstants.Z_STREAM_END)
    ///             throw new Exception("inflating: " + decompressor.Message);
    /// 
    ///         ms.Write(decompressor.OutputBuffer, 0, buffer.Length - decompressor.AvailableBytesOut);
    ///     }
    ///     while (decompressor.AvailableBytesIn &gt; 0 || decompressor.AvailableBytesOut == 0);
    /// 
    ///     // pass 2: finish and flush
    ///     do
    ///     {
    ///         decompressor.NextOut = 0;
    ///         decompressor.AvailableBytesOut = buffer.Length;
    ///         rc = decompressor.Inflate(ZlibConstants.Z_FINISH);
    /// 
    ///         if (rc != ZlibConstants.Z_STREAM_END &amp;&amp; rc != ZlibConstants.Z_OK)
    ///             throw new Exception("inflating: " + decompressor.Message);
    /// 
    ///         if (buffer.Length - decompressor.AvailableBytesOut &gt; 0)
    ///             ms.Write(buffer, 0, buffer.Length - decompressor.AvailableBytesOut);
    ///     }
    ///     while (decompressor.AvailableBytesIn &gt; 0 || decompressor.AvailableBytesOut == 0);
    /// 
    ///     decompressor.EndInflate();
    /// }
    /// 
    /// </code>
    /// </example>
    /// <param name="flush">The flush to use when inflating.</param>
    /// <returns>Z_OK if everything goes well.</returns>
    public int Inflate(FlushType flush)
    {
      return this.istate != null ? this.istate.Inflate(flush) : throw new ZlibException("No Inflate State!");
    }

    /// <summary>Ends an inflation session.</summary>
    /// <remarks>
    /// Call this after successively calling Inflate().  This will cause all buffers to be flushed.
    /// After calling this you cannot call Inflate() without a intervening call to one of the
    /// InitializeInflate() overloads.
    /// </remarks>
    /// <returns>Z_OK if everything goes well.</returns>
    public int EndInflate()
    {
      int num = this.istate != null ? this.istate.End() : throw new ZlibException("No Inflate State!");
      this.istate = (InflateManager) null;
      return num;
    }

    /// <summary>I don't know what this does!</summary>
    /// <returns>Z_OK if everything goes well.</returns>
    public int SyncInflate()
    {
      return this.istate != null ? this.istate.Sync() : throw new ZlibException("No Inflate State!");
    }

    /// <summary>
    /// Set the dictionary to be used for either Inflation or Deflation.
    /// </summary>
    /// <param name="dictionary">The dictionary bytes to use.</param>
    /// <returns>Z_OK if all goes well.</returns>
    public int SetDictionary(byte[] dictionary)
    {
      return this.istate != null ? this.istate.SetDictionary(dictionary) : throw new ZlibException("No Inflate state!");
    }
  }
}
