// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.ZipInputStream
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
namespace ICSharpCode.SharpZipLib.Zip
{
  /// <summary>
  /// This is an InflaterInputStream that reads the files baseInputStream an zip archive
  /// one after another.  It has a special method to get the zip entry of
  /// the next file.  The zip entry contains information about the file name
  /// size, compressed size, Crc, etc.
  /// It includes support for Stored and Deflated entries.
  /// <br />
  /// <br />Author of the original java version : Jochen Hoenicke
  /// </summary>
  /// <example> This sample shows how to read a zip file
  /// <code lang="C#">
  /// using System;
  /// using System.Text;
  /// using System.IO;
  /// 
  /// using ICSharpCode.SharpZipLib.Zip;
  /// 
  /// class MainClass
  /// {
  /// 	public static void Main(string[] args)
  /// 	{
  /// 		using ( ZipInputStream s = new ZipInputStream(File.OpenRead(args[0]))) {
  /// 
  /// 			ZipEntry theEntry;
  /// 			const int size = 2048;
  /// 			byte[] data = new byte[2048];
  /// 
  /// 			while ((theEntry = s.GetNextEntry()) != null) {
  ///                 if ( entry.IsFile ) {
  /// 				    Console.Write("Show contents (y/n) ?");
  /// 				    if (Console.ReadLine() == "y") {
  /// 				    	while (true) {
  /// 				    		size = s.Read(data, 0, data.Length);
  /// 				    		if (size &gt; 0) {
  /// 				    			Console.Write(new ASCIIEncoding().GetString(data, 0, size));
  /// 				    		} else {
  /// 				    			break;
  /// 				    		}
  /// 				    	}
  /// 				    }
  /// 				}
  /// 			}
  /// 		}
  /// 	}
  /// }
  /// </code>
  /// </example>
  public class ZipInputStream : InflaterInputStream
  {
    /// <summary>The current reader this instance.</summary>
    private ZipInputStream.ReadDataHandler internalReader;
    private Crc32 crc = new Crc32();
    private ZipEntry entry;
    private long size;
    private int method;
    private int flags;
    private string password;

    /// <summary>
    /// Creates a new Zip input stream, for reading a zip archive.
    /// </summary>
    /// <param name="baseInputStream">The underlying <see cref="T:System.IO.Stream" /> providing data.</param>
    public ZipInputStream(Stream baseInputStream)
      : base(baseInputStream, new Inflater(true))
    {
      this.internalReader = new ZipInputStream.ReadDataHandler(this.ReadingNotAvailable);
    }

    /// <summary>
    /// Creates a new Zip input stream, for reading a zip archive.
    /// </summary>
    /// <param name="baseInputStream">The underlying <see cref="T:System.IO.Stream" /> providing data.</param>
    /// <param name="bufferSize">Size of the buffer.</param>
    public ZipInputStream(Stream baseInputStream, int bufferSize)
      : base(baseInputStream, new Inflater(true), bufferSize)
    {
      this.internalReader = new ZipInputStream.ReadDataHandler(this.ReadingNotAvailable);
    }

    /// <summary>Optional password used for encryption when non-null</summary>
    /// <value>A password for all encrypted <see cref="T:ICSharpCode.SharpZipLib.Zip.ZipEntry">entries </see> in this <see cref="T:ICSharpCode.SharpZipLib.Zip.ZipInputStream" /></value>
    public string Password
    {
      get => this.password;
      set => this.password = value;
    }

    /// <summary>
    /// Gets a value indicating if there is a current entry and it can be decompressed
    /// </summary>
    /// <remarks>
    /// The entry can only be decompressed if the library supports the zip features required to extract it.
    /// See the <see cref="P:ICSharpCode.SharpZipLib.Zip.ZipEntry.Version">ZipEntry Version</see> property for more details.
    /// </remarks>
    public bool CanDecompressEntry => this.entry != null && this.entry.CanDecompress;

