// Decompiled with JetBrains decompiler
// Type: RestSharp.RestResponseBase
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
  /// <summary>
  /// Base class for common properties shared by RestResponse and RestResponse[[T]]
  /// </summary>
  public abstract class RestResponseBase
  {
    private string _content;
    private ResponseStatus _responseStatus;

    /// <summary>Default constructor</summary>
    public RestResponseBase()
    {
      this.Headers = (IList<Parameter>) new List<Parameter>();
      this.Cookies = (IList<RestResponseCookie>) new List<RestResponseCookie>();
    }

    /// <summary>The RestRequest that was made to get this RestResponse</summary>
    /// <remarks>Mainly for debugging if ResponseStatus is not OK</remarks>
    public IRestRequest Request { get; set; }

    /// <summary>MIME content type of response</summary>
    public string ContentType { get; set; }

    /// <summary>Length in bytes of the response content</summary>
    public long ContentLength { get; set; }

    /// <summary>Encoding of the response content</summary>
    public string ContentEncoding { get; set; }

    /// <summary>String representation of response content</summary>
    public string Content
    {
      get
      {
        if (this._content == null)
          this._content = this.RawBytes.AsString();
        return this._content;
      }
      set => this._content = value;
    }

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

    /// <summary>Cookies returned by server with the response</summary>
    public IList<RestResponseCookie> Cookies { get; protected internal set; }

    /// <summary>Headers returned by server with the response</summary>
    public IList<Parameter> Headers { get; protected internal set; }

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

    /// <summary>The exception thrown during the request, if any</summary>
    public Exception ErrorException { get; set; }
  }
}
