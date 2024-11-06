// Decompiled with JetBrains decompiler
// Type: RestSharp.Compression.ZLib.Adler
// Assembly: RestSharp.WindowsPhone, Version=105.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EBEB4E14-2430-4192-BEF2-49D8C49DBCCE
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.xml

#nullable disable
namespace RestSharp.Compression.ZLib
{
  /// <summary>Computes an Adler-32 checksum.</summary>
  /// <remarks>
  /// The Adler checksum is similar to a CRC checksum, but faster to compute, though less
  /// reliable.  It is used in producing RFC1950 compressed streams.  The Adler checksum
  /// is a required part of the "ZLIB" standard.  Applications will almost never need to
  /// use this class directly.
  /// </remarks>
  internal sealed class Adler
  {
    private static int BASE = 65521;
    private static int NMAX = 5552;

    internal static long Adler32(long adler, byte[] buf, int index, int len)
    {
      if (buf == null)
        return 1;
      long num1 = adler & (long) ushort.MaxValue;
      long num2 = adler >> 16 & (long) ushort.MaxValue;
      while (len > 0)
      {
        int num3 = len < Adler.NMAX ? len : Adler.NMAX;
        len -= num3;
        for (; num3 >= 16; num3 -= 16)
        {
          long num4 = num1 + (long) ((int) buf[index++] & (int) byte.MaxValue);
          long num5 = num2 + num4;
          long num6 = num4 + (long) ((int) buf[index++] & (int) byte.MaxValue);
          long num7 = num5 + num6;
          long num8 = num6 + (long) ((int) buf[index++] & (int) byte.MaxValue);
          long num9 = num7 + num8;
          long num10 = num8 + (long) ((int) buf[index++] & (int) byte.MaxValue);
          long num11 = num9 + num10;
          long num12 = num10 + (long) ((int) buf[index++] & (int) byte.MaxValue);
          long num13 = num11 + num12;
          long num14 = num12 + (long) ((int) buf[index++] & (int) byte.MaxValue);
          long num15 = num13 + num14;
          long num16 = num14 + (long) ((int) buf[index++] & (int) byte.MaxValue);
          long num17 = num15 + num16;
          long num18 = num16 + (long) ((int) buf[index++] & (int) byte.MaxValue);
          long num19 = num17 + num18;
          long num20 = num18 + (long) ((int) buf[index++] & (int) byte.MaxValue);
          long num21 = num19 + num20;
          long num22 = num20 + (long) ((int) buf[index++] & (int) byte.MaxValue);
          long num23 = num21 + num22;
          long num24 = num22 + (long) ((int) buf[index++] & (int) byte.MaxValue);
          long num25 = num23 + num24;
          long num26 = num24 + (long) ((int) buf[index++] & (int) byte.MaxValue);
          long num27 = num25 + num26;
          long num28 = num26 + (long) ((int) buf[index++] & (int) byte.MaxValue);
          long num29 = num27 + num28;
          long num30 = num28 + (long) ((int) buf[index++] & (int) byte.MaxValue);
          long num31 = num29 + num30;
          long num32 = num30 + (long) ((int) buf[index++] & (int) byte.MaxValue);
          long num33 = num31 + num32;
          num1 = num32 + (long) ((int) buf[index++] & (int) byte.MaxValue);
          num2 = num33 + num1;
        }
        if (num3 != 0)
        {
          do
          {
            num1 += (long) ((int) buf[index++] & (int) byte.MaxValue);
            num2 += num1;
          }
          while (--num3 != 0);
        }
        num1 %= (long) Adler.BASE;
        num2 %= (long) Adler.BASE;
      }
      return num2 << 16 | num1;
    }
  }
}
