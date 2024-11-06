// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.ZipOutputStream
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51802, Culture=neutral, PublicKeyToken=null
// MVID: 6C361F8B-A5CA-49B8-ABC1-A50EE493C003
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.xml

using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Zip.Compression;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using System;
using System.Collections.Generic;
using System.IO;

#nullable disable
namespace ICSharpCode.SharpZipLib.Zip
{
  /// <summary>
  /// This is a DeflaterOutputStream that writes the files into a zip
  /// archive one after another.  It has a special method to start a new
  /// zip entry.  The zip entries contains information about the file name
  /// size, compressed size, CRC, etc.
  /// 
  /// It includes support for Stored and Deflated entries.
  /// This class is not thread safe.
  /// <br />
  /// <br />Author of the original java version : Jochen Hoenicke
  /// </summary>
  /// <example> This sample shows how to create a zip file
  /// <code>
  /// using System;
  /// using System.IO;
  /// 
  /// using ICSharpCode.SharpZipLib.Core;
  /// using ICSharpCode.SharpZipLib.Zip;
  /// 
  /// class MainClass
  /// {
  /// 	public static void Main(string[] args)
  /// 	{
  /// 		string[] filenames = Directory.GetFiles(args[0]);
  /// 		byte[] buffer = new byte[4096];
  /// 
  /// 		using ( ZipOutputStream s = new ZipOutputStream(File.Create(args[1])) ) {
  /// 
  /// 			s.SetLevel(9); // 0 - store only to 9 - means best compression
  /// 
  /// 			foreach (string file in filenames) {
  /// 				ZipEntry entry = new ZipEntry(file);
  /// 				s.PutNextEntry(entry);
  /// 
  /// 				using (FileStream fs = File.OpenRead(file)) {
  /// 					StreamUtils.Copy(fs, s, buffer);
  /// 				}
  /// 			}
  /// 		}
  /// 	}
  /// }
  /// </code>
  /// </example>
  public class ZipOutputStream : DeflaterOutputStream
  {
    /// <summary>The entries for the archive.</summary>
    private ArrayList entries = new ArrayList();
    /// <summary>Used to track the crc of data added to entries.</summary>
    private Crc32 crc = new Crc32();
    /// <summary>The current entry being added.</summary>
    private ZipEntry curEntry;
    private int defaultCompressionLevel = -1;
    private CompressionMethod curMethod = CompressionMethod.Deflated;
    /// <summary>
    /// Used to track the size of data for an entry during writing.
    /// </summary>
    private long size;
    /// <summary>
    /// Offset to be recorded for each entry in the central header.
    /// </summary>
    private long offset;
    /// <summary>
    /// Comment for the entire archive recorded in central header.
    /// </summary>
    private byte[] zipComment = new byte[0];
    /// <summary>
    /// Flag indicating that header patching is required for the current entry.
    /// </summary>
    private bool patchEntryHeader;
    /// <summary>Position to patch crc</summary>
    private long crcPatchPos = -1;
    /// <summary>Position to patch size.</summary>
    private long sizePatchPos = -1;
    private UseZip64 useZip64_ = UseZip64.Dynamic;

    /// <summary>Creates a new Zip output stream, writing a zip archive.</summary>
    /// <param name="baseOutputStream">
    /// The output stream to which the archive contents are written.
    /// </param>
    public ZipOutputStream(Stream baseOutputStream)
      : base(baseOutputStream, new Deflater(-1, true))
    {
    }

    /// <summary>Creates a new Zip output stream, writing a zip archive.</summary>
    /// <param name="baseOutputStream">The output stream to which the archive contents are written.</param>
    /// <param name="bufferSize">Size of the buffer to use.</param>
    public ZipOutputStream(Stream baseOutputStream, int bufferSize)
      : base(baseOutputStream, new Deflater(-1, true), bufferSize)
    {
    }

    /// <summary>
    /// Gets a flag value of true if the central header has been added for this archive; false if it has not been added.
    /// </summary>
    /// <remarks>No further entries can be added once this has been done.</remarks>
    public bool IsFinished => this.entries == null;

