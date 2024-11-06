// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.BZip2.BZip2
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51802, Culture=neutral, PublicKeyToken=null
// MVID: 6C361F8B-A5CA-49B8-ABC1-A50EE493C003
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.xml

using ICSharpCode.SharpZipLib.Core;
using System;
using System.IO;

#nullable disable
namespace ICSharpCode.SharpZipLib.BZip2
{
  /// <summary>
  /// An example class to demonstrate compression and decompression of BZip2 streams.
  /// </summary>
  public static class BZip2
  {
    /// <summary>
    /// Decompress the <paramref name="inStream">input</paramref> writing
    /// uncompressed data to the <paramref name="outStream">output stream</paramref>
    /// </summary>
    /// <param name="inStream">The readable stream containing data to decompress.</param>
    /// <param name="outStream">The output stream to receive the decompressed data.</param>
    /// <param name="isStreamOwner">Both streams are closed on completion if true.</param>
    public static void Decompress(Stream inStream, Stream outStream, bool isStreamOwner)
    {
      if (inStream != null)
      {
        if (outStream != null)
        {
          try
          {
            using (BZip2InputStream source = new BZip2InputStream(inStream))
            {
              source.IsStreamOwner = isStreamOwner;
              StreamUtils.Copy((Stream) source, outStream, new byte[4096]);
              return;
            }
          }
          finally
          {
            if (isStreamOwner)
              outStream.Dispose();
          }
        }
      }
      throw new Exception("Null Stream");
    }

    /// <summary>
    /// Compress the <paramref name="inStream">input stream</paramref> sending
    /// result data to <paramref name="outStream">output stream</paramref>
    /// </summary>
    /// <param name="inStream">The readable stream to compress.</param>
    /// <param name="outStream">The output stream to receive the compressed data.</param>
    /// <param name="isStreamOwner">Both streams are closed on completion if true.</param>
    /// <param name="level">Block size acts as compression level (1 to 9) with 1 giving
    /// the lowest compression and 9 the highest.</param>
    public static void Compress(Stream inStream, Stream outStream, bool isStreamOwner, int level)
    {
      if (inStream != null)
      {
        if (outStream != null)
        {
          try
          {
            using (BZip2OutputStream destination = new BZip2OutputStream(outStream, level))
            {
              destination.IsStreamOwner = isStreamOwner;
              StreamUtils.Copy(inStream, (Stream) destination, new byte[4096]);
              return;
            }
          }
          finally
          {
            if (isStreamOwner)
              inStream.Dispose();
          }
        }
      }
      throw new Exception("Null Stream");
    }
  }
}
