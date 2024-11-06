// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.ZipExtraData
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51802, Culture=neutral, PublicKeyToken=null
// MVID: 6C361F8B-A5CA-49B8-ABC1-A50EE493C003
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.xml

using System;
using System.IO;

#nullable disable
namespace ICSharpCode.SharpZipLib.Zip
{
  /// <summary>A class to handle the extra data field for Zip entries</summary>
  /// <remarks>
  /// Extra data contains 0 or more values each prefixed by a header tag and length.
  /// They contain zero or more bytes of actual data.
  /// The data is held internally using a copy on write strategy.  This is more efficient but
  /// means that for extra data created by passing in data can have the values modified by the caller
  /// in some circumstances.
  /// </remarks>
  public sealed class ZipExtraData : IDisposable
  {
    private int _index;
    private int _readValueStart;
    private int _readValueLength;
    private MemoryStream _newEntry;
    private byte[] _data;

    /// <summary>Initialise a default instance.</summary>
    public ZipExtraData() => this.Clear();

    /// <summary>Initialise with known extra data.</summary>
    /// <param name="data">The extra data.</param>
    public ZipExtraData(byte[] data)
    {
      if (data == null)
        this._data = new byte[0];
      else
        this._data = data;
    }

    /// <summary>Get the raw extra data value</summary>
    /// <returns>Returns the raw byte[] extra data this instance represents.</returns>
    public byte[] GetEntryData()
    {
      if (this.Length > (int) ushort.MaxValue)
        throw new ZipException("Data exceeds maximum length");
      return (byte[]) this._data.Clone();
    }

    /// <summary>Clear the stored data.</summary>
    public void Clear()
    {
      if (this._data != null && this._data.Length == 0)
        return;
      this._data = new byte[0];
    }

    /// <summary>Gets the current extra data length.</summary>
    public int Length => this._data.Length;

    /// <summary>
    /// Get a read-only <see cref="T:System.IO.Stream" /> for the associated tag.
    /// </summary>
    /// <param name="tag">The tag to locate data for.</param>
    /// <returns>Returns a <see cref="T:System.IO.Stream" /> containing tag data or null if no tag was found.</returns>
    public Stream GetStreamForTag(int tag)
    {
      Stream streamForTag = (Stream) null;
      if (this.Find(tag))
        streamForTag = (Stream) new MemoryStream(this._data, this._index, this._readValueLength, false);
      return streamForTag;
    }

    /// <summary>
    /// Get the <see cref="T:ICSharpCode.SharpZipLib.Zip.ITaggedData">tagged data</see> for a tag.
    /// </summary>
    /// <param name="tag">The tag to search for.</param>
    /// <returns>Returns a <see cref="T:ICSharpCode.SharpZipLib.Zip.ITaggedData">tagged value</see> or null if none found.</returns>
    private ITaggedData GetData(short tag)
    {
      ITaggedData data = (ITaggedData) null;
      if (this.Find((int) tag))
        data = ZipExtraData.Create(tag, this._data, this._readValueStart, this._readValueLength);
      return data;
    }

    private static ITaggedData Create(short tag, byte[] data, int offset, int count)
    {
      ITaggedData taggedData;
      switch (tag)
      {
        case 10:
          taggedData = (ITaggedData) new NTTaggedData();
          break;
        case 21589:
          taggedData = (ITaggedData) new ExtendedUnixData();
          break;
        default:
          taggedData = (ITaggedData) new RawTaggedData(tag);
          break;
      }
      taggedData.SetData(data, offset, count);
      return taggedData;
    }

    /// <summary>
    /// Get the length of the last value found by <see cref="M:ICSharpCode.SharpZipLib.Zip.ZipExtraData.Find(System.Int32)" />
    /// </summary>
    /// <remarks>This is only valid if <see cref="M:ICSharpCode.SharpZipLib.Zip.ZipExtraData.Find(System.Int32)" /> has previously returned true.</remarks>
    public int ValueLength => this._readValueLength;

    /// <summary>Get the index for the current read value.</summary>
    /// <remarks>This is only valid if <see cref="M:ICSharpCode.SharpZipLib.Zip.ZipExtraData.Find(System.Int32)" /> has previously returned true.
    /// Initially the result will be the index of the first byte of actual data.  The value is updated after calls to
    /// <see cref="M:ICSharpCode.SharpZipLib.Zip.ZipExtraData.ReadInt" />, <see cref="M:ICSharpCode.SharpZipLib.Zip.ZipExtraData.ReadShort" /> and <see cref="M:ICSharpCode.SharpZipLib.Zip.ZipExtraData.ReadLong" />. </remarks>
    public int CurrentReadIndex => this._index;

    /// <summary>
    /// Get the number of bytes remaining to be read for the current value;
    /// </summary>
    public int UnreadCount
    {
      get
      {
        if (this._readValueStart > this._data.Length || this._readValueStart < 4)
          throw new ZipException("Find must be called before calling a Read method");
        return this._readValueStart + this._readValueLength - this._index;
      }
    }

