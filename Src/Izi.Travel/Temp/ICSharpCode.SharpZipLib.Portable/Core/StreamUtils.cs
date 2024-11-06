// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Core.StreamUtils
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51802, Culture=neutral, PublicKeyToken=null
// MVID: 6C361F8B-A5CA-49B8-ABC1-A50EE493C003
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.xml

using System;
using System.IO;

#nullable disable
namespace ICSharpCode.SharpZipLib.Core
{
  /// <summary>
  /// Provides simple <see cref="T:System.IO.Stream" />" utilities.
  /// </summary>
  public sealed class StreamUtils
  {
    /// <summary>
    /// Read from a <see cref="T:System.IO.Stream" /> ensuring all the required data is read.
    /// </summary>
    /// <param name="stream">The stream to read.</param>
    /// <param name="buffer">The buffer to fill.</param>
    /// <seealso cref="M:ICSharpCode.SharpZipLib.Core.StreamUtils.ReadFully(System.IO.Stream,System.Byte[],System.Int32,System.Int32)" />
    public static void ReadFully(Stream stream, byte[] buffer)
    {
      StreamUtils.ReadFully(stream, buffer, 0, buffer.Length);
    }

    /// <summary>
    /// Read from a <see cref="T:System.IO.Stream" />" ensuring all the required data is read.
    /// </summary>
    /// <param name="stream">The stream to read data from.</param>
    /// <param name="buffer">The buffer to store data in.</param>
    /// <param name="offset">The offset at which to begin storing data.</param>
    /// <param name="count">The number of bytes of data to store.</param>
    /// <exception cref="T:System.ArgumentNullException">Required parameter is null</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="offset" /> and or <paramref name="count" /> are invalid.</exception>
    /// <exception cref="T:System.IO.EndOfStreamException">End of stream is encountered before all the data has been read.</exception>
    public static void ReadFully(Stream stream, byte[] buffer, int offset, int count)
    {
      if (stream == null)
        throw new ArgumentNullException(nameof (stream));
      if (buffer == null)
        throw new ArgumentNullException(nameof (buffer));
      if (offset < 0 || offset > buffer.Length)
        throw new ArgumentOutOfRangeException(nameof (offset));
      if (count < 0 || offset + count > buffer.Length)
        throw new ArgumentOutOfRangeException(nameof (count));
      int num;
      for (; count > 0; count -= num)
      {
        num = stream.Read(buffer, offset, count);
        if (num <= 0)
          throw new EndOfStreamException();
        offset += num;
      }
    }

    /// <summary>
    /// Copy the contents of one <see cref="T:System.IO.Stream" /> to another.
    /// </summary>
    /// <param name="source">The stream to source data from.</param>
    /// <param name="destination">The stream to write data to.</param>
    /// <param name="buffer">The buffer to use during copying.</param>
    public static void Copy(Stream source, Stream destination, byte[] buffer)
    {
      if (source == null)
        throw new ArgumentNullException(nameof (source));
      if (destination == null)
        throw new ArgumentNullException(nameof (destination));
      if (buffer == null)
        throw new ArgumentNullException(nameof (buffer));
      if (buffer.Length < 128)
        throw new ArgumentException("Buffer is too small", nameof (buffer));
      bool flag = true;
      while (flag)
      {
        int count = source.Read(buffer, 0, buffer.Length);
        if (count > 0)
        {
          destination.Write(buffer, 0, count);
        }
        else
        {
          destination.Flush();
          flag = false;
        }
      }
    }

    /// <summary>
    /// Copy the contents of one <see cref="T:System.IO.Stream" /> to another.
    /// </summary>
    /// <param name="source">The stream to source data from.</param>
    /// <param name="destination">The stream to write data to.</param>
    /// <param name="buffer">The buffer to use during copying.</param>
    /// <param name="progressHandler">The <see cref="T:ICSharpCode.SharpZipLib.Core.ProgressHandler">progress handler delegate</see> to use.</param>
    /// <param name="updateInterval">The minimum <see cref="T:System.TimeSpan" /> between progress updates.</param>
    /// <param name="sender">The source for this event.</param>
    /// <param name="name">The name to use with the event.</param>
    /// <remarks>This form is specialised for use within #Zip to support events during archive operations.</remarks>
    public static void Copy(
      Stream source,
      Stream destination,
      byte[] buffer,
      ProgressHandler progressHandler,
      TimeSpan updateInterval,
      object sender,
      string name)
    {
      StreamUtils.Copy(source, destination, buffer, progressHandler, updateInterval, sender, name, -1L);
    }

    /// <summary>
    /// Copy the contents of one <see cref="T:System.IO.Stream" /> to another.
    /// </summary>
    /// <param name="source">The stream to source data from.</param>
    /// <param name="destination">The stream to write data to.</param>
    /// <param name="buffer">The buffer to use during copying.</param>
    /// <param name="progressHandler">The <see cref="T:ICSharpCode.SharpZipLib.Core.ProgressHandler">progress handler delegate</see> to use.</param>
    /// <param name="updateInterval">The minimum <see cref="T:System.TimeSpan" /> between progress updates.</param>
    /// <param name="sender">The source for this event.</param>
    /// <param name="name">The name to use with the event.</param>
    /// <param name="fixedTarget">A predetermined fixed target value to use with progress updates.
    /// If the value is negative the target is calculated by looking at the stream.</param>
    /// <remarks>This form is specialised for use within #Zip to support events during archive operations.</remarks>
    public static void Copy(
      Stream source,
      Stream destination,
      byte[] buffer,
      ProgressHandler progressHandler,
      TimeSpan updateInterval,
      object sender,
      string name,
      long fixedTarget)
    {
      if (source == null)
        throw new ArgumentNullException(nameof (source));
      if (destination == null)
        throw new ArgumentNullException(nameof (destination));
      if (buffer == null)
        throw new ArgumentNullException(nameof (buffer));
      if (buffer.Length < 128)
        throw new ArgumentException("Buffer is too small", nameof (buffer));
      if (progressHandler == null)
        throw new ArgumentNullException(nameof (progressHandler));
      bool flag1 = true;
      DateTime now = DateTime.Now;
      long processed = 0;
      long target = 0;
      if (fixedTarget >= 0L)
        target = fixedTarget;
      else if (source.CanSeek)
        target = source.Length - source.Position;
      ProgressEventArgs e1 = new ProgressEventArgs(name, processed, target);
      progressHandler(sender, e1);
      bool flag2 = true;
      while (flag1)
      {
        int count = source.Read(buffer, 0, buffer.Length);
        if (count > 0)
        {
          processed += (long) count;
          flag2 = false;
          destination.Write(buffer, 0, count);
        }
        else
        {
          destination.Flush();
          flag1 = false;
        }
        if (DateTime.Now - now > updateInterval)
        {
          flag2 = true;
          now = DateTime.Now;
          ProgressEventArgs e2 = new ProgressEventArgs(name, processed, target);
          progressHandler(sender, e2);
          flag1 = e2.ContinueRunning;
        }
      }
      if (flag2)
        return;
      ProgressEventArgs e3 = new ProgressEventArgs(name, processed, target);
      progressHandler(sender, e3);
    }

    /// <summary>
    /// Initialise an instance of <see cref="T:ICSharpCode.SharpZipLib.Core.StreamUtils"></see>
    /// </summary>
    private StreamUtils()
    {
    }
  }
}
