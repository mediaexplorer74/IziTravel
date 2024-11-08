// Decompiled with JetBrains decompiler
// Type: RestSharp.Compression.ZLib.ZlibException
// Assembly: RestSharp.WindowsPhone, Version=105.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EBEB4E14-2430-4192-BEF2-49D8C49DBCCE
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.xml

using System;

#nullable disable
namespace RestSharp.Compression.ZLib
{
  /// <summary>
  /// A general purpose exception class for exceptions in the Zlib library.
  /// </summary>
  internal class ZlibException : Exception
  {
    /// <summary>
    /// The ZlibException class captures exception information generated
    /// by the Zlib library.
    /// </summary>
    public ZlibException()
    {
    }

    /// <summary>This ctor collects a message attached to the exception.</summary>
    /// <param name="s"></param>
    public ZlibException(string s)
      : base(s)
    {
    }
  }
}
