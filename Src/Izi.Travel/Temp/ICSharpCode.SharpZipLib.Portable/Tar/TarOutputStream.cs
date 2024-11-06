// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Tar.TarOutputStream
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51802, Culture=neutral, PublicKeyToken=null
// MVID: 6C361F8B-A5CA-49B8-ABC1-A50EE493C003
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.xml

using System;
using System.IO;

#nullable disable
namespace ICSharpCode.SharpZipLib.Tar
{
  /// <summary>
  /// The TarOutputStream writes a UNIX tar archive as an OutputStream.
  /// Methods are provided to put entries, and then write their contents
  /// by writing to this stream using write().
  /// </summary>
  /// 
  ///             public
  public class TarOutputStream : Stream
  {
    /// <summary>bytes written for this entry so far</summary>
    private long currBytes;
    /// <summary>current 'Assembly' buffer length</summary>
    private int assemblyBufferLength;
    /// <summary>
    /// Flag indicating wether this instance has been closed or not.
    /// </summary>
    private bool isClosed;
    /// <summary>Size for the current entry</summary>
    protected long currSize;
    /// <summary>single block working buffer</summary>
    protected byte[] blockBuffer;
    /// <summary>'Assembly' buffer used to assemble data before writing</summary>
    protected byte[] assemblyBuffer;
    /// <summary>TarBuffer used to provide correct blocking factor</summary>
    protected TarBuffer buffer;
    /// <summary>the destination stream for the archive contents</summary>
    protected Stream outputStream;

    /// <summary>Construct TarOutputStream using default block factor</summary>
    /// <param name="outputStream">stream to write to</param>
    public TarOutputStream(Stream outputStream)
      : this(outputStream, 20)
    {
    }

    /// <summary>
    /// Construct TarOutputStream with user specified block factor
    /// </summary>
    /// <param name="outputStream">stream to write to</param>
    /// <param name="blockFactor">blocking factor</param>
    public TarOutputStream(Stream outputStream, int blockFactor)
    {
      this.outputStream = outputStream != null ? outputStream : throw new ArgumentNullException(nameof (outputStream));
      this.buffer = TarBuffer.CreateOutputTarBuffer(outputStream, blockFactor);
      this.assemblyBuffer = new byte[512];
      this.blockBuffer = new byte[512];
    }

    /// <summary>
    /// Get/set flag indicating ownership of the underlying stream.
    /// When the flag is true <see cref="M:ICSharpCode.SharpZipLib.Tar.TarOutputStream.Dispose(System.Boolean)"></see> will close the underlying stream also.
    /// </summary>
    public bool IsStreamOwner
    {
      get => this.buffer.IsStreamOwner;
      set => this.buffer.IsStreamOwner = value;
    }

    /// <summary>true if the stream supports reading; otherwise, false.</summary>
    public override bool CanRead => this.outputStream.CanRead;

    /// <summary>true if the stream supports seeking; otherwise, false.</summary>
    public override bool CanSeek => this.outputStream.CanSeek;

    /// <summary>true if stream supports writing; otherwise, false.</summary>
    public override bool CanWrite => this.outputStream.CanWrite;

    /// <summary>length of stream in bytes</summary>
    public override long Length => this.outputStream.Length;

    /// <summary>gets or sets the position within the current stream.</summary>
    public override long Position
    {
      get => this.outputStream.Position;
      set => this.outputStream.Position = value;
    }

    /// <summary>set the position within the current stream</summary>
    /// <param name="offset">The offset relative to the <paramref name="origin" /> to seek to</param>
    /// <param name="origin">The <see cref="T:System.IO.SeekOrigin" /> to seek from.</param>
    /// <returns>The new position in the stream.</returns>
    public override long Seek(long offset, SeekOrigin origin)
    {
      return this.outputStream.Seek(offset, origin);
    }

    /// <summary>Set the length of the current stream</summary>
    /// <param name="value">The new stream length.</param>
    public override void SetLength(long value) => this.outputStream.SetLength(value);

    /// <summary>
    /// Read a byte from the stream and advance the position within the stream
    /// by one byte or returns -1 if at the end of the stream.
    /// </summary>
    /// <returns>The byte value or -1 if at end of stream</returns>
    public override int ReadByte() => this.outputStream.ReadByte();

