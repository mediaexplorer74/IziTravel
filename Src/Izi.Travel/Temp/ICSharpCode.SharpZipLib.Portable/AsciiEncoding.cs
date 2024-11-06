// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.AsciiEncoding
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51802, Culture=neutral, PublicKeyToken=null
// MVID: 6C361F8B-A5CA-49B8-ABC1-A50EE493C003
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.xml

using System.Text;

#nullable disable
namespace ICSharpCode.SharpZipLib
{
  /// <summary>Implements a simple ASCII encoding</summary>
  public class AsciiEncoding : Encoding
  {
    /// <summary>Default encoding</summary>
    public static readonly AsciiEncoding Default = new AsciiEncoding();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="chars"></param>
    /// <param name="index"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    public override int GetByteCount(char[] chars, int index, int count) => count;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="chars"></param>
    /// <param name="charIndex"></param>
    /// <param name="charCount"></param>
    /// <param name="bytes"></param>
    /// <param name="byteIndex"></param>
    /// <returns></returns>
    public override int GetBytes(
      char[] chars,
      int charIndex,
      int charCount,
      byte[] bytes,
      int byteIndex)
    {
      for (int index = 0; index < charCount; ++index)
        bytes[byteIndex + index] = (byte) chars[charIndex + index];
      return charCount;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="bytes"></param>
    /// <param name="index"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    public override int GetCharCount(byte[] bytes, int index, int count) => count;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="bytes"></param>
    /// <param name="byteIndex"></param>
    /// <param name="byteCount"></param>
    /// <param name="chars"></param>
    /// <param name="charIndex"></param>
    /// <returns></returns>
    public override int GetChars(
      byte[] bytes,
      int byteIndex,
      int byteCount,
      char[] chars,
      int charIndex)
    {
      for (int index = 0; index < byteCount; ++index)
        chars[charIndex + index] = (char) bytes[byteIndex + index];
      return byteCount;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="charCount"></param>
    /// <returns></returns>
    public override int GetMaxByteCount(int charCount) => charCount;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="byteCount"></param>
    /// <returns></returns>
    public override int GetMaxCharCount(int byteCount) => byteCount;
  }
}
