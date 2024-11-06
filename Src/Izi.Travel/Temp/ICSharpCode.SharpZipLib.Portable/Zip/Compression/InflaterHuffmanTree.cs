// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.Compression.InflaterHuffmanTree
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51802, Culture=neutral, PublicKeyToken=null
// MVID: 6C361F8B-A5CA-49B8-ABC1-A50EE493C003
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.xml

using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using System;

#nullable disable
namespace ICSharpCode.SharpZipLib.Zip.Compression
{
  /// <summary>Huffman tree used for inflation</summary>
  public class InflaterHuffmanTree
  {
    private const int MAX_BITLEN = 15;
    private short[] tree;
    /// <summary>Literal length tree</summary>
    public static InflaterHuffmanTree defLitLenTree;
    /// <summary>Distance tree</summary>
    public static InflaterHuffmanTree defDistTree;

    static InflaterHuffmanTree()
    {
      try
      {
        byte[] codeLengths1 = new byte[288];
        int num1 = 0;
        while (num1 < 144)
          codeLengths1[num1++] = (byte) 8;
        while (num1 < 256)
          codeLengths1[num1++] = (byte) 9;
        while (num1 < 280)
          codeLengths1[num1++] = (byte) 7;
        while (num1 < 288)
          codeLengths1[num1++] = (byte) 8;
        InflaterHuffmanTree.defLitLenTree = new InflaterHuffmanTree(codeLengths1);
        byte[] codeLengths2 = new byte[32];
        int num2 = 0;
        while (num2 < 32)
          codeLengths2[num2++] = (byte) 5;
        InflaterHuffmanTree.defDistTree = new InflaterHuffmanTree(codeLengths2);
      }
      catch (Exception ex)
      {
        throw new SharpZipBaseException("InflaterHuffmanTree: static tree length illegal");
      }
    }

    /// <summary>
    /// Constructs a Huffman tree from the array of code lengths.
    /// </summary>
    /// <param name="codeLengths">the array of code lengths</param>
    public InflaterHuffmanTree(byte[] codeLengths) => this.BuildTree(codeLengths);

    private void BuildTree(byte[] codeLengths)
    {
      int[] numArray1 = new int[16];
      int[] numArray2 = new int[16];
      for (int index = 0; index < codeLengths.Length; ++index)
      {
        int codeLength = (int) codeLengths[index];
        if (codeLength > 0)
          ++numArray1[codeLength];
      }
      int num1 = 0;
      int length = 512;
      for (int index = 1; index <= 15; ++index)
      {
        numArray2[index] = num1;
        num1 += numArray1[index] << 16 - index;
        if (index >= 10)
        {
          int num2 = numArray2[index] & 130944;
          int num3 = num1 & 130944;
          length += num3 - num2 >> 16 - index;
        }
      }
      this.tree = new short[length];
      int num4 = 512;
      for (int index = 15; index >= 10; --index)
      {
        int num5 = num1 & 130944;
        num1 -= numArray1[index] << 16 - index;
        for (int toReverse = num1 & 130944; toReverse < num5; toReverse += 128)
        {
          this.tree[(int) DeflaterHuffman.BitReverse(toReverse)] = (short) (-num4 << 4 | index);
          num4 += 1 << index - 9;
        }
      }
      for (int index1 = 0; index1 < codeLengths.Length; ++index1)
      {
        int codeLength = (int) codeLengths[index1];
        if (codeLength != 0)
        {
          int toReverse = numArray2[codeLength];
          int index2 = (int) DeflaterHuffman.BitReverse(toReverse);
          if (codeLength <= 9)
          {
            do
            {
              this.tree[index2] = (short) (index1 << 4 | codeLength);
              index2 += 1 << codeLength;
            }
            while (index2 < 512);
          }
          else
          {
            int num6 = (int) this.tree[index2 & 511];
            int num7 = 1 << (num6 & 15);
            int num8 = -(num6 >> 4);
            do
            {
              this.tree[num8 | index2 >> 9] = (short) (index1 << 4 | codeLength);
              index2 += 1 << codeLength;
            }
            while (index2 < num7);
          }
          numArray2[codeLength] = toReverse + (1 << 16 - codeLength);
        }
      }
    }

    /// <summary>
    /// Reads the next symbol from input.  The symbol is encoded using the
    /// huffman tree.
    /// </summary>
    /// <param name="input">input the input source.</param>
    /// <returns>
    /// the next symbol, or -1 if not enough input is available.
    /// </returns>
    public int GetSymbol(StreamManipulator input)
    {
      int index;
      if ((index = input.PeekBits(9)) >= 0)
      {
        int num1;
        if ((num1 = (int) this.tree[index]) >= 0)
        {
          input.DropBits(num1 & 15);
          return num1 >> 4;
        }
        int num2 = -(num1 >> 4);
        int bitCount = num1 & 15;
        int num3;
        if ((num3 = input.PeekBits(bitCount)) >= 0)
        {
          int num4 = (int) this.tree[num2 | num3 >> 9];
          input.DropBits(num4 & 15);
          return num4 >> 4;
        }
        int availableBits = input.AvailableBits;
        int num5 = input.PeekBits(availableBits);
        int num6 = (int) this.tree[num2 | num5 >> 9];
        if ((num6 & 15) > availableBits)
          return -1;
        input.DropBits(num6 & 15);
        return num6 >> 4;
      }
      int availableBits1 = input.AvailableBits;
      int num = (int) this.tree[input.PeekBits(availableBits1)];
      if (num < 0 || (num & 15) > availableBits1)
        return -1;
      input.DropBits(num & 15);
      return num >> 4;
    }
  }
}
