// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.ZipHelperStream
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51802, Culture=neutral, PublicKeyToken=null
// MVID: 6C361F8B-A5CA-49B8-ABC1-A50EE493C003
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.xml

using System;
using System.IO;

#nullable disable
namespace ICSharpCode.SharpZipLib.Zip
{
  /// <summary>This class assists with writing/reading from Zip files.</summary>
  internal class ZipHelperStream : Stream
  {
    private bool isOwner_;
    private Stream stream_;

    /// <summary>Initialise an instance of this class.</summary>
    /// <param name="name">The name of the file to open.</param>
    public ZipHelperStream(string name)
    {
      this.stream_ = (Stream) VFS.Current.OpenWriteFile(name);
      this.isOwner_ = true;
    }

    /// <summary>
    /// Initialise a new instance of <see cref="T:ICSharpCode.SharpZipLib.Zip.ZipHelperStream" />.
    /// </summary>
    /// <param name="stream">The stream to use.</param>
    public ZipHelperStream(Stream stream) => this.stream_ = stream;

    /// <summary>
    /// Get / set a value indicating wether the the underlying stream is owned or not.
    /// </summary>
    /// <remarks>If the stream is owned it is closed when this instance is closed.</remarks>
    public bool IsStreamOwner
    {
      get => this.isOwner_;
      set => this.isOwner_ = value;
    }

    public override bool CanRead => this.stream_.CanRead;

    public override bool CanSeek => this.stream_.CanSeek;

    public override bool CanTimeout => this.stream_.CanTimeout;

    public override long Length => this.stream_.Length;

    public override long Position
    {
      get => this.stream_.Position;
      set => this.stream_.Position = value;
    }

    public override bool CanWrite => this.stream_.CanWrite;

    public override void Flush() => this.stream_.Flush();

    public override long Seek(long offset, SeekOrigin origin) => this.stream_.Seek(offset, origin);

    public override void SetLength(long value) => this.stream_.SetLength(value);

    public override int Read(byte[] buffer, int offset, int count)
    {
      return this.stream_.Read(buffer, offset, count);
    }

    public override void Write(byte[] buffer, int offset, int count)
    {
      this.stream_.Write(buffer, offset, count);
    }

    /// <summary>Close the stream.</summary>
    /// <remarks>
    /// The underlying stream is closed only if <see cref="P:ICSharpCode.SharpZipLib.Zip.ZipHelperStream.IsStreamOwner" /> is true.
    /// </remarks>
    protected override void Dispose(bool disposing)
    {
      base.Dispose(disposing);
      if (!disposing)
        return;
      Stream stream = this.stream_;
      this.stream_ = (Stream) null;
      if (!this.isOwner_ || stream == null)
        return;
      this.isOwner_ = false;
      stream.Dispose();
    }

    private void WriteLocalHeader(ZipEntry entry, EntryPatchData patchData)
    {
      CompressionMethod compressionMethod = entry.CompressionMethod;
      bool flag1 = true;
      bool flag2 = false;
      this.WriteLEInt(67324752);
      this.WriteLEShort(entry.Version);
      this.WriteLEShort(entry.Flags);
      this.WriteLEShort((int) (byte) compressionMethod);
      this.WriteLEInt((int) entry.DosTime);
      if (flag1)
      {
        this.WriteLEInt((int) entry.Crc);
        if (entry.LocalHeaderRequiresZip64)
        {
          this.WriteLEInt(-1);
          this.WriteLEInt(-1);
        }
        else
        {
          this.WriteLEInt(entry.IsCrypted ? (int) entry.CompressedSize + 12 : (int) entry.CompressedSize);
          this.WriteLEInt((int) entry.Size);
        }
      }
      else
      {
        if (patchData != null)
          patchData.CrcPatchOffset = this.stream_.Position;
        this.WriteLEInt(0);
        if (patchData != null)
          patchData.SizePatchOffset = this.stream_.Position;
        if (entry.LocalHeaderRequiresZip64 && flag2)
        {
          this.WriteLEInt(-1);
          this.WriteLEInt(-1);
        }
        else
        {
          this.WriteLEInt(0);
          this.WriteLEInt(0);
        }
      }
      byte[] array = ZipConstants.ConvertToArray(entry.Flags, entry.Name);
      if (array.Length > (int) ushort.MaxValue)
        throw new ZipException("Entry name too long.");
      ZipExtraData zipExtraData = new ZipExtraData(entry.ExtraData);
      if (entry.LocalHeaderRequiresZip64 && (flag1 || flag2))
      {
        zipExtraData.StartNewEntry();
        if (flag1)
        {
          zipExtraData.AddLeLong(entry.Size);
          zipExtraData.AddLeLong(entry.CompressedSize);
        }
        else
        {
          zipExtraData.AddLeLong(-1L);
          zipExtraData.AddLeLong(-1L);
        }
        zipExtraData.AddNewEntry(1);
        if (!zipExtraData.Find(1))
          throw new ZipException("Internal error cant find extra data");
        if (patchData != null)
          patchData.SizePatchOffset = (long) zipExtraData.CurrentReadIndex;
      }
      else
        zipExtraData.Delete(1);
      byte[] entryData = zipExtraData.GetEntryData();
      this.WriteLEShort(array.Length);
      this.WriteLEShort(entryData.Length);
      if (array.Length > 0)
        this.stream_.Write(array, 0, array.Length);
      if (entry.LocalHeaderRequiresZip64 && flag2)
        patchData.SizePatchOffset += this.stream_.Position;
      if (entryData.Length <= 0)
        return;
      this.stream_.Write(entryData, 0, entryData.Length);
    }

