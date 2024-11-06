// Decompiled with JetBrains decompiler
// Type: RestSharp.Http
// Assembly: RestSharp.WindowsPhone, Version=105.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EBEB4E14-2430-4192-BEF2-49D8C49DBCCE
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.xml

using RestSharp.Compression.ZLib;
using RestSharp.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

#nullable disable
namespace RestSharp
{
  /// <summary>HttpWebRequest wrapper (async methods)</summary>
  /// <summary>HttpWebRequest wrapper</summary>
  public class Http : IHttp, IHttpFactory
  {
    private const string LINE_BREAK = "\r\n";
    private const string FORM_BOUNDARY = "-----------------------------28947758029299";
    private Http.TimeOutState timeoutState;
    private Encoding encoding = Encoding.UTF8;
    private readonly IDictionary<string, Action<HttpWebRequest, string>> restrictedHeaderActions;

    public HttpWebRequest DeleteAsync(Action<HttpResponse> action)
    {
      return this.GetStyleMethodInternalAsync("DELETE", action);
    }

    public HttpWebRequest GetAsync(Action<HttpResponse> action)
    {
      return this.GetStyleMethodInternalAsync("GET", action);
    }

    public HttpWebRequest HeadAsync(Action<HttpResponse> action)
    {
      return this.GetStyleMethodInternalAsync("HEAD", action);
    }

    public HttpWebRequest OptionsAsync(Action<HttpResponse> action)
    {
      return this.GetStyleMethodInternalAsync("OPTIONS", action);
    }

    public HttpWebRequest PostAsync(Action<HttpResponse> action)
    {
      return this.PutPostInternalAsync("POST", action);
    }

    public HttpWebRequest PutAsync(Action<HttpResponse> action)
    {
      return this.PutPostInternalAsync("PUT", action);
    }

    public HttpWebRequest PatchAsync(Action<HttpResponse> action)
    {
      return this.PutPostInternalAsync("PATCH", action);
    }

    public HttpWebRequest MergeAsync(Action<HttpResponse> action)
    {
      return this.PutPostInternalAsync("MERGE", action);
    }

    /// <summary>
    /// Execute an async POST-style request with the specified HTTP Method.
    /// </summary>
    /// <param name="action"></param>
    /// <param name="httpMethod">The HTTP method to execute.</param>
    /// <returns></returns>
    public HttpWebRequest AsPostAsync(Action<HttpResponse> action, string httpMethod)
    {
      return this.PutPostInternalAsync(httpMethod.ToUpperInvariant(), action);
    }

    /// <summary>
    /// Execute an async GET-style request with the specified HTTP Method.
    /// </summary>
    /// <param name="action"></param>
    /// <param name="httpMethod">The HTTP method to execute.</param>
    /// <returns></returns>
    public HttpWebRequest AsGetAsync(Action<HttpResponse> action, string httpMethod)
    {
      return this.GetStyleMethodInternalAsync(httpMethod.ToUpperInvariant(), action);
    }

    private HttpWebRequest GetStyleMethodInternalAsync(string method, Action<HttpResponse> callback)
    {
      HttpWebRequest methodInternalAsync = (HttpWebRequest) null;
      try
      {
        Uri url = this.Url;
        methodInternalAsync = this.ConfigureAsyncWebRequest(method, url);
        if (this.HasBody && (method == "DELETE" || method == "OPTIONS"))
        {
          methodInternalAsync.ContentType = this.RequestContentType;
          this.WriteRequestBodyAsync(methodInternalAsync, callback);
        }
        else
        {
          this.timeoutState = new Http.TimeOutState()
          {
            Request = methodInternalAsync
          };
          this.SetTimeout(methodInternalAsync.BeginGetResponse((AsyncCallback) (result => this.ResponseCallback(result, callback)), (object) methodInternalAsync), this.timeoutState);
        }
      }
      catch (Exception ex)
      {
        Http.ExecuteCallback(this.CreateErrorResponse(ex), callback);
      }
      return methodInternalAsync;
    }

