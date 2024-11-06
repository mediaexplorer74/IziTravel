// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.NTTaggedData
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51802, Culture=neutral, PublicKeyToken=null
// MVID: 6C361F8B-A5CA-49B8-ABC1-A50EE493C003
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.xml

using System;
using System.IO;

#nullable disable
namespace ICSharpCode.SharpZipLib.Zip
{
  /// <summary>Class handling NT date time values.</summary>
  public class NTTaggedData : ITaggedData
  {
    private DateTime _lastAccessTime = DateTime.FromFileTime(0L);
    private DateTime _lastModificationTime = DateTime.FromFileTime(0L);
    private DateTime _createTime = DateTime.FromFileTime(0L);

    /// <summary>Get the ID for this tagged data value.</summary>
    public short TagID => 10;

    /// <summary>Set the data from the raw values provided.</summary>
    /// <param name="data">The raw data to extract values from.</param>
    /// <param name="index">The index to start extracting values from.</param>
    /// <param name="count">The number of bytes available.</param>
    public void SetData(byte[] data, int index, int count)
    {
      using (MemoryStream memoryStream = new MemoryStream(data, index, count, false))
      {
        using (ZipHelperStream zipHelperStream = new ZipHelperStream((Stream) memoryStream))
        {
          zipHelperStream.ReadLEInt();
          while (zipHelperStream.Position < zipHelperStream.Length)
          {
            int num = zipHelperStream.ReadLEShort();
            int offset = zipHelperStream.ReadLEShort();
            if (num == 1)
            {
              if (offset < 24)
                break;
              this._lastModificationTime = DateTime.FromFileTime(zipHelperStream.ReadLELong());
              this._lastAccessTime = DateTime.FromFileTime(zipHelperStream.ReadLELong());
              this._createTime = DateTime.FromFileTime(zipHelperStream.ReadLELong());
              break;
            }
            zipHelperStream.Seek((long) offset, SeekOrigin.Current);
          }
        }
      }
    }

    /// <summary>Get the binary data representing this instance.</summary>
    /// <returns>The raw binary data representing this instance.</returns>
    public byte[] GetData()
    {
      using (MemoryStream memoryStream = new MemoryStream())
      {
        using (ZipHelperStream zipHelperStream = new ZipHelperStream((Stream) memoryStream))
        {
          zipHelperStream.IsStreamOwner = false;
          zipHelperStream.WriteLEInt(0);
          zipHelperStream.WriteLEShort(1);
          zipHelperStream.WriteLEShort(24);
          zipHelperStream.WriteLELong(this._lastModificationTime.ToFileTime());
          zipHelperStream.WriteLELong(this._lastAccessTime.ToFileTime());
          zipHelperStream.WriteLELong(this._createTime.ToFileTime());
          return memoryStream.ToArray();
        }
      }
    }

    /// <summary>
    /// Test a <see cref="T:System.DateTime"> valuie to see if is valid and can be represented here.</see>
    /// </summary>
    /// <param name="value">The <see cref="T:System.DateTime">value</see> to test.</param>
    /// <returns>Returns true if the value is valid and can be represented; false if not.</returns>
    /// <remarks>
    /// NTFS filetimes are 64-bit unsigned integers, stored in Intel
    /// (least significant byte first) byte order. They determine the
    /// number of 1.0E-07 seconds (1/10th microseconds!) past WinNT "epoch",
    /// which is "01-Jan-1601 00:00:00 UTC". 28 May 60056 is the upper limit
    /// </remarks>
    public static bool IsValidValue(DateTime value)
    {
      bool flag = true;
      try
      {
        value.ToFileTimeUtc();
      }
      catch
      {
        flag = false;
      }
      return flag;
    }

    /// <summary>
    /// Get/set the <see cref="T:System.DateTime">last modification time</see>.
    /// </summary>
    public DateTime LastModificationTime
    {
      get => this._lastModificationTime;
      set
      {
        this._lastModificationTime = NTTaggedData.IsValidValue(value) ? value : throw new ArgumentOutOfRangeException(nameof (value));
      }
    }

    /// <summary>
    /// Get /set the <see cref="T:System.DateTime">create time</see>
    /// </summary>
    public DateTime CreateTime
    {
      get => this._createTime;
      set
      {
        this._createTime = NTTaggedData.IsValidValue(value) ? value : throw new ArgumentOutOfRangeException(nameof (value));
      }
    }

    /// <summary>
    /// Get /set the <see cref="T:System.DateTime">last access time</see>.
    /// </summary>
    public DateTime LastAccessTime
    {
      get => this._lastAccessTime;
      set
      {
        this._lastAccessTime = NTTaggedData.IsValidValue(value) ? value : throw new ArgumentOutOfRangeException(nameof (value));
      }
    }
  }
}
