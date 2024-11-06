// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Tar.TarInputStream
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51802, Culture=neutral, PublicKeyToken=null
// MVID: 6C361F8B-A5CA-49B8-ABC1-A50EE493C003
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.xml

using System;
using System.IO;
using System.Text;

#nullable disable
namespace ICSharpCode.SharpZipLib.Tar
{
  /// <summary>
  /// The TarInputStream reads a UNIX tar archive as an InputStream.
  /// methods are provided to position at each successive entry in
  /// the archive, and the read each entry as a normal input stream
  /// using read().
  /// </summary>
  public class TarInputStream : Stream
  {
    /// <summary>Flag set when last block has been read</summary>
    protected bool hasHitEOF;
    /// <summary>Size of this entry as recorded in header</summary>
    protected long entrySize;
    /// <summary>Number of bytes read for this entry so far</summary>
    protected long entryOffset;
    /// <summary>
    /// Buffer used with calls to <code>Read()</code>
    /// </summary>
    protected byte[] readBuffer;
    /// <summary>Working buffer</summary>
    protected TarBuffer tarBuffer;
    /// <summary>Current entry being read</summary>
    private TarEntry currentEntry;
    /// <summary>
    /// Factory used to create TarEntry or descendant class instance
    /// </summary>
    protected TarInputStream.IEntryFactory entryFactory;
    /// <summary>Stream used as the source of input data.</summary>
    private readonly Stream inputStream;

    /// <summary>Construct a TarInputStream with default block factor</summary>
    /// <param name="inputStream">stream to source data from</param>
    public TarInputStream(Stream inputStream)
      : this(inputStream, 20)
    {
    }

    /// <summary>
    /// Construct a TarInputStream with user specified block factor
    /// </summary>
    /// <param name="inputStream">stream to source data from</param>
    /// <param name="blockFactor">block factor to apply to archive</param>
    public TarInputStream(Stream inputStream, int blockFactor)
    {
      this.inputStream = inputStream;
      this.tarBuffer = TarBuffer.CreateInputTarBuffer(inputStream, blockFactor);
    }

    /// <summary>
    /// Get/set flag indicating ownership of the underlying stream.
    /// When the flag is true <see cref="M:ICSharpCode.SharpZipLib.Tar.TarInputStream.Dispose(System.Boolean)"></see> will close the underlying stream also.
    /// </summary>
    public bool IsStreamOwner
    {
      get => this.tarBuffer.IsStreamOwner;
      set => this.tarBuffer.IsStreamOwner = value;
    }

    /// <summary>
    /// Gets a value indicating whether the current stream supports reading
    /// </summary>
    public override bool CanRead => this.inputStream.CanRead;

    /// <summary>
    /// Gets a value indicating whether the current stream supports seeking
    /// This property always returns false.
    /// </summary>
    public override bool CanSeek => false;

    /// <summary>
    /// Gets a value indicating if the stream supports writing.
    /// This property always returns false.
    /// </summary>
    public override bool CanWrite => false;

    /// <summary>The length in bytes of the stream</summary>
    public override long Length => this.inputStream.Length;

    /// <summary>
    /// Gets or sets the position within the stream.
    /// Setting the Position is not supported and throws a NotSupportedExceptionNotSupportedException
    /// </summary>
    /// <exception cref="T:System.NotSupportedException">Any attempt to set position</exception>
    public override long Position
    {
      get => this.inputStream.Position;
      set => throw new NotSupportedException("TarInputStream Seek not supported");
    }

    /// <summary>Flushes the baseInputStream</summary>
    public override void Flush() => this.inputStream.Flush();

    /// <summary>
    /// Set the streams position.  This operation is not supported and will throw a NotSupportedException
    /// </summary>
    /// <param name="offset">The offset relative to the origin to seek to.</param>
    /// <param name="origin">The <see cref="T:System.IO.SeekOrigin" /> to start seeking from.</param>
    /// <returns>The new position in the stream.</returns>
    /// <exception cref="T:System.NotSupportedException">Any access</exception>
    public override long Seek(long offset, SeekOrigin origin)
    {
      throw new NotSupportedException("TarInputStream Seek not supported");
    }

    /// <summary>
    /// Sets the length of the stream
    /// This operation is not supported and will throw a NotSupportedException
    /// </summary>
    /// <param name="value">The new stream length.</param>
    /// <exception cref="T:System.NotSupportedException">Any access</exception>
    public override void SetLength(long value)
    {
      throw new NotSupportedException("TarInputStream SetLength not supported");
    }