    private HttpResponse CreateErrorResponse(Exception ex)
    {
      HttpResponse errorResponse = new HttpResponse();
      if (ex is WebException webException && webException.Status == WebExceptionStatus.RequestCanceled)
      {
        errorResponse.ResponseStatus = this.timeoutState.TimedOut ? ResponseStatus.TimedOut : ResponseStatus.Aborted;
        return errorResponse;
      }
      errorResponse.ErrorMessage = ex.Message;
      errorResponse.ErrorException = ex;
      errorResponse.ResponseStatus = ResponseStatus.Error;
      return errorResponse;
    }

    private HttpWebRequest PutPostInternalAsync(string method, Action<HttpResponse> callback)
    {
      HttpWebRequest webRequest = (HttpWebRequest) null;
      try
      {
        webRequest = this.ConfigureAsyncWebRequest(method, this.Url);
        this.PreparePostBody(webRequest);
        this.WriteRequestBodyAsync(webRequest, callback);
      }
      catch (Exception ex)
      {
        Http.ExecuteCallback(this.CreateErrorResponse(ex), callback);
      }
      return webRequest;
    }

    private void WriteRequestBodyAsync(HttpWebRequest webRequest, Action<HttpResponse> callback)
    {
      this.timeoutState = new Http.TimeOutState()
      {
        Request = webRequest
      };
      this.SetTimeout(this.HasBody || this.HasFiles || this.AlwaysMultipartFormData ? webRequest.BeginGetRequestStream((AsyncCallback) (result => this.RequestStreamCallback(result, callback)), (object) webRequest) : webRequest.BeginGetResponse((AsyncCallback) (r => this.ResponseCallback(r, callback)), (object) webRequest), this.timeoutState);
    }

    private long CalculateContentLength()
    {
      if (this.RequestBodyBytes != null)
        return (long) this.RequestBodyBytes.Length;
      if (!this.HasFiles && !this.AlwaysMultipartFormData)
        return (long) this.encoding.GetByteCount(this.RequestBody);
      long seed = 0;
      foreach (HttpFile file in (IEnumerable<HttpFile>) this.Files)
      {
        seed += (long) this.Encoding.GetByteCount(Http.GetMultipartFileHeader(file));
        seed += file.ContentLength;
        seed += (long) this.Encoding.GetByteCount("\r\n");
      }
      return this.Parameters.Aggregate<HttpParameter, long>(seed, (Func<long, HttpParameter, long>) ((current, param) => current + (long) this.Encoding.GetByteCount(this.GetMultipartFormData(param)))) + (long) this.Encoding.GetByteCount(Http.GetMultipartFooter());
    }

    private void RequestStreamCallback(IAsyncResult result, Action<HttpResponse> callback)
    {
      HttpWebRequest asyncState = (HttpWebRequest) result.AsyncState;
      if (this.timeoutState.TimedOut)
      {
        Http.ExecuteCallback(new HttpResponse()
        {
          ResponseStatus = ResponseStatus.TimedOut
        }, callback);
      }
      else
      {
        try
        {
          using (Stream requestStream = asyncState.EndGetRequestStream(result))
          {
            if (this.HasFiles || this.AlwaysMultipartFormData)
              this.WriteMultipartFormData(requestStream);
            else if (this.RequestBodyBytes != null)
              requestStream.Write(this.RequestBodyBytes, 0, this.RequestBodyBytes.Length);
            else
              this.WriteStringTo(requestStream, this.RequestBody);
          }
        }
        catch (Exception ex)
        {
          Http.ExecuteCallback(this.CreateErrorResponse(ex), callback);
          return;
        }
        this.SetTimeout(asyncState.BeginGetResponse((AsyncCallback) (r => this.ResponseCallback(r, callback)), (object) asyncState), this.timeoutState);
      }
    }

    private void SetTimeout(IAsyncResult asyncResult, Http.TimeOutState timeOutState)
    {
    }

    private static void TimeoutCallback(object state, bool timedOut)
    {
      if (!timedOut || !(state is Http.TimeOutState timeOutState))
        return;
      lock (timeOutState)
        timeOutState.TimedOut = true;
      if (timeOutState.Request == null)
        return;
      timeOutState.Request.Abort();
    }

    private static void GetRawResponseAsync(IAsyncResult result, Action<HttpWebResponse> callback)
    {
      HttpWebResponse response;
      try
      {
        response = ((WebRequest) result.AsyncState).EndGetResponse(result) as HttpWebResponse;
      }
      catch (WebException ex)
      {
        if (ex.Status == WebExceptionStatus.RequestCanceled)
          throw;
        else if (ex.Response is HttpWebResponse)
          response = ex.Response as HttpWebResponse;
        else
          throw;
      }
      callback(response);
      response?.Close();
    }

