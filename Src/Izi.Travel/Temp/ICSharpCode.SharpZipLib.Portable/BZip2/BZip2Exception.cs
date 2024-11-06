// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.BZip2.BZip2Exception
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51802, Culture=neutral, PublicKeyToken=null
// MVID: 6C361F8B-A5CA-49B8-ABC1-A50EE493C003
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.xml

using System;

#nullable disable
namespace ICSharpCode.SharpZipLib.BZip2
{
  /// <summary>
  /// BZip2Exception represents exceptions specific to Bzip2 algorithm
  /// </summary>
  public class BZip2Exception : SharpZipBaseException
  {
    /// <summary>Initialise a new instance of BZip2Exception.</summary>
    public BZip2Exception()
    {
    }

    /// <summary>
    /// Initialise a new instance of BZip2Exception with its message set to message.
    /// </summary>
    /// <param name="message">The message describing the error.</param>
    public BZip2Exception(string message)
      : base(message)
    {
    }

    /// <summary>Initialise an instance of BZip2Exception</summary>
    /// <param name="message">A message describing the error.</param>
    /// <param name="exception">The exception that is the cause of the current exception.</param>
    public BZip2Exception(string message, Exception exception)
      : base(message, exception)
    {
    }
  }
}
