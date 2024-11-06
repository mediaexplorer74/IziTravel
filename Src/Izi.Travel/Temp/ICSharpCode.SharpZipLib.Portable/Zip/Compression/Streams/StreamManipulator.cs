// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.Compression.Streams.StreamManipulator
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51802, Culture=neutral, PublicKeyToken=null
// MVID: 6C361F8B-A5CA-49B8-ABC1-A50EE493C003
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.xml

using System;

#nullable disable
namespace ICSharpCode.SharpZipLib.Zip.Compression.Streams
{
  /// <summary>
  /// This class allows us to retrieve a specified number of bits from
  /// the input buffer, as well as copy big byte blocks.
  /// 
  /// It uses an int buffer to store up to 31 bits for direct
  /// manipulation.  This guarantees that we can get at least 16 bits,
  /// but we only need at most 15, so this is all safe.
  /// 
  /// There are some optimizations in this class, for example, you must
  /// never peek more than 8 bits more than needed, and you must first
  /// peek bits before you may drop them.  This is not a general purpose
  /// class but optimized for the behaviour of the Inflater.
  /// 
  /// authors of the original java version : John Leuner, Jochen Hoenicke
  /// </summary>
  public class StreamManipulator
  {
    private byte[] window_;
    private int windowStart_;
    private int windowEnd_;
    private uint buffer_;
    private int bitsInBuffer_;

    /// <summary>
    /// Get the next sequence of bits but don't increase input pointer.  bitCount must be
    /// less or equal 16 and if this call succeeds, you must drop
    /// at least n - 8 bits in the next call.
    /// </summary>
    /// <param name="bitCount">The number of bits to peek.</param>
    /// <returns>
    /// the value of the bits, or -1 if not enough bits available.  */
    /// </returns>
    public int PeekBits(int bitCount)
    {
      if (this.bitsInBuffer_ < bitCount)
      {
        if (this.windowStart_ == this.windowEnd_)
          return -1;
        this.buffer_ |= (uint) (((int) this.window_[this.windowStart_++] & (int) byte.MaxValue | ((int) this.window_[this.windowStart_++] & (int) byte.MaxValue) << 8) << this.bitsInBuffer_);
        this.bitsInBuffer_ += 16;
      }
      return (int) ((long) this.buffer_ & (long) ((1 << bitCount) - 1));
    }

    /// <summary>
    /// Drops the next n bits from the input.  You should have called PeekBits
    /// with a bigger or equal n before, to make sure that enough bits are in
    /// the bit buffer.
    /// </summary>
    /// <param name="bitCount">The number of bits to drop.</param>
    public void DropBits(int bitCount)
    {
      this.buffer_ >>= bitCount;
      this.bitsInBuffer_ -= bitCount;
    }

    /// <summary>
    /// Gets the next n bits and increases input pointer.  This is equivalent
    /// to <see cref="M:ICSharpCode.SharpZipLib.Zip.Compression.Streams.StreamManipulator.PeekBits(System.Int32)" /> followed by <see cref="M:ICSharpCode.SharpZipLib.Zip.Compression.Streams.StreamManipulator.DropBits(System.Int32)" />, except for correct error handling.
    /// </summary>
    /// <param name="bitCount">The number of bits to retrieve.</param>
    /// <returns>
    /// the value of the bits, or -1 if not enough bits available.
    /// </returns>
    public int GetBits(int bitCount)
    {
      int bits = this.PeekBits(bitCount);
      if (bits >= 0)
        this.DropBits(bitCount);
      return bits;
    }

    /// <summary>
    /// Gets the number of bits available in the bit buffer.  This must be
    /// only called when a previous PeekBits() returned -1.
    /// </summary>
    /// <returns>the number of bits available.</returns>
    public int AvailableBits => this.bitsInBuffer_;

    /// <summary>Gets the number of bytes available.</summary>
    /// <returns>The number of bytes available.</returns>
    public int AvailableBytes => this.windowEnd_ - this.windowStart_ + (this.bitsInBuffer_ >> 3);

    /// <summary>Skips to the next byte boundary.</summary>
    public void SkipToByteBoundary()
    {
      this.buffer_ >>= this.bitsInBuffer_ & 7;
      this.bitsInBuffer_ &= -8;
    }

    /// <summary>Returns true when SetInput can be called</summary>
    public bool IsNeedingInput => this.windowStart_ == this.windowEnd_;

    /// <summary>
    /// Copies bytes from input buffer to output buffer starting
    /// at output[offset].  You have to make sure, that the buffer is
    /// byte aligned.  If not enough bytes are available, copies fewer
    /// bytes.
    /// </summary>
    /// <param name="output">The buffer to copy bytes to.</param>
    /// <param name="offset">
    /// The offset in the buffer at which copying starts
    /// </param>
    /// <param name="length">The length to copy, 0 is allowed.</param>
    /// <returns>
    /// The number of bytes copied, 0 if no bytes were available.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// Length is less than zero
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    /// Bit buffer isnt byte aligned
    /// </exception>
    public int CopyBytes(byte[] output, int offset, int length)
    {
      if (length < 0)
        throw new ArgumentOutOfRangeException(nameof (length));
      if ((this.bitsInBuffer_ & 7) != 0)
        throw new InvalidOperationException("Bit buffer is not byte aligned!");
      int num1 = 0;
      while (this.bitsInBuffer_ > 0 && length > 0)
      {
        output[offset++] = (byte) this.buffer_;
        this.buffer_ >>= 8;
        this.bitsInBuffer_ -= 8;
        --length;
        ++num1;
      }
      if (length == 0)
        return num1;
      int num2 = this.windowEnd_ - this.windowStart_;
      if (length > num2)
        length = num2;
      Array.Copy((Array) this.window_, this.windowStart_, (Array) output, offset, length);
      this.windowStart_ += length;
      if ((this.windowStart_ - this.windowEnd_ & 1) != 0)
      {
        this.buffer_ = (uint) this.window_[this.windowStart_++] & (uint) byte.MaxValue;
        this.bitsInBuffer_ = 8;
      }
      return num1 + length;
    }

    /// <summary>Resets state and empties internal buffers</summary>
    public void Reset()
    {
      this.buffer_ = 0U;
      this.windowStart_ = this.windowEnd_ = this.bitsInBuffer_ = 0;
    }

    /// <summary>
    /// Add more input for consumption.
    /// Only call when IsNeedingInput returns true
    /// </summary>
    /// <param name="buffer">data to be input</param>
    /// <param name="offset">offset of first byte of input</param>
    /// <param name="count">number of bytes of input to add.</param>
    public void SetInput(byte[] buffer, int offset, int count)
    {
      if (buffer == null)
        throw new ArgumentNullException(nameof (buffer));
      if (offset < 0)
        throw new ArgumentOutOfRangeException(nameof (offset), "Cannot be negative");
      if (count < 0)
        throw new ArgumentOutOfRangeException(nameof (count), "Cannot be negative");
      if (this.windowStart_ < this.windowEnd_)
        throw new InvalidOperationException("Old input was not completely processed");
      int num = offset + count;
      if (offset > num || num > buffer.Length)
        throw new ArgumentOutOfRangeException(nameof (count));
      if ((count & 1) != 0)
      {
        this.buffer_ |= (uint) (((int) buffer[offset++] & (int) byte.MaxValue) << this.bitsInBuffer_);
        this.bitsInBuffer_ += 8;
      }
      this.window_ = buffer;
      this.windowStart_ = offset;
      this.windowEnd_ = num;
    }
  }
}