    private void ResponseCallback(IAsyncResult result, Action<HttpResponse> callback)
    {
      HttpResponse response = new HttpResponse()
      {
        ResponseStatus = ResponseStatus.None
      };
      try
      {
        if (this.timeoutState.TimedOut)
        {
          response.ResponseStatus = ResponseStatus.TimedOut;
          Http.ExecuteCallback(response, callback);
        }
        else
          Http.GetRawResponseAsync(result, (Action<HttpWebResponse>) (webResponse =>
          {
            this.ExtractResponseData(response, webResponse);
            Http.ExecuteCallback(response, callback);
          }));
      }
      catch (Exception ex)
      {
        Http.ExecuteCallback(this.CreateErrorResponse(ex), callback);
      }
    }

    private static void ExecuteCallback(HttpResponse response, Action<HttpResponse> callback)
    {
      Http.PopulateErrorForIncompleteResponse(response);
      callback(response);
    }

    private static void PopulateErrorForIncompleteResponse(HttpResponse response)
    {
      if (response.ResponseStatus == ResponseStatus.Completed || response.ErrorException != null)
        return;
      response.ErrorException = (Exception) response.ResponseStatus.ToWebException();
      response.ErrorMessage = response.ErrorException.Message;
    }

    private void AddAsyncHeaderActions()
    {
      this.restrictedHeaderActions.Add("Content-Length", (Action<HttpWebRequest, string>) ((r, v) => { }));
    }

    private HttpWebRequest ConfigureAsyncWebRequest(string method, Uri url)
    {
      HttpWebRequest webRequest = (HttpWebRequest) WebRequest.Create(url);
      webRequest.UseDefaultCredentials = this.UseDefaultCredentials;
      this.AppendHeaders(webRequest);
      this.AppendCookies(webRequest);
      webRequest.Method = method;
      if (this.Credentials != null)
        webRequest.Credentials = this.Credentials;
      if (this.UserAgent.HasValue())
        webRequest.UserAgent = this.UserAgent;
      webRequest.AllowAutoRedirect = this.FollowRedirects;
      return webRequest;
    }

    /// <summary>Creates an IHttp</summary>
    /// <returns></returns>
    public IHttp Create() => (IHttp) new Http();

    /// <summary>True if this HTTP request has any HTTP parameters</summary>
    protected bool HasParameters => this.Parameters.Any<HttpParameter>();

    /// <summary>True if this HTTP request has any HTTP cookies</summary>
    protected bool HasCookies => this.Cookies.Any<HttpCookie>();

    /// <summary>True if a request body has been specified</summary>
    protected bool HasBody
    {
      get => this.RequestBodyBytes != null || !string.IsNullOrEmpty(this.RequestBody);
    }

    /// <summary>True if files have been set to be uploaded</summary>
    protected bool HasFiles => this.Files.Any<HttpFile>();

    /// <summary>
    /// Always send a multipart/form-data request - even when no Files are present.
    /// </summary>
    public bool AlwaysMultipartFormData { get; set; }

    /// <summary>UserAgent to be sent with request</summary>
    public string UserAgent { get; set; }

    /// <summary>Timeout in milliseconds to be used for the request</summary>
    public int Timeout { get; set; }

    /// <summary>
    /// The number of milliseconds before the writing or reading times out.
    /// </summary>
    public int ReadWriteTimeout { get; set; }

    /// <summary>System.Net.ICredentials to be sent with request</summary>
    public ICredentials Credentials { get; set; }

    /// <summary>
    /// The System.Net.CookieContainer to be used for the request
    /// </summary>
    public CookieContainer CookieContainer { get; set; }

    /// <summary>
    /// The method to use to write the response instead of reading into RawBytes
    /// </summary>
    public Action<Stream> ResponseWriter { get; set; }

    /// <summary>Collection of files to be sent with request</summary>
    public IList<HttpFile> Files { get; private set; }

    /// <summary>
    /// Whether or not HTTP 3xx response redirects should be automatically followed
    /// </summary>
    public bool FollowRedirects { get; set; }