    /// <summary>
    /// read bytes from the current stream and advance the position within the
    /// stream by the number of bytes read.
    /// </summary>
    /// <param name="buffer">The buffer to store read bytes in.</param>
    /// <param name="offset">The index into the buffer to being storing bytes at.</param>
    /// <param name="count">The desired number of bytes to read.</param>
    /// <returns>The total number of bytes read, or zero if at the end of the stream.
    /// The number of bytes may be less than the <paramref name="count">count</paramref>
    /// requested if data is not avialable.</returns>
    public override int Read(byte[] buffer, int offset, int count)
    {
      return this.outputStream.Read(buffer, offset, count);
    }

    /// <summary>All buffered data is written to destination</summary>
    public override void Flush() => this.outputStream.Flush();

    /// <summary>
    /// Ends the TAR archive without closing the underlying OutputStream.
    /// The result is that the EOF block of nulls is written.
    /// </summary>
    public void Finish()
    {
      if (this.IsEntryOpen)
        this.CloseEntry();
      this.WriteEofBlock();
    }

    /// <summary>
    /// Ends the TAR archive and closes the underlying OutputStream.
    /// </summary>
    /// <remarks>This means that Finish() is called followed by calling the
    /// TarBuffer's Close().</remarks>
    protected override void Dispose(bool disposing)
    {
      base.Dispose(disposing);
      if (!disposing || this.isClosed)
        return;
      this.isClosed = true;
      this.Finish();
      this.buffer.Close();
    }

    /// <summary>
    /// Get the record size being used by this stream's TarBuffer.
    /// </summary>
    public int RecordSize => this.buffer.RecordSize;

    /// <summary>
    /// Get the record size being used by this stream's TarBuffer.
    /// </summary>
    /// <returns>The TarBuffer record size.</returns>
    [Obsolete("Use RecordSize property instead")]
    public int GetRecordSize() => this.buffer.RecordSize;

    /// <summary>
    /// Get a value indicating wether an entry is open, requiring more data to be written.
    /// </summary>
    private bool IsEntryOpen => this.currBytes < this.currSize;

    /// <summary>
    /// Put an entry on the output stream. This writes the entry's
    /// header and positions the output stream for writing
    /// the contents of the entry. Once this method is called, the
    /// stream is ready for calls to write() to write the entry's
    /// contents. Once the contents are written, closeEntry()
    /// <B>MUST</B> be called to ensure that all buffered data
    /// is completely written to the output stream.
    /// </summary>
    /// <param name="entry">The TarEntry to be written to the archive.</param>
    public void PutNextEntry(TarEntry entry)
    {
      if (entry == null)
        throw new ArgumentNullException(nameof (entry));
      if (entry.TarHeader.Name.Length >= 100)
      {
        TarHeader tarHeader = new TarHeader()
        {
          TypeFlag = 76
        };
        tarHeader.Name += "././@LongLink";
        tarHeader.UserId = 0;
        tarHeader.GroupId = 0;
        tarHeader.GroupName = "";
        tarHeader.UserName = "";
        tarHeader.LinkName = "";
        tarHeader.Size = (long) (entry.TarHeader.Name.Length + 1);
        tarHeader.WriteHeader(this.blockBuffer);
        this.buffer.WriteBlock(this.blockBuffer);
        int nameOffset = 0;
        while (nameOffset <= entry.TarHeader.Name.Length)
        {
          Array.Clear((Array) this.blockBuffer, 0, this.blockBuffer.Length);
          TarHeader.GetAsciiBytes(entry.TarHeader.Name, nameOffset, this.blockBuffer, 0, 512);
          nameOffset += 512;
          this.buffer.WriteBlock(this.blockBuffer);
        }
      }
      entry.WriteEntryHeader(this.blockBuffer);
      this.buffer.WriteBlock(this.blockBuffer);
      this.currBytes = 0L;
      this.currSize = entry.IsDirectory ? 0L : entry.Size;
    }

