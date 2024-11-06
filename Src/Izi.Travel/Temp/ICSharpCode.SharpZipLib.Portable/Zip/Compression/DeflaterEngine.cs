﻿// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.Compression.DeflaterEngine
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51802, Culture=neutral, PublicKeyToken=null
// MVID: 6C361F8B-A5CA-49B8-ABC1-A50EE493C003
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.xml

using ICSharpCode.SharpZipLib.Checksums;
using System;

#nullable disable
namespace ICSharpCode.SharpZipLib.Zip.Compression
{
  /// <summary>
  /// Low level compression engine for deflate algorithm which uses a 32K sliding window
  /// with secondary compression from Huffman/Shannon-Fano codes.
  /// </summary>
  public class DeflaterEngine : DeflaterConstants
  {
    private const int TooFar = 4096;
    private int ins_h;
    /// <summary>
    /// Hashtable, hashing three characters to an index for window, so
    /// that window[index]..window[index+2] have this hash code.
    /// Note that the array should really be unsigned short, so you need
    /// to and the values with 0xffff.
    /// </summary>
    private short[] head;
    /// <summary>
    /// <code>prev[index &amp; WMASK]</code> points to the previous index that has the
    /// same hash code as the string starting at index.  This way
    /// entries with the same hash code are in a linked list.
    /// Note that the array should really be unsigned short, so you need
    /// to and the values with 0xffff.
    /// </summary>
    private short[] prev;
    private int matchStart;
    private int matchLen;
    private bool prevAvailable;
    private int blockStart;
    /// <summary>Points to the current character in the window.</summary>
    private int strstart;
    /// <summary>
    /// lookahead is the number of characters starting at strstart in
    /// window that are valid.
    /// So window[strstart] until window[strstart+lookahead-1] are valid
    /// characters.
    /// </summary>
    private int lookahead;
    /// <summary>
    /// This array contains the part of the uncompressed stream that
    /// is of relevance.  The current character is indexed by strstart.
    /// </summary>
    private byte[] window;
    private DeflateStrategy strategy;
    private int max_chain;
    private int max_lazy;
    private int niceLength;
    private int goodLength;
    /// <summary>The current compression function.</summary>
    private int compressionFunction;
    /// <summary>The input data for compression.</summary>
    private byte[] inputBuf;
    /// <summary>The total bytes of input read.</summary>
    private long totalIn;
    /// <summary>The offset into inputBuf, where input data starts.</summary>
    private int inputOff;
    /// <summary>The end offset of the input data.</summary>
    private int inputEnd;
    private DeflaterPending pending;
    private DeflaterHuffman huffman;
    /// <summary>The adler checksum</summary>
    private Adler32 adler;

    /// <summary>Construct instance with pending buffer</summary>
    /// <param name="pending">Pending buffer to use</param>
    /// &gt;
    public DeflaterEngine(DeflaterPending pending)
    {
      this.pending = pending;
      this.huffman = new DeflaterHuffman(pending);
      this.adler = new Adler32();
      this.window = new byte[65536];
      this.head = new short[32768];
      this.prev = new short[32768];
      this.blockStart = this.strstart = 1;
    }

    /// <summary>Deflate drives actual compression of data</summary>
    /// <param name="flush">True to flush input buffers</param>
    /// <param name="finish">Finish deflation with the current input.</param>
    /// <returns>Returns true if progress has been made.</returns>
    public bool Deflate(bool flush, bool finish)
    {
      bool flag;
      do
      {
        this.FillWindow();
        bool flush1 = flush && this.inputOff == this.inputEnd;
        switch (this.compressionFunction)
        {
          case 0:
            flag = this.DeflateStored(flush1, finish);
            break;
          case 1:
            flag = this.DeflateFast(flush1, finish);
            break;
          case 2:
            flag = this.DeflateSlow(flush1, finish);
            break;
          default:
            throw new InvalidOperationException("unknown compressionFunction");
        }
      }
      while (this.pending.IsFlushed && flag);
      return flag;
    }

    /// <summary>
    /// Sets input data to be deflated.  Should only be called when <code>NeedsInput()</code>
    /// returns true
    /// </summary>
    /// <param name="buffer">The buffer containing input data.</param>
    /// <param name="offset">The offset of the first byte of data.</param>
    /// <param name="count">The number of bytes of data to use as input.</param>
    public void SetInput(byte[] buffer, int offset, int count)
    {
      if (buffer == null)
        throw new ArgumentNullException(nameof (buffer));
      if (offset < 0)
        throw new ArgumentOutOfRangeException(nameof (offset));
      if (count < 0)
        throw new ArgumentOutOfRangeException(nameof (count));
      if (this.inputOff < this.inputEnd)
        throw new InvalidOperationException("Old input was not completely processed");
      int num = offset + count;
      if (offset > num || num > buffer.Length)
        throw new ArgumentOutOfRangeException(nameof (count));
      this.inputBuf = buffer;
      this.inputOff = offset;
      this.inputEnd = num;
    }

