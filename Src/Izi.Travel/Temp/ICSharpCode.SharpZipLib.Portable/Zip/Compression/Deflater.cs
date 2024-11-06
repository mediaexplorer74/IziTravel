// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.Compression.Deflater
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51802, Culture=neutral, PublicKeyToken=null
// MVID: 6C361F8B-A5CA-49B8-ABC1-A50EE493C003
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.xml

using System;

#nullable disable
namespace ICSharpCode.SharpZipLib.Zip.Compression
{
  /// <summary>
  /// This is the Deflater class.  The deflater class compresses input
  /// with the deflate algorithm described in RFC 1951.  It has several
  /// compression levels and three different strategies described below.
  /// 
  /// This class is <i>not</i> thread safe.  This is inherent in the API, due
  /// to the split of deflate and setInput.
  /// 
  /// author of the original java version : Jochen Hoenicke
  /// </summary>
  public class Deflater
  {
    /// <summary>
    /// The best and slowest compression level.  This tries to find very
    /// long and distant string repetitions.
    /// </summary>
    public const int BEST_COMPRESSION = 9;
    /// <summary>The worst but fastest compression level.</summary>
    public const int BEST_SPEED = 1;
    /// <summary>The default compression level.</summary>
    public const int DEFAULT_COMPRESSION = -1;
    /// <summary>
    /// This level won't compress at all but output uncompressed blocks.
    /// </summary>
    public const int NO_COMPRESSION = 0;
    /// <summary>
    /// The compression method.  This is the only method supported so far.
    /// There is no need to use this constant at all.
    /// </summary>
    public const int DEFLATED = 8;
    private const int IS_SETDICT = 1;
    private const int IS_FLUSHING = 4;
    private const int IS_FINISHING = 8;
    private const int INIT_STATE = 0;
    private const int SETDICT_STATE = 1;
    private const int BUSY_STATE = 16;
    private const int FLUSHING_STATE = 20;
    private const int FINISHING_STATE = 28;
    private const int FINISHED_STATE = 30;
    private const int CLOSED_STATE = 127;
    /// <summary>Compression level.</summary>
    private int level;
    /// <summary>
    /// If true no Zlib/RFC1950 headers or footers are generated
    /// </summary>
    private bool noZlibHeaderOrFooter;
    /// <summary>The current state.</summary>
    private int state;
    /// <summary>The total bytes of output written.</summary>
    private long totalOut;
    /// <summary>The pending output.</summary>
    private DeflaterPending pending;
    /// <summary>The deflater engine.</summary>
    private DeflaterEngine engine;

    /// <summary>Creates a new deflater with default compression level.</summary>
    public Deflater()
      : this(-1, false)
    {
    }

    /// <summary>Creates a new deflater with given compression level.</summary>
    /// <param name="level">
    /// the compression level, a value between NO_COMPRESSION
    /// and BEST_COMPRESSION, or DEFAULT_COMPRESSION.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">if lvl is out of range.</exception>
    public Deflater(int level)
      : this(level, false)
    {
    }

    /// <summary>Creates a new deflater with given compression level.</summary>
    /// <param name="level">
    /// the compression level, a value between NO_COMPRESSION
    /// and BEST_COMPRESSION.
    /// </param>
    /// <param name="noZlibHeaderOrFooter">
    /// true, if we should suppress the Zlib/RFC1950 header at the
    /// beginning and the adler checksum at the end of the output.  This is
    /// useful for the GZIP/PKZIP formats.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">if lvl is out of range.</exception>
    public Deflater(int level, bool noZlibHeaderOrFooter)
    {
      if (level == -1)
        level = 6;
      else if (level < 0 || level > 9)
        throw new ArgumentOutOfRangeException(nameof (level));
      this.pending = new DeflaterPending();
      this.engine = new DeflaterEngine(this.pending);
      this.noZlibHeaderOrFooter = noZlibHeaderOrFooter;
      this.SetStrategy(DeflateStrategy.Default);
      this.SetLevel(level);
      this.Reset();
    }

