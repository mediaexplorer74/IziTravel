// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.ExtendedUnixData
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51802, Culture=neutral, PublicKeyToken=null
// MVID: 6C361F8B-A5CA-49B8-ABC1-A50EE493C003
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.xml

using System;
using System.IO;

#nullable disable
namespace ICSharpCode.SharpZipLib.Zip
{
  /// <summary>Class representing extended unix date time values.</summary>
  public class ExtendedUnixData : ITaggedData
  {
    private ExtendedUnixData.Flags _flags;
    private DateTime _modificationTime = new DateTime(1970, 1, 1);
    private DateTime _lastAccessTime = new DateTime(1970, 1, 1);
    private DateTime _createTime = new DateTime(1970, 1, 1);

    /// <summary>Get the ID</summary>
    public short TagID => 21589;

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
          this._flags = (ExtendedUnixData.Flags) zipHelperStream.ReadByte();
          if ((this._flags & ExtendedUnixData.Flags.ModificationTime) != (ExtendedUnixData.Flags) 0 && count >= 5)
          {
            int seconds = zipHelperStream.ReadLEInt();
            this._modificationTime = (new DateTime(1970, 1, 1, 0, 0, 0).ToUniversalTime() + new TimeSpan(0, 0, 0, seconds, 0)).ToLocalTime();
          }
          if ((this._flags & ExtendedUnixData.Flags.AccessTime) != (ExtendedUnixData.Flags) 0)
          {
            int seconds = zipHelperStream.ReadLEInt();
            this._lastAccessTime = (new DateTime(1970, 1, 1, 0, 0, 0).ToUniversalTime() + new TimeSpan(0, 0, 0, seconds, 0)).ToLocalTime();
          }
          if ((this._flags & ExtendedUnixData.Flags.CreateTime) == (ExtendedUnixData.Flags) 0)
            return;
          int seconds1 = zipHelperStream.ReadLEInt();
          this._createTime = (new DateTime(1970, 1, 1, 0, 0, 0).ToUniversalTime() + new TimeSpan(0, 0, 0, seconds1, 0)).ToLocalTime();
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
          zipHelperStream.WriteByte((byte) this._flags);
          if ((this._flags & ExtendedUnixData.Flags.ModificationTime) != (ExtendedUnixData.Flags) 0)
          {
            int totalSeconds = (int) (this._modificationTime.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0).ToUniversalTime()).TotalSeconds;
            zipHelperStream.WriteLEInt(totalSeconds);
          }
          if ((this._flags & ExtendedUnixData.Flags.AccessTime) != (ExtendedUnixData.Flags) 0)
          {
            int totalSeconds = (int) (this._lastAccessTime.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0).ToUniversalTime()).TotalSeconds;
            zipHelperStream.WriteLEInt(totalSeconds);
          }
          if ((this._flags & ExtendedUnixData.Flags.CreateTime) != (ExtendedUnixData.Flags) 0)
          {
            int totalSeconds = (int) (this._createTime.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0).ToUniversalTime()).TotalSeconds;
            zipHelperStream.WriteLEInt(totalSeconds);
          }
          return memoryStream.ToArray();
        }
      }
    }

    /// <summary>
    /// Test a <see cref="T:System.DateTime"> value to see if is valid and can be represented here.</see>
    /// </summary>
    /// <param name="value">The <see cref="T:System.DateTime">value</see> to test.</param>
    /// <returns>Returns true if the value is valid and can be represented; false if not.</returns>
    /// <remarks>The standard Unix time is a signed integer data type, directly encoding the Unix time number,
    /// which is the number of seconds since 1970-01-01.
    /// Being 32 bits means the values here cover a range of about 136 years.
    /// The minimum representable time is 1901-12-13 20:45:52,
    /// and the maximum representable time is 2038-01-19 03:14:07.
    /// </remarks>
    public static bool IsValidValue(DateTime value)
    {
      return value >= new DateTime(1901, 12, 13, 20, 45, 52) || value <= new DateTime(2038, 1, 19, 3, 14, 7);
    }

    /// <summary>Get /set the Modification Time</summary>
    /// <exception cref="T:System.ArgumentOutOfRangeException"></exception>
    /// <seealso cref="M:ICSharpCode.SharpZipLib.Zip.ExtendedUnixData.IsValidValue(System.DateTime)"></seealso>
    public DateTime ModificationTime
    {
      get => this._modificationTime;
      set
      {
        if (!ExtendedUnixData.IsValidValue(value))
          throw new ArgumentOutOfRangeException(nameof (value));
        this._flags |= ExtendedUnixData.Flags.ModificationTime;
        this._modificationTime = value;
      }
    }

    /// <summary>Get / set the Access Time</summary>
    /// <exception cref="T:System.ArgumentOutOfRangeException"></exception>
    /// <seealso cref="M:ICSharpCode.SharpZipLib.Zip.ExtendedUnixData.IsValidValue(System.DateTime)"></seealso>
    public DateTime AccessTime
    {
      get => this._lastAccessTime;
      set
      {
        if (!ExtendedUnixData.IsValidValue(value))
          throw new ArgumentOutOfRangeException(nameof (value));
        this._flags |= ExtendedUnixData.Flags.AccessTime;
        this._lastAccessTime = value;
      }
    }

    /// <summary>Get / Set the Create Time</summary>
    /// <exception cref="T:System.ArgumentOutOfRangeException"></exception>
    /// <seealso cref="M:ICSharpCode.SharpZipLib.Zip.ExtendedUnixData.IsValidValue(System.DateTime)"></seealso>
    public DateTime CreateTime
    {
      get => this._createTime;
      set
      {
        if (!ExtendedUnixData.IsValidValue(value))
          throw new ArgumentOutOfRangeException(nameof (value));
        this._flags |= ExtendedUnixData.Flags.CreateTime;
        this._createTime = value;
      }
    }

    /// <summary>
    /// Get/set the <see cref="T:ICSharpCode.SharpZipLib.Zip.ExtendedUnixData.Flags">values</see> to include.
    /// </summary>
    private ExtendedUnixData.Flags Include
    {
      get => this._flags;
      set => this._flags = value;
    }

    /// <summary>
    /// Flags indicate which values are included in this instance.
    /// </summary>
    [System.Flags]
    public enum Flags : byte
    {
      /// <summary>The modification time is included</summary>
      ModificationTime = 1,
      /// <summary>The access time is included</summary>
      AccessTime = 2,
      /// <summary>The create time is included.</summary>
      CreateTime = 4,
    }
  }
}