    /// <summary>
    /// Determines if more <see cref="M:ICSharpCode.SharpZipLib.Zip.Compression.DeflaterEngine.SetInput(System.Byte[],System.Int32,System.Int32)">input</see> is needed.
    /// </summary>
    /// <returns>Return true if input is needed via <see cref="M:ICSharpCode.SharpZipLib.Zip.Compression.DeflaterEngine.SetInput(System.Byte[],System.Int32,System.Int32)">SetInput</see></returns>
    public bool NeedsInput() => this.inputEnd == this.inputOff;

    /// <summary>Set compression dictionary</summary>
    /// <param name="buffer">The buffer containing the dictionary data</param>
    /// <param name="offset">The offset in the buffer for the first byte of data</param>
    /// <param name="length">The length of the dictionary data.</param>
    public void SetDictionary(byte[] buffer, int offset, int length)
    {
      this.adler.Update(buffer, offset, length);
      if (length < 3)
        return;
      if (length > 32506)
      {
        offset += length - 32506;
        length = 32506;
      }
      Array.Copy((Array) buffer, offset, (Array) this.window, this.strstart, length);
      this.UpdateHash();
      --length;
      while (--length > 0)
      {
        this.InsertString();
        ++this.strstart;
      }
      this.strstart += 2;
      this.blockStart = this.strstart;
    }

    /// <summary>Reset internal state</summary>
    public void Reset()
    {
      this.huffman.Reset();
      this.adler.Reset();
      this.blockStart = this.strstart = 1;
      this.lookahead = 0;
      this.totalIn = 0L;
      this.prevAvailable = false;
      this.matchLen = 2;
      for (int index = 0; index < 32768; ++index)
        this.head[index] = (short) 0;
      for (int index = 0; index < 32768; ++index)
        this.prev[index] = (short) 0;
    }

    /// <summary>Reset Adler checksum</summary>
    public void ResetAdler() => this.adler.Reset();

    /// <summary>Get current value of Adler checksum</summary>
    public int Adler => (int) this.adler.Value;

    /// <summary>Total data processed</summary>
    public long TotalIn => this.totalIn;

    /// <summary>
    /// Get/set the <see cref="T:ICSharpCode.SharpZipLib.Zip.Compression.DeflateStrategy">deflate strategy</see>
    /// </summary>
    public DeflateStrategy Strategy
    {
      get => this.strategy;
      set => this.strategy = value;
    }

    /// <summary>Set the deflate level (0-9)</summary>
    /// <param name="level">The value to set the level to.</param>
    public void SetLevel(int level)
    {
      this.goodLength = level >= 0 && level <= 9 ? DeflaterConstants.GOOD_LENGTH[level] : throw new ArgumentOutOfRangeException(nameof (level));
      this.max_lazy = DeflaterConstants.MAX_LAZY[level];
      this.niceLength = DeflaterConstants.NICE_LENGTH[level];
      this.max_chain = DeflaterConstants.MAX_CHAIN[level];
      if (DeflaterConstants.COMPR_FUNC[level] == this.compressionFunction)
        return;
      switch (this.compressionFunction)
      {
        case 0:
          if (this.strstart > this.blockStart)
          {
            this.huffman.FlushStoredBlock(this.window, this.blockStart, this.strstart - this.blockStart, false);
            this.blockStart = this.strstart;
          }
          this.UpdateHash();
          break;
        case 1:
          if (this.strstart > this.blockStart)
          {
            this.huffman.FlushBlock(this.window, this.blockStart, this.strstart - this.blockStart, false);
            this.blockStart = this.strstart;
            break;
          }
          break;
        case 2:
          if (this.prevAvailable)
            this.huffman.TallyLit((int) this.window[this.strstart - 1] & (int) byte.MaxValue);
          if (this.strstart > this.blockStart)
          {
            this.huffman.FlushBlock(this.window, this.blockStart, this.strstart - this.blockStart, false);
            this.blockStart = this.strstart;
          }
          this.prevAvailable = false;
          this.matchLen = 2;
          break;
      }
      this.compressionFunction = DeflaterConstants.COMPR_FUNC[level];
    }

