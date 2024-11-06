// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.Compression.Inflater
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51802, Culture=neutral, PublicKeyToken=null
// MVID: 6C361F8B-A5CA-49B8-ABC1-A50EE493C003
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.xml

using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using System;

#nullable disable
namespace ICSharpCode.SharpZipLib.Zip.Compression
{
  /// <summary>
  /// Inflater is used to decompress data that has been compressed according
  /// to the "deflate" standard described in rfc1951.
  /// 
  /// By default Zlib (rfc1950) headers and footers are expected in the input.
  /// You can use constructor <code> public Inflater(bool noHeader)</code> passing true
  /// if there is no Zlib header information
  /// 
  /// The usage is as following.  First you have to set some input with
  /// <code>SetInput()</code>, then Inflate() it.  If inflate doesn't
  /// inflate any bytes there may be three reasons:
  /// <ul>
  /// <li>IsNeedingInput() returns true because the input buffer is empty.
  /// You have to provide more input with <code>SetInput()</code>.
  /// NOTE: IsNeedingInput() also returns true when, the stream is finished.
  /// </li>
  /// <li>IsNeedingDictionary() returns true, you have to provide a preset
  ///    dictionary with <code>SetDictionary()</code>.</li>
  /// <li>IsFinished returns true, the inflater has finished.</li>
  /// </ul>
  /// Once the first output byte is produced, a dictionary will not be
  /// needed at a later stage.
  /// 
  /// author of the original java version : John Leuner, Jochen Hoenicke
  /// </summary>
  public class Inflater
  {
    /// <summary>These are the possible states for an inflater</summary>
    private const int DECODE_HEADER = 0;
    private const int DECODE_DICT = 1;
    private const int DECODE_BLOCKS = 2;
    private const int DECODE_STORED_LEN1 = 3;
    private const int DECODE_STORED_LEN2 = 4;
    private const int DECODE_STORED = 5;
    private const int DECODE_DYN_HEADER = 6;
    private const int DECODE_HUFFMAN = 7;
    private const int DECODE_HUFFMAN_LENBITS = 8;
    private const int DECODE_HUFFMAN_DIST = 9;
    private const int DECODE_HUFFMAN_DISTBITS = 10;
    private const int DECODE_CHKSUM = 11;
    private const int FINISHED = 12;
    /// <summary>Copy lengths for literal codes 257..285</summary>
    private static readonly int[] CPLENS = new int[29]
    {
      3,
      4,
      5,
      6,
      7,
      8,
      9,
      10,
      11,
      13,
      15,
      17,
      19,
      23,
      27,
      31,
      35,
      43,
      51,
      59,
      67,
      83,
      99,
      115,
      131,
      163,
      195,
      227,
      258
    };
    /// <summary>Extra bits for literal codes 257..285</summary>
    private static readonly int[] CPLEXT = new int[29]
    {
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      1,
      1,
      1,
      1,
      2,
      2,
      2,
      2,
      3,
      3,
      3,
      3,
      4,
      4,
      4,
      4,
      5,
      5,
      5,
      5,
      0
    };
    /// <summary>Copy offsets for distance codes 0..29</summary>
    private static readonly int[] CPDIST = new int[30]
    {
      1,
      2,
      3,
      4,
      5,
      7,
      9,
      13,
      17,
      25,
      33,
      49,
      65,
      97,
      129,
      193,
      257,
      385,
      513,
      769,
      1025,
      1537,
      2049,
      3073,
      4097,
      6145,
      8193,
      12289,
      16385,
      24577
    };
    /// <summary>Extra bits for distance codes</summary>
    private static readonly int[] CPDEXT = new int[30]
    {
      0,
      0,
      0,
      0,
      1,
      1,
      2,
      2,
      3,
      3,
      4,
      4,
      5,
      5,
      6,
      6,
      7,
      7,
      8,
      8,
      9,
      9,
      10,
      10,
      11,
      11,
      12,
      12,
      13,
      13
    };
    /// <summary>This variable contains the current state.</summary>
    private int mode;
    /// <summary>
    /// The adler checksum of the dictionary or of the decompressed
    /// stream, as it is written in the header resp. footer of the
    /// compressed stream.
    /// Only valid if mode is DECODE_DICT or DECODE_CHKSUM.
    /// </summary>
    private int readAdler;
    /// <summary>
    /// The number of bits needed to complete the current state.  This
    /// is valid, if mode is DECODE_DICT, DECODE_CHKSUM,
    /// DECODE_HUFFMAN_LENBITS or DECODE_HUFFMAN_DISTBITS.
    /// </summary>
    private int neededBits;
    private int repLength;
    private int repDist;
    private int uncomprLen;
    /// <summary>
    /// True, if the last block flag was set in the last block of the
    /// inflated stream.  This means that the stream ends after the
    /// current block.
    /// </summary>
    private bool isLastBlock;
    /// <summary>The total number of inflated bytes.</summary>
    private long totalOut;
    /// <summary>
    /// The total number of bytes set with setInput().  This is not the
    /// value returned by the TotalIn property, since this also includes the
    /// unprocessed input.
    /// </summary>
    private long totalIn;
    /// <summary>
    /// This variable stores the noHeader flag that was given to the constructor.
    /// True means, that the inflated stream doesn't contain a Zlib header or
    /// footer.
    /// </summary>
    private bool noHeader;
    private StreamManipulator input;
    private OutputWindow outputWindow;
    private InflaterDynHeader dynHeader;
    private InflaterHuffmanTree litlenTree;
    private InflaterHuffmanTree distTree;
    private Adler32 adler;