    /// <summary>Set the zip file comment.</summary>
    /// <param name="comment">The comment text for the entire archive.</param>
    /// <exception name="ArgumentOutOfRangeException">
    /// The converted comment is longer than 0xffff bytes.
    /// </exception>
    public void SetComment(string comment)
    {
      byte[] array = ZipConstants.ConvertToArray(comment);
      this.zipComment = array.Length <= (int) ushort.MaxValue ? array : throw new ArgumentOutOfRangeException(nameof (comment));
    }

    /// <summary>
    /// Sets the compression level.  The new level will be activated
    /// immediately.
    /// </summary>
    /// <param name="level">The new compression level (1 to 9).</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// Level specified is not supported.
    /// </exception>
    /// <see cref="T:ICSharpCode.SharpZipLib.Zip.Compression.Deflater" />
    public void SetLevel(int level)
    {
      this.deflater_.SetLevel(level);
      this.defaultCompressionLevel = level;
    }

    /// <summary>Get the current deflater compression level</summary>
    /// <returns>The current compression level</returns>
    public int GetLevel() => this.deflater_.GetLevel();

    /// <summary>
    /// Get / set a value indicating how Zip64 Extension usage is determined when adding entries.
    /// </summary>
    /// <remarks>Older archivers may not understand Zip64 extensions.
    /// If backwards compatability is an issue be careful when adding <see cref="P:ICSharpCode.SharpZipLib.Zip.ZipEntry.Size">entries</see> to an archive.
    /// Setting this property to off is workable but less desirable as in those circumstances adding a file
    /// larger then 4GB will fail.</remarks>
    public UseZip64 UseZip64
    {
      get => this.useZip64_;
      set => this.useZip64_ = value;
    }

    /// <summary>Write an unsigned short in little endian byte order.</summary>
    private void WriteLeShort(int value)
    {
      this.baseOutputStream_.WriteByte((byte) (value & (int) byte.MaxValue));
      this.baseOutputStream_.WriteByte((byte) (value >> 8 & (int) byte.MaxValue));
    }

    /// <summary>Write an int in little endian byte order.</summary>
    private void WriteLeInt(int value)
    {
      this.WriteLeShort(value);
      this.WriteLeShort(value >> 16);
    }

    /// <summary>Write an int in little endian byte order.</summary>
    private void WriteLeLong(long value)
    {
      this.WriteLeInt((int) value);
      this.WriteLeInt((int) (value >> 32));
    }