    /// <summary>Advances to the next entry in the archive</summary>
    /// <returns>
    /// The next <see cref="T:ICSharpCode.SharpZipLib.Zip.ZipEntry">entry</see> in the archive or null if there are no more entries.
    /// </returns>
    /// <remarks>
    /// If the previous entry is still open <see cref="M:ICSharpCode.SharpZipLib.Zip.ZipInputStream.CloseEntry">CloseEntry</see> is called.
    /// </remarks>
    /// <exception cref="T:System.InvalidOperationException">
    /// Input stream is closed
    /// </exception>
    /// <exception cref="T:ICSharpCode.SharpZipLib.Zip.ZipException">
    /// Password is not set, password is invalid, compression method is invalid,
    /// version required to extract is not supported
    /// </exception>
    public ZipEntry GetNextEntry()
    {
      if (this.crc == null)
        throw new InvalidOperationException("Closed.");
      if (this.entry != null)
        this.CloseEntry();
      int num1 = this.inputBuffer.ReadLeInt();
      if (num1 == 33639248 || num1 == 101010256 || num1 == 84233040 || num1 == 117853008 || num1 == 101075792)
      {
        this.Dispose();
        return (ZipEntry) null;
      }
      if (num1 == 808471376 || num1 == 134695760)
        num1 = this.inputBuffer.ReadLeInt();
      if (num1 != 67324752)
        throw new ZipException("Wrong Local header signature: 0x" + string.Format("{0:X}", (object) num1));
      short versionRequiredToExtract = (short) this.inputBuffer.ReadLeShort();
      this.flags = this.inputBuffer.ReadLeShort();
      this.method = this.inputBuffer.ReadLeShort();
      uint num2 = (uint) this.inputBuffer.ReadLeInt();
      int num3 = this.inputBuffer.ReadLeInt();
      this.csize = (long) this.inputBuffer.ReadLeInt();
      this.size = (long) this.inputBuffer.ReadLeInt();
      int length1 = this.inputBuffer.ReadLeShort();
      int length2 = this.inputBuffer.ReadLeShort();
      bool flag = (this.flags & 1) == 1;
      byte[] numArray = new byte[length1];
      this.inputBuffer.ReadRawBuffer(numArray);
      this.entry = new ZipEntry(ZipConstants.ConvertToStringExt(this.flags, numArray), (int) versionRequiredToExtract);
      this.entry.Flags = this.flags;
      this.entry.CompressionMethod = (CompressionMethod) this.method;
      if ((this.flags & 8) == 0)
      {
        this.entry.Crc = (long) num3 & (long) uint.MaxValue;
        this.entry.Size = this.size & (long) uint.MaxValue;
        this.entry.CompressedSize = this.csize & (long) uint.MaxValue;
        this.entry.CryptoCheckValue = (byte) (num3 >> 24 & (int) byte.MaxValue);
      }
      else
      {
        if (num3 != 0)
          this.entry.Crc = (long) num3 & (long) uint.MaxValue;
        if (this.size != 0L)
          this.entry.Size = this.size & (long) uint.MaxValue;
        if (this.csize != 0L)
          this.entry.CompressedSize = this.csize & (long) uint.MaxValue;
        this.entry.CryptoCheckValue = (byte) (num2 >> 8 & (uint) byte.MaxValue);
      }
      this.entry.DosTime = (long) num2;
      if (length2 > 0)
      {
        byte[] buffer = new byte[length2];
        this.inputBuffer.ReadRawBuffer(buffer);
        this.entry.ExtraData = buffer;
      }
      this.entry.ProcessExtraData(true);
      if (this.entry.CompressedSize >= 0L)
        this.csize = this.entry.CompressedSize;
      if (this.entry.Size >= 0L)
        this.size = this.entry.Size;
      if (this.method == 0 && (!flag && this.csize != this.size || flag && this.csize - 12L != this.size))
        throw new ZipException("Stored, but compressed != uncompressed");
      this.internalReader = !this.entry.IsCompressionMethodSupported() ? new ZipInputStream.ReadDataHandler(this.ReadingNotSupported) : new ZipInputStream.ReadDataHandler(this.InitialRead);
      return this.entry;
    }

    /// <summary>Read data descriptor at the end of compressed data.</summary>
    private void ReadDataDescriptor()
    {
      if (this.inputBuffer.ReadLeInt() != 134695760)
        throw new ZipException("Data descriptor signature not found");
      this.entry.Crc = (long) this.inputBuffer.ReadLeInt() & (long) uint.MaxValue;
      if (this.entry.LocalHeaderRequiresZip64)
      {
        this.csize = this.inputBuffer.ReadLeLong();
        this.size = this.inputBuffer.ReadLeLong();
      }
      else
      {
        this.csize = (long) this.inputBuffer.ReadLeInt();
        this.size = (long) this.inputBuffer.ReadLeInt();
      }
      this.entry.CompressedSize = this.csize;
      this.entry.Size = this.size;
    }