    /// <summary>
    /// Creates a new inflater or RFC1951 decompressor
    /// RFC1950/Zlib headers and footers will be expected in the input data
    /// </summary>
    public Inflater()
      : this(false)
    {
    }

    /// <summary>Creates a new inflater.</summary>
    /// <param name="noHeader">
    /// True if no RFC1950/Zlib header and footer fields are expected in the input data
    /// 
    /// This is used for GZIPed/Zipped input.
    /// 
    /// For compatibility with
    /// Sun JDK you should provide one byte of input more than needed in
    /// this case.
    /// </param>
    public Inflater(bool noHeader)
    {
      this.noHeader = noHeader;
      this.adler = new Adler32();
      this.input = new StreamManipulator();
      this.outputWindow = new OutputWindow();
      this.mode = noHeader ? 2 : 0;
    }

    /// <summary>
    /// Resets the inflater so that a new stream can be decompressed.  All
    /// pending input and output will be discarded.
    /// </summary>
    public void Reset()
    {
      this.mode = this.noHeader ? 2 : 0;
      this.totalIn = 0L;
      this.totalOut = 0L;
      this.input.Reset();
      this.outputWindow.Reset();
      this.dynHeader = (InflaterDynHeader) null;
      this.litlenTree = (InflaterHuffmanTree) null;
      this.distTree = (InflaterHuffmanTree) null;
      this.isLastBlock = false;
      this.adler.Reset();
    }

    /// <summary>Decodes a zlib/RFC1950 header.</summary>
    /// <returns>False if more input is needed.</returns>
    /// <exception cref="T:ICSharpCode.SharpZipLib.SharpZipBaseException">
    /// The header is invalid.
    /// </exception>
    private bool DecodeHeader()
    {
      int num1 = this.input.PeekBits(16);
      if (num1 < 0)
        return false;
      this.input.DropBits(16);
      int num2 = (num1 << 8 | num1 >> 8) & (int) ushort.MaxValue;
      if (num2 % 31 != 0)
        throw new SharpZipBaseException("Header checksum illegal");
      if ((num2 & 3840) != 2048)
        throw new SharpZipBaseException("Compression Method unknown");
      if ((num2 & 32) == 0)
      {
        this.mode = 2;
      }
      else
      {
        this.mode = 1;
        this.neededBits = 32;
      }
      return true;
    }

    /// <summary>
    /// Decodes the dictionary checksum after the deflate header.
    /// </summary>
    /// <returns>False if more input is needed.</returns>
    private bool DecodeDict()
    {
      for (; this.neededBits > 0; this.neededBits -= 8)
      {
        int num = this.input.PeekBits(8);
        if (num < 0)
          return false;
        this.input.DropBits(8);
        this.readAdler = this.readAdler << 8 | num;
      }
      return false;
    }