    /// <summary>
    /// Locates a block with the desired <paramref name="signature" />.
    /// </summary>
    /// <param name="signature">The signature to find.</param>
    /// <param name="endLocation">Location, marking the end of block.</param>
    /// <param name="minimumBlockSize">Minimum size of the block.</param>
    /// <param name="maximumVariableData">The maximum variable data.</param>
    /// <returns>Eeturns the offset of the first byte after the signature; -1 if not found</returns>
    public long LocateBlockWithSignature(
      int signature,
      long endLocation,
      int minimumBlockSize,
      int maximumVariableData)
    {
      long num1 = endLocation - (long) minimumBlockSize;
      if (num1 < 0L)
        return -1;
      long num2 = Math.Max(num1 - (long) maximumVariableData, 0L);
      while (num1 >= num2)
      {
        this.Seek(num1--, SeekOrigin.Begin);
        if (this.ReadLEInt() == signature)
          return this.Position;
      }
      return -1;
    }

    /// <summary>
    /// Write Zip64 end of central directory records (File header and locator).
    /// </summary>
    /// <param name="noOfEntries">The number of entries in the central directory.</param>
    /// <param name="sizeEntries">The size of entries in the central directory.</param>
    /// <param name="centralDirOffset">The offset of the dentral directory.</param>
    public void WriteZip64EndOfCentralDirectory(
      long noOfEntries,
      long sizeEntries,
      long centralDirOffset)
    {
      long position = this.stream_.Position;
      this.WriteLEInt(101075792);
      this.WriteLELong(44L);
      this.WriteLEShort(51);
      this.WriteLEShort(45);
      this.WriteLEInt(0);
      this.WriteLEInt(0);
      this.WriteLELong(noOfEntries);
      this.WriteLELong(noOfEntries);
      this.WriteLELong(sizeEntries);
      this.WriteLELong(centralDirOffset);
      this.WriteLEInt(117853008);
      this.WriteLEInt(0);
      this.WriteLELong(position);
      this.WriteLEInt(1);
    }

    /// <summary>
    /// Write the required records to end the central directory.
    /// </summary>
    /// <param name="noOfEntries">The number of entries in the directory.</param>
    /// <param name="sizeEntries">The size of the entries in the directory.</param>
    /// <param name="startOfCentralDirectory">The start of the central directory.</param>
    /// <param name="comment">The archive comment.  (This can be null).</param>
    public void WriteEndOfCentralDirectory(
      long noOfEntries,
      long sizeEntries,
      long startOfCentralDirectory,
      byte[] comment)
    {
      if (noOfEntries >= (long) ushort.MaxValue || startOfCentralDirectory >= (long) uint.MaxValue || sizeEntries >= (long) uint.MaxValue)
        this.WriteZip64EndOfCentralDirectory(noOfEntries, sizeEntries, startOfCentralDirectory);
      this.WriteLEInt(101010256);
      this.WriteLEShort(0);
      this.WriteLEShort(0);
      if (noOfEntries >= (long) ushort.MaxValue)
      {
        this.WriteLEUshort(ushort.MaxValue);
        this.WriteLEUshort(ushort.MaxValue);
      }
      else
      {
        this.WriteLEShort((int) (short) noOfEntries);
        this.WriteLEShort((int) (short) noOfEntries);
      }
      if (sizeEntries >= (long) uint.MaxValue)
        this.WriteLEUint(uint.MaxValue);
      else
        this.WriteLEInt((int) sizeEntries);
      if (startOfCentralDirectory >= (long) uint.MaxValue)
        this.WriteLEUint(uint.MaxValue);
      else
        this.WriteLEInt((int) startOfCentralDirectory);
      int length = comment != null ? comment.Length : 0;
      if (length > (int) ushort.MaxValue)
        throw new ZipException(string.Format("Comment length({0}) is too long can only be 64K", (object) length));
      this.WriteLEShort(length);
      if (length <= 0)
        return;
      this.Write(comment, 0, comment.Length);
    }