    /// <summary>Complete cleanup as the final part of closing.</summary>
    /// <param name="testCrc">True if the crc value should be tested</param>
    private void CompleteCloseEntry(bool testCrc)
    {
      this.StopDecrypting();
      if ((this.flags & 8) != 0)
        this.ReadDataDescriptor();
      this.size = 0L;
      if (testCrc && (this.crc.Value & (long) uint.MaxValue) != this.entry.Crc && this.entry.Crc != -1L)
        throw new ZipException("CRC mismatch");
      this.crc.Reset();
      if (this.method == 8)
        this.inf.Reset();
      this.entry = (ZipEntry) null;
    }

    /// <summary>Closes the current zip entry and moves to the next one.</summary>
    /// <exception cref="T:System.InvalidOperationException">
    /// The stream is closed
    /// </exception>
    /// <exception cref="T:ICSharpCode.SharpZipLib.Zip.ZipException">
    /// The Zip stream ends early
    /// </exception>
    public void CloseEntry()
    {
      if (this.crc == null)
        throw new InvalidOperationException("Closed");
      if (this.entry == null)
        return;
      if (this.method == 8)
      {
        if ((this.flags & 8) != 0)
        {
          byte[] buffer = new byte[4096];
          do
            ;
          while (this.Read(buffer, 0, buffer.Length) > 0);
          return;
        }
        this.csize -= this.inf.TotalIn;
        this.inputBuffer.Available += this.inf.RemainingInput;
      }
      if ((long) this.inputBuffer.Available > this.csize && this.csize >= 0L)
      {
        this.inputBuffer.Available = (int) ((long) this.inputBuffer.Available - this.csize);
      }
      else
      {
        this.csize -= (long) this.inputBuffer.Available;
        this.inputBuffer.Available = 0;
        long num;
        ZipInputStream zipInputStream;
        for (; this.csize != 0L; zipInputStream.csize -= num)
        {
          num = this.Skip(this.csize);
          if (num <= 0L)
            throw new ZipException("Zip archive ends early.");
          zipInputStream = this;
        }
      }
      this.CompleteCloseEntry(false);
    }

    /// <summary>
    /// Returns 1 if there is an entry available
    /// Otherwise returns 0.
    /// </summary>
    public override int Available => this.entry == null ? 0 : 1;

    /// <summary>
    /// Returns the current size that can be read from the current entry if available
    /// </summary>
    /// <exception cref="T:ICSharpCode.SharpZipLib.Zip.ZipException">Thrown if the entry size is not known.</exception>
    /// <exception cref="T:System.InvalidOperationException">Thrown if no entry is currently available.</exception>
    public override long Length
    {
      get
      {
        if (this.entry == null)
          throw new InvalidOperationException("No current entry");
        return this.entry.Size >= 0L ? this.entry.Size : throw new ZipException("Length not available for the current entry");
      }
    }

    /// <summary>Reads a byte from the current zip entry.</summary>
    /// <returns>The byte or -1 if end of stream is reached.</returns>
    public override int ReadByte()
    {
      byte[] buffer = new byte[1];
      return this.Read(buffer, 0, 1) <= 0 ? -1 : (int) buffer[0] & (int) byte.MaxValue;
    }

    /// <summary>
    /// Handle attempts to read by throwing an <see cref="T:System.InvalidOperationException" />.
    /// </summary>
    /// <param name="destination">The destination array to store data in.</param>
    /// <param name="offset">The offset at which data read should be stored.</param>
    /// <param name="count">The maximum number of bytes to read.</param>
    /// <returns>Returns the number of bytes actually read.</returns>
    private int ReadingNotAvailable(byte[] destination, int offset, int count)
    {
      throw new InvalidOperationException("Unable to read from this stream");
    }

    /// <summary>
    /// Handle attempts to read from this entry by throwing an exception
    /// </summary>
    private int ReadingNotSupported(byte[] destination, int offset, int count)
    {
      throw new ZipException("The compression method for this entry is not supported");
    }