    /// <summary>
    /// Decodes the huffman encoded symbols in the input stream.
    /// </summary>
    /// <returns>
    /// false if more input is needed, true if output window is
    /// full or the current block ends.
    /// </returns>
    /// <exception cref="T:ICSharpCode.SharpZipLib.SharpZipBaseException">
    /// if deflated stream is invalid.
    /// </exception>
    private bool DecodeHuffman()
    {
      int freeSpace = this.outputWindow.GetFreeSpace();
      while (freeSpace >= 258)
      {
        switch (this.mode)
        {
          case 7:
            int symbol1;
            while (((symbol1 = this.litlenTree.GetSymbol(this.input)) & -256) == 0)
            {
              this.outputWindow.Write(symbol1);
              if (--freeSpace < 258)
                return true;
            }
            if (symbol1 < 257)
            {
              if (symbol1 < 0)
                return false;
              this.distTree = (InflaterHuffmanTree) null;
              this.litlenTree = (InflaterHuffmanTree) null;
              this.mode = 2;
              return true;
            }
            try
            {
              this.repLength = Inflater.CPLENS[symbol1 - 257];
              this.neededBits = Inflater.CPLEXT[symbol1 - 257];
              goto case 8;
            }
            catch (Exception ex)
            {
              throw new SharpZipBaseException("Illegal rep length code");
            }
          case 8:
            if (this.neededBits > 0)
            {
              this.mode = 8;
              int num = this.input.PeekBits(this.neededBits);
              if (num < 0)
                return false;
              this.input.DropBits(this.neededBits);
              this.repLength += num;
            }
            this.mode = 9;
            goto case 9;
          case 9:
            int symbol2 = this.distTree.GetSymbol(this.input);
            if (symbol2 < 0)
              return false;
            try
            {
              this.repDist = Inflater.CPDIST[symbol2];
              this.neededBits = Inflater.CPDEXT[symbol2];
              goto case 10;
            }
            catch (Exception ex)
            {
              throw new SharpZipBaseException("Illegal rep dist code");
            }
          case 10:
            if (this.neededBits > 0)
            {
              this.mode = 10;
              int num = this.input.PeekBits(this.neededBits);
              if (num < 0)
                return false;
              this.input.DropBits(this.neededBits);
              this.repDist += num;
            }
            this.outputWindow.Repeat(this.repLength, this.repDist);
            freeSpace -= this.repLength;
            this.mode = 7;
            continue;
          default:
            throw new SharpZipBaseException("Inflater unknown mode");
        }
      }
      return true;
    }

    /// <summary>Decodes the adler checksum after the deflate stream.</summary>
    /// <returns>false if more input is needed.</returns>
    /// <exception cref="T:ICSharpCode.SharpZipLib.SharpZipBaseException">
    /// If checksum doesn't match.
    /// </exception>
    private bool DecodeChksum()
    {
      for (; this.neededBits > 0; this.neededBits -= 8)
      {
        int num = this.input.PeekBits(8);
        if (num < 0)
          return false;
        this.input.DropBits(8);
        this.readAdler = this.readAdler << 8 | num;
      }
      if ((int) this.adler.Value != this.readAdler)
        throw new SharpZipBaseException("Adler chksum doesn't match: " + (object) (int) this.adler.Value + " vs. " + (object) this.readAdler);
      this.mode = 12;
      return false;
    }

