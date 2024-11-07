// Decompiled with JetBrains decompiler
// Type: RestSharp.Compression.ZLib.InflateBlocks
// Assembly: RestSharp.WindowsPhone, Version=105.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EBEB4E14-2430-4192-BEF2-49D8C49DBCCE
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.xml

using System;

#nullable disable
namespace RestSharp.Compression.ZLib
{
  internal sealed class InflateBlocks
  {
    private const int MANY = 1440;
    private const int TYPE = 0;
    private const int LENS = 1;
    private const int STORED = 2;
    private const int TABLE = 3;
    private const int BTREE = 4;
    private const int DTREE = 5;
    private const int CODES = 6;
    private const int DRY = 7;
    private const int DONE = 8;
    private const int BAD = 9;
    private static readonly int[] inflate_mask = new int[17]
    {
      0,
      1,
      3,
      7,
      15,
      31,
      63,
      (int) sbyte.MaxValue,
      (int) byte.MaxValue,
      511,
      1023,
      2047,
      4095,
      8191,
      16383,
      (int) short.MaxValue,
      (int) ushort.MaxValue
    };
    internal static readonly int[] border = new int[19]
    {
      16,
      17,
      18,
      0,
      8,
      7,
      9,
      6,
      10,
      5,
      11,
      4,
      12,
      3,
      13,
      2,
      14,
      1,
      15
    };
    internal int mode;
    internal int left;
    internal int table;
    internal int index;
    internal int[] blens;
    internal int[] bb = new int[1];
    internal int[] tb = new int[1];
    internal InflateCodes codes = new InflateCodes();
    internal int last;
    internal ZlibCodec _codec;
    internal int bitk;
    internal int bitb;
    internal int[] hufts;
    internal byte[] window;
    internal int end;
    internal int read;
    internal int write;
    internal object checkfn;
    internal long check;
    internal InfTree inftree = new InfTree();

    internal InflateBlocks(ZlibCodec codec, object checkfn, int w)
    {
      this._codec = codec;
      this.hufts = new int[4320];
      this.window = new byte[w];
      this.end = w;
      this.checkfn = checkfn;
      this.mode = 0;
      this.Reset((long[]) null);
    }

    internal void Reset(long[] c)
    {
      if (c != null)
        c[0] = this.check;
      if (this.mode != 4)
      {
        int mode1 = this.mode;
      }
      int mode2 = this.mode;
      this.mode = 0;
      this.bitk = 0;
      this.bitb = 0;
      this.read = this.write = 0;
      if (this.checkfn == null)
        return;
      this._codec._Adler32 = this.check = Adler.Adler32(0L, (byte[]) null, 0, 0);
    }