    /// <summary>
    /// Determine whether or not the "default credentials" (e.g. the user account under which the current process is running)
    /// will be sent along to the server.
    /// </summary>
    public bool UseDefaultCredentials { get; set; }

    public Encoding Encoding
    {
      get => this.encoding;
      set => this.encoding = value;
    }

    /// <summary>HTTP headers to be sent with request</summary>
    public IList<HttpHeader> Headers { get; private set; }

    /// <summary>
    /// HTTP parameters (QueryString or Form values) to be sent with request
    /// </summary>
    public IList<HttpParameter> Parameters { get; private set; }

    /// <summary>HTTP cookies to be sent with request</summary>
    public IList<HttpCookie> Cookies { get; private set; }

    /// <summary>Request body to be sent with request</summary>
    public string RequestBody { get; set; }

    /// <summary>Content type of the request body.</summary>
    public string RequestContentType { get; set; }

    /// <summary>
    /// An alternative to RequestBody, for when the caller already has the byte array.
    /// </summary>
    public byte[] RequestBodyBytes { get; set; }

    /// <summary>URL to call for this request</summary>
    public Uri Url { get; set; }

    /// <summary>
    /// Flag to send authorisation header with the HttpWebRequest
    /// </summary>
    public bool PreAuthenticate { get; set; }

    /// <summary>Default constructor</summary>
    public Http()
    {
      this.Headers = (IList<HttpHeader>) new List<HttpHeader>();
      this.Files = (IList<HttpFile>) new List<HttpFile>();
      this.Parameters = (IList<HttpParameter>) new List<HttpParameter>();
      this.Cookies = (IList<HttpCookie>) new List<HttpCookie>();
      this.restrictedHeaderActions = (IDictionary<string, Action<HttpWebRequest, string>>) new Dictionary<string, Action<HttpWebRequest, string>>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
      this.AddSharedHeaderActions();
    }

    private void AddSharedHeaderActions()
    {
      this.restrictedHeaderActions.Add("Accept", (Action<HttpWebRequest, string>) ((r, v) => r.Accept = v));
      this.restrictedHeaderActions.Add("Content-Type", (Action<HttpWebRequest, string>) ((r, v) => r.ContentType = v));
      this.restrictedHeaderActions.Add("Date", (Action<HttpWebRequest, string>) ((r, v) => { }));
      this.restrictedHeaderActions.Add("Host", (Action<HttpWebRequest, string>) ((r, v) => { }));
    }

    private static string GetMultipartFormContentType()
    {
      return string.Format("multipart/form-data; boundary={0}", (object) "-----------------------------28947758029299");
    }

    private static string GetMultipartFileHeader(HttpFile file)
    {
      return string.Format("--{0}{4}Content-Disposition: form-data; name=\"{1}\"; filename=\"{2}\"{4}Content-Type: {3}{4}{4}", (object) "-----------------------------28947758029299", (object) file.Name, (object) file.FileName, (object) (file.ContentType ?? "application/octet-stream"), (object) "\r\n");
    }

    private string GetMultipartFormData(HttpParameter param)
    {
      return string.Format(param.Name == this.RequestContentType ? "--{0}{3}Content-Type: {1}{3}Content-Disposition: form-data; name=\"{1}\"{3}{3}{2}{3}" : "--{0}{3}Content-Disposition: form-data; name=\"{1}\"{3}{3}{2}{3}", (object) "-----------------------------28947758029299", (object) param.Name, (object) param.Value, (object) "\r\n");
    }

    private static string GetMultipartFooter()
    {
      return string.Format("--{0}--{1}", (object) "-----------------------------28947758029299", (object) "\r\n");
    }

    private void AppendHeaders(HttpWebRequest webRequest)
    {
      foreach (HttpHeader header in (IEnumerable<HttpHeader>) this.Headers)
      {
        if (this.restrictedHeaderActions.ContainsKey(header.Name))
          this.restrictedHeaderActions[header.Name](webRequest, header.Value);
        else
          webRequest.Headers[header.Name] = header.Value;
      }
    }