    /// <summary>Decodes the deflated stream.</summary>
    /// <returns>false if more input is needed, or if finished.</returns>
    /// <exception cref="T:ICSharpCode.SharpZipLib.SharpZipBaseException">
    /// if deflated stream is invalid.
    /// </exception>
    private bool Decode()
    {
      switch (this.mode)
      {
        case 0:
          return this.DecodeHeader();
        case 1:
          return this.DecodeDict();
        case 2:
          if (this.isLastBlock)
          {
            if (this.noHeader)
            {
              this.mode = 12;
              return false;
            }
            this.input.SkipToByteBoundary();
            this.neededBits = 32;
            this.mode = 11;
            return true;
          }
          int num1 = this.input.PeekBits(3);
          if (num1 < 0)
            return false;
          this.input.DropBits(3);
          if ((num1 & 1) != 0)
            this.isLastBlock = true;
          switch (num1 >> 1)
          {
            case 0:
              this.input.SkipToByteBoundary();
              this.mode = 3;
              break;
            case 1:
              this.litlenTree = InflaterHuffmanTree.defLitLenTree;
              this.distTree = InflaterHuffmanTree.defDistTree;
              this.mode = 7;
              break;
            case 2:
              this.dynHeader = new InflaterDynHeader();
              this.mode = 6;
              break;
            default:
              throw new SharpZipBaseException("Unknown block type " + (object) num1);
          }
          return true;
        case 3:
          if ((this.uncomprLen = this.input.PeekBits(16)) < 0)
            return false;
          this.input.DropBits(16);
          this.mode = 4;
          goto case 4;
        case 4:
          int num2 = this.input.PeekBits(16);
          if (num2 < 0)
            return false;
          this.input.DropBits(16);
          if (num2 != (this.uncomprLen ^ (int) ushort.MaxValue))
            throw new SharpZipBaseException("broken uncompressed block");
          this.mode = 5;
          goto case 5;
        case 5:
          this.uncomprLen -= this.outputWindow.CopyStored(this.input, this.uncomprLen);
          if (this.uncomprLen != 0)
            return !this.input.IsNeedingInput;
          this.mode = 2;
          return true;
        case 6:
          if (!this.dynHeader.Decode(this.input))
            return false;
          this.litlenTree = this.dynHeader.BuildLitLenTree();
          this.distTree = this.dynHeader.BuildDistTree();
          this.mode = 7;
          goto case 7;
        case 7:
        case 8:
        case 9:
        case 10:
          return this.DecodeHuffman();
        case 11:
          return this.DecodeChksum();
        case 12:
          return false;
        default:
          throw new SharpZipBaseException("Inflater.Decode unknown mode");
      }
    }

    /// <summary>
    /// Sets the preset dictionary.  This should only be called, if
    /// needsDictionary() returns true and it should set the same
    /// dictionary, that was used for deflating.  The getAdler()
    /// function returns the checksum of the dictionary needed.
    /// </summary>
    /// <param name="buffer">The dictionary.</param>
    public void SetDictionary(byte[] buffer) => this.SetDictionary(buffer, 0, buffer.Length);

    /// <summary>
    /// Sets the preset dictionary.  This should only be called, if
    /// needsDictionary() returns true and it should set the same
    /// dictionary, that was used for deflating.  The getAdler()
    /// function returns the checksum of the dictionary needed.
    /// </summary>
    /// <param name="buffer">The dictionary.</param>
    /// <param name="index">
    /// The index into buffer where the dictionary starts.
    /// </param>
    /// <param name="count">The number of bytes in the dictionary.</param>
    /// <exception cref="T:System.InvalidOperationException">
    /// No dictionary is needed.
    /// </exception>
    /// <exception cref="T:ICSharpCode.SharpZipLib.SharpZipBaseException">
    /// The adler checksum for the buffer is invalid
    /// </exception>
    public void SetDictionary(byte[] buffer, int index, int count)
    {
      if (buffer == null)
        throw new ArgumentNullException(nameof (buffer));
      if (index < 0)
        throw new ArgumentOutOfRangeException(nameof (index));
      if (count < 0)
        throw new ArgumentOutOfRangeException(nameof (count));
      if (!this.IsNeedingDictionary)
        throw new InvalidOperationException("Dictionary is not needed");
      this.adler.Update(buffer, index, count);
      if ((int) this.adler.Value != this.readAdler)
        throw new SharpZipBaseException("Wrong adler checksum");
      this.adler.Reset();
      this.outputWindow.CopyDict(buffer, index, count);
      this.mode = 2;
    }

    /// <summary>
    /// Sets the input.  This should only be called, if needsInput()
    /// returns true.
    /// </summary>
    /// <param name="buffer">the input.</param>
    public void SetInput(byte[] buffer) => this.SetInput(buffer, 0, buffer.Length);

    /// <summary>
    /// Sets the input.  This should only be called, if needsInput()
    /// returns true.
    /// </summary>
    /// <param name="buffer">The source of input data</param>
    /// <param name="index">The index into buffer where the input starts.</param>
    /// <param name="count">The number of bytes of input to use.</param>
    /// <exception cref="T:System.InvalidOperationException">
    /// No input is needed.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// The index and/or count are wrong.
    /// </exception>
    public void SetInput(byte[] buffer, int index, int count)
    {
      this.input.SetInput(buffer, index, count);
      this.totalIn += (long) count;
    }

