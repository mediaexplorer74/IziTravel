// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Checksums.Adler32
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51802, Culture=neutral, PublicKeyToken=null
// MVID: 6C361F8B-A5CA-49B8-ABC1-A50EE493C003
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.xml

using System;

#nullable disable
namespace ICSharpCode.SharpZipLib.Checksums
{
  /// <summary>
  /// Computes Adler32 checksum for a stream of data. An Adler32
  /// checksum is not as reliable as a CRC32 checksum, but a lot faster to
  /// compute.
  /// 
  /// The specification for Adler32 may be found in RFC 1950.
  /// ZLIB Compressed Data Format Specification version 3.3)
  /// 
  /// 
  /// From that document:
  /// 
  ///      "ADLER32 (Adler-32 checksum)
  ///       This contains a checksum value of the uncompressed data
  ///       (excluding any dictionary data) computed according to Adler-32
  ///       algorithm. This algorithm is a 32-bit extension and improvement
  ///       of the Fletcher algorithm, used in the ITU-T X.224 / ISO 8073
  ///       standard.
  /// 
  ///       Adler-32 is composed of two sums accumulated per byte: s1 is
  ///       the sum of all bytes, s2 is the sum of all s1 values. Both sums
  ///       are done modulo 65521. s1 is initialized to 1, s2 to zero.  The
  ///       Adler-32 checksum is stored as s2*65536 + s1 in most-
  ///       significant-byte first (network) order."
  /// 
  ///  "8.2. The Adler-32 algorithm
  /// 
  ///    The Adler-32 algorithm is much faster than the CRC32 algorithm yet
  ///    still provides an extremely low probability of undetected errors.
  /// 
  ///    The modulo on unsigned long accumulators can be delayed for 5552
  ///    bytes, so the modulo operation time is negligible.  If the bytes
  ///    are a, b, c, the second sum is 3a + 2b + c + 3, and so is position
  ///    and order sensitive, unlike the first sum, which is just a
  ///    checksum.  That 65521 is prime is important to avoid a possible
  ///    large class of two-byte errors that leave the check unchanged.
  ///    (The Fletcher checksum uses 255, which is not prime and which also
  ///    makes the Fletcher check insensitive to single byte changes 0 -
  ///    255.)
  /// 
  ///    The sum s1 is initialized to 1 instead of zero to make the length
  ///    of the sequence part of s2, so that the length does not have to be
  ///    checked separately. (Any sequence of zeroes has a Fletcher
  ///    checksum of zero.)"
  /// </summary>
  /// <see cref="T:ICSharpCode.SharpZipLib.Zip.Compression.Streams.InflaterInputStream" />
  /// <see cref="T:ICSharpCode.SharpZipLib.Zip.Compression.Streams.DeflaterOutputStream" />
  public sealed class Adler32 : IChecksum
  {
    /// <summary>largest prime smaller than 65536</summary>
    private const uint BASE = 65521;
    private uint checksum;

    /// <summary>Returns the Adler32 data checksum computed so far.</summary>
    public long Value => (long) this.checksum;

    /// <summary>
    /// Creates a new instance of the Adler32 class.
    /// The checksum starts off with a value of 1.
    /// </summary>
    public Adler32() => this.Reset();

    /// <summary>Resets the Adler32 checksum to the initial value.</summary>
    public void Reset() => this.checksum = 1U;

    /// <summary>Updates the checksum with a byte value.</summary>
    /// <param name="value">
    /// The data value to add. The high byte of the int is ignored.
    /// </param>
    public void Update(int value)
    {
      uint num1 = this.checksum & (uint) ushort.MaxValue;
      uint num2 = this.checksum >> 16;
      uint num3 = (num1 + (uint) (value & (int) byte.MaxValue)) % 65521U;
      this.checksum = ((num3 + num2) % 65521U << 16) + num3;
    }

    /// <summary>Updates the checksum with an array of bytes.</summary>
    /// <param name="buffer">The source of the data to update with.</param>
    public void Update(byte[] buffer)
    {
      if (buffer == null)
        throw new ArgumentNullException(nameof (buffer));
      this.Update(buffer, 0, buffer.Length);
    }

    /// <summary>
    /// Updates the checksum with the bytes taken from the array.
    /// </summary>
    /// <param name="buffer">an array of bytes</param>
    /// <param name="offset">the start of the data used for this update</param>
    /// <param name="count">the number of bytes to use for this update</param>
    public void Update(byte[] buffer, int offset, int count)
    {
      if (buffer == null)
        throw new ArgumentNullException(nameof (buffer));
      if (offset < 0)
        throw new ArgumentOutOfRangeException(nameof (offset), "cannot be negative");
      if (count < 0)
        throw new ArgumentOutOfRangeException(nameof (count), "cannot be negative");
      if (offset >= buffer.Length)
        throw new ArgumentOutOfRangeException(nameof (offset), "not a valid index into buffer");
      if (offset + count > buffer.Length)
        throw new ArgumentOutOfRangeException(nameof (count), "exceeds buffer size");
      uint num1 = this.checksum & (uint) ushort.MaxValue;
      uint num2 = this.checksum >> 16;
      while (count > 0)
      {
        int num3 = 3800;
        if (num3 > count)
          num3 = count;
        count -= num3;
        while (--num3 >= 0)
        {
          num1 += (uint) buffer[offset++] & (uint) byte.MaxValue;
          num2 += num1;
        }
        num1 %= 65521U;
        num2 %= 65521U;
      }
      this.checksum = num2 << 16 | num1;
    }
  }
}