    internal int Process(int r)
    {
      int nextIn = this._codec.NextIn;
      int availableBytesIn = this._codec.AvailableBytesIn;
      int number1 = this.bitb;
      int num1 = this.bitk;
      int destinationIndex = this.write;
      int num2 = destinationIndex < this.read ? this.read - destinationIndex - 1 : this.end - destinationIndex;
      int num3;
      int num4;
      while (true)
      {
        int length1;
        do
        {
          switch (this.mode)
          {
            case 0:
              for (; num1 < 3; num1 += 8)
              {
                if (availableBytesIn != 0)
                {
                  r = 0;
                  --availableBytesIn;
                  number1 |= ((int) this._codec.InputBuffer[nextIn++] & (int) byte.MaxValue) << num1;
                }
                else
                {
                  this.bitb = number1;
                  this.bitk = num1;
                  this._codec.AvailableBytesIn = availableBytesIn;
                  this._codec.TotalBytesIn += (long) (nextIn - this._codec.NextIn);
                  this._codec.NextIn = nextIn;
                  this.write = destinationIndex;
                  return this.Flush(r);
                }
              }
              int number2 = number1 & 7;
              this.last = number2 & 1;
              switch (SharedUtils.URShift(number2, 1))
              {
                case 0:
                  int number3 = SharedUtils.URShift(number1, 3);
                  int num5 = num1 - 3;
                  int bits1 = num5 & 7;
                  number1 = SharedUtils.URShift(number3, bits1);
                  num1 = num5 - bits1;
                  this.mode = 1;
                  continue;
                case 1:
                  int[] bl1 = new int[1];
                  int[] bd1 = new int[1];
                  int[][] tl1 = new int[1][];
                  int[][] td1 = new int[1][];
                  InfTree.inflate_trees_fixed(bl1, bd1, tl1, td1, this._codec);
                  this.codes.Init(bl1[0], bd1[0], tl1[0], 0, td1[0], 0);
                  number1 = SharedUtils.URShift(number1, 3);
                  num1 -= 3;
                  this.mode = 6;
                  continue;
                case 2:
                  number1 = SharedUtils.URShift(number1, 3);
                  num1 -= 3;
                  this.mode = 3;
                  continue;
                case 3:
                  int num6 = SharedUtils.URShift(number1, 3);
                  int num7 = num1 - 3;
                  this.mode = 9;
                  this._codec.Message = "invalid block type";
                  r = -3;
                  this.bitb = num6;
                  this.bitk = num7;
                  this._codec.AvailableBytesIn = availableBytesIn;
                  this._codec.TotalBytesIn += (long) (nextIn - this._codec.NextIn);
                  this._codec.NextIn = nextIn;
                  this.write = destinationIndex;
                  return this.Flush(r);
                default:
                  continue;
              }
            case 1:
              for (; num1 < 32; num1 += 8)
              {
                if (availableBytesIn != 0)
                {
                  r = 0;
                  --availableBytesIn;
                  number1 |= ((int) this._codec.InputBuffer[nextIn++] & (int) byte.MaxValue) << num1;
                }
                else
                {
                  this.bitb = number1;
                  this.bitk = num1;
                  this._codec.AvailableBytesIn = availableBytesIn;
                  this._codec.TotalBytesIn += (long) (nextIn - this._codec.NextIn);
                  this._codec.NextIn = nextIn;
                  this.write = destinationIndex;
                  return this.Flush(r);
                }
              }
              if ((SharedUtils.URShift(~number1, 16) & (int) ushort.MaxValue) != (number1 & (int) ushort.MaxValue))
              {
                this.mode = 9;
                this._codec.Message = "invalid stored block lengths";
                r = -3;
                this.bitb = number1;
                this.bitk = num1;
                this._codec.AvailableBytesIn = availableBytesIn;
                this._codec.TotalBytesIn += (long) (nextIn - this._codec.NextIn);
                this._codec.NextIn = nextIn;
                this.write = destinationIndex;
                return this.Flush(r);
              }
              this.left = number1 & (int) ushort.MaxValue;
              number1 = num1 = 0;
              this.mode = this.left != 0 ? 2 : (this.last != 0 ? 7 : 0);
              continue;
            case 2:
              if (availableBytesIn == 0)
              {
                this.bitb = number1;
                this.bitk = num1;
                this._codec.AvailableBytesIn = availableBytesIn;
                this._codec.TotalBytesIn += (long) (nextIn - this._codec.NextIn);
                this._codec.NextIn = nextIn;
                this.write = destinationIndex;
                return this.Flush(r);
              }
              if (num2 == 0)
              {
                if (destinationIndex == this.end && this.read != 0)
                {
                  destinationIndex = 0;
                  num2 = destinationIndex < this.read ? this.read - destinationIndex - 1 : this.end - destinationIndex;
                }
                if (num2 == 0)
                {
                  this.write = destinationIndex;
                  r = this.Flush(r);
                  destinationIndex = this.write;
                  num2 = destinationIndex < this.read ? this.read - destinationIndex - 1 : this.end - destinationIndex;
                  if (destinationIndex == this.end && this.read != 0)
                  {
                    destinationIndex = 0;
                    num2 = destinationIndex < this.read ? this.read - destinationIndex - 1 : this.end - destinationIndex;
                  }
                  if (num2 == 0)
                  {
                    this.bitb = number1;
                    this.bitk = num1;
                    this._codec.AvailableBytesIn = availableBytesIn;
                    this._codec.TotalBytesIn += (long) (nextIn - this._codec.NextIn);
                    this._codec.NextIn = nextIn;
                    this.write = destinationIndex;
                    return this.Flush(r);
                  }
                }
              }
              r = 0;
              length1 = this.left;
              if (length1 > availableBytesIn)
                length1 = availableBytesIn;
              if (length1 > num2)
                length1 = num2;
              Array.Copy((Array) this._codec.InputBuffer, nextIn, (Array) this.window, destinationIndex, length1);
              nextIn += length1;
              availableBytesIn -= length1;
              destinationIndex += length1;
              num2 -= length1;
              continue;
            case 3:
              goto label_37;
            case 4:
              goto label_51;
            case 5:
              goto label_59;
            case 6:
              goto label_81;
            case 7:
              goto label_86;
            case 8:
              goto label_89;
            case 9:
              goto label_90;
            default:
              goto label_91;
          }
        }
        while ((this.left -= length1) != 0);
        this.mode = this.last != 0 ? 7 : 0;
        continue;
label_37:
        for (; num1 < 14; num1 += 8)
        {
          if (availableBytesIn != 0)
          {
            r = 0;
            --availableBytesIn;
            number1 |= ((int) this._codec.InputBuffer[nextIn++] & (int) byte.MaxValue) << num1;
          }
          else
          {
            this.bitb = number1;
            this.bitk = num1;
            this._codec.AvailableBytesIn = availableBytesIn;
            this._codec.TotalBytesIn += (long) (nextIn - this._codec.NextIn);
            this._codec.NextIn = nextIn;
            this.write = destinationIndex;
            return this.Flush(r);
          }
        }
        int num8;
        this.table = num8 = number1 & 16383;
        if ((num8 & 31) <= 29 && (num8 >> 5 & 31) <= 29)
        {
          int length2 = 258 + (num8 & 31) + (num8 >> 5 & 31);
          if (this.blens == null || this.blens.Length < length2)
          {
            this.blens = new int[length2];
          }
          else
          {
            for (int index = 0; index < length2; ++index)
              this.blens[index] = 0;
          }
          number1 = SharedUtils.URShift(number1, 14);
          num1 -= 14;
          this.index = 0;
          this.mode = 4;
        }
        else
          break;
label_51:
        while (this.index < 4 + SharedUtils.URShift(this.table, 10))
        {
          for (; num1 < 3; num1 += 8)
          {
            if (availableBytesIn != 0)
            {
              r = 0;
              --availableBytesIn;
              number1 |= ((int) this._codec.InputBuffer[nextIn++] & (int) byte.MaxValue) << num1;
            }
            else
            {
              this.bitb = number1;
              this.bitk = num1;
              this._codec.AvailableBytesIn = availableBytesIn;
              this._codec.TotalBytesIn += (long) (nextIn - this._codec.NextIn);
              this._codec.NextIn = nextIn;
              this.write = destinationIndex;
              return this.Flush(r);
            }
          }
          this.blens[InflateBlocks.border[this.index++]] = number1 & 7;
          number1 = SharedUtils.URShift(number1, 3);
          num1 -= 3;
        }
        while (this.index < 19)
          this.blens[InflateBlocks.border[this.index++]] = 0;
        this.bb[0] = 7;
        num3 = this.inftree.inflate_trees_bits(this.blens, this.bb, this.tb, this.hufts, this._codec);
        if (num3 == 0)
        {
          this.index = 0;
          this.mode = 5;
        }
        else
          goto label_55;
label_59:
        while (true)
        {
          int table1 = this.table;
          if (this.index < 258 + (table1 & 31) + (table1 >> 5 & 31))
          {
            int index1;
            for (index1 = this.bb[0]; num1 < index1; num1 += 8)
            {
              if (availableBytesIn != 0)
              {
                r = 0;
                --availableBytesIn;
                number1 |= ((int) this._codec.InputBuffer[nextIn++] & (int) byte.MaxValue) << num1;
              }
              else
              {
                this.bitb = number1;
                this.bitk = num1;
                this._codec.AvailableBytesIn = availableBytesIn;
                this._codec.TotalBytesIn += (long) (nextIn - this._codec.NextIn);
                this._codec.NextIn = nextIn;
                this.write = destinationIndex;
                return this.Flush(r);
              }
            }
            int num9 = this.tb[0];
            int huft1 = this.hufts[(this.tb[0] + (number1 & InflateBlocks.inflate_mask[index1])) * 3 + 1];
            int huft2 = this.hufts[(this.tb[0] + (number1 & InflateBlocks.inflate_mask[huft1])) * 3 + 2];
            if (huft2 < 16)
            {
              number1 = SharedUtils.URShift(number1, huft1);
              num1 -= huft1;
              this.blens[this.index++] = huft2;
            }
            else
            {
              int bits2 = huft2 == 18 ? 7 : huft2 - 14;
              int num10 = huft2 == 18 ? 11 : 3;
              for (; num1 < huft1 + bits2; num1 += 8)
              {
                if (availableBytesIn != 0)
                {
                  r = 0;
                  --availableBytesIn;
                  number1 |= ((int) this._codec.InputBuffer[nextIn++] & (int) byte.MaxValue) << num1;
                }
                else
                {
                  this.bitb = number1;
                  this.bitk = num1;
                  this._codec.AvailableBytesIn = availableBytesIn;
                  this._codec.TotalBytesIn += (long) (nextIn - this._codec.NextIn);
                  this._codec.NextIn = nextIn;
                  this.write = destinationIndex;
                  return this.Flush(r);
                }
              }
              int number4 = SharedUtils.URShift(number1, huft1);
              int num11 = num1 - huft1;
              int num12 = num10 + (number4 & InflateBlocks.inflate_mask[bits2]);
              number1 = SharedUtils.URShift(number4, bits2);
              num1 = num11 - bits2;
              int index2 = this.index;
              int table2 = this.table;
              if (index2 + num12 <= 258 + (table2 & 31) + (table2 >> 5 & 31) && (huft2 != 16 || index2 >= 1))
              {
                int blen = huft2 == 16 ? this.blens[index2 - 1] : 0;
                do
                {
                  this.blens[index2++] = blen;
                }
                while (--num12 != 0);
                this.index = index2;
              }
              else
                goto label_73;
            }
          }
          else
            break;
        }
        this.tb[0] = -1;
        int[] bl2 = new int[1]{ 9 };
        int[] bd2 = new int[1]{ 6 };
        int[] tl2 = new int[1];
        int[] td2 = new int[1];
        int table = this.table;
        num4 = this.inftree.inflate_trees_dynamic(257 + (table & 31), 1 + (table >> 5 & 31), this.blens, bl2, bd2, tl2, td2, this.hufts, this._codec);
        switch (num4)
        {
          case -3:
            goto label_78;
          case 0:
            this.codes.Init(bl2[0], bd2[0], this.hufts, tl2[0], this.hufts, td2[0]);
            this.mode = 6;
            break;
          default:
            goto label_79;
        }
label_81:
        this.bitb = number1;
        this.bitk = num1;
        this._codec.AvailableBytesIn = availableBytesIn;
        this._codec.TotalBytesIn += (long) (nextIn - this._codec.NextIn);
        this._codec.NextIn = nextIn;
        this.write = destinationIndex;
        if ((r = this.codes.Process(this, r)) == 1)
        {
          r = 0;
          nextIn = this._codec.NextIn;
          availableBytesIn = this._codec.AvailableBytesIn;
          number1 = this.bitb;
          num1 = this.bitk;
          destinationIndex = this.write;
          num2 = destinationIndex < this.read ? this.read - destinationIndex - 1 : this.end - destinationIndex;
          if (this.last == 0)
            this.mode = 0;
          else
            goto label_85;
        }
        else
          goto label_82;
      }
      this.mode = 9;
      this._codec.Message = "too many length or distance symbols";
      r = -3;
      this.bitb = number1;
      this.bitk = num1;
      this._codec.AvailableBytesIn = availableBytesIn;
      this._codec.TotalBytesIn += (long) (nextIn - this._codec.NextIn);
      this._codec.NextIn = nextIn;
      this.write = destinationIndex;
      return this.Flush(r);
label_55:
      r = num3;
      if (r == -3)
      {
        this.blens = (int[]) null;
        this.mode = 9;
      }
      this.bitb = number1;
      this.bitk = num1;
      this._codec.AvailableBytesIn = availableBytesIn;
      this._codec.TotalBytesIn += (long) (nextIn - this._codec.NextIn);
      this._codec.NextIn = nextIn;
      this.write = destinationIndex;
      return this.Flush(r);
label_73:
      this.blens = (int[]) null;
      this.mode = 9;
      this._codec.Message = "invalid bit length repeat";
      r = -3;
      this.bitb = number1;
      this.bitk = num1;
      this._codec.AvailableBytesIn = availableBytesIn;
      this._codec.TotalBytesIn += (long) (nextIn - this._codec.NextIn);
      this._codec.NextIn = nextIn;
      this.write = destinationIndex;
      return this.Flush(r);
label_78:
      this.blens = (int[]) null;
      this.mode = 9;
label_79:
      r = num4;
      this.bitb = number1;
      this.bitk = num1;
      this._codec.AvailableBytesIn = availableBytesIn;
      this._codec.TotalBytesIn += (long) (nextIn - this._codec.NextIn);
      this._codec.NextIn = nextIn;
      this.write = destinationIndex;
      return this.Flush(r);
label_82:
      return this.Flush(r);
label_85:
      this.mode = 7;
label_86:
      this.write = destinationIndex;
      r = this.Flush(r);
      destinationIndex = this.write;
      int num13 = destinationIndex < this.read ? this.read - destinationIndex - 1 : this.end - destinationIndex;
      if (this.read != this.write)
      {
        this.bitb = number1;
        this.bitk = num1;
        this._codec.AvailableBytesIn = availableBytesIn;
        this._codec.TotalBytesIn += (long) (nextIn - this._codec.NextIn);
        this._codec.NextIn = nextIn;
        this.write = destinationIndex;
        return this.Flush(r);
      }
      this.mode = 8;
label_89:
      r = 1;
      this.bitb = number1;
      this.bitk = num1;
      this._codec.AvailableBytesIn = availableBytesIn;
      this._codec.TotalBytesIn += (long) (nextIn - this._codec.NextIn);
      this._codec.NextIn = nextIn;
      this.write = destinationIndex;
      return this.Flush(r);
label_90:
      r = -3;
      this.bitb = number1;
      this.bitk = num1;
      this._codec.AvailableBytesIn = availableBytesIn;
      this._codec.TotalBytesIn += (long) (nextIn - this._codec.NextIn);
      this._codec.NextIn = nextIn;
      this.write = destinationIndex;
      return this.Flush(r);
label_91:
      r = -2;
      this.bitb = number1;
      this.bitk = num1;
      this._codec.AvailableBytesIn = availableBytesIn;
      this._codec.TotalBytesIn += (long) (nextIn - this._codec.NextIn);
      this._codec.NextIn = nextIn;
      this.write = destinationIndex;
      return this.Flush(r);
    }