    /// <summary>Read an unsigned short in little endian byte order.</summary>
    /// <returns>Returns the value read.</returns>
    /// <exception cref="T:System.IO.IOException">An i/o error occurs.</exception>
    /// <exception cref="T:System.IO.EndOfStreamException">
    /// The file ends prematurely
    /// </exception>
    public int ReadLEShort()
    {
      int num1 = this.stream_.ReadByte();
      if (num1 < 0)
        throw new EndOfStreamException();
      int num2 = this.stream_.ReadByte();
      if (num2 < 0)
        throw new EndOfStreamException();
      return num1 | num2 << 8;
    }

    /// <summary>Read an int in little endian byte order.</summary>
    /// <returns>Returns the value read.</returns>
    /// <exception cref="T:System.IO.IOException">An i/o error occurs.</exception>
    /// <exception cref="T:System.IO.EndOfStreamException">
    /// The file ends prematurely
    /// </exception>
    public int ReadLEInt() => this.ReadLEShort() | this.ReadLEShort() << 16;

    /// <summary>Read a long in little endian byte order.</summary>
    /// <returns>The value read.</returns>
    public long ReadLELong() => (long) (uint) this.ReadLEInt() | (long) this.ReadLEInt() << 32;

    /// <summary>Write an unsigned short in little endian byte order.</summary>
    /// <param name="value">The value to write.</param>
    public void WriteLEShort(int value)
    {
      this.stream_.WriteByte((byte) (value & (int) byte.MaxValue));
      this.stream_.WriteByte((byte) (value >> 8 & (int) byte.MaxValue));
    }

    /// <summary>Write a ushort in little endian byte order.</summary>
    /// <param name="value">The value to write.</param>
    public void WriteLEUshort(ushort value)
    {
      this.stream_.WriteByte((byte) ((uint) value & (uint) byte.MaxValue));
      this.stream_.WriteByte((byte) ((uint) value >> 8));
    }

    /// <summary>Write an int in little endian byte order.</summary>
    /// <param name="value">The value to write.</param>
    public void WriteLEInt(int value)
    {
      this.WriteLEShort(value);
      this.WriteLEShort(value >> 16);
    }

    /// <summary>Write a uint in little endian byte order.</summary>
    /// <param name="value">The value to write.</param>
    public void WriteLEUint(uint value)
    {
      this.WriteLEUshort((ushort) (value & (uint) ushort.MaxValue));
      this.WriteLEUshort((ushort) (value >> 16));
    }

    /// <summary>Write a long in little endian byte order.</summary>
    /// <param name="value">The value to write.</param>
    public void WriteLELong(long value)
    {
      this.WriteLEInt((int) value);
      this.WriteLEInt((int) (value >> 32));
    }

    /// <summary>Write a ulong in little endian byte order.</summary>
    /// <param name="value">The value to write.</param>
    public void WriteLEUlong(ulong value)
    {
      this.WriteLEUint((uint) (value & (ulong) uint.MaxValue));
      this.WriteLEUint((uint) (value >> 32));
    }

    /// <summary>Write a data descriptor.</summary>
    /// <param name="entry">The entry to write a descriptor for.</param>
    /// <returns>Returns the number of descriptor bytes written.</returns>
    public int WriteDataDescriptor(ZipEntry entry)
    {
      if (entry == null)
        throw new ArgumentNullException(nameof (entry));
      int num1 = 0;
      if ((entry.Flags & 8) != 0)
      {
        this.WriteLEInt(134695760);
        this.WriteLEInt((int) entry.Crc);
        int num2 = num1 + 8;
        if (entry.LocalHeaderRequiresZip64)
        {
          this.WriteLELong(entry.CompressedSize);
          this.WriteLELong(entry.Size);
          num1 = num2 + 16;
        }
        else
        {
          this.WriteLEInt((int) entry.CompressedSize);
          this.WriteLEInt((int) entry.Size);
          num1 = num2 + 8;
        }
      }
      return num1;
    }

    /// <summary>Read data descriptor at the end of compressed data.</summary>
    /// <param name="zip64">if set to <c>true</c> [zip64].</param>
    /// <param name="data">The data to fill in.</param>
    /// <returns>Returns the number of bytes read in the descriptor.</returns>
    public void ReadDataDescriptor(bool zip64, DescriptorData data)
    {
      if (this.ReadLEInt() != 134695760)
        throw new ZipException("Data descriptor signature not found");
      data.Crc = (long) this.ReadLEInt();
      if (zip64)
      {
        data.CompressedSize = this.ReadLELong();
        data.Size = this.ReadLELong();
      }
      else
      {
        data.CompressedSize = (long) this.ReadLEInt();
        data.Size = (long) this.ReadLEInt();
      }
    }
  }
}
