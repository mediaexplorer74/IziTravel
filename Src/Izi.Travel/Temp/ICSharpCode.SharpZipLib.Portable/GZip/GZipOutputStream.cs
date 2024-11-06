// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.GZip.GZipOutputStream
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51802, Culture=neutral, PublicKeyToken=null
// MVID: 6C361F8B-A5CA-49B8-ABC1-A50EE493C003
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.xml

using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Zip.Compression;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using System;
using System.IO;

#nullable disable
namespace ICSharpCode.SharpZipLib.GZip
{
  /// <summary>
  /// This filter stream is used to compress a stream into a "GZIP" stream.
  /// The "GZIP" format is described in RFC 1952.
  /// 
  /// author of the original java version : John Leuner
  /// </summary>
  /// <example> This sample shows how to gzip a file
  /// <code>
  /// using System;
  /// using System.IO;
  /// 
  /// using ICSharpCode.SharpZipLib.GZip;
  /// using ICSharpCode.SharpZipLib.Core;
  /// 
  /// class MainClass
  /// {
  /// 	public static void Main(string[] args)
  /// 	{
  /// 			using (Stream s = new GZipOutputStream(File.Create(args[0] + ".gz")))
  /// 			using (FileStream fs = File.OpenRead(args[0])) {
  /// 				byte[] writeData = new byte[4096];
  /// 				Streamutils.Copy(s, fs, writeData);
  /// 			}
  /// 		}
  /// 	}
  /// }
  /// </code>
  /// </example>
  public class GZipOutputStream : DeflaterOutputStream
  {
    /// <summary>CRC-32 value for uncompressed data</summary>
    protected Crc32 crc = new Crc32();
    private GZipOutputStream.OutputState state_;

    /// <summary>Creates a GzipOutputStream with the default buffer size</summary>
    /// <param name="baseOutputStream">
    /// The stream to read data (to be compressed) from
    /// </param>
    public GZipOutputStream(Stream baseOutputStream)
      : this(baseOutputStream, 4096)
    {
    }

    /// <summary>
    /// Creates a GZipOutputStream with the specified buffer size
    /// </summary>
    /// <param name="baseOutputStream">
    /// The stream to read data (to be compressed) from
    /// </param>
    /// <param name="size">Size of the buffer to use</param>
    public GZipOutputStream(Stream baseOutputStream, int size)
      : base(baseOutputStream, new Deflater(-1, true), size)
    {
    }

    /// <summary>
    /// Sets the active compression level (1-9).  The new level will be activated
    /// immediately.
    /// </summary>
    /// <param name="level">The compression level to set.</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// Level specified is not supported.
    /// </exception>
    /// <see cref="T:ICSharpCode.SharpZipLib.Zip.Compression.Deflater" />
    public void SetLevel(int level)
    {
      if (level < 1)
        throw new ArgumentOutOfRangeException(nameof (level));
      this.deflater_.SetLevel(level);
    }

    /// <summary>Get the current compression level.</summary>
    /// <returns>The current compression level.</returns>
    public int GetLevel() => this.deflater_.GetLevel();

    /// <summary>Write given buffer to output updating crc</summary>
    /// <param name="buffer">Buffer to write</param>
    /// <param name="offset">Offset of first byte in buf to write</param>
    /// <param name="count">Number of bytes to write</param>
    public override void Write(byte[] buffer, int offset, int count)
    {
      if (this.state_ == GZipOutputStream.OutputState.Header)
        this.WriteHeader();
      if (this.state_ != GZipOutputStream.OutputState.Footer)
        throw new InvalidOperationException("Write not permitted in current state");
      this.crc.Update(buffer, offset, count);
      base.Write(buffer, offset, count);
    }

    /// <summary>
    /// Writes remaining compressed output data to the output stream
    /// and closes it.
    /// </summary>
    protected override void Dispose(bool disposing)
    {
      if (!disposing)
        return;
      try
      {
        this.Finish();
      }
      finally
      {
        if (this.state_ != GZipOutputStream.OutputState.Closed)
        {
          this.state_ = GZipOutputStream.OutputState.Closed;
          if (this.IsStreamOwner)
            this.baseOutputStream_.Dispose();
        }
      }
    }

    /// <summary>
    /// Finish compression and write any footer information required to stream
    /// </summary>
    public override void Finish()
    {
      if (this.state_ == GZipOutputStream.OutputState.Header)
        this.WriteHeader();
      if (this.state_ != GZipOutputStream.OutputState.Footer)
        return;
      this.state_ = GZipOutputStream.OutputState.Finished;
      base.Finish();
      uint num1 = (uint) ((ulong) this.deflater_.TotalIn & (ulong) uint.MaxValue);
      uint num2 = (uint) ((ulong) this.crc.Value & (ulong) uint.MaxValue);
      byte[] buffer = new byte[8]
      {
        (byte) num2,
        (byte) (num2 >> 8),
        (byte) (num2 >> 16),
        (byte) (num2 >> 24),
        (byte) num1,
        (byte) (num1 >> 8),
        (byte) (num1 >> 16),
        (byte) (num1 >> 24)
      };
      this.baseOutputStream_.Write(buffer, 0, buffer.Length);
    }

    private void WriteHeader()
    {
      if (this.state_ != GZipOutputStream.OutputState.Header)
        return;
      this.state_ = GZipOutputStream.OutputState.Footer;
      int num = (int) ((DateTime.Now.Ticks - new DateTime(1970, 1, 1).Ticks) / 10000000L);
      byte[] buffer = new byte[10]
      {
        (byte) 31,
        (byte) 139,
        (byte) 8,
        (byte) 0,
        (byte) num,
        (byte) (num >> 8),
        (byte) (num >> 16),
        (byte) (num >> 24),
        (byte) 0,
        byte.MaxValue
      };
      this.baseOutputStream_.Write(buffer, 0, buffer.Length);
    }

    private enum OutputState
    {
      Header,
      Footer,
      Finished,
      Closed,
    }
  }
}
