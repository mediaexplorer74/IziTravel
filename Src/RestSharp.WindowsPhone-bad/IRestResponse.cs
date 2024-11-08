// Decompiled with JetBrains decompiler
// Type: RestSharp.IRestResponse
// Assembly: RestSharp.WindowsPhone, Version=105.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EBEB4E14-2430-4192-BEF2-49D8C49DBCCE
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.xml

using System;
using System.Collections.Generic;
using System.Net;

#nullable disable
namespace RestSharp
{
  /// <summary>Container for data sent back from API</summary>
  public interface IRestResponse
  {
    /// <summary>The RestRequest that was made to get this RestResponse</summary>
    /// <remarks>Mainly for debugging if ResponseStatus is not OK</remarks>
    IRestRequest Request { get; set; }

    /// <summary>MIME content type of response</summary>
    string ContentType { get; set; }

    /// <summary>Length in bytes of the response content</summary>
    long ContentLength { get; set; }

    /// <summary>Encoding of the response content</summary>
    string ContentEncoding { get; set; }

    /// <summary>String representation of response content</summary>
    string Content { get; set; }

    /// <summary>HTTP response status code</summary>
    HttpStatusCode StatusCode { get; set; }

    /// <summary>Description of HTTP status returned</summary>
    string StatusDescription { get; set; }

    /// <summary>Response content</summary>
    byte[] RawBytes { get; set; }

    /// <summary>
    /// The URL that actually responded to the content (different from request if redirected)
    /// </summary>
    Uri ResponseUri { get; set; }

    /// <summary>HttpWebResponse.Server</summary>
    string Server { get; set; }

    /// <summary>Cookies returned by server with the response</summary>
    IList<RestResponseCookie> Cookies { get; }

    /// <summary>Headers returned by server with the response</summary>
    IList<Parameter> Headers { get; }

    /// <summary>
    /// Status of the request. Will return Error for transport errors.
    /// HTTP errors will still return ResponseStatus.Completed, check StatusCode instead
    /// </summary>
    ResponseStatus ResponseStatus { get; set; }

    /// <summary>
    /// Transport or other non-HTTP error generated while attempting request
    /// </summary>
    string ErrorMessage { get; set; }

    /// <summary>Exceptions thrown during the request, if any.</summary>
    /// <remarks>Will contain only network transport or framework exceptions thrown during the request.
    /// HTTP protocol errors are handled by RestSharp and will not appear here.</remarks>
    Exception ErrorException { get; set; }
  }
}
