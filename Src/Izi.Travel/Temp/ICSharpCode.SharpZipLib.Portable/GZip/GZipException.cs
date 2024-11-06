// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.GZip.GZipException
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51802, Culture=neutral, PublicKeyToken=null
// MVID: 6C361F8B-A5CA-49B8-ABC1-A50EE493C003
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.xml

using System;

#nullable disable
namespace ICSharpCode.SharpZipLib.GZip
{
  /// <summary>GZipException represents a Gzip specific exception</summary>
  public class GZipException : SharpZipBaseException
  {
    /// <summary>Initialise a new instance of GZipException</summary>
    public GZipException()
    {
    }

    /// <summary>
    /// Initialise a new instance of GZipException with its message string.
    /// </summary>
    /// <param name="message">A <see cref="T:System.String" /> that describes the error.</param>
    public GZipException(string message)
      : base(message)
    {
    }

    /// <summary>
    /// Initialise a new instance of <see cref="T:ICSharpCode.SharpZipLib.GZip.GZipException"></see>.
    /// </summary>
    /// <param name="message">A <see cref="T:System.String" /> that describes the error.</param>
    /// <param name="innerException">The <see cref="T:System.Exception" /> that caused this exception.</param>
    public GZipException(string message, Exception innerException)
      : base(message, innerException)
    {
    }
  }
}