    /// <summary>
    /// Writes a block of bytes to this stream using data from a buffer.
    /// This operation is not supported and will throw a NotSupportedException
    /// </summary>
    /// <param name="buffer">The buffer containing bytes to write.</param>
    /// <param name="offset">The offset in the buffer of the frist byte to write.</param>
    /// <param name="count">The number of bytes to write.</param>
    /// <exception cref="T:System.NotSupportedException">Any access</exception>
    public override void Write(byte[] buffer, int offset, int count)
    {
      throw new NotSupportedException("TarInputStream Write not supported");
    }

    /// <summary>
    /// Writes a byte to the current position in the file stream.
    /// This operation is not supported and will throw a NotSupportedException
    /// </summary>
    /// <param name="value">The byte value to write.</param>
    /// <exception cref="T:System.NotSupportedException">Any access</exception>
    public override void WriteByte(byte value)
    {
      throw new NotSupportedException("TarInputStream WriteByte not supported");
    }

    /// <summary>Reads a byte from the current tar archive entry.</summary>
    /// <returns>A byte cast to an int; -1 if the at the end of the stream.</returns>
    public override int ReadByte()
    {
      byte[] buffer = new byte[1];
      return this.Read(buffer, 0, 1) <= 0 ? -1 : (int) buffer[0];
    }

    /// <summary>
    /// Reads bytes from the current tar archive entry.
    /// 
    /// This method is aware of the boundaries of the current
    /// entry in the archive and will deal with them appropriately
    /// </summary>
    /// <param name="buffer">The buffer into which to place bytes read.</param>
    /// <param name="offset">The offset at which to place bytes read.</param>
    /// <param name="count">The number of bytes to read.</param>
    /// <returns>The number of bytes read, or 0 at end of stream/EOF.</returns>
    public override int Read(byte[] buffer, int offset, int count)
    {
      if (buffer == null)
        throw new ArgumentNullException(nameof (buffer));
      int num1 = 0;
      if (this.entryOffset >= this.entrySize)
        return 0;
      long num2 = (long) count;
      if (num2 + this.entryOffset > this.entrySize)
        num2 = this.entrySize - this.entryOffset;
      if (this.readBuffer != null)
      {
        int num3 = num2 > (long) this.readBuffer.Length ? this.readBuffer.Length : (int) num2;
        Array.Copy((Array) this.readBuffer, 0, (Array) buffer, offset, num3);
        if (num3 >= this.readBuffer.Length)
        {
          this.readBuffer = (byte[]) null;
        }
        else
        {
          int length = this.readBuffer.Length - num3;
          byte[] destinationArray = new byte[length];
          Array.Copy((Array) this.readBuffer, num3, (Array) destinationArray, 0, length);
          this.readBuffer = destinationArray;
        }
        num1 += num3;
        num2 -= (long) num3;
        offset += num3;
      }
      while (num2 > 0L)
      {
        byte[] sourceArray = this.tarBuffer.ReadBlock();
        if (sourceArray == null)
          throw new TarException("unexpected EOF with " + (object) num2 + " bytes unread");
        int num4 = (int) num2;
        int length = sourceArray.Length;
        if (length > num4)
        {
          Array.Copy((Array) sourceArray, 0, (Array) buffer, offset, num4);
          this.readBuffer = new byte[length - num4];
          Array.Copy((Array) sourceArray, num4, (Array) this.readBuffer, 0, length - num4);
        }
        else
        {
          num4 = length;
          Array.Copy((Array) sourceArray, 0, (Array) buffer, offset, length);
        }
        num1 += num4;
        num2 -= (long) num4;
        offset += num4;
      }
      this.entryOffset += (long) num1;
      return num1;
    }

    /// <summary>
    /// Closes this stream. Calls the TarBuffer's close() method.
    /// The underlying stream is closed by the TarBuffer.
    /// </summary>
    protected override void Dispose(bool disposing)
    {
      base.Dispose(disposing);
      if (!disposing)
        return;
      this.tarBuffer.Close();
    }

    /// <summary>Set the entry factory for this instance.</summary>
    /// <param name="factory">The factory for creating new entries</param>
    public void SetEntryFactory(TarInputStream.IEntryFactory factory)
    {
      this.entryFactory = factory;
    }

    /// <summary>
    /// Get the record size being used by this stream's TarBuffer.
    /// </summary>
    public int RecordSize => this.tarBuffer.RecordSize;

