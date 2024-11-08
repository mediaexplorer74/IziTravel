// Decompiled with JetBrains decompiler
// Type: RestSharp.Extensions.ResponseStatusExtensions
// Assembly: RestSharp.WindowsPhone, Version=105.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EBEB4E14-2430-4192-BEF2-49D8C49DBCCE
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.xml

using System;
using System.Net;

#nullable disable
namespace RestSharp.Extensions
{
  public static class ResponseStatusExtensions
  {
    /// <summary>
    /// Convert a <see cref="T:RestSharp.ResponseStatus" /> to a <see cref="T:System.Net.WebException" /> instance.
    /// </summary>
    /// <param name="responseStatus">The response status.</param>
    /// <returns></returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">responseStatus</exception>
    public static WebException ToWebException(this ResponseStatus responseStatus)
    {
      switch (responseStatus)
      {
        case ResponseStatus.None:
          return new WebException("The request could not be processed.", WebExceptionStatus.ServerProtocolViolation);
        case ResponseStatus.Error:
          return new WebException("An error occurred while processing the request.", WebExceptionStatus.ServerProtocolViolation);
        case ResponseStatus.TimedOut:
          return new WebException("The request timed-out.", WebExceptionStatus.Timeout);
        case ResponseStatus.Aborted:
          return new WebException("The request was aborted.", WebExceptionStatus.Timeout);
        default:
          throw new ArgumentOutOfRangeException(nameof (responseStatus));
      }
    }
  }
}