    /// <summary>
    /// Resets the deflater.  The deflater acts afterwards as if it was
    /// just created with the same compression level and strategy as it
    /// had before.
    /// </summary>
    public void Reset()
    {
      this.state = this.noZlibHeaderOrFooter ? 16 : 0;
      this.totalOut = 0L;
      this.pending.Reset();
      this.engine.Reset();
    }

    /// <summary>
    /// Gets the current adler checksum of the data that was processed so far.
    /// </summary>
    public int Adler => this.engine.Adler;

    /// <summary>Gets the number of input bytes processed so far.</summary>
    public long TotalIn => this.engine.TotalIn;

    /// <summary>Gets the number of output bytes so far.</summary>
    public long TotalOut => this.totalOut;

    /// <summary>
    /// Flushes the current input block.  Further calls to deflate() will
    /// produce enough output to inflate everything in the current input
    /// block.  This is not part of Sun's JDK so I have made it package
    /// private.  It is used by DeflaterOutputStream to implement
    /// flush().
    /// </summary>
    public void Flush() => this.state |= 4;

    /// <summary>
    /// Finishes the deflater with the current input block.  It is an error
    /// to give more input after this method was called.  This method must
    /// be called to force all bytes to be flushed.
    /// </summary>
    public void Finish() => this.state |= 12;

    /// <summary>
    /// Returns true if the stream was finished and no more output bytes
    /// are available.
    /// </summary>
    public bool IsFinished => this.state == 30 && this.pending.IsFlushed;

    /// <summary>
    /// Returns true, if the input buffer is empty.
    /// You should then call setInput().
    /// NOTE: This method can also return true when the stream
    /// was finished.
    /// </summary>
    public bool IsNeedingInput => this.engine.NeedsInput();

    /// <summary>
    /// Sets the data which should be compressed next.  This should be only
    /// called when needsInput indicates that more input is needed.
    /// If you call setInput when needsInput() returns false, the
    /// previous input that is still pending will be thrown away.
    /// The given byte array should not be changed, before needsInput() returns
    /// true again.
    /// This call is equivalent to <code>setInput(input, 0, input.length)</code>.
    /// </summary>
    /// <param name="input">the buffer containing the input data.</param>
    /// <exception cref="T:System.InvalidOperationException">
    /// if the buffer was finished() or ended().
    /// </exception>
    public void SetInput(byte[] input) => this.SetInput(input, 0, input.Length);

    /// <summary>
    /// Sets the data which should be compressed next.  This should be
    /// only called when needsInput indicates that more input is needed.
    /// The given byte array should not be changed, before needsInput() returns
    /// true again.
    /// </summary>
    /// <param name="input">the buffer containing the input data.</param>
    /// <param name="offset">the start of the data.</param>
    /// <param name="count">the number of data bytes of input.</param>
    /// <exception cref="T:System.InvalidOperationException">
    /// if the buffer was Finish()ed or if previous input is still pending.
    /// </exception>
    public void SetInput(byte[] input, int offset, int count)
    {
      if ((this.state & 8) != 0)
        throw new InvalidOperationException("Finish() already called");
      this.engine.SetInput(input, offset, count);
    }

    /// <summary>
    /// Sets the compression level.  There is no guarantee of the exact
    /// position of the change, but if you call this when needsInput is
    /// true the change of compression level will occur somewhere near
    /// before the end of the so far given input.
    /// </summary>
    /// <param name="level">the new compression level.</param>
    public void SetLevel(int level)
    {
      if (level == -1)
        level = 6;
      else if (level < 0 || level > 9)
        throw new ArgumentOutOfRangeException(nameof (level));
      if (this.level == level)
        return;
      this.level = level;
      this.engine.SetLevel(level);
    }

    /// <summary>Get current compression level</summary>
    /// <returns>Returns the current compression level</returns>
    public int GetLevel() => this.level;

    /// <summary>
    /// Sets the compression strategy. Strategy is one of
    /// DEFAULT_STRATEGY, HUFFMAN_ONLY and FILTERED.  For the exact
    /// position where the strategy is changed, the same as for
    /// SetLevel() applies.
    /// </summary>
    /// <param name="strategy">The new compression strategy.</param>
    public void SetStrategy(DeflateStrategy strategy) => this.engine.Strategy = strategy;