    /// <summary>
    /// Get the record size being used by this stream's TarBuffer.
    /// </summary>
    /// <returns>TarBuffer record size.</returns>
    [Obsolete("Use RecordSize property instead")]
    public int GetRecordSize() => this.tarBuffer.RecordSize;

    /// <summary>
    /// Get the available data that can be read from the current
    /// entry in the archive. This does not indicate how much data
    /// is left in the entire archive, only in the current entry.
    /// This value is determined from the entry's size header field
    /// and the amount of data already read from the current entry.
    /// </summary>
    /// <returns>The number of available bytes for the current entry.</returns>
    public long Available => this.entrySize - this.entryOffset;

    /// <summary>
    /// Skip bytes in the input buffer. This skips bytes in the
    /// current entry's data, not the entire archive, and will
    /// stop at the end of the current entry's data if the number
    /// to skip extends beyond that point.
    /// </summary>
    /// <param name="skipCount">The number of bytes to skip.</param>
    public void Skip(long skipCount)
    {
      byte[] buffer = new byte[8192];
      int num;
      for (long index = skipCount; index > 0L; index -= (long) num)
      {
        int count = index > (long) buffer.Length ? buffer.Length : (int) index;
        num = this.Read(buffer, 0, count);
        if (num == -1)
          break;
      }
    }

    /// <summary>
    /// Return a value of true if marking is supported; false otherwise.
    /// </summary>
    /// <remarks>Currently marking is not supported, the return value is always false.</remarks>
    public bool IsMarkSupported => false;

    /// <summary>
    /// Since we do not support marking just yet, we do nothing.
    /// </summary>
    /// <param name="markLimit">The limit to mark.</param>
    public void Mark(int markLimit)
    {
    }

    /// <summary>
    /// Since we do not support marking just yet, we do nothing.
    /// </summary>
    public void Reset()
    {
    }

    /// <summary>
    /// Get the next entry in this tar archive. This will skip
    /// over any remaining data in the current entry, if there
    /// is one, and place the input stream at the header of the
    /// next entry, and read the header and instantiate a new
    /// TarEntry from the header bytes and return that entry.
    /// If there are no more entries in the archive, null will
    /// be returned to indicate that the end of the archive has
    /// been reached.
    /// </summary>
    /// <returns>The next TarEntry in the archive, or null.</returns>
    public TarEntry GetNextEntry()
    {
      if (this.hasHitEOF)
        return (TarEntry) null;
      if (this.currentEntry != null)
        this.SkipToNextEntry();
      byte[] numArray1 = this.tarBuffer.ReadBlock();
      if (numArray1 == null)
        this.hasHitEOF = true;
      else if (TarBuffer.IsEndOfArchiveBlock(numArray1))
        this.hasHitEOF = true;
      if (this.hasHitEOF)
      {
        this.currentEntry = (TarEntry) null;
      }
      else
      {
        try
        {
          TarHeader tarHeader = new TarHeader();
          tarHeader.ParseBuffer(numArray1);
          if (!tarHeader.IsChecksumValid)
            throw new TarException("Header checksum is invalid");
          this.entryOffset = 0L;
          this.entrySize = tarHeader.Size;
          StringBuilder stringBuilder = (StringBuilder) null;
          if (tarHeader.TypeFlag == (byte) 76)
          {
            byte[] numArray2 = new byte[512];
            long entrySize = this.entrySize;
            stringBuilder = new StringBuilder();
            int length;
            for (; entrySize > 0L; entrySize -= (long) length)
            {
              length = this.Read(numArray2, 0, entrySize > (long) numArray2.Length ? numArray2.Length : (int) entrySize);
              if (length == -1)
                throw new InvalidHeaderException("Failed to read long name entry");
              stringBuilder.Append(TarHeader.ParseName(numArray2, 0, length).ToString());
            }
            this.SkipToNextEntry();
            numArray1 = this.tarBuffer.ReadBlock();
          }
          else if (tarHeader.TypeFlag == (byte) 103)
          {
            this.SkipToNextEntry();
            numArray1 = this.tarBuffer.ReadBlock();
          }
          else if (tarHeader.TypeFlag == (byte) 120)
          {
            this.SkipToNextEntry();
            numArray1 = this.tarBuffer.ReadBlock();
          }
          else if (tarHeader.TypeFlag == (byte) 86)
          {
            this.SkipToNextEntry();
            numArray1 = this.tarBuffer.ReadBlock();
          }
          else if (tarHeader.TypeFlag != (byte) 48 && tarHeader.TypeFlag != (byte) 0 && tarHeader.TypeFlag != (byte) 53)
          {
            this.SkipToNextEntry();
            numArray1 = this.tarBuffer.ReadBlock();
          }
          if (this.entryFactory == null)
          {
            this.currentEntry = new TarEntry(numArray1);
            if (stringBuilder != null)
              this.currentEntry.Name = stringBuilder.ToString();
          }
          else
            this.currentEntry = this.entryFactory.CreateEntry(numArray1);
          this.entryOffset = 0L;
          this.entrySize = this.currentEntry.Size;
        }
        catch (InvalidHeaderException ex)
        {
          this.entrySize = 0L;
          this.entryOffset = 0L;
          this.currentEntry = (TarEntry) null;
          throw new InvalidHeaderException(string.Format("Bad header in record {0} block {1} {2}", (object) this.tarBuffer.CurrentRecord, (object) this.tarBuffer.CurrentBlock, (object) ex.Message));
        }
      }
      return this.currentEntry;
    }

