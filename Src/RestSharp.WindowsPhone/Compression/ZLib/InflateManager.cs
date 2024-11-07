// Decompiled with JetBrains decompiler
// Type: RestSharp.Compression.ZLib.InflateManager
// Assembly: RestSharp.WindowsPhone, Version=105.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EBEB4E14-2430-4192-BEF2-49D8C49DBCCE
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.xml

#nullable disable
namespace RestSharp.Compression.ZLib
{
  internal sealed class InflateManager
  {
    private const int PRESET_DICT = 32;
    private const int Z_DEFLATED = 8;
    private const int METHOD = 0;
    private const int FLAG = 1;
    private const int DICT4 = 2;
    private const int DICT3 = 3;
    private const int DICT2 = 4;
    private const int DICT1 = 5;
    private const int DICT0 = 6;
    private const int BLOCKS = 7;
    private const int CHECK4 = 8;
    private const int CHECK3 = 9;
    private const int CHECK2 = 10;
    private const int CHECK1 = 11;
    private const int DONE = 12;
    private const int BAD = 13;
    internal int mode;
    internal ZlibCodec _codec;
    internal int method;
    internal long[] was = new long[1];
    internal long need;
    internal int marker;
    private bool _handleRfc1950HeaderBytes = true;
    internal int wbits;
    internal InflateBlocks blocks;
    private static byte[] mark = new byte[4]
    {
      (byte) 0,
      (byte) 0,
      byte.MaxValue,
      byte.MaxValue
    };

    internal bool HandleRfc1950HeaderBytes
    {
      get => this._handleRfc1950HeaderBytes;
      set => this._handleRfc1950HeaderBytes = value;
    }

    public InflateManager()
    {
    }

    public InflateManager(bool expectRfc1950HeaderBytes)
    {
      this._handleRfc1950HeaderBytes = expectRfc1950HeaderBytes;
    }

    internal int Reset()
    {
      this._codec.TotalBytesIn = this._codec.TotalBytesOut = 0L;
      this._codec.Message = (string) null;
      this.mode = this.HandleRfc1950HeaderBytes ? 0 : 7;
      this.blocks.Reset((long[]) null);
      return 0;
    }

    internal int End()
    {
      if (this.blocks != null)
        this.blocks.Free();
      this.blocks = (InflateBlocks) null;
      return 0;
    }

    internal int Initialize(ZlibCodec codec, int w)
    {
      this._codec = codec;
      this._codec.Message = (string) null;
      this.blocks = (InflateBlocks) null;
      if (w < 8 || w > 15)
      {
        this.End();
        throw new ZlibException("Bad window size.");
      }
      this.wbits = w;
      this.blocks = new InflateBlocks(codec, this.HandleRfc1950HeaderBytes ? (object) this : (object) (InflateManager) null, 1 << w);
      this.Reset();
      return 0;
    }