    /// <summary>Fill the window</summary>
    public void FillWindow()
    {
      if (this.strstart >= 65274)
        this.SlideWindow();
      int num;
      for (; this.lookahead < 262 && this.inputOff < this.inputEnd; this.lookahead += num)
      {
        num = 65536 - this.lookahead - this.strstart;
        if (num > this.inputEnd - this.inputOff)
          num = this.inputEnd - this.inputOff;
        Array.Copy((Array) this.inputBuf, this.inputOff, (Array) this.window, this.strstart + this.lookahead, num);
        this.adler.Update(this.inputBuf, this.inputOff, num);
        this.inputOff += num;
        this.totalIn += (long) num;
      }
      if (this.lookahead < 3)
        return;
      this.UpdateHash();
    }

    private void UpdateHash()
    {
      this.ins_h = (int) this.window[this.strstart] << 5 ^ (int) this.window[this.strstart + 1];
    }

    /// <summary>
    /// Inserts the current string in the head hash and returns the previous
    /// value for this hash.
    /// </summary>
    /// <returns>The previous hash value</returns>
    private int InsertString()
    {
      int index = (this.ins_h << 5 ^ (int) this.window[this.strstart + 2]) & (int) short.MaxValue;
      short num;
      this.prev[this.strstart & (int) short.MaxValue] = num = this.head[index];
      this.head[index] = (short) this.strstart;
      this.ins_h = index;
      return (int) num & (int) ushort.MaxValue;
    }

    private void SlideWindow()
    {
      Array.Copy((Array) this.window, 32768, (Array) this.window, 0, 32768);
      this.matchStart -= 32768;
      this.strstart -= 32768;
      this.blockStart -= 32768;
      for (int index = 0; index < 32768; ++index)
      {
        int num = (int) this.head[index] & (int) ushort.MaxValue;
        this.head[index] = num >= 32768 ? (short) (num - 32768) : (short) 0;
      }
      for (int index = 0; index < 32768; ++index)
      {
        int num = (int) this.prev[index] & (int) ushort.MaxValue;
        this.prev[index] = num >= 32768 ? (short) (num - 32768) : (short) 0;
      }
    }

    /// <summary>
    /// Find the best (longest) string in the window matching the
    /// string starting at strstart.
    /// 
    /// Preconditions:
    /// <code>
    /// strstart + MAX_MATCH &lt;= window.length.</code>
    /// </summary>
    /// <param name="curMatch"></param>
    /// <returns>True if a match greater than the minimum length is found</returns>
    private bool FindLongestMatch(int curMatch)
    {
      int maxChain = this.max_chain;
      int num1 = this.niceLength;
      short[] prev = this.prev;
      int strstart = this.strstart;
      int index = this.strstart + this.matchLen;
      int val1 = Math.Max(this.matchLen, 2);
      int num2 = Math.Max(this.strstart - 32506, 0);
      int num3 = this.strstart + 258 - 1;
      byte num4 = this.window[index - 1];
      byte num5 = this.window[index];
      if (val1 >= this.goodLength)
        maxChain >>= 2;
      if (num1 > this.lookahead)
        num1 = this.lookahead;
      do
      {
        if ((int) this.window[curMatch + val1] == (int) num5 && (int) this.window[curMatch + val1 - 1] == (int) num4 && (int) this.window[curMatch] == (int) this.window[strstart] && (int) this.window[curMatch + 1] == (int) this.window[strstart + 1])
        {
          int num6 = curMatch + 2;
          int num7 = strstart + 2;
          int num8;
          int num9;
          int num10;
          int num11;
          int num12;
          int num13;
          int num14;
          do
            ;
          while ((int) this.window[++num7] == (int) this.window[num8 = num6 + 1] && (int) this.window[++num7] == (int) this.window[num9 = num8 + 1] && (int) this.window[++num7] == (int) this.window[num10 = num9 + 1] && (int) this.window[++num7] == (int) this.window[num11 = num10 + 1] && (int) this.window[++num7] == (int) this.window[num12 = num11 + 1] && (int) this.window[++num7] == (int) this.window[num13 = num12 + 1] && (int) this.window[++num7] == (int) this.window[num14 = num13 + 1] && (int) this.window[++num7] == (int) this.window[num6 = num14 + 1] && num7 < num3);
          if (num7 > index)
          {
            this.matchStart = curMatch;
            index = num7;
            val1 = num7 - this.strstart;
            if (val1 < num1)
            {
              num4 = this.window[index - 1];
              num5 = this.window[index];
            }
            else
              break;
          }
          strstart = this.strstart;
        }
      }
      while ((curMatch = (int) prev[curMatch & (int) short.MaxValue] & (int) ushort.MaxValue) > num2 && --maxChain != 0);
      this.matchLen = Math.Min(val1, this.lookahead);
      return this.matchLen >= 3;
    }