    /// <summary>
    /// Close an entry. This method MUST be called for all file
    /// entries that contain data. The reason is that we must
    /// buffer data written to the stream in order to satisfy
    /// the buffer's block based writes. Thus, there may be
    /// data fragments still being assembled that must be written
    /// to the output stream before this entry is closed and the
    /// next entry written.
    /// </summary>
    public void CloseEntry()
    {
      if (this.assemblyBufferLength > 0)
      {
        Array.Clear((Array) this.assemblyBuffer, this.assemblyBufferLength, this.assemblyBuffer.Length - this.assemblyBufferLength);
        this.buffer.WriteBlock(this.assemblyBuffer);
        this.currBytes += (long) this.assemblyBufferLength;
        this.assemblyBufferLength = 0;
      }
      if (this.currBytes < this.currSize)
        throw new TarException(string.Format("Entry closed at '{0}' before the '{1}' bytes specified in the header were written", (object) this.currBytes, (object) this.currSize));
    }

    /// <summary>
    /// Writes a byte to the current tar archive entry.
    /// This method simply calls Write(byte[], int, int).
    /// </summary>
    /// <param name="value">The byte to be written.</param>
    public override void WriteByte(byte value)
    {
      this.Write(new byte[1]{ value }, 0, 1);
    }

    /// <summary>
    /// Writes bytes to the current tar archive entry. This method
    /// is aware of the current entry and will throw an exception if
    /// you attempt to write bytes past the length specified for the
    /// current entry. The method is also (painfully) aware of the
    /// record buffering required by TarBuffer, and manages buffers
    /// that are not a multiple of recordsize in length, including
    /// assembling records from small buffers.
    /// </summary>
    /// <param name="buffer">The buffer to write to the archive.</param>
    /// <param name="offset">
    /// The offset in the buffer from which to get bytes.
    /// </param>
    /// <param name="count">The number of bytes to write.</param>
    public override void Write(byte[] buffer, int offset, int count)
    {
      if (buffer == null)
        throw new ArgumentNullException(nameof (buffer));
      if (offset < 0)
        throw new ArgumentOutOfRangeException(nameof (offset), "Cannot be negative");
      if (buffer.Length - offset < count)
        throw new ArgumentException("offset and count combination is invalid");
      if (count < 0)
        throw new ArgumentOutOfRangeException(nameof (count), "Cannot be negative");
      if (this.currBytes + (long) count > this.currSize)
        throw new ArgumentOutOfRangeException(nameof (count), string.Format("request to write '{0}' bytes exceeds size in header of '{1}' bytes", (object) count, (object) this.currSize));
      if (this.assemblyBufferLength > 0)
      {
        if (this.assemblyBufferLength + count >= this.blockBuffer.Length)
        {
          int length = this.blockBuffer.Length - this.assemblyBufferLength;
          Array.Copy((Array) this.assemblyBuffer, 0, (Array) this.blockBuffer, 0, this.assemblyBufferLength);
          Array.Copy((Array) buffer, offset, (Array) this.blockBuffer, this.assemblyBufferLength, length);
          this.buffer.WriteBlock(this.blockBuffer);
          this.currBytes += (long) this.blockBuffer.Length;
          offset += length;
          count -= length;
          this.assemblyBufferLength = 0;
        }
        else
        {
          Array.Copy((Array) buffer, offset, (Array) this.assemblyBuffer, this.assemblyBufferLength, count);
          offset += count;
          this.assemblyBufferLength += count;
          count -= count;
        }
      }
      while (count > 0)
      {
        if (count < this.blockBuffer.Length)
        {
          Array.Copy((Array) buffer, offset, (Array) this.assemblyBuffer, this.assemblyBufferLength, count);
          this.assemblyBufferLength += count;
          break;
        }
        this.buffer.WriteBlock(buffer, offset);
        int length = this.blockBuffer.Length;
        this.currBytes += (long) length;
        count -= length;
        offset += length;
      }
    }

    /// <summary>
    /// Write an EOF (end of archive) block to the tar archive.
    /// An EOF block consists of all zeros.
    /// </summary>
    private void WriteEofBlock()
    {
      Array.Clear((Array) this.blockBuffer, 0, this.blockBuffer.Length);
      this.buffer.WriteBlock(this.blockBuffer);
    }
  }
}
