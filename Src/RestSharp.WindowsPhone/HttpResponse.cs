// Decompiled with JetBrains decompiler
// Type: RestSharp.HttpResponse
// Assembly: RestSharp.WindowsPhone, Version=105.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EBEB4E14-2430-4192-BEF2-49D8C49DBCCE
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.xml

using RestSharp.Extensions;
using System;
using System.Collections.Generic;
using System.Net;

#nullable disable
namespace RestSharp
{
  /// <summary>HTTP response data</summary>
  public class HttpResponse : IHttpResponse
  {
    private string _content;
    private ResponseStatus _responseStatus;

    /// <summary>Default constructor</summary>
    public HttpResponse()
    {
      this.Headers = (IList<HttpHeader>) new List<HttpHeader>();
      this.Cookies = (IList<HttpCookie>) new List<HttpCookie>();
    }

    /// <summary>MIME content type of response</summary>
    public string ContentType { get; set; }

    /// <summary>Length in bytes of the response content</summary>
    public long ContentLength { get; set; }

    /// <summary>Encoding of the response content</summary>
    public string ContentEncoding { get; set; }

    /// <summary>Lazy-loaded string representation of response content</summary>
    public string Content => this._content ?? (this._content = this.RawBytes.AsString());

    /// <summary>HTTP response status code</summary>
    public HttpStatusCode StatusCode { get; set; }

    /// <summary>Description of HTTP status returned</summary>
    public string StatusDescription { get; set; }

    /// <summary>Response content</summary>
    public byte[] RawBytes { get; set; }

    /// <summary>
    /// The URL that actually responded to the content (different from request if redirected)
    /// </summary>
    public Uri ResponseUri { get; set; }

    /// <summary>HttpWebResponse.Server</summary>
    public string Server { get; set; }

    /// <summary>Headers returned by server with the response</summary>
    public IList<HttpHeader> Headers { get; private set; }

    /// <summary>Cookies returned by server with the response</summary>
    public IList<HttpCookie> Cookies { get; private set; }

    /// <summary>
    /// Status of the request. Will return Error for transport errors.
    /// HTTP errors will still return ResponseStatus.Completed, check StatusCode instead
    /// </summary>
    public ResponseStatus ResponseStatus
    {
      get => this._responseStatus;
      set => this._responseStatus = value;
    }

    /// <summary>
    /// Transport or other non-HTTP error generated while attempting request
    /// </summary>
    public string ErrorMessage { get; set; }

    /// <summary>Exception thrown when error is encountered.</summary>
    public Exception ErrorException { get; set; }
  }
}