    /// <summary>
    /// Perform the initial read on an entry which may include
    /// reading encryption headers and setting up inflation.
    /// </summary>
    /// <param name="destination">The destination to fill with data read.</param>
    /// <param name="offset">The offset to start reading at.</param>
    /// <param name="count">The maximum number of bytes to read.</param>
    /// <returns>The actual number of bytes read.</returns>
    private int InitialRead(byte[] destination, int offset, int count)
    {
      if (!this.CanDecompressEntry)
        throw new ZipException("Library cannot extract this entry. Version required is (" + this.entry.Version.ToString() + ")");
      if (this.entry.IsCrypted)
        throw new ZipException("Encryption not supported for Portable Class Library");
      if (this.csize > 0L || (this.flags & 8) != 0)
      {
        if (this.method == 8 && this.inputBuffer.Available > 0)
          this.inputBuffer.SetInflaterInput(this.inf);
        this.internalReader = new ZipInputStream.ReadDataHandler(this.BodyRead);
        return this.BodyRead(destination, offset, count);
      }
      this.internalReader = new ZipInputStream.ReadDataHandler(this.ReadingNotAvailable);
      return 0;
    }

    /// <summary>Read a block of bytes from the stream.</summary>
    /// <param name="buffer">The destination for the bytes.</param>
    /// <param name="offset">The index to start storing data.</param>
    /// <param name="count">The number of bytes to attempt to read.</param>
    /// <returns>Returns the number of bytes read.</returns>
    /// <remarks>Zero bytes read means end of stream.</remarks>
    public override int Read(byte[] buffer, int offset, int count)
    {
      if (buffer == null)
        throw new ArgumentNullException(nameof (buffer));
      if (offset < 0)
        throw new ArgumentOutOfRangeException(nameof (offset), "Cannot be negative");
      if (count < 0)
        throw new ArgumentOutOfRangeException(nameof (count), "Cannot be negative");
      if (buffer.Length - offset < count)
        throw new ArgumentException("Invalid offset/count combination");
      return this.internalReader(buffer, offset, count);
    }

    /// <summary>Reads a block of bytes from the current zip entry.</summary>
    /// <returns>
    /// The number of bytes read (this may be less than the length requested, even before the end of stream), or 0 on end of stream.
    /// </returns>
    /// <exception name="IOException">An i/o error occured.</exception>
    /// <exception cref="T:ICSharpCode.SharpZipLib.Zip.ZipException">
    /// The deflated stream is corrupted.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    /// The stream is not open.
    /// </exception>
    private int BodyRead(byte[] buffer, int offset, int count)
    {
      if (this.crc == null)
        throw new InvalidOperationException("Closed");
      if (this.entry == null || count <= 0)
        return 0;
      if (offset + count > buffer.Length)
        throw new ArgumentException("Offset + count exceeds buffer size");
      bool flag = false;
      switch (this.method)
      {
        case 0:
          if ((long) count > this.csize && this.csize >= 0L)
            count = (int) this.csize;
          if (count > 0)
          {
            count = this.inputBuffer.ReadClearTextBuffer(buffer, offset, count);
            if (count > 0)
            {
              this.csize -= (long) count;
              this.size -= (long) count;
            }
          }
          if (this.csize == 0L)
          {
            flag = true;
            break;
          }
          if (count < 0)
            throw new ZipException("EOF in stored block");
          break;
        case 8:
          count = base.Read(buffer, offset, count);
          if (count <= 0)
          {
            this.inputBuffer.Available = this.inf.IsFinished ? this.inf.RemainingInput : throw new ZipException("Inflater not finished!");
            if ((this.flags & 8) == 0 && (this.inf.TotalIn != this.csize && this.csize != (long) uint.MaxValue && this.csize != -1L || this.inf.TotalOut != this.size))
              throw new ZipException("Size mismatch: " + (object) this.csize + ";" + (object) this.size + " <-> " + (object) this.inf.TotalIn + ";" + (object) this.inf.TotalOut);
            this.inf.Reset();
            flag = true;
            break;
          }
          break;
      }
      if (count > 0)
        this.crc.Update(buffer, offset, count);
      if (flag)
        this.CompleteCloseEntry(true);
      return count;
    }

    /// <summary>Closes the zip input stream</summary>
    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        this.internalReader = new ZipInputStream.ReadDataHandler(this.ReadingNotAvailable);
        this.crc = (Crc32) null;
        this.entry = (ZipEntry) null;
      }
      base.Dispose(disposing);
    }

    /// <summary>Delegate for reading bytes from a stream.</summary>
    private delegate int ReadDataHandler(byte[] b, int offset, int length);
  }
}
