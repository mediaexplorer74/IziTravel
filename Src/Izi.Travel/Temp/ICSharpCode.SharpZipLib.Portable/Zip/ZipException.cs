// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.ZipException
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51802, Culture=neutral, PublicKeyToken=null
// MVID: 6C361F8B-A5CA-49B8-ABC1-A50EE493C003
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.xml

using System;

#nullable disable
namespace ICSharpCode.SharpZipLib.Zip
{
  /// <summary>
  /// Represents exception conditions specific to Zip archive handling
  /// </summary>
  public class ZipException : SharpZipBaseException
  {
    /// <summary>Initializes a new instance of the ZipException class.</summary>
    public ZipException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the ZipException class with a specified error message.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    public ZipException(string message)
      : base(message)
    {
    }

    /// <summary>Initialise a new instance of ZipException.</summary>
    /// <param name="message">A message describing the error.</param>
    /// <param name="exception">The exception that is the cause of the current exception.</param>
    public ZipException(string message, Exception exception)
      : base(message, exception)
    {
    }
  }
}
