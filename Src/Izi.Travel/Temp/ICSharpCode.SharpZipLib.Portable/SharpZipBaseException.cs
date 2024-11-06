// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.SharpZipBaseException
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51802, Culture=neutral, PublicKeyToken=null
// MVID: 6C361F8B-A5CA-49B8-ABC1-A50EE493C003
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.xml

using System;

#nullable disable
namespace ICSharpCode.SharpZipLib
{
  /// <summary>
  /// SharpZipBaseException is the base exception class for the SharpZipLibrary.
  /// All library exceptions are derived from this.
  /// </summary>
  /// <remarks>NOTE: Not all exceptions thrown will be derived from this class.
  /// A variety of other exceptions are possible for example <see cref="T:System.ArgumentNullException"></see></remarks>
  public class SharpZipBaseException : Exception
  {
    /// <summary>
    /// Initializes a new instance of the SharpZipBaseException class.
    /// </summary>
    public SharpZipBaseException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the SharpZipBaseException class with a specified error message.
    /// </summary>
    /// <param name="message">A message describing the exception.</param>
    public SharpZipBaseException(string message)
      : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the SharpZipBaseException class with a specified
    /// error message and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">A message describing the exception.</param>
    /// <param name="innerException">The inner exception</param>
    public SharpZipBaseException(string message, Exception innerException)
      : base(message, innerException)
    {
    }
  }
}
