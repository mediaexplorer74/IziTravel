// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Tar.TarBuffer
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
  /// The TarBuffer class implements the tar archive concept
  /// of a buffered input stream. This concept goes back to the
  /// days of blocked tape drives and special io devices. In the
  /// C# universe, the only real function that this class
  /// performs is to ensure that files have the correct "record"
  /// size, or other tars will complain.
  /// <p>
  /// You should never have a need to access this class directly.
  /// TarBuffers are created by Tar IO Streams.
  /// </p>
  /// </summary>
  public class TarBuffer
  {
    /// <summary>The size of a block in a tar archive in bytes.</summary>
    /// <remarks>This is 512 bytes.</remarks>
    public const int BlockSize = 512;
    /// <summary>The number of blocks in a default record.</summary>
    /// <remarks>The default value is 20 blocks per record.</remarks>
    public const int DefaultBlockFactor = 20;
    /// <summary>The size in bytes of a default record.</summary>
    /// <remarks>The default size is 10KB.</remarks>
    public const int DefaultRecordSize = 10240;
    private Stream inputStream;
    private Stream outputStream;
    private byte[] recordBuffer;
    private int currentBlockIndex;
    private int currentRecordIndex;
    private int recordSize = 10240;
    private int blockFactor = 20;
    private bool isStreamOwner_ = true;

    /// <summary>Get the record size for this buffer</summary>
    /// <value>The record size in bytes.
    /// This is equal to the <see cref="P:ICSharpCode.SharpZipLib.Tar.TarBuffer.BlockFactor" /> multiplied by the <see cref="F:ICSharpCode.SharpZipLib.Tar.TarBuffer.BlockSize" /></value>
    public int RecordSize => this.recordSize;

    /// <summary>Get the TAR Buffer's record size.</summary>
    /// <returns>The record size in bytes.
    /// This is equal to the <see cref="P:ICSharpCode.SharpZipLib.Tar.TarBuffer.BlockFactor" /> multiplied by the <see cref="F:ICSharpCode.SharpZipLib.Tar.TarBuffer.BlockSize" /></returns>
    [Obsolete("Use RecordSize property instead")]
    public int GetRecordSize() => this.recordSize;

    /// <summary>Get the Blocking factor for the buffer</summary>
    /// <value>This is the number of blocks in each record.</value>
    public int BlockFactor => this.blockFactor;

    /// <summary>Get the TAR Buffer's block factor</summary>
    /// <returns>The block factor; the number of blocks per record.</returns>
    [Obsolete("Use BlockFactor property instead")]
    public int GetBlockFactor() => this.blockFactor;

    /// <summary>Construct a default TarBuffer</summary>
    protected TarBuffer()
    {
    }

    /// <summary>Create TarBuffer for reading with default BlockFactor</summary>
    /// <param name="inputStream">Stream to buffer</param>
    /// <returns>A new <see cref="T:ICSharpCode.SharpZipLib.Tar.TarBuffer" /> suitable for input.</returns>
    public static TarBuffer CreateInputTarBuffer(Stream inputStream)
    {
      return inputStream != null ? TarBuffer.CreateInputTarBuffer(inputStream, 20) : throw new ArgumentNullException(nameof (inputStream));
    }

    /// <summary>
    /// Construct TarBuffer for reading inputStream setting BlockFactor
    /// </summary>
    /// <param name="inputStream">Stream to buffer</param>
    /// <param name="blockFactor">Blocking factor to apply</param>
    /// <returns>A new <see cref="T:ICSharpCode.SharpZipLib.Tar.TarBuffer" /> suitable for input.</returns>
    public static TarBuffer CreateInputTarBuffer(Stream inputStream, int blockFactor)
    {
      if (inputStream == null)
        throw new ArgumentNullException(nameof (inputStream));
      if (blockFactor <= 0)
        throw new ArgumentOutOfRangeException(nameof (blockFactor), "Factor cannot be negative");
      TarBuffer inputTarBuffer = new TarBuffer();
      inputTarBuffer.inputStream = inputStream;
      inputTarBuffer.outputStream = (Stream) null;
      inputTarBuffer.Initialize(blockFactor);
      return inputTarBuffer;
    }

    /// <summary>
    /// Construct TarBuffer for writing with default BlockFactor
    /// </summary>
    /// <param name="outputStream">output stream for buffer</param>
    /// <returns>A new <see cref="T:ICSharpCode.SharpZipLib.Tar.TarBuffer" /> suitable for output.</returns>
    public static TarBuffer CreateOutputTarBuffer(Stream outputStream)
    {
      return outputStream != null ? TarBuffer.CreateOutputTarBuffer(outputStream, 20) : throw new ArgumentNullException(nameof (outputStream));
    }

    /// <summary>Construct TarBuffer for writing Tar output to streams.</summary>
    /// <param name="outputStream">Output stream to write to.</param>
    /// <param name="blockFactor">Blocking factor to apply</param>
    /// <returns>A new <see cref="T:ICSharpCode.SharpZipLib.Tar.TarBuffer" /> suitable for output.</returns>
    public static TarBuffer CreateOutputTarBuffer(Stream outputStream, int blockFactor)
    {
      if (outputStream == null)
        throw new ArgumentNullException(nameof (outputStream));
      if (blockFactor <= 0)
        throw new ArgumentOutOfRangeException(nameof (blockFactor), "Factor cannot be negative");
      TarBuffer outputTarBuffer = new TarBuffer();
      outputTarBuffer.inputStream = (Stream) null;
      outputTarBuffer.outputStream = outputStream;
      outputTarBuffer.Initialize(blockFactor);
      return outputTarBuffer;
    }

    /// <summary>Initialization common to all constructors.</summary>
    private void Initialize(int archiveBlockFactor)
    {
      this.blockFactor = archiveBlockFactor;
      this.recordSize = archiveBlockFactor * 512;
      this.recordBuffer = new byte[this.RecordSize];
      if (this.inputStream != null)
      {
        this.currentRecordIndex = -1;
        this.currentBlockIndex = this.BlockFactor;
      }
      else
      {
        this.currentRecordIndex = 0;
        this.currentBlockIndex = 0;
      }
    }

    /// <summary>
    /// Determine if an archive block indicates End of Archive. End of
    /// archive is indicated by a block that consists entirely of null bytes.
    /// All remaining blocks for the record should also be null's
    /// However some older tars only do a couple of null blocks (Old GNU tar for one)
    /// and also partial records
    /// </summary>
    /// <param name="block">The data block to check.</param>
    /// <returns>Returns true if the block is an EOF block; false otherwise.</returns>
    [Obsolete("Use IsEndOfArchiveBlock instead")]
    public bool IsEOFBlock(byte[] block)
    {
      if (block == null)
        throw new ArgumentNullException(nameof (block));
      if (block.Length != 512)
        throw new ArgumentException("block length is invalid");
      for (int index = 0; index < 512; ++index)
      {
        if (block[index] != (byte) 0)
          return false;
      }
      return true;
    }

    /// <summary>
    /// Determine if an archive block indicates the End of an Archive has been reached.
    /// End of archive is indicated by a block that consists entirely of null bytes.
    /// All remaining blocks for the record should also be null's
    /// However some older tars only do a couple of null blocks (Old GNU tar for one)
    /// and also partial records
    /// </summary>
    /// <param name="block">The data block to check.</param>
    /// <returns>Returns true if the block is an EOF block; false otherwise.</returns>
    public static bool IsEndOfArchiveBlock(byte[] block)
    {
      if (block == null)
        throw new ArgumentNullException(nameof (block));
      if (block.Length != 512)
        throw new ArgumentException("block length is invalid");
      for (int index = 0; index < 512; ++index)
      {
        if (block[index] != (byte) 0)
          return false;
      }
      return true;
    }

    /// <summary>Skip over a block on the input stream.</summary>
    public void SkipBlock()
    {
      if (this.inputStream == null)
        throw new TarException("no input stream defined");
      if (this.currentBlockIndex >= this.BlockFactor && !this.ReadRecord())
        throw new TarException("Failed to read a record");
      ++this.currentBlockIndex;
    }

    /// <summary>Read a block from the input stream.</summary>
    /// <returns>The block of data read.</returns>
    public byte[] ReadBlock()
    {
      if (this.inputStream == null)
        throw new TarException("TarBuffer.ReadBlock - no input stream defined");
      if (this.currentBlockIndex >= this.BlockFactor && !this.ReadRecord())
        throw new TarException("Failed to read a record");
      byte[] destinationArray = new byte[512];
      Array.Copy((Array) this.recordBuffer, this.currentBlockIndex * 512, (Array) destinationArray, 0, 512);
      ++this.currentBlockIndex;
      return destinationArray;
    }

    /// <summary>Read a record from data stream.</summary>
    /// <returns>false if End-Of-File, else true.</returns>
    private bool ReadRecord()
    {
      if (this.inputStream == null)
        throw new TarException("no input stream stream defined");
      this.currentBlockIndex = 0;
      int offset = 0;
      long num;
      for (int recordSize = this.RecordSize; recordSize > 0; recordSize -= (int) num)
      {
        num = (long) this.inputStream.Read(this.recordBuffer, offset, recordSize);
        if (num > 0L)
          offset += (int) num;
        else
          break;
      }
      ++this.currentRecordIndex;
      return true;
    }

    /// <summary>
    /// Get the current block number, within the current record, zero based.
    /// </summary>
    /// <remarks>Block numbers are zero based values</remarks>
    /// <seealso cref="P:ICSharpCode.SharpZipLib.Tar.TarBuffer.RecordSize" />
    public int CurrentBlock => this.currentBlockIndex;

    /// <summary>
    /// Get/set flag indicating ownership of the underlying stream.
    /// When the flag is true <see cref="M:ICSharpCode.SharpZipLib.Tar.TarBuffer.Close"></see> will close the underlying stream also.
    /// </summary>
    public bool IsStreamOwner
    {
      get => this.isStreamOwner_;
      set => this.isStreamOwner_ = value;
    }

    /// <summary>
    /// Get the current block number, within the current record, zero based.
    /// </summary>
    /// <returns>The current zero based block number.</returns>
    /// <remarks>
    /// The absolute block number = (<see cref="M:ICSharpCode.SharpZipLib.Tar.TarBuffer.GetCurrentRecordNum">record number</see> * <see cref="P:ICSharpCode.SharpZipLib.Tar.TarBuffer.BlockFactor">block factor</see>) + <see cref="M:ICSharpCode.SharpZipLib.Tar.TarBuffer.GetCurrentBlockNum">block number</see>.
    /// </remarks>
    [Obsolete("Use CurrentBlock property instead")]
    public int GetCurrentBlockNum() => this.currentBlockIndex;

    /// <summary>Get the current record number.</summary>
    /// <returns>The current zero based record number.</returns>
    public int CurrentRecord => this.currentRecordIndex;

    /// <summary>Get the current record number.</summary>
    /// <returns>The current zero based record number.</returns>
    [Obsolete("Use CurrentRecord property instead")]
    public int GetCurrentRecordNum() => this.currentRecordIndex;

    /// <summary>Write a block of data to the archive.</summary>
    /// <param name="block">The data to write to the archive.</param>
    public void WriteBlock(byte[] block)
    {
      if (block == null)
        throw new ArgumentNullException(nameof (block));
      if (this.outputStream == null)
        throw new TarException("TarBuffer.WriteBlock - no output stream defined");
      if (block.Length != 512)
        throw new TarException(string.Format("TarBuffer.WriteBlock - block to write has length '{0}' which is not the block size of '{1}'", (object) block.Length, (object) 512));
      if (this.currentBlockIndex >= this.BlockFactor)
        this.WriteRecord();
      Array.Copy((Array) block, 0, (Array) this.recordBuffer, this.currentBlockIndex * 512, 512);
      ++this.currentBlockIndex;
    }

    /// <summary>
    /// Write an archive record to the archive, where the record may be
    /// inside of a larger array buffer. The buffer must be "offset plus
    /// record size" long.
    /// </summary>
    /// <param name="buffer">
    /// The buffer containing the record data to write.
    /// </param>
    /// <param name="offset">The offset of the record data within buffer.</param>
    public void WriteBlock(byte[] buffer, int offset)
    {
      if (buffer == null)
        throw new ArgumentNullException(nameof (buffer));
      if (this.outputStream == null)
        throw new TarException("TarBuffer.WriteBlock - no output stream stream defined");
      if (offset < 0 || offset >= buffer.Length)
        throw new ArgumentOutOfRangeException(nameof (offset));
      if (offset + 512 > buffer.Length)
        throw new TarException(string.Format("TarBuffer.WriteBlock - record has length '{0}' with offset '{1}' which is less than the record size of '{2}'", (object) buffer.Length, (object) offset, (object) this.recordSize));
      if (this.currentBlockIndex >= this.BlockFactor)
        this.WriteRecord();
      Array.Copy((Array) buffer, offset, (Array) this.recordBuffer, this.currentBlockIndex * 512, 512);
      ++this.currentBlockIndex;
    }

    /// <summary>Write a TarBuffer record to the archive.</summary>
    private void WriteRecord()
    {
      if (this.outputStream == null)
        throw new TarException("TarBuffer.WriteRecord no output stream defined");
      this.outputStream.Write(this.recordBuffer, 0, this.RecordSize);
      this.outputStream.Flush();
      this.currentBlockIndex = 0;
      ++this.currentRecordIndex;
    }

    /// <summary>
    /// WriteFinalRecord writes the current record buffer to output any unwritten data is present.
    /// </summary>
    /// <remarks>Any trailing bytes are set to zero which is by definition correct behaviour
    /// for the end of a tar stream.</remarks>
    private void WriteFinalRecord()
    {
      if (this.outputStream == null)
        throw new TarException("TarBuffer.WriteFinalRecord no output stream defined");
      if (this.currentBlockIndex > 0)
      {
        int index = this.currentBlockIndex * 512;
        Array.Clear((Array) this.recordBuffer, index, this.RecordSize - index);
        this.WriteRecord();
      }
      this.outputStream.Flush();
    }

    /// <summary>
    /// Close the TarBuffer. If this is an output buffer, also flush the
    /// current block before closing.
    /// </summary>
    public void Close()
    {
      if (this.outputStream != null)
      {
        this.WriteFinalRecord();
        if (this.isStreamOwner_)
          this.outputStream.Dispose();
        this.outputStream = (Stream) null;
      }
      else
      {
        if (this.inputStream == null)
          return;
        if (this.isStreamOwner_)
          this.inputStream.Dispose();
        this.inputStream = (Stream) null;
      }
    }
  }
}