    /// <summary>
    /// Copies the contents of the current tar archive entry directly into
    /// an output stream.
    /// </summary>
    /// <param name="outputStream">
    /// The OutputStream into which to write the entry's data.
    /// </param>
    public void CopyEntryContents(Stream outputStream)
    {
      byte[] buffer = new byte[32768];
      while (true)
      {
        int count = this.Read(buffer, 0, buffer.Length);
        if (count > 0)
          outputStream.Write(buffer, 0, count);
        else
          break;
      }
    }

    private void SkipToNextEntry()
    {
      long skipCount = this.entrySize - this.entryOffset;
      if (skipCount > 0L)
        this.Skip(skipCount);
      this.readBuffer = (byte[]) null;
    }

    /// <summary>
    /// This interface is provided, along with the method <see cref="M:ICSharpCode.SharpZipLib.Tar.TarInputStream.SetEntryFactory(ICSharpCode.SharpZipLib.Tar.TarInputStream.IEntryFactory)" />, to allow
    /// the programmer to have their own <see cref="T:ICSharpCode.SharpZipLib.Tar.TarEntry" /> subclass instantiated for the
    /// entries return from <see cref="M:ICSharpCode.SharpZipLib.Tar.TarInputStream.GetNextEntry" />.
    /// </summary>
    public interface IEntryFactory
    {
      /// <summary>Create an entry based on name alone</summary>
      /// <param name="name">
      /// Name of the new EntryPointNotFoundException to create
      /// </param>
      /// <returns>created TarEntry or descendant class</returns>
      TarEntry CreateEntry(string name);

      /// <summary>Create an instance based on an actual file</summary>
      /// <param name="fileName">Name of file to represent in the entry</param>
      /// <returns>Created TarEntry or descendant class</returns>
      TarEntry CreateEntryFromFile(string fileName);

      /// <summary>
      /// Create a tar entry based on the header information passed
      /// </summary>
      /// <param name="headerBuffer">
      /// Buffer containing header information to create an an entry from.
      /// </param>
      /// <returns>Created TarEntry or descendant class</returns>
      TarEntry CreateEntry(byte[] headerBuffer);
    }

    /// <summary>
    /// Standard entry factory class creating instances of the class TarEntry
    /// </summary>
    public class EntryFactoryAdapter : TarInputStream.IEntryFactory
    {
      /// <summary>
      /// Create a <see cref="T:ICSharpCode.SharpZipLib.Tar.TarEntry" /> based on named
      /// </summary>
      /// <param name="name">The name to use for the entry</param>
      /// <returns>A new <see cref="T:ICSharpCode.SharpZipLib.Tar.TarEntry" /></returns>
      public TarEntry CreateEntry(string name) => TarEntry.CreateTarEntry(name);

      /// <summary>
      /// Create a tar entry with details obtained from <paramref name="fileName">file</paramref>
      /// </summary>
      /// <param name="fileName">The name of the file to retrieve details from.</param>
      /// <returns>A new <see cref="T:ICSharpCode.SharpZipLib.Tar.TarEntry" /></returns>
      public TarEntry CreateEntryFromFile(string fileName)
      {
        return TarEntry.CreateEntryFromFile(fileName);
      }

      /// <summary>
      /// Create an entry based on details in <paramref name="headerBuffer">header</paramref>
      /// </summary>
      /// <param name="headerBuffer">The buffer containing entry details.</param>
      /// <returns>A new <see cref="T:ICSharpCode.SharpZipLib.Tar.TarEntry" /></returns>
      public TarEntry CreateEntry(byte[] headerBuffer) => new TarEntry(headerBuffer);
    }
  }
}