    /// <summary>
    /// Starts a new Zip entry. It automatically closes the previous
    /// entry if present.
    /// All entry elements bar name are optional, but must be correct if present.
    /// If the compression method is stored and the output is not patchable
    /// the compression for that entry is automatically changed to deflate level 0
    /// </summary>
    /// <param name="entry">the entry.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// if entry passed is null.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    /// if an I/O error occured.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    /// if stream was finished
    /// </exception>
    /// <exception cref="T:ICSharpCode.SharpZipLib.Zip.ZipException">
    /// Too many entries in the Zip file<br />
    /// Entry name is too long<br />
    /// Finish has already been called<br />
    /// </exception>
    public void PutNextEntry(ZipEntry entry)
    {
      if (entry == null)
        throw new ArgumentNullException(nameof (entry));
      if (this.entries == null)
        throw new InvalidOperationException("ZipOutputStream was finished");
      if (this.curEntry != null)
        this.CloseEntry();
      if (this.entries.Count == int.MaxValue)
        throw new ZipException("Too many entries for Zip file");
      CompressionMethod compressionMethod = entry.CompressionMethod;
      int level = this.defaultCompressionLevel;
      entry.Flags &= 2048;
      this.patchEntryHeader = false;
      bool flag;
      if (entry.Size == 0L)
      {
        entry.CompressedSize = entry.Size;
        entry.Crc = 0L;
        compressionMethod = CompressionMethod.Stored;
        flag = true;
      }
      else
      {
        flag = entry.Size >= 0L && entry.HasCrc && entry.CompressedSize >= 0L;
        if (compressionMethod == CompressionMethod.Stored)
        {
          if (!flag)
          {
            if (!this.CanPatchEntries)
            {
              compressionMethod = CompressionMethod.Deflated;
              level = 0;
            }
          }
          else
          {
            entry.CompressedSize = entry.Size;
            flag = entry.HasCrc;
          }
        }
      }
      if (!flag)
      {
        if (!this.CanPatchEntries)
          entry.Flags |= 8;
        else
          this.patchEntryHeader = true;
      }
      if (this.Password != null)
      {
        entry.IsCrypted = true;
        if (entry.Crc < 0L)
          entry.Flags |= 8;
      }
      entry.Offset = this.offset;
      entry.CompressionMethod = compressionMethod;
      this.curMethod = compressionMethod;
      this.sizePatchPos = -1L;
      if (this.useZip64_ == UseZip64.On || entry.Size < 0L && this.useZip64_ == UseZip64.Dynamic)
        entry.ForceZip64();
      this.WriteLeInt(67324752);
      this.WriteLeShort(entry.Version);
      this.WriteLeShort(entry.Flags);
      this.WriteLeShort((int) (byte) entry.CompressionMethodForHeader);
      this.WriteLeInt((int) entry.DosTime);
      if (flag)
      {
        this.WriteLeInt((int) entry.Crc);
        if (entry.LocalHeaderRequiresZip64)
        {
          this.WriteLeInt(-1);
          this.WriteLeInt(-1);
        }
        else
        {
          this.WriteLeInt(entry.IsCrypted ? (int) entry.CompressedSize + 12 : (int) entry.CompressedSize);
          this.WriteLeInt((int) entry.Size);
        }
      }
      else
      {
        if (this.patchEntryHeader)
          this.crcPatchPos = this.baseOutputStream_.Position;
        this.WriteLeInt(0);
        if (this.patchEntryHeader)
          this.sizePatchPos = this.baseOutputStream_.Position;
        if (entry.LocalHeaderRequiresZip64 || this.patchEntryHeader)
        {
          this.WriteLeInt(-1);
          this.WriteLeInt(-1);
        }
        else
        {
          this.WriteLeInt(0);
          this.WriteLeInt(0);
        }
      }
      byte[] array = ZipConstants.ConvertToArray(entry.Flags, entry.Name);
      if (array.Length > (int) ushort.MaxValue)
        throw new ZipException("Entry name too long.");
      ZipExtraData zipExtraData = new ZipExtraData(entry.ExtraData);
      if (entry.LocalHeaderRequiresZip64)
      {
        zipExtraData.StartNewEntry();
        if (flag)
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
        if (this.patchEntryHeader)
          this.sizePatchPos = (long) zipExtraData.CurrentReadIndex;
      }
      else
        zipExtraData.Delete(1);
      byte[] entryData = zipExtraData.GetEntryData();
      this.WriteLeShort(array.Length);
      this.WriteLeShort(entryData.Length);
      if (array.Length > 0)
        this.baseOutputStream_.Write(array, 0, array.Length);
      if (entry.LocalHeaderRequiresZip64 && this.patchEntryHeader)
        this.sizePatchPos += this.baseOutputStream_.Position;
      if (entryData.Length > 0)
        this.baseOutputStream_.Write(entryData, 0, entryData.Length);
      this.offset += (long) (30 + array.Length + entryData.Length);
      if (entry.AESKeySize > 0)
        this.offset += (long) entry.AESOverheadSize;
      this.curEntry = entry;
      this.crc.Reset();
      if (compressionMethod == CompressionMethod.Deflated)
      {
        this.deflater_.Reset();
        this.deflater_.SetLevel(level);
      }
      this.size = 0L;
      if (!entry.IsCrypted)
        return;
      if (entry.Crc < 0L)
        this.WriteEncryptionHeader(entry.DosTime << 16);
      else
        this.WriteEncryptionHeader(entry.Crc);
    }

