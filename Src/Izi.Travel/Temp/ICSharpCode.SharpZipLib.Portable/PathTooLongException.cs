// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.PathTooLongException
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51802, Culture=neutral, PublicKeyToken=null
// MVID: 6C361F8B-A5CA-49B8-ABC1-A50EE493C003
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.xml

using System;

#nullable disable
namespace ICSharpCode.SharpZipLib
{
  /// <summary>Simulate System.IO.PathTooLongException</summary>
  public class PathTooLongException : Exception
  {
    /// <summary>Create a new exception</summary>
    public PathTooLongException()
      : base("Path too long")
    {
    }

    /// <summary>Create a new exception</summary>
    public PathTooLongException(string message)
      : base(message)
    {
    }

    /// <summary>Create a new exception</summary>
    public PathTooLongException(string message, Exception innerException)
      : base(message, innerException)
    {
    }
  }
}
