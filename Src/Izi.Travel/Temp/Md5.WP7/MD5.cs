// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.MD5
// Assembly: Md5.WP7, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8FB26C7F-4178-49C4-9EFB-45B897AC7C4E
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Md5.WP7.dll

#nullable disable
namespace System.Security.Cryptography
{
  public class MD5 : HashAlgorithm
  {
    private byte[] _data;
    private ABCDStruct _abcd;
    private long _totalLength;
    private int _dataSize;

    public MD5()
    {
      this.HashSizeValue = 128;
      this.Initialize();
    }

    public override void Initialize()
    {
      this._data = new byte[64];
      this._dataSize = 0;
      this._totalLength = 0L;
      this._abcd = new ABCDStruct()
      {
        A = 1732584193U,
        B = 4023233417U,
        C = 2562383102U,
        D = 271733878U
      };
    }

    protected override void HashCore(byte[] array, int ibStart, int cbSize)
    {
      int sourceIndex = ibStart;
      int num1 = this._dataSize + cbSize;
      if (num1 >= 64)
      {
        Array.Copy((Array) array, sourceIndex, (Array) this._data, this._dataSize, 64 - this._dataSize);
        MD5Core.GetHashBlock(this._data, ref this._abcd, 0);
        int num2 = sourceIndex + (64 - this._dataSize);
        int length = num1 - 64;
        while (length >= 64)
        {
          Array.Copy((Array) array, num2, (Array) this._data, 0, 64);
          MD5Core.GetHashBlock(array, ref this._abcd, num2);
          length -= 64;
          num2 += 64;
        }
        this._dataSize = length;
        Array.Copy((Array) array, num2, (Array) this._data, 0, length);
      }
      else
      {
        Array.Copy((Array) array, sourceIndex, (Array) this._data, this._dataSize, cbSize);
        this._dataSize = num1;
      }
      this._totalLength += (long) cbSize;
    }

    protected override byte[] HashFinal()
    {
      this.HashValue = MD5Core.GetHashFinalBlock(this._data, 0, this._dataSize, this._abcd, this._totalLength * 8L);
      return this.HashValue;
    }
  }
}