    /// <summary>
    /// Deflates the current input block with to the given array.
    /// </summary>
    /// <param name="output">The buffer where compressed data is stored</param>
    /// <returns>
    /// The number of compressed bytes added to the output, or 0 if either
    /// IsNeedingInput() or IsFinished returns true or length is zero.
    /// </returns>
    public int Deflate(byte[] output) => this.Deflate(output, 0, output.Length);

    /// <summary>Deflates the current input block to the given array.</summary>
    /// <param name="output">Buffer to store the compressed data.</param>
    /// <param name="offset">Offset into the output array.</param>
    /// <param name="length">
    /// The maximum number of bytes that may be stored.
    /// </param>
    /// <returns>
    /// The number of compressed bytes added to the output, or 0 if either
    /// needsInput() or finished() returns true or length is zero.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    /// If Finish() was previously called.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// If offset or length don't match the array length.
    /// </exception>
    public int Deflate(byte[] output, int offset, int length)
    {
      int num1 = length;
      if (this.state == (int) sbyte.MaxValue)
        throw new InvalidOperationException("Deflater closed");
      if (this.state < 16)
      {
        int num2 = 30720;
        int num3 = this.level - 1 >> 1;
        if (num3 < 0 || num3 > 3)
          num3 = 3;
        int num4 = num2 | num3 << 6;
        if ((this.state & 1) != 0)
          num4 |= 32;
        this.pending.WriteShortMSB(num4 + (31 - num4 % 31));
        if ((this.state & 1) != 0)
        {
          int adler = this.engine.Adler;
          this.engine.ResetAdler();
          this.pending.WriteShortMSB(adler >> 16);
          this.pending.WriteShortMSB(adler & (int) ushort.MaxValue);
        }
        this.state = 16 | this.state & 12;
      }
      while (true)
      {
        do
        {
          do
          {
            int num5 = this.pending.Flush(output, offset, length);
            offset += num5;
            this.totalOut += (long) num5;
            length -= num5;
            if (length == 0 || this.state == 30)
              goto label_24;
          }
          while (this.engine.Deflate((this.state & 4) != 0, (this.state & 8) != 0));
          if (this.state == 16)
            return num1 - length;
          if (this.state == 20)
          {
            if (this.level != 0)
            {
              for (int index = 8 + (-this.pending.BitCount & 7); index > 0; index -= 10)
                this.pending.WriteBits(2, 10);
            }
            this.state = 16;
          }
        }
        while (this.state != 28);
        this.pending.AlignToByte();
        if (!this.noZlibHeaderOrFooter)
        {
          int adler = this.engine.Adler;
          this.pending.WriteShortMSB(adler >> 16);
          this.pending.WriteShortMSB(adler & (int) ushort.MaxValue);
        }
        this.state = 30;
      }
label_24:
      return num1 - length;
    }

    /// <summary>
    /// Sets the dictionary which should be used in the deflate process.
    /// This call is equivalent to <code>setDictionary(dict, 0, dict.Length)</code>.
    /// </summary>
    /// <param name="dictionary">the dictionary.</param>
    /// <exception cref="T:System.InvalidOperationException">
    /// if SetInput () or Deflate () were already called or another dictionary was already set.
    /// </exception>
    public void SetDictionary(byte[] dictionary)
    {
      this.SetDictionary(dictionary, 0, dictionary.Length);
    }

    /// <summary>
    /// Sets the dictionary which should be used in the deflate process.
    /// The dictionary is a byte array containing strings that are
    /// likely to occur in the data which should be compressed.  The
    /// dictionary is not stored in the compressed output, only a
    /// checksum.  To decompress the output you need to supply the same
    /// dictionary again.
    /// </summary>
    /// <param name="dictionary">The dictionary data</param>
    /// <param name="index">
    /// The index where dictionary information commences.
    /// </param>
    /// <param name="count">The number of bytes in the dictionary.</param>
    /// <exception cref="T:System.InvalidOperationException">
    /// If SetInput () or Deflate() were already called or another dictionary was already set.
    /// </exception>
    public void SetDictionary(byte[] dictionary, int index, int count)
    {
      this.state = this.state == 0 ? 1 : throw new InvalidOperationException();
      this.engine.SetDictionary(dictionary, index, count);
    }
  }
}