    private bool DeflateStored(bool flush, bool finish)
    {
      if (!flush && this.lookahead == 0)
        return false;
      this.strstart += this.lookahead;
      this.lookahead = 0;
      int storedLength = this.strstart - this.blockStart;
      if (storedLength < DeflaterConstants.MAX_BLOCK_SIZE && (this.blockStart >= 32768 || storedLength < 32506) && !flush)
        return true;
      bool lastBlock = finish;
      if (storedLength > DeflaterConstants.MAX_BLOCK_SIZE)
      {
        storedLength = DeflaterConstants.MAX_BLOCK_SIZE;
        lastBlock = false;
      }
      this.huffman.FlushStoredBlock(this.window, this.blockStart, storedLength, lastBlock);
      this.blockStart += storedLength;
      return !lastBlock;
    }

    private bool DeflateFast(bool flush, bool finish)
    {
      if (this.lookahead < 262 && !flush)
        return false;
      while (this.lookahead >= 262 || flush)
      {
        if (this.lookahead == 0)
        {
          this.huffman.FlushBlock(this.window, this.blockStart, this.strstart - this.blockStart, finish);
          this.blockStart = this.strstart;
          return false;
        }
        if (this.strstart > 65274)
          this.SlideWindow();
        int curMatch;
        if (this.lookahead >= 3 && (curMatch = this.InsertString()) != 0 && this.strategy != DeflateStrategy.HuffmanOnly && this.strstart - curMatch <= 32506 && this.FindLongestMatch(curMatch))
        {
          bool flag = this.huffman.TallyDist(this.strstart - this.matchStart, this.matchLen);
          this.lookahead -= this.matchLen;
          if (this.matchLen <= this.max_lazy && this.lookahead >= 3)
          {
            while (--this.matchLen > 0)
            {
              ++this.strstart;
              this.InsertString();
            }
            ++this.strstart;
          }
          else
          {
            this.strstart += this.matchLen;
            if (this.lookahead >= 2)
              this.UpdateHash();
          }
          this.matchLen = 2;
          if (!flag)
            continue;
        }
        else
        {
          this.huffman.TallyLit((int) this.window[this.strstart] & (int) byte.MaxValue);
          ++this.strstart;
          --this.lookahead;
        }
        if (this.huffman.IsFull())
        {
          bool lastBlock = finish && this.lookahead == 0;
          this.huffman.FlushBlock(this.window, this.blockStart, this.strstart - this.blockStart, lastBlock);
          this.blockStart = this.strstart;
          return !lastBlock;
        }
      }
      return true;
    }

    private bool DeflateSlow(bool flush, bool finish)
    {
      if (this.lookahead < 262 && !flush)
        return false;
      while (this.lookahead >= 262 || flush)
      {
        if (this.lookahead == 0)
        {
          if (this.prevAvailable)
            this.huffman.TallyLit((int) this.window[this.strstart - 1] & (int) byte.MaxValue);
          this.prevAvailable = false;
          this.huffman.FlushBlock(this.window, this.blockStart, this.strstart - this.blockStart, finish);
          this.blockStart = this.strstart;
          return false;
        }
        if (this.strstart >= 65274)
          this.SlideWindow();
        int matchStart = this.matchStart;
        int matchLen = this.matchLen;
        if (this.lookahead >= 3)
        {
          int curMatch = this.InsertString();
          if (this.strategy != DeflateStrategy.HuffmanOnly && curMatch != 0 && this.strstart - curMatch <= 32506 && this.FindLongestMatch(curMatch) && this.matchLen <= 5 && (this.strategy == DeflateStrategy.Filtered || this.matchLen == 3 && this.strstart - this.matchStart > 4096))
            this.matchLen = 2;
        }
        if (matchLen >= 3 && this.matchLen <= matchLen)
        {
          this.huffman.TallyDist(this.strstart - 1 - matchStart, matchLen);
          int num = matchLen - 2;
          do
          {
            ++this.strstart;
            --this.lookahead;
            if (this.lookahead >= 3)
              this.InsertString();
          }
          while (--num > 0);
          ++this.strstart;
          --this.lookahead;
          this.prevAvailable = false;
          this.matchLen = 2;
        }
        else
        {
          if (this.prevAvailable)
            this.huffman.TallyLit((int) this.window[this.strstart - 1] & (int) byte.MaxValue);
          this.prevAvailable = true;
          ++this.strstart;
          --this.lookahead;
        }
        if (this.huffman.IsFull())
        {
          int storedLength = this.strstart - this.blockStart;
          if (this.prevAvailable)
            --storedLength;
          bool lastBlock = finish && this.lookahead == 0 && !this.prevAvailable;
          this.huffman.FlushBlock(this.window, this.blockStart, storedLength, lastBlock);
          this.blockStart += storedLength;
          return !lastBlock;
        }
      }
      return true;
    }
  }
}