    /// <summary>
    /// Closes the current entry, updating header and footer information as required
    /// </summary>
    /// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
    /// <exception cref="T:System.InvalidOperationException">
    /// No entry is active.
    /// </exception>
    public void CloseEntry()
    {
      if (this.curEntry == null)
        throw new InvalidOperationException("No open entry");
      long num = this.size;
      if (this.curMethod == CompressionMethod.Deflated)
      {
        if (this.size >= 0L)
        {
          base.Finish();
          num = this.deflater_.TotalOut;
        }
        else
          this.deflater_.Reset();
      }
      if (this.curEntry.Size < 0L)
        this.curEntry.Size = this.size;
      else if (this.curEntry.Size != this.size)
        throw new ZipException("size was " + (object) this.size + ", but I expected " + (object) this.curEntry.Size);
      if (this.curEntry.CompressedSize < 0L)
        this.curEntry.CompressedSize = num;
      else if (this.curEntry.CompressedSize != num)
        throw new ZipException("compressed size was " + (object) num + ", but I expected " + (object) this.curEntry.CompressedSize);
      if (this.curEntry.Crc < 0L)
        this.curEntry.Crc = this.crc.Value;
      else if (this.curEntry.Crc != this.crc.Value)
        throw new ZipException("crc was " + (object) this.crc.Value + ", but I expected " + (object) this.curEntry.Crc);
      this.offset += num;
      if (this.curEntry.IsCrypted)
      {
        if (this.curEntry.AESKeySize > 0)
          this.curEntry.CompressedSize += (long) this.curEntry.AESOverheadSize;
        else
          this.curEntry.CompressedSize += 12L;
      }
      if (this.patchEntryHeader)
      {
        this.patchEntryHeader = false;
        long position = this.baseOutputStream_.Position;
        this.baseOutputStream_.Seek(this.crcPatchPos, SeekOrigin.Begin);
        this.WriteLeInt((int) this.curEntry.Crc);
        if (this.curEntry.LocalHeaderRequiresZip64)
        {
          if (this.sizePatchPos == -1L)
            throw new ZipException("Entry requires zip64 but this has been turned off");
          this.baseOutputStream_.Seek(this.sizePatchPos, SeekOrigin.Begin);
          this.WriteLeLong(this.curEntry.Size);
          this.WriteLeLong(this.curEntry.CompressedSize);
        }
        else
        {
          this.WriteLeInt((int) this.curEntry.CompressedSize);
          this.WriteLeInt((int) this.curEntry.Size);
        }
        this.baseOutputStream_.Seek(position, SeekOrigin.Begin);
      }
      if ((this.curEntry.Flags & 8) != 0)
      {
        this.WriteLeInt(134695760);
        this.WriteLeInt((int) this.curEntry.Crc);
        if (this.curEntry.LocalHeaderRequiresZip64)
        {
          this.WriteLeLong(this.curEntry.CompressedSize);
          this.WriteLeLong(this.curEntry.Size);
          this.offset += 24L;
        }
        else
        {
          this.WriteLeInt((int) this.curEntry.CompressedSize);
          this.WriteLeInt((int) this.curEntry.Size);
          this.offset += 16L;
        }
      }
      this.entries.Add((object) this.curEntry);
      this.curEntry = (ZipEntry) null;
    }

    private void WriteEncryptionHeader(long crcValue)
    {
      this.offset += 12L;
      this.InitializePassword(this.Password);
      byte[] buffer = new byte[12];
      new Random().NextBytes(buffer);
      buffer[11] = (byte) (crcValue >> 24);
      this.EncryptBlock(buffer, 0, buffer.Length);
      this.baseOutputStream_.Write(buffer, 0, buffer.Length);
    }

    /// <summary>Writes the given buffer to the current entry.</summary>
    /// <param name="buffer">The buffer containing data to write.</param>
    /// <param name="offset">The offset of the first byte to write.</param>
    /// <param name="count">The number of bytes to write.</param>
    /// <exception cref="T:ICSharpCode.SharpZipLib.Zip.ZipException">Archive size is invalid</exception>
    /// <exception cref="T:System.InvalidOperationException">No entry is active.</exception>
    public override void Write(byte[] buffer, int offset, int count)
    {
      if (this.curEntry == null)
        throw new InvalidOperationException("No open entry.");
      if (buffer == null)
        throw new ArgumentNullException(nameof (buffer));
      if (offset < 0)
        throw new ArgumentOutOfRangeException(nameof (offset), "Cannot be negative");
      if (count < 0)
        throw new ArgumentOutOfRangeException(nameof (count), "Cannot be negative");
      if (buffer.Length - offset < count)
        throw new ArgumentException("Invalid offset/count combination");
      this.crc.Update(buffer, offset, count);
      this.size += (long) count;
      switch (this.curMethod)
      {
        case CompressionMethod.Stored:
          if (this.Password != null)
          {
            this.CopyAndEncrypt(buffer, offset, count);
            break;
          }
          this.baseOutputStream_.Write(buffer, offset, count);
          break;
        case CompressionMethod.Deflated:
          base.Write(buffer, offset, count);
          break;
      }
    }

