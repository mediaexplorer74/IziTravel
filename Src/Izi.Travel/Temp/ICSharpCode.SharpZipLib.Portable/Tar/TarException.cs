// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Tar.TarException
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51802, Culture=neutral, PublicKeyToken=null
// MVID: 6C361F8B-A5CA-49B8-ABC1-A50EE493C003
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.xml

using System;

#nullable disable
namespace ICSharpCode.SharpZipLib.Tar
{
  /// <summary>
  /// TarExceptions are used for exceptions specific to tar classes and code.
  /// </summary>
  public class TarException : SharpZipBaseException
  {
    /// <summary>Initialises a new instance of the TarException class.</summary>
    public TarException()
    {
    }

    /// <summary>
    /// Initialises a new instance of the TarException class with a specified message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public TarException(string message)
      : base(message)
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="message">A message describing the error.</param>
    /// <param name="exception">The exception that is the cause of the current exception.</param>
    public TarException(string message, Exception exception)
      : base(message, exception)
    {
    }
  }
}