    internal void Free()
    {
      this.Reset((long[]) null);
      this.window = (byte[]) null;
      this.hufts = (int[]) null;
    }

    internal void SetDictionary(byte[] d, int start, int n)
    {
      Array.Copy((Array) d, start, (Array) this.window, 0, n);
      this.read = this.write = n;
    }

    internal int SyncPoint() => this.mode != 1 ? 0 : 1;

    internal int Flush(int r)
    {
      int nextOut = this._codec.NextOut;
      int read = this.read;
      int num1 = (read <= this.write ? this.write : this.end) - read;
      if (num1 > this._codec.AvailableBytesOut)
        num1 = this._codec.AvailableBytesOut;
      if (num1 != 0 && r == -5)
        r = 0;
      this._codec.AvailableBytesOut -= num1;
      this._codec.TotalBytesOut += (long) num1;
      if (this.checkfn != null)
        this._codec._Adler32 = this.check = Adler.Adler32(this.check, this.window, read, num1);
      Array.Copy((Array) this.window, read, (Array) this._codec.OutputBuffer, nextOut, num1);
      int destinationIndex = nextOut + num1;
      int num2 = read + num1;
      if (num2 == this.end)
      {
        int num3 = 0;
        if (this.write == this.end)
          this.write = 0;
        int num4 = this.write - num3;
        if (num4 > this._codec.AvailableBytesOut)
          num4 = this._codec.AvailableBytesOut;
        if (num4 != 0 && r == -5)
          r = 0;
        this._codec.AvailableBytesOut -= num4;
        this._codec.TotalBytesOut += (long) num4;
        if (this.checkfn != null)
          this._codec._Adler32 = this.check = Adler.Adler32(this.check, this.window, num3, num4);
        Array.Copy((Array) this.window, num3, (Array) this._codec.OutputBuffer, destinationIndex, num4);
        destinationIndex += num4;
        num2 = num3 + num4;
      }
      this._codec.NextOut = destinationIndex;
      this.read = num2;
      return r;
    }
  }
}
