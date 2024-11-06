// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.Compression.Streams.OutputWindow
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51802, Culture=neutral, PublicKeyToken=null
// MVID: 6C361F8B-A5CA-49B8-ABC1-A50EE493C003
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.xml

using System;

#nullable disable
namespace ICSharpCode.SharpZipLib.Zip.Compression.Streams
{
  /// <summary>
  /// Contains the output from the Inflation process.
  /// We need to have a window so that we can refer backwards into the output stream
  /// to repeat stuff.<br />
  /// Author of the original java version : John Leuner
  /// </summary>
  public class OutputWindow
  {
    private const int WindowSize = 32768;
    private const int WindowMask = 32767;
    private byte[] window = new byte[32768];
    private int windowEnd;
    private int windowFilled;

    /// <summary>Write a byte to this output window</summary>
    /// <param name="value">value to write</param>
    /// <exception cref="T:System.InvalidOperationException">
    /// if window is full
    /// </exception>
    public void Write(int value)
    {
      if (this.windowFilled++ == 32768)
        throw new InvalidOperationException("Window full");
      this.window[this.windowEnd++] = (byte) value;
      this.windowEnd &= (int) short.MaxValue;
    }

    private void SlowRepeat(int repStart, int length, int distance)
    {
      while (length-- > 0)
      {
        this.window[this.windowEnd++] = this.window[repStart++];
        this.windowEnd &= (int) short.MaxValue;
        repStart &= (int) short.MaxValue;
      }
    }

    /// <summary>Append a byte pattern already in the window itself</summary>
    /// <param name="length">length of pattern to copy</param>
    /// <param name="distance">distance from end of window pattern occurs</param>
    /// <exception cref="T:System.InvalidOperationException">
    /// If the repeated data overflows the window
    /// </exception>
    public void Repeat(int length, int distance)
    {
      if ((this.windowFilled += length) > 32768)
        throw new InvalidOperationException("Window full");
      int num1 = this.windowEnd - distance & (int) short.MaxValue;
      int num2 = 32768 - length;
      if (num1 <= num2 && this.windowEnd < num2)
      {
        if (length <= distance)
        {
          Array.Copy((Array) this.window, num1, (Array) this.window, this.windowEnd, length);
          this.windowEnd += length;
        }
        else
        {
          while (length-- > 0)
            this.window[this.windowEnd++] = this.window[num1++];
        }
      }
      else
        this.SlowRepeat(num1, length, distance);
    }

    /// <summary>Copy from input manipulator to internal window</summary>
    /// <param name="input">source of data</param>
    /// <param name="length">length of data to copy</param>
    /// <returns>the number of bytes copied</returns>
    public int CopyStored(StreamManipulator input, int length)
    {
      length = Math.Min(Math.Min(length, 32768 - this.windowFilled), input.AvailableBytes);
      int length1 = 32768 - this.windowEnd;
      int num;
      if (length > length1)
      {
        num = input.CopyBytes(this.window, this.windowEnd, length1);
        if (num == length1)
          num += input.CopyBytes(this.window, 0, length - length1);
      }
      else
        num = input.CopyBytes(this.window, this.windowEnd, length);
      this.windowEnd = this.windowEnd + num & (int) short.MaxValue;
      this.windowFilled += num;
      return num;
    }

    /// <summary>Copy dictionary to window</summary>
    /// <param name="dictionary">source dictionary</param>
    /// <param name="offset">offset of start in source dictionary</param>
    /// <param name="length">length of dictionary</param>
    /// <exception cref="T:System.InvalidOperationException">
    /// If window isnt empty
    /// </exception>
    public void CopyDict(byte[] dictionary, int offset, int length)
    {
      if (dictionary == null)
        throw new ArgumentNullException(nameof (dictionary));
      if (this.windowFilled > 0)
        throw new InvalidOperationException();
      if (length > 32768)
      {
        offset += length - 32768;
        length = 32768;
      }
      Array.Copy((Array) dictionary, offset, (Array) this.window, 0, length);
      this.windowEnd = length & (int) short.MaxValue;
    }

    /// <summary>Get remaining unfilled space in window</summary>
    /// <returns>Number of bytes left in window</returns>
    public int GetFreeSpace() => 32768 - this.windowFilled;

    /// <summary>Get bytes available for output in window</summary>
    /// <returns>Number of bytes filled</returns>
    public int GetAvailable() => this.windowFilled;

    /// <summary>Copy contents of window to output</summary>
    /// <param name="output">buffer to copy to</param>
    /// <param name="offset">offset to start at</param>
    /// <param name="len">number of bytes to count</param>
    /// <returns>The number of bytes copied</returns>
    /// <exception cref="T:System.InvalidOperationException">
    /// If a window underflow occurs
    /// </exception>
    public int CopyOutput(byte[] output, int offset, int len)
    {
      int num1 = this.windowEnd;
      if (len > this.windowFilled)
        len = this.windowFilled;
      else
        num1 = this.windowEnd - this.windowFilled + len & (int) short.MaxValue;
      int num2 = len;
      int length = len - num1;
      if (length > 0)
      {
        Array.Copy((Array) this.window, 32768 - length, (Array) output, offset, length);
        offset += length;
        len = num1;
      }
      Array.Copy((Array) this.window, num1 - len, (Array) output, offset, len);
      this.windowFilled -= num2;
      if (this.windowFilled < 0)
        throw new InvalidOperationException();
      return num2;
    }

    /// <summary>
    /// Reset by clearing window so <see cref="M:ICSharpCode.SharpZipLib.Zip.Compression.Streams.OutputWindow.GetAvailable">GetAvailable</see> returns 0
    /// </summary>
    public void Reset() => this.windowFilled = this.windowEnd = 0;
  }
}