    /// <summary>Find an extra data value</summary>
    /// <param name="headerID">The identifier for the value to find.</param>
    /// <returns>Returns true if the value was found; false otherwise.</returns>
    public bool Find(int headerID)
    {
      this._readValueStart = this._data.Length;
      this._readValueLength = 0;
      this._index = 0;
      int num1 = this._readValueStart;
      int num2 = headerID - 1;
      while (num2 != headerID && this._index < this._data.Length - 3)
      {
        num2 = this.ReadShortInternal();
        num1 = this.ReadShortInternal();
        if (num2 != headerID)
          this._index += num1;
      }
      bool flag = num2 == headerID && this._index + num1 <= this._data.Length;
      if (flag)
      {
        this._readValueStart = this._index;
        this._readValueLength = num1;
      }
      return flag;
    }

    /// <summary>Add a new entry to extra data.</summary>
    /// <param name="taggedData">The <see cref="T:ICSharpCode.SharpZipLib.Zip.ITaggedData" /> value to add.</param>
    public void AddEntry(ITaggedData taggedData)
    {
      if (taggedData == null)
        throw new ArgumentNullException(nameof (taggedData));
      this.AddEntry((int) taggedData.TagID, taggedData.GetData());
    }

    /// <summary>Add a new entry to extra data</summary>
    /// <param name="headerID">The ID for this entry.</param>
    /// <param name="fieldData">The data to add.</param>
    /// <remarks>If the ID already exists its contents are replaced.</remarks>
    public void AddEntry(int headerID, byte[] fieldData)
    {
      if (headerID > (int) ushort.MaxValue || headerID < 0)
        throw new ArgumentOutOfRangeException(nameof (headerID));
      int length1 = fieldData == null ? 0 : fieldData.Length;
      if (length1 > (int) ushort.MaxValue)
        throw new ArgumentOutOfRangeException(nameof (fieldData), "exceeds maximum length");
      int length2 = this._data.Length + length1 + 4;
      if (this.Find(headerID))
        length2 -= this.ValueLength + 4;
      if (length2 > (int) ushort.MaxValue)
        throw new ZipException("Data exceeds maximum length");
      this.Delete(headerID);
      byte[] numArray = new byte[length2];
      this._data.CopyTo((Array) numArray, 0);
      int length3 = this._data.Length;
      this._data = numArray;
      this.SetShort(ref length3, headerID);
      this.SetShort(ref length3, length1);
      fieldData?.CopyTo((Array) numArray, length3);
    }

    /// <summary>Start adding a new entry.</summary>
    /// <remarks>Add data using <see cref="M:ICSharpCode.SharpZipLib.Zip.ZipExtraData.AddData(System.Byte[])" />, <see cref="M:ICSharpCode.SharpZipLib.Zip.ZipExtraData.AddLeShort(System.Int32)" />, <see cref="M:ICSharpCode.SharpZipLib.Zip.ZipExtraData.AddLeInt(System.Int32)" />, or <see cref="M:ICSharpCode.SharpZipLib.Zip.ZipExtraData.AddLeLong(System.Int64)" />.
    /// The new entry is completed and actually added by calling <see cref="M:ICSharpCode.SharpZipLib.Zip.ZipExtraData.AddNewEntry(System.Int32)" /></remarks>
    /// <seealso cref="M:ICSharpCode.SharpZipLib.Zip.ZipExtraData.AddEntry(ICSharpCode.SharpZipLib.Zip.ITaggedData)" />
    public void StartNewEntry() => this._newEntry = new MemoryStream();

    /// <summary>
    /// Add entry data added since <see cref="M:ICSharpCode.SharpZipLib.Zip.ZipExtraData.StartNewEntry" /> using the ID passed.
    /// </summary>
    /// <param name="headerID">The identifier to use for this entry.</param>
    public void AddNewEntry(int headerID)
    {
      byte[] array = this._newEntry.ToArray();
      this._newEntry = (MemoryStream) null;
      this.AddEntry(headerID, array);
    }

    /// <summary>Add a byte of data to the pending new entry.</summary>
    /// <param name="data">The byte to add.</param>
    /// <seealso cref="M:ICSharpCode.SharpZipLib.Zip.ZipExtraData.StartNewEntry" />
    public void AddData(byte data) => this._newEntry.WriteByte(data);

    /// <summary>Add data to a pending new entry.</summary>
    /// <param name="data">The data to add.</param>
    /// <seealso cref="M:ICSharpCode.SharpZipLib.Zip.ZipExtraData.StartNewEntry" />
    public void AddData(byte[] data)
    {
      if (data == null)
        throw new ArgumentNullException(nameof (data));
      this._newEntry.Write(data, 0, data.Length);
    }

    /// <summary>
    /// Add a short value in little endian order to the pending new entry.
    /// </summary>
    /// <param name="toAdd">The data to add.</param>
    /// <seealso cref="M:ICSharpCode.SharpZipLib.Zip.ZipExtraData.StartNewEntry" />
    public void AddLeShort(int toAdd)
    {
      this._newEntry.WriteByte((byte) toAdd);
      this._newEntry.WriteByte((byte) (toAdd >> 8));
    }