    private void CopyAndEncrypt(byte[] buffer, int offset, int count)
    {
      byte[] numArray = new byte[4096];
      while (count > 0)
      {
        int num = count < 4096 ? count : 4096;
        Array.Copy((Array) buffer, offset, (Array) numArray, 0, num);
        this.EncryptBlock(numArray, 0, num);
        this.baseOutputStream_.Write(numArray, 0, num);
        count -= num;
        offset += num;
      }
    }

    /// <summary>
    /// Finishes the stream.  This will write the central directory at the
    /// end of the zip file and flush the stream.
    /// </summary>
    /// <remarks>This is automatically called when the stream is closed.</remarks>
    /// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
    /// <exception cref="T:ICSharpCode.SharpZipLib.Zip.ZipException">
    /// Comment exceeds the maximum length<br />
    /// Entry name exceeds the maximum length
    /// </exception>
    public override void Finish()
    {
      if (this.entries == null)
        return;
      if (this.curEntry != null)
        this.CloseEntry();
      long count = (long) this.entries.Count;
      long sizeEntries = 0;
      foreach (ZipEntry entry in (List<object>) this.entries)
      {
        this.WriteLeInt(33639248);
        this.WriteLeShort(51);
        this.WriteLeShort(entry.Version);
        this.WriteLeShort(entry.Flags);
        this.WriteLeShort((int) (short) entry.CompressionMethodForHeader);
        this.WriteLeInt((int) entry.DosTime);
        this.WriteLeInt((int) entry.Crc);
        if (entry.IsZip64Forced() || entry.CompressedSize >= (long) uint.MaxValue)
          this.WriteLeInt(-1);
        else
          this.WriteLeInt((int) entry.CompressedSize);
        if (entry.IsZip64Forced() || entry.Size >= (long) uint.MaxValue)
          this.WriteLeInt(-1);
        else
          this.WriteLeInt((int) entry.Size);
        byte[] array = ZipConstants.ConvertToArray(entry.Flags, entry.Name);
        if (array.Length > (int) ushort.MaxValue)
          throw new ZipException("Name too long.");
        ZipExtraData zipExtraData = new ZipExtraData(entry.ExtraData);
        if (entry.CentralHeaderRequiresZip64)
        {
          zipExtraData.StartNewEntry();
          if (entry.IsZip64Forced() || entry.Size >= (long) uint.MaxValue)
            zipExtraData.AddLeLong(entry.Size);
          if (entry.IsZip64Forced() || entry.CompressedSize >= (long) uint.MaxValue)
            zipExtraData.AddLeLong(entry.CompressedSize);
          if (entry.Offset >= (long) uint.MaxValue)
            zipExtraData.AddLeLong(entry.Offset);
          zipExtraData.AddNewEntry(1);
        }
        else
          zipExtraData.Delete(1);
        byte[] entryData = zipExtraData.GetEntryData();
        byte[] buffer = entry.Comment != null ? ZipConstants.ConvertToArray(entry.Flags, entry.Comment) : new byte[0];
        if (buffer.Length > (int) ushort.MaxValue)
          throw new ZipException("Comment too long.");
        this.WriteLeShort(array.Length);
        this.WriteLeShort(entryData.Length);
        this.WriteLeShort(buffer.Length);
        this.WriteLeShort(0);
        this.WriteLeShort(0);
        if (entry.ExternalFileAttributes != -1)
          this.WriteLeInt(entry.ExternalFileAttributes);
        else if (entry.IsDirectory)
          this.WriteLeInt(16);
        else
          this.WriteLeInt(0);
        if (entry.Offset >= (long) uint.MaxValue)
          this.WriteLeInt(-1);
        else
          this.WriteLeInt((int) entry.Offset);
        if (array.Length > 0)
          this.baseOutputStream_.Write(array, 0, array.Length);
        if (entryData.Length > 0)
          this.baseOutputStream_.Write(entryData, 0, entryData.Length);
        if (buffer.Length > 0)
          this.baseOutputStream_.Write(buffer, 0, buffer.Length);
        sizeEntries += (long) (46 + array.Length + entryData.Length + buffer.Length);
      }
      using (ZipHelperStream zipHelperStream = new ZipHelperStream(this.baseOutputStream_))
        zipHelperStream.WriteEndOfCentralDirectory(count, sizeEntries, this.offset, this.zipComment);
      this.entries = (ArrayList) null;
    }
  }
}
