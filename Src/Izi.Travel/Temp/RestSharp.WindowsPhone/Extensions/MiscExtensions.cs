// Decompiled with JetBrains decompiler
// Type: RestSharp.Extensions.MiscExtensions
// Assembly: RestSharp.WindowsPhone, Version=105.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EBEB4E14-2430-4192-BEF2-49D8C49DBCCE
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.xml

using System.IO;
using System.Text;

#nullable disable
namespace RestSharp.Extensions
{
  /// <summary>Extension method overload!</summary>
  public static class MiscExtensions
  {
    /// <summary>Read a stream into a byte array</summary>
    /// <param name="input">Stream to read</param>
    /// <returns>byte[]</returns>
    public static byte[] ReadAsBytes(this Stream input)
    {
      byte[] buffer = new byte[16384];
      using (MemoryStream memoryStream = new MemoryStream())
      {
        int count;
        while ((count = input.Read(buffer, 0, buffer.Length)) > 0)
          memoryStream.Write(buffer, 0, count);
        return memoryStream.ToArray();
      }
    }

    /// <summary>Copies bytes from one stream to another</summary>
    /// <param name="input">The input stream.</param>
    /// <param name="output">The output stream.</param>
    public static void CopyTo(this Stream input, Stream output)
    {
      byte[] buffer = new byte[32768];
      while (true)
      {
        int count = input.Read(buffer, 0, buffer.Length);
        if (count > 0)
          output.Write(buffer, 0, count);
        else
          break;
      }
    }

    /// <summary>
    /// Converts a byte array to a string, using its byte order mark to convert it to the right encoding.
    /// http://www.shrinkrays.net/code-snippets/csharp/an-extension-method-for-converting-a-byte-array-to-a-string.aspx
    /// </summary>
    /// <param name="buffer">An array of bytes to convert</param>
    /// <returns>The byte as a string.</returns>
    public static string AsString(this byte[] buffer)
    {
      if (buffer == null)
        return "";
      Encoding encoding = Encoding.UTF8;
      if (buffer == null || buffer.Length == 0)
        return "";
      if (buffer[0] == (byte) 239 && buffer[1] == (byte) 187 && buffer[2] == (byte) 191)
        encoding = Encoding.UTF8;
      else if (buffer[0] == (byte) 254 && buffer[1] == byte.MaxValue)
        encoding = Encoding.Unicode;
      else if (buffer[0] == (byte) 254 && buffer[1] == byte.MaxValue)
        encoding = Encoding.BigEndianUnicode;
      using (MemoryStream memoryStream = new MemoryStream())
      {
        memoryStream.Write(buffer, 0, buffer.Length);
        memoryStream.Seek(0L, SeekOrigin.Begin);
        using (StreamReader streamReader = new StreamReader((Stream) memoryStream, encoding))
          return streamReader.ReadToEnd();
      }
    }
  }
}