    private void AppendCookies(HttpWebRequest webRequest)
    {
      webRequest.CookieContainer = this.CookieContainer ?? new CookieContainer();
      foreach (HttpCookie cookie1 in (IEnumerable<HttpCookie>) this.Cookies)
      {
        Cookie cookie2 = new Cookie()
        {
          Name = cookie1.Name,
          Value = cookie1.Value
        };
        Uri requestUri = webRequest.RequestUri;
        webRequest.CookieContainer.Add(new Uri(string.Format("{0}://{1}", (object) requestUri.Scheme, (object) requestUri.Host)), cookie2);
      }
    }

    private string EncodeParameters()
    {
      StringBuilder stringBuilder = new StringBuilder();
      foreach (HttpParameter parameter in (IEnumerable<HttpParameter>) this.Parameters)
      {
        if (stringBuilder.Length > 1)
          stringBuilder.Append("&");
        stringBuilder.AppendFormat("{0}={1}", (object) parameter.Name.UrlEncode(), (object) parameter.Value.UrlEncode());
      }
      return stringBuilder.ToString();
    }

    private void PreparePostBody(HttpWebRequest webRequest)
    {
      if (this.HasFiles || this.AlwaysMultipartFormData)
        webRequest.ContentType = Http.GetMultipartFormContentType();
      else if (this.HasParameters)
      {
        webRequest.ContentType = "application/x-www-form-urlencoded";
        this.RequestBody = this.EncodeParameters();
      }
      else
      {
        if (!this.HasBody)
          return;
        webRequest.ContentType = this.RequestContentType;
      }
    }

    private void WriteStringTo(Stream stream, string toWrite)
    {
      byte[] bytes = this.Encoding.GetBytes(toWrite);
      stream.Write(bytes, 0, bytes.Length);
    }

    private void WriteMultipartFormData(Stream requestStream)
    {
      foreach (HttpParameter parameter in (IEnumerable<HttpParameter>) this.Parameters)
        this.WriteStringTo(requestStream, this.GetMultipartFormData(parameter));
      foreach (HttpFile file in (IEnumerable<HttpFile>) this.Files)
      {
        this.WriteStringTo(requestStream, Http.GetMultipartFileHeader(file));
        file.Writer(requestStream);
        this.WriteStringTo(requestStream, "\r\n");
      }
      this.WriteStringTo(requestStream, Http.GetMultipartFooter());
    }

    private void ExtractResponseData(HttpResponse response, HttpWebResponse webResponse)
    {
      using (webResponse)
      {
        response.ContentType = webResponse.ContentType;
        response.ContentLength = webResponse.ContentLength;
        Stream responseStream = webResponse.GetResponseStream();
        if (string.Equals(webResponse.Headers[HttpRequestHeader.ContentEncoding], "gzip", StringComparison.OrdinalIgnoreCase))
          this.ProcessResponseStream((Stream) new GZipStream(responseStream), response);
        else
          this.ProcessResponseStream(responseStream, response);
        response.StatusCode = webResponse.StatusCode;
        response.StatusDescription = webResponse.StatusDescription;
        response.ResponseUri = webResponse.ResponseUri;
        response.ResponseStatus = ResponseStatus.Completed;
        if (webResponse.Cookies != null)
        {
          foreach (Cookie cookie in webResponse.Cookies)
            response.Cookies.Add(new HttpCookie()
            {
              Comment = cookie.Comment,
              CommentUri = cookie.CommentUri,
              Discard = cookie.Discard,
              Domain = cookie.Domain,
              Expired = cookie.Expired,
              Expires = cookie.Expires,
              HttpOnly = cookie.HttpOnly,
              Name = cookie.Name,
              Path = cookie.Path,
              Port = cookie.Port,
              Secure = cookie.Secure,
              TimeStamp = cookie.TimeStamp,
              Value = cookie.Value,
              Version = cookie.Version
            });
        }
        foreach (string allKey in webResponse.Headers.AllKeys)
        {
          string header = webResponse.Headers[allKey];
          response.Headers.Add(new HttpHeader()
          {
            Name = allKey,
            Value = header
          });
        }
        webResponse.Close();
      }
    }

    private void ProcessResponseStream(Stream webResponseStream, HttpResponse response)
    {
      if (this.ResponseWriter == null)
        response.RawBytes = webResponseStream.ReadAsBytes();
      else
        this.ResponseWriter(webResponseStream);
    }

    private class TimeOutState
    {
      public bool TimedOut { get; set; }

      public HttpWebRequest Request { get; set; }
    }
  }
}