    internal int Inflate(FlushType flush)
    {
      int num1 = (int) flush;
      if (this._codec.InputBuffer == null)
        throw new ZlibException("InputBuffer is null. ");
      int num2 = num1 == 4 ? -5 : 0;
      int r = -5;
      while (true)
      {
        switch (this.mode)
        {
          case 0:
            if (this._codec.AvailableBytesIn != 0)
            {
              r = num2;
              --this._codec.AvailableBytesIn;
              ++this._codec.TotalBytesIn;
              if (((this.method = (int) this._codec.InputBuffer[this._codec.NextIn++]) & 15) != 8)
              {
                this.mode = 13;
                this._codec.Message = string.Format("unknown compression method (0x{0:X2})", (object) this.method);
                this.marker = 5;
                continue;
              }
              if ((this.method >> 4) + 8 > this.wbits)
              {
                this.mode = 13;
                this._codec.Message = string.Format("invalid window size ({0})", (object) ((this.method >> 4) + 8));
                this.marker = 5;
                continue;
              }
              this.mode = 1;
              goto case 1;
            }
            else
              goto label_5;
          case 1:
            if (this._codec.AvailableBytesIn != 0)
            {
              r = num2;
              --this._codec.AvailableBytesIn;
              ++this._codec.TotalBytesIn;
              int num3 = (int) this._codec.InputBuffer[this._codec.NextIn++] & (int) byte.MaxValue;
              if (((this.method << 8) + num3) % 31 != 0)
              {
                this.mode = 13;
                this._codec.Message = "incorrect header check";
                this.marker = 5;
                continue;
              }
              if ((num3 & 32) == 0)
              {
                this.mode = 7;
                continue;
              }
              goto label_17;
            }
            else
              goto label_12;
          case 2:
            goto label_18;
          case 3:
            goto label_21;
          case 4:
            goto label_24;
          case 5:
            goto label_27;
          case 6:
            goto label_30;
          case 7:
            r = this.blocks.Process(r);
            if (r == -3)
            {
              this.mode = 13;
              this.marker = 0;
              continue;
            }
            if (r == 0)
              r = num2;
            if (r == 1)
            {
              r = num2;
              this.blocks.Reset(this.was);
              if (!this.HandleRfc1950HeaderBytes)
              {
                this.mode = 12;
                continue;
              }
              this.mode = 8;
              goto case 8;
            }
            else
              goto label_36;
          case 8:
            if (this._codec.AvailableBytesIn != 0)
            {
              r = num2;
              --this._codec.AvailableBytesIn;
              ++this._codec.TotalBytesIn;
              this.need = (long) (((int) this._codec.InputBuffer[this._codec.NextIn++] & (int) byte.MaxValue) << 24 & -16777216);
              this.mode = 9;
              goto case 9;
            }
            else
              goto label_41;
          case 9:
            if (this._codec.AvailableBytesIn != 0)
            {
              r = num2;
              --this._codec.AvailableBytesIn;
              ++this._codec.TotalBytesIn;
              this.need += (long) (((int) this._codec.InputBuffer[this._codec.NextIn++] & (int) byte.MaxValue) << 16) & 16711680L;
              this.mode = 10;
              goto case 10;
            }
            else
              goto label_44;
          case 10:
            if (this._codec.AvailableBytesIn != 0)
            {
              r = num2;
              --this._codec.AvailableBytesIn;
              ++this._codec.TotalBytesIn;
              this.need += (long) (((int) this._codec.InputBuffer[this._codec.NextIn++] & (int) byte.MaxValue) << 8) & 65280L;
              this.mode = 11;
              goto case 11;
            }
            else
              goto label_47;
          case 11:
            if (this._codec.AvailableBytesIn != 0)
            {
              r = num2;
              --this._codec.AvailableBytesIn;
              ++this._codec.TotalBytesIn;
              this.need += (long) this._codec.InputBuffer[this._codec.NextIn++] & (long) byte.MaxValue;
              if ((int) this.was[0] != (int) this.need)
              {
                this.mode = 13;
                this._codec.Message = "incorrect data check";
                this.marker = 5;
                continue;
              }
              goto label_53;
            }
            else
              goto label_50;
          case 12:
            goto label_54;
          case 13:
            goto label_55;
          default:
            goto label_56;
        }
      }
label_5:
      return r;
label_12:
      return r;
label_17:
      this.mode = 2;
label_18:
      if (this._codec.AvailableBytesIn == 0)
        return r;
      r = num2;
      --this._codec.AvailableBytesIn;
      ++this._codec.TotalBytesIn;
      this.need = (long) (((int) this._codec.InputBuffer[this._codec.NextIn++] & (int) byte.MaxValue) << 24 & -16777216);
      this.mode = 3;
label_21:
      if (this._codec.AvailableBytesIn == 0)
        return r;
      r = num2;
      --this._codec.AvailableBytesIn;
      ++this._codec.TotalBytesIn;
      this.need += (long) (((int) this._codec.InputBuffer[this._codec.NextIn++] & (int) byte.MaxValue) << 16) & 16711680L;
      this.mode = 4;
label_24:
      if (this._codec.AvailableBytesIn == 0)
        return r;
      r = num2;
      --this._codec.AvailableBytesIn;
      ++this._codec.TotalBytesIn;
      this.need += (long) (((int) this._codec.InputBuffer[this._codec.NextIn++] & (int) byte.MaxValue) << 8) & 65280L;
      this.mode = 5;
label_27:
      if (this._codec.AvailableBytesIn == 0)
        return r;
      --this._codec.AvailableBytesIn;
      ++this._codec.TotalBytesIn;
      this.need += (long) this._codec.InputBuffer[this._codec.NextIn++] & (long) byte.MaxValue;
      this._codec._Adler32 = this.need;
      this.mode = 6;
      return 2;
label_30:
      this.mode = 13;
      this._codec.Message = "need dictionary";
      this.marker = 0;
      return -2;
label_36:
      return r;
label_41:
      return r;
label_44:
      return r;
label_47:
      return r;
label_50:
      return r;
label_53:
      this.mode = 12;
label_54:
      return 1;
label_55:
      throw new ZlibException(string.Format("Bad state ({0})", (object) this._codec.Message));
label_56:
      throw new ZlibException("Stream error.");
    }

    internal int SetDictionary(byte[] dictionary)
    {
      int start = 0;
      int n = dictionary.Length;
      if (this.mode != 6)
        throw new ZlibException("Stream error.");
      if (Adler.Adler32(1L, dictionary, 0, dictionary.Length) != this._codec._Adler32)
        return -3;
      this._codec._Adler32 = Adler.Adler32(0L, (byte[]) null, 0, 0);
      if (n >= 1 << this.wbits)
      {
        n = (1 << this.wbits) - 1;
        start = dictionary.Length - n;
      }
      this.blocks.SetDictionary(dictionary, start, n);
      this.mode = 7;
      return 0;
    }

    internal int Sync()
    {
      if (this.mode != 13)
      {
        this.mode = 13;
        this.marker = 0;
      }
      int availableBytesIn;
      if ((availableBytesIn = this._codec.AvailableBytesIn) == 0)
        return -5;
      int nextIn = this._codec.NextIn;
      int index;
      for (index = this.marker; availableBytesIn != 0 && index < 4; --availableBytesIn)
      {
        if ((int) this._codec.InputBuffer[nextIn] == (int) InflateManager.mark[index])
          ++index;
        else
          index = this._codec.InputBuffer[nextIn] == (byte) 0 ? 4 - index : 0;
        ++nextIn;
      }
      this._codec.TotalBytesIn += (long) (nextIn - this._codec.NextIn);
      this._codec.NextIn = nextIn;
      this._codec.AvailableBytesIn = availableBytesIn;
      this.marker = index;
      if (index != 4)
        return -3;
      long totalBytesIn = this._codec.TotalBytesIn;
      long totalBytesOut = this._codec.TotalBytesOut;
      this.Reset();
      this._codec.TotalBytesIn = totalBytesIn;
      this._codec.TotalBytesOut = totalBytesOut;
      this.mode = 7;
      return 0;
    }

    internal int SyncPoint(ZlibCodec z) => this.blocks.SyncPoint();
  }
}