    /// <summary>
    /// Add an integer value in little endian order to the pending new entry.
    /// </summary>
    /// <param name="toAdd">The data to add.</param>
    /// <seealso cref="M:ICSharpCode.SharpZipLib.Zip.ZipExtraData.StartNewEntry" />
    public void AddLeInt(int toAdd)
    {
      this.AddLeShort((int) (short) toAdd);
      this.AddLeShort((int) (short) (toAdd >> 16));
    }

    /// <summary>
    /// Add a long value in little endian order to the pending new entry.
    /// </summary>
    /// <param name="toAdd">The data to add.</param>
    /// <seealso cref="M:ICSharpCode.SharpZipLib.Zip.ZipExtraData.StartNewEntry" />
    public void AddLeLong(long toAdd)
    {
      this.AddLeInt((int) (toAdd & (long) uint.MaxValue));
      this.AddLeInt((int) (toAdd >> 32));
    }

    /// <summary>Delete an extra data field.</summary>
    /// <param name="headerID">The identifier of the field to delete.</param>
    /// <returns>Returns true if the field was found and deleted.</returns>
    public bool Delete(int headerID)
    {
      bool flag = false;
      if (this.Find(headerID))
      {
        flag = true;
        int num = this._readValueStart - 4;
        byte[] destinationArray = new byte[this._data.Length - (this.ValueLength + 4)];
        Array.Copy((Array) this._data, 0, (Array) destinationArray, 0, num);
        int sourceIndex = num + this.ValueLength + 4;
        Array.Copy((Array) this._data, sourceIndex, (Array) destinationArray, num, this._data.Length - sourceIndex);
        this._data = destinationArray;
      }
      return flag;
    }

    /// <summary>
    /// Read a long in little endian form from the last <see cref="M:ICSharpCode.SharpZipLib.Zip.ZipExtraData.Find(System.Int32)">found</see> data value
    /// </summary>
    /// <returns>Returns the long value read.</returns>
    public long ReadLong()
    {
      this.ReadCheck(8);
      return (long) this.ReadInt() & (long) uint.MaxValue | (long) this.ReadInt() << 32;
    }

    /// <summary>
    /// Read an integer in little endian form from the last <see cref="M:ICSharpCode.SharpZipLib.Zip.ZipExtraData.Find(System.Int32)">found</see> data value.
    /// </summary>
    /// <returns>Returns the integer read.</returns>
    public int ReadInt()
    {
      this.ReadCheck(4);
      int num = (int) this._data[this._index] + ((int) this._data[this._index + 1] << 8) + ((int) this._data[this._index + 2] << 16) + ((int) this._data[this._index + 3] << 24);
      this._index += 4;
      return num;
    }

    /// <summary>
    /// Read a short value in little endian form from the last <see cref="M:ICSharpCode.SharpZipLib.Zip.ZipExtraData.Find(System.Int32)">found</see> data value.
    /// </summary>
    /// <returns>Returns the short value read.</returns>
    public int ReadShort()
    {
      this.ReadCheck(2);
      int num = (int) this._data[this._index] + ((int) this._data[this._index + 1] << 8);
      this._index += 2;
      return num;
    }

    /// <summary>Read a byte from an extra data</summary>
    /// <returns>The byte value read or -1 if the end of data has been reached.</returns>
    public int ReadByte()
    {
      int num = -1;
      if (this._index < this._data.Length && this._readValueStart + this._readValueLength > this._index)
      {
        num = (int) this._data[this._index];
        ++this._index;
      }
      return num;
    }

    /// <summary>Skip data during reading.</summary>
    /// <param name="amount">The number of bytes to skip.</param>
    public void Skip(int amount)
    {
      this.ReadCheck(amount);
      this._index += amount;
    }

    private void ReadCheck(int length)
    {
      if (this._readValueStart > this._data.Length || this._readValueStart < 4)
        throw new ZipException("Find must be called before calling a Read method");
      if (this._index > this._readValueStart + this._readValueLength - length)
        throw new ZipException("End of extra data");
      if (this._index + length < 4)
        throw new ZipException("Cannot read before start of tag");
    }

    /// <summary>
    /// Internal form of <see cref="M:ICSharpCode.SharpZipLib.Zip.ZipExtraData.ReadShort" /> that reads data at any location.
    /// </summary>
    /// <returns>Returns the short value read.</returns>
    private int ReadShortInternal()
    {
      if (this._index > this._data.Length - 2)
        throw new ZipException("End of extra data");
      int num = (int) this._data[this._index] + ((int) this._data[this._index + 1] << 8);
      this._index += 2;
      return num;
    }

    private void SetShort(ref int index, int source)
    {
      this._data[index] = (byte) source;
      this._data[index + 1] = (byte) (source >> 8);
      index += 2;
    }

    /// <summary>Dispose of this instance.</summary>
    public void Dispose()
    {
      if (this._newEntry == null)
        return;
      this._newEntry.Dispose();
    }
  }
}
