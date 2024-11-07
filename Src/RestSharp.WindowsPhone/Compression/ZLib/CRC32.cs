// Decompiled with JetBrains decompiler
// Type: RestSharp.Compression.ZLib.CRC32
// Assembly: RestSharp.WindowsPhone, Version=105.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EBEB4E14-2430-4192-BEF2-49D8C49DBCCE
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.xml

using System;
using System.IO;

#nullable disable
namespace RestSharp.Compression.ZLib
{
  /// <summary>
  /// Calculates a 32bit Cyclic Redundancy Checksum (CRC) using the same polynomial
  /// used by Zip. This type is used internally by DotNetZip; it is generally not used
  /// directly by applications wishing to create, read, or manipulate zip archive
  /// files.
  /// </summary>
  internal class CRC32
  {
    private const int BUFFER_SIZE = 8192;
    private long _TotalBytesRead;
    private static uint[] crc32Table;
    private uint _RunningCrc32Result = uint.MaxValue;

    /// <summary>
    /// indicates the total number of bytes read on the CRC stream.
    /// This is used when writing the ZipDirEntry when compressing files.
    /// </summary>
    public long TotalBytesRead => this._TotalBytesRead;

    /// <summary>Indicates the current CRC for all blocks slurped in.</summary>
    public int Crc32Result => ~(int) this._RunningCrc32Result;

    /// <summary>Returns the CRC32 for the specified stream.</summary>
    /// <param name="input">The stream over which to calculate the CRC32</param>
    /// <returns>the CRC32 calculation</returns>
    public int GetCrc32(Stream input) => this.GetCrc32AndCopy(input, (Stream) null);

    /// <summary>
    /// Returns the CRC32 for the specified stream, and writes the input into the
    /// output stream.
    /// </summary>
    /// <param name="input">The stream over which to calculate the CRC32</param>
    /// <param name="output">The stream into which to deflate the input</param>
    /// <returns>the CRC32 calculation</returns>
    public int GetCrc32AndCopy(Stream input, Stream output)
    {
      if (input == null)
        throw new ZlibException("The input stream must not be null.");
      byte[] numArray = new byte[8192];
      int count1 = 8192;
      this._TotalBytesRead = 0L;
      int count2 = input.Read(numArray, 0, count1);
      output?.Write(numArray, 0, count2);
      this._TotalBytesRead += (long) count2;
      while (count2 > 0)
      {
        this.SlurpBlock(numArray, 0, count2);
        count2 = input.Read(numArray, 0, count1);
        output?.Write(numArray, 0, count2);
        this._TotalBytesRead += (long) count2;
      }
      return ~(int) this._RunningCrc32Result;
    }

    /// <summary>
    /// Get the CRC32 for the given (word,byte) combo.  This is a computation
    /// defined by PKzip.
    /// </summary>
    /// <param name="W">The word to start with.</param>
    /// <param name="B">The byte to combine it with.</param>
    /// <returns>The CRC-ized result.</returns>
    public int ComputeCrc32(int W, byte B) => this._InternalComputeCrc32((uint) W, B);

    internal int _InternalComputeCrc32(uint W, byte B)
    {
      return (int) CRC32.crc32Table[(IntPtr) (uint) (((int) W ^ (int) B) & (int) byte.MaxValue)] ^ (int) (W >> 8);
    }

    /// <summary>
    /// Update the value for the running CRC32 using the given block of bytes.
    /// This is useful when using the CRC32() class in a Stream.
    /// </summary>
    /// <param name="block">block of bytes to slurp</param>
    /// <param name="offset">starting point in the block</param>
    /// <param name="count">how many bytes within the block to slurp</param>
    public void SlurpBlock(byte[] block, int offset, int count)
    {
      if (block == null)
        throw new ZlibException("The data buffer must not be null.");
      for (int index1 = 0; index1 < count; ++index1)
      {
        int index2 = offset + index1;
        this._RunningCrc32Result = this._RunningCrc32Result >> 8 ^ CRC32.crc32Table[(IntPtr) ((uint) block[index2] ^ this._RunningCrc32Result & (uint) byte.MaxValue)];
      }
      this._TotalBytesRead += (long) count;
    }

    static CRC32()
    {
      uint num1 = 3988292384;
      CRC32.crc32Table = new uint[256];
      for (uint index1 = 0; index1 < 256U; ++index1)
      {
        uint num2 = index1;
        for (uint index2 = 8; index2 > 0U; --index2)
        {
          if (((int) num2 & 1) == 1)
            num2 = num2 >> 1 ^ num1;
          else
            num2 >>= 1;
        }
        CRC32.crc32Table[(IntPtr) index1] = num2;
      }
    }
  }
}