    /// <summary>
    /// Inflates the compressed stream to the output buffer.  If this
    /// returns 0, you should check, whether IsNeedingDictionary(),
    /// IsNeedingInput() or IsFinished() returns true, to determine why no
    /// further output is produced.
    /// </summary>
    /// <param name="buffer">the output buffer.</param>
    /// <returns>
    /// The number of bytes written to the buffer, 0 if no further
    /// output can be produced.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// if buffer has length 0.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    /// if deflated stream is invalid.
    /// </exception>
    public int Inflate(byte[] buffer)
    {
      return buffer != null ? this.Inflate(buffer, 0, buffer.Length) : throw new ArgumentNullException(nameof (buffer));
    }

    /// <summary>
    /// Inflates the compressed stream to the output buffer.  If this
    /// returns 0, you should check, whether needsDictionary(),
    /// needsInput() or finished() returns true, to determine why no
    /// further output is produced.
    /// </summary>
    /// <param name="buffer">the output buffer.</param>
    /// <param name="offset">the offset in buffer where storing starts.</param>
    /// <param name="count">the maximum number of bytes to output.</param>
    /// <returns>
    /// the number of bytes written to the buffer, 0 if no further output can be produced.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// if count is less than 0.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// if the index and / or count are wrong.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    /// if deflated stream is invalid.
    /// </exception>
    public int Inflate(byte[] buffer, int offset, int count)
    {
      if (buffer == null)
        throw new ArgumentNullException(nameof (buffer));
      if (count < 0)
        throw new ArgumentOutOfRangeException(nameof (count), "count cannot be negative");
      if (offset < 0)
        throw new ArgumentOutOfRangeException(nameof (offset), "offset cannot be negative");
      if (offset + count > buffer.Length)
        throw new ArgumentException("count exceeds buffer bounds");
      if (count == 0)
      {
        if (!this.IsFinished)
          this.Decode();
        return 0;
      }
      int num = 0;
      do
      {
        if (this.mode != 11)
        {
          int count1 = this.outputWindow.CopyOutput(buffer, offset, count);
          if (count1 > 0)
          {
            this.adler.Update(buffer, offset, count1);
            offset += count1;
            num += count1;
            this.totalOut += (long) count1;
            count -= count1;
            if (count == 0)
              return num;
          }
        }
      }
      while (this.Decode() || this.outputWindow.GetAvailable() > 0 && this.mode != 11);
      return num;
    }

    /// <summary>
    /// Returns true, if the input buffer is empty.
    /// You should then call setInput().
    /// NOTE: This method also returns true when the stream is finished.
    /// </summary>
    public bool IsNeedingInput => this.input.IsNeedingInput;

    /// <summary>
    /// Returns true, if a preset dictionary is needed to inflate the input.
    /// </summary>
    public bool IsNeedingDictionary => this.mode == 1 && this.neededBits == 0;

    /// <summary>
    /// Returns true, if the inflater has finished.  This means, that no
    /// input is needed and no output can be produced.
    /// </summary>
    public bool IsFinished => this.mode == 12 && this.outputWindow.GetAvailable() == 0;

    /// <summary>
    /// Gets the adler checksum.  This is either the checksum of all
    /// uncompressed bytes returned by inflate(), or if needsDictionary()
    /// returns true (and thus no output was yet produced) this is the
    /// adler checksum of the expected dictionary.
    /// </summary>
    /// <returns>the adler checksum.</returns>
    public int Adler => !this.IsNeedingDictionary ? (int) this.adler.Value : this.readAdler;

    /// <summary>
    /// Gets the total number of output bytes returned by Inflate().
    /// </summary>
    /// <returns>the total number of output bytes.</returns>
    public long TotalOut => this.totalOut;

    /// <summary>
    /// Gets the total number of processed compressed input bytes.
    /// </summary>
    /// <returns>The total number of bytes of processed input bytes.</returns>
    public long TotalIn => this.totalIn - (long) this.RemainingInput;

    /// <summary>
    /// Gets the number of unprocessed input bytes.  Useful, if the end of the
    /// stream is reached and you want to further process the bytes after
    /// the deflate stream.
    /// </summary>
    /// <returns>
    /// The number of bytes of the input which have not been processed.
    /// </returns>
    public int RemainingInput => this.input.AvailableBytes;
  }
}
