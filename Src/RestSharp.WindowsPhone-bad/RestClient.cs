// Decompiled with JetBrains decompiler
// Type: RestSharp.RestClient
// Assembly: RestSharp.WindowsPhone, Version=105.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EBEB4E14-2430-4192-BEF2-49D8C49DBCCE
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.xml

using RestSharp.Deserializers;
using RestSharp.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

#nullable disable
namespace RestSharp
{
  /// <summary>
  /// Client to translate RestRequests into Http requests and process response result
  /// </summary>
  public class RestClient : IRestClient
  {
    //private static readonly Version version = new AssemblyName(Assembly.GetExecutingAssembly().FullName).Version;
    public IHttpFactory HttpFactory = (IHttpFactory) new SimpleFactory<Http>();
    private Encoding encoding = Encoding.UTF8;

    /// <summary>
    /// Executes the request and callback asynchronously, authenticating if needed
    /// </summary>
    /// <param name="request">Request to be executed</param>
    /// <param name="callback">Callback function to be executed upon completion providing access to the async handle.</param>
    public virtual RestRequestAsyncHandle ExecuteAsync(
      IRestRequest request,
      Action<IRestResponse, RestRequestAsyncHandle> callback)
    {
      string name = Enum.GetName(typeof (Method), (object) request.Method);
      switch (request.Method)
      {
        case Method.POST:
        case Method.PUT:
        case Method.PATCH:
        case Method.MERGE:
          return this.ExecuteAsync(request, callback, name, new Func<IHttp, Action<HttpResponse>, 
              string, HttpWebRequest>(RestClient.DoAsPostAsync));
        default:
          return this.ExecuteAsync(request, callback, name, new Func<IHttp, Action<HttpResponse>, 
              string, HttpWebRequest>(RestClient.DoAsGetAsync));
      }
    }

    /// <summary>
    /// Executes a GET-style request and callback asynchronously, authenticating if needed
    /// </summary>
    /// <param name="request">Request to be executed</param>
    /// <param name="callback">Callback function to be executed upon completion providing access to the async handle.</param>
    /// <param name="httpMethod">The HTTP method to execute</param>
    public virtual RestRequestAsyncHandle ExecuteAsyncGet(
      IRestRequest request,
      Action<IRestResponse, RestRequestAsyncHandle> callback,
      string httpMethod)
    {
      return this.ExecuteAsync(request, callback, httpMethod, new Func<IHttp, Action<HttpResponse>, string, HttpWebRequest>(RestClient.DoAsGetAsync));
    }

    /// <summary>
    /// Executes a POST-style request and callback asynchronously, authenticating if needed
    /// </summary>
    /// <param name="request">Request to be executed</param>
    /// <param name="callback">Callback function to be executed upon completion providing access to the async handle.</param>
    /// <param name="httpMethod">The HTTP method to execute</param>
    public virtual RestRequestAsyncHandle ExecuteAsyncPost(
      IRestRequest request,
      Action<IRestResponse, RestRequestAsyncHandle> callback,
      string httpMethod)
    {
      request.Method = Method.POST;
      return this.ExecuteAsync(request, callback, httpMethod, new Func<IHttp, Action<HttpResponse>, 
          string, HttpWebRequest>(RestClient.DoAsPostAsync));
    }

    // RnD / TODO
    private RestRequestAsyncHandle ExecuteAsync(
      IRestRequest request,
      Action<IRestResponse, RestRequestAsyncHandle> callback,
      string httpMethod,
      Func<IHttp, Action<HttpResponse>, string, HttpWebRequest> getWebRequest)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      RestClient.c__DisplayClass3 cDisplayClass3_1 = 
                new RestClient.c__DisplayClass3();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass3_1.request = request;
      // ISSUE: reference to a compiler-generated field
      cDisplayClass3_1.callback = callback;
      // ISSUE: reference to a compiler-generated field
      //cDisplayClass3_1.this = this;



      IHttp http = this.HttpFactory.Create();

      // ISSUE: reference to a compiler-generated field
      this.AuthenticateIfNeeded(this, cDisplayClass3_1.request);

      // ISSUE: reference to a compiler-generated field
      this.ConfigureHttp(cDisplayClass3_1.request, http);

      // ISSUE: reference to a compiler-generated field
      cDisplayClass3_1.asyncHandle = new RestRequestAsyncHandle();

      // ISSUE: reference to a compiler-generated method
      Action<HttpResponse> action = new Action<HttpResponse>(
          cDisplayClass3_1.AHttpResponce);

      if (this.UseSynchronizationContext && SynchronizationContext.Current != null)
      {
        SynchronizationContext ctx = SynchronizationContext.Current;
        Action<HttpResponse> cb = action;
        action = (Action<HttpResponse>) (resp =>
        {
          // ISSUE: variable of a compiler-generated type
          RestClient.c__DisplayClass3 cDisplayClass3 = cDisplayClass3_1;
          HttpResponse resp1 = resp;
          ctx.Post((SendOrPostCallback) (s => cb(resp1)), (object) null);
        });
      }
      // ISSUE: reference to a compiler-generated field
      cDisplayClass3_1.asyncHandle.WebRequest = getWebRequest(http, action, httpMethod);
      // ISSUE: reference to a compiler-generated field
      return cDisplayClass3_1.asyncHandle;
    }

    private static HttpWebRequest DoAsGetAsync(
      IHttp http,
      Action<HttpResponse> response_cb,
      string method)
    {
      return http.AsGetAsync(response_cb, method);
    }

    private static HttpWebRequest DoAsPostAsync(
      IHttp http,
      Action<HttpResponse> response_cb,
      string method)
    {
      return http.AsPostAsync(response_cb, method);
    }

    private void ProcessResponse(
      IRestRequest request,
      HttpResponse httpResponse,
      RestRequestAsyncHandle asyncHandle,
      Action<IRestResponse, RestRequestAsyncHandle> callback)
    {
      RestResponse restResponse = RestClient.ConvertToRestResponse(request, httpResponse);
      callback((IRestResponse) restResponse, asyncHandle);
    }

    /// <summary>
    /// Executes the request and callback asynchronously, authenticating if needed
    /// </summary>
    /// <typeparam name="T">Target deserialization type</typeparam>
    /// <param name="request">Request to be executed</param>
    /// <param name="callback">Callback function to be executed upon completion</param>
    public virtual RestRequestAsyncHandle ExecuteAsync<T>(
      IRestRequest request,
      Action<IRestResponse<T>, RestRequestAsyncHandle> callback)
    {
      return this.ExecuteAsync(request, (Action<IRestResponse, RestRequestAsyncHandle>)
          ((response, asyncHandle) => this.DeserializeResponse<T>(request, callback, response, asyncHandle)));
    }

    /// <summary>
    /// Executes a GET-style request and callback asynchronously, authenticating if needed
    /// </summary>
    /// <typeparam name="T">Target deserialization type</typeparam>
    /// <param name="request">Request to be executed</param>
    /// <param name="callback">Callback function to be executed upon completion</param>
    /// <param name="httpMethod">The HTTP method to execute</param>
    public virtual RestRequestAsyncHandle ExecuteAsyncGet<T>(
      IRestRequest request,
      Action<IRestResponse<T>, RestRequestAsyncHandle> callback,
      string httpMethod)
    {
      return this.ExecuteAsyncGet(request, (Action<IRestResponse, RestRequestAsyncHandle>) 
          ((response, asyncHandle) => this.DeserializeResponse<T>(request, callback, response, asyncHandle)), httpMethod);
    }

    /// <summary>
    /// Executes a POST-style request and callback asynchronously, authenticating if needed
    /// </summary>
    /// <typeparam name="T">Target deserialization type</typeparam>
    /// <param name="request">Request to be executed</param>
    /// <param name="callback">Callback function to be executed upon completion</param>
    /// <param name="httpMethod">The HTTP method to execute</param>
    public virtual RestRequestAsyncHandle ExecuteAsyncPost<T>(
      IRestRequest request,
      Action<IRestResponse<T>, RestRequestAsyncHandle> callback,
      string httpMethod)
    {
      return this.ExecuteAsyncPost(request, (Action<IRestResponse, RestRequestAsyncHandle>)
          ((response, asyncHandle) => this.DeserializeResponse<T>(request, callback, response, asyncHandle)), httpMethod);
    }

    private void DeserializeResponse<T>(
      IRestRequest request,
      Action<IRestResponse<T>, RestRequestAsyncHandle> callback,
      IRestResponse response,
      RestRequestAsyncHandle asyncHandle)
    {
      IRestResponse<T> restResponse1;
      try
      {
        restResponse1 = this.Deserialize<T>(request, response);
      }
      catch (Exception ex)
      {
        RestResponse<T> restResponse2 = new RestResponse<T>();
        restResponse2.Request = request;
        restResponse2.ResponseStatus = ResponseStatus.Error;
        restResponse2.ErrorMessage = ex.Message;
        restResponse2.ErrorException = ex;
        restResponse1 = (IRestResponse<T>) restResponse2;
      }
      callback(restResponse1, asyncHandle);
    }

    /// <summary>
    /// Executes a GET-style request asynchronously, authenticating if needed
    /// </summary>
    /// <typeparam name="T">Target deserialization type</typeparam>
    /// <param name="request">Request to be executed</param>
    public virtual Task<IRestResponse<T>> ExecuteGetTaskAsync<T>(IRestRequest request)
    {
      return this.ExecuteGetTaskAsync<T>(request, CancellationToken.None);
    }

    /// <summary>
    /// Executes a GET-style request asynchronously, authenticating if needed
    /// </summary>
    /// <typeparam name="T">Target deserialization type</typeparam>
    /// <param name="request">Request to be executed</param>
    /// <param name="token">The cancellation token</param>
    public virtual Task<IRestResponse<T>> ExecuteGetTaskAsync<T>(
      IRestRequest request,
      CancellationToken token)
    {
      if (request == null)
        throw new ArgumentNullException(nameof (request));
      request.Method = Method.GET;
      return this.ExecuteTaskAsync<T>(request, token);
    }

    /// <summary>
    /// Executes a POST-style request asynchronously, authenticating if needed
    /// </summary>
    /// <typeparam name="T">Target deserialization type</typeparam>
    /// <param name="request">Request to be executed</param>
    public virtual Task<IRestResponse<T>> ExecutePostTaskAsync<T>(IRestRequest request)
    {
      return this.ExecutePostTaskAsync<T>(request, CancellationToken.None);
    }

    /// <summary>
    /// Executes a POST-style request asynchronously, authenticating if needed
    /// </summary>
    /// <typeparam name="T">Target deserialization type</typeparam>
    /// <param name="request">Request to be executed</param>
    /// <param name="token">The cancellation token</param>
    public virtual Task<IRestResponse<T>> ExecutePostTaskAsync<T>(
      IRestRequest request,
      CancellationToken token)
    {
      if (request == null)
        throw new ArgumentNullException(nameof (request));
      request.Method = Method.POST;
      return this.ExecuteTaskAsync<T>(request, token);
    }

    /// <summary>
    /// Executes the request asynchronously, authenticating if needed
    /// </summary>
    /// <typeparam name="T">Target deserialization type</typeparam>
    /// <param name="request">Request to be executed</param>
    public virtual Task<IRestResponse<T>> ExecuteTaskAsync<T>(IRestRequest request)
    {
      return this.ExecuteTaskAsync<T>(request, CancellationToken.None);
    }

    /// <summary>
    /// Executes the request asynchronously, authenticating if needed
    /// </summary>
    /// <typeparam name="T">Target deserialization type</typeparam>
    /// <param name="request">Request to be executed</param>
    /// <param name="token">The cancellation token</param>
    public virtual Task<IRestResponse<T>> ExecuteTaskAsync<T>(
      IRestRequest request,
      CancellationToken token)
    {
      if (request == null)
        throw new ArgumentNullException(nameof (request));
      TaskCompletionSource<IRestResponse<T>> taskCompletionSource = new TaskCompletionSource<IRestResponse<T>>();
      try
      {
        RestRequestAsyncHandle async = this.ExecuteAsync<T>(request, (Action<IRestResponse<T>, RestRequestAsyncHandle>)
            ((response, _) =>
        {
          if (token.IsCancellationRequested)
            taskCompletionSource.TrySetCanceled();
          else
            taskCompletionSource.TrySetResult(response);
        }));
        token.Register((Action) (() =>
        {
          async.Abort();
          taskCompletionSource.TrySetCanceled();
        }));
      }
      catch (Exception ex)
      {
        taskCompletionSource.TrySetException(ex);
      }
      return taskCompletionSource.Task;
    }

    /// <summary>
    /// Executes the request asynchronously, authenticating if needed
    /// </summary>
    /// <param name="request">Request to be executed</param>
    public virtual Task<IRestResponse> ExecuteTaskAsync(IRestRequest request)
    {
      return this.ExecuteTaskAsync(request, CancellationToken.None);
    }

    /// <summary>
    /// Executes a GET-style asynchronously, authenticating if needed
    /// </summary>
    /// <param name="request">Request to be executed</param>
    public virtual Task<IRestResponse> ExecuteGetTaskAsync(IRestRequest request)
    {
      return this.ExecuteGetTaskAsync(request, CancellationToken.None);
    }

    /// <summary>
    /// Executes a GET-style asynchronously, authenticating if needed
    /// </summary>
    /// <param name="request">Request to be executed</param>
    /// <param name="token">The cancellation token</param>
    public virtual Task<IRestResponse> ExecuteGetTaskAsync(
      IRestRequest request,
      CancellationToken token)
    {
      if (request == null)
        throw new ArgumentNullException(nameof (request));
      request.Method = Method.GET;
      return this.ExecuteTaskAsync(request, token);
    }

    /// <summary>
    /// Executes a POST-style asynchronously, authenticating if needed
    /// </summary>
    /// <param name="request">Request to be executed</param>
    public virtual Task<IRestResponse> ExecutePostTaskAsync(IRestRequest request)
    {
      return this.ExecutePostTaskAsync(request, CancellationToken.None);
    }

    /// <summary>
    /// Executes a POST-style asynchronously, authenticating if needed
    /// </summary>
    /// <param name="request">Request to be executed</param>
    /// <param name="token">The cancellation token</param>
    public virtual Task<IRestResponse> ExecutePostTaskAsync(
      IRestRequest request,
      CancellationToken token)
    {
      if (request == null)
        throw new ArgumentNullException(nameof (request));
      request.Method = Method.POST;
      return this.ExecuteTaskAsync(request, token);
    }

    /// <summary>
    /// Executes the request asynchronously, authenticating if needed
    /// </summary>
    /// <param name="request">Request to be executed</param>
    /// <param name="token">The cancellation token</param>
    public virtual Task<IRestResponse> ExecuteTaskAsync(
      IRestRequest request,
      CancellationToken token)
    {
      if (request == null)
        throw new ArgumentNullException(nameof (request));
      TaskCompletionSource<IRestResponse> taskCompletionSource = new TaskCompletionSource<IRestResponse>();
      try
      {
        RestRequestAsyncHandle async = this.ExecuteAsync(request, (Action<IRestResponse, RestRequestAsyncHandle>) ((response, _) =>
        {
          if (token.IsCancellationRequested)
            taskCompletionSource.TrySetCanceled();
          else
            taskCompletionSource.TrySetResult(response);
        }));
        token.Register((Action) (() =>
        {
          async.Abort();
          taskCompletionSource.TrySetCanceled();
        }));
      }
      catch (Exception ex)
      {
        taskCompletionSource.TrySetException(ex);
      }
      return taskCompletionSource.Task;
    }

    /// <summary>
    /// Maximum number of redirects to follow if FollowRedirects is true
    /// </summary>
    public int? MaxRedirects { get; set; }

    /// <summary>
    /// Default is true. Determine whether or not requests that result in
    /// HTTP status codes of 3xx should follow returned redirect
    /// </summary>
    public bool FollowRedirects { get; set; }

    /// <summary>
    /// The CookieContainer used for requests made by this client instance
    /// </summary>
    public CookieContainer CookieContainer { get; set; }

    /// <summary>
    /// UserAgent to use for requests made by this client instance
    /// </summary>
    public string UserAgent { get; set; }

    /// <summary>
    /// Timeout in milliseconds to use for requests made by this client instance
    /// </summary>
    public int Timeout { get; set; }

    /// <summary>
    /// The number of milliseconds before the writing or reading times out.
    /// </summary>
    public int ReadWriteTimeout { get; set; }

    /// <summary>
    /// Whether to invoke async callbacks using the SynchronizationContext.Current captured when invoked
    /// </summary>
    public bool UseSynchronizationContext { get; set; }

    /// <summary>
    /// Authenticator to use for requests made by this client instance
    /// </summary>
    public IAuthenticator Authenticator { get; set; }

    /// <summary>
    /// Combined with Request.Resource to construct URL for request
    /// Should include scheme and domain without trailing slash.
    /// </summary>
    /// <example>client.BaseUrl = new Uri("http://example.com");</example>
    public virtual Uri BaseUrl { get; set; }

    public Encoding Encoding
    {
      get => this.encoding;
      set => this.encoding = value;
    }

    public bool PreAuthenticate { get; set; }

    /// <summary>
    /// Default constructor that registers default content handlers
    /// </summary>
    public RestClient()
    {
      this.UseSynchronizationContext = true;
      this.ContentHandlers = (IDictionary<string, IDeserializer>) new Dictionary<string, IDeserializer>();
      this.AcceptTypes = (IList<string>) new List<string>();
      this.DefaultParameters = (IList<Parameter>) new List<Parameter>();
      this.AddHandler("application/json", (IDeserializer) new JsonDeserializer());
      this.AddHandler("application/xml", (IDeserializer) new XmlDeserializer());
      this.AddHandler("text/json", (IDeserializer) new JsonDeserializer());
      this.AddHandler("text/x-json", (IDeserializer) new JsonDeserializer());
      this.AddHandler("text/javascript", (IDeserializer) new JsonDeserializer());
      this.AddHandler("text/xml", (IDeserializer) new XmlDeserializer());
      this.AddHandler("*", (IDeserializer) new XmlDeserializer());
      this.FollowRedirects = true;
    }

    /// <summary>
    /// Sets the BaseUrl property for requests made by this client instance
    /// </summary>
    /// <param name="baseUrl"></param>
    public RestClient(Uri baseUrl)
      : this()
    {
      this.BaseUrl = baseUrl;
    }

    /// <summary>
    /// Sets the BaseUrl property for requests made by this client instance
    /// </summary>
    /// <param name="baseUrl"></param>
    public RestClient(string baseUrl)
      : this()
    {
      this.BaseUrl = !string.IsNullOrEmpty(baseUrl) 
                ? new Uri(baseUrl)
                : throw new ArgumentNullException(nameof (baseUrl));
    }

    private IDictionary<string, IDeserializer> ContentHandlers { get; set; }

    private IList<string> AcceptTypes { get; set; }

    /// <summary>
    /// Parameters included with every request made with this instance of RestClient
    /// If specified in both client and request, the request wins
    /// </summary>
    public IList<Parameter> DefaultParameters { get; private set; }

    /// <summary>Registers a content handler to process response content</summary>
    /// <param name="contentType">MIME content type of the response content</param>
    /// <param name="deserializer">Deserializer to use to process content</param>
    public void AddHandler(string contentType, IDeserializer deserializer)
    {
      this.ContentHandlers[contentType] = deserializer;
      if (!(contentType != "*"))
        return;
      this.AcceptTypes.Add(contentType);
      string str = string.Join(", ", this.AcceptTypes.ToArray<string>());
      this.RemoveDefaultParameter("Accept");
      this.AddDefaultParameter("Accept", (object) str, ParameterType.HttpHeader);
    }

    /// <summary>
    /// Remove a content handler for the specified MIME content type
    /// </summary>
    /// <param name="contentType">MIME content type to remove</param>
    public void RemoveHandler(string contentType)
    {
      this.ContentHandlers.Remove(contentType);
      this.AcceptTypes.Remove(contentType);
      this.RemoveDefaultParameter("Accept");
    }

    /// <summary>Remove all content handlers</summary>
    public void ClearHandlers()
    {
      this.ContentHandlers.Clear();
      this.AcceptTypes.Clear();
      this.RemoveDefaultParameter("Accept");
    }

    /// <summary>
    /// Retrieve the handler for the specified MIME content type
    /// </summary>
    /// <param name="contentType">MIME content type to retrieve</param>
    /// <returns>IDeserializer instance</returns>
    private IDeserializer GetHandler(string contentType)
    {
      if (contentType == null)
        throw new ArgumentNullException(nameof (contentType));
      if (string.IsNullOrEmpty(contentType) && this.ContentHandlers.ContainsKey("*"))
        return this.ContentHandlers["*"];
      int length = contentType.IndexOf(';');
      if (length > -1)
        contentType = contentType.Substring(0, length);
      IDeserializer handler = (IDeserializer) null;
      if (this.ContentHandlers.ContainsKey(contentType))
        handler = this.ContentHandlers[contentType];
      else if (this.ContentHandlers.ContainsKey("*"))
        handler = this.ContentHandlers["*"];
      return handler;
    }

    private void AuthenticateIfNeeded(RestClient client, IRestRequest request)
    {
      if (this.Authenticator == null)
        return;
      this.Authenticator.Authenticate((IRestClient) client, request);
    }

    /// <summary>
    /// Assembles URL to call based on parameters, method and resource
    /// </summary>
    /// <param name="request">RestRequest to execute</param>
    /// <returns>Assembled System.Uri</returns>
    public Uri BuildUri(IRestRequest request)
    {
      if (this.BaseUrl == (Uri) null)
        throw new NullReferenceException("RestClient must contain a value for BaseUrl");

      string uriString = request.Resource;

      IEnumerable<Parameter> parameters1 = request.Parameters.Where<Parameter>((Func<Parameter, bool>)
          (p => p.Type == ParameterType.UrlSegment));

      UriBuilder uriBuilder = new UriBuilder(this.BaseUrl);
     
            foreach (Parameter parameter in parameters1)
      {
        if (parameter.Value == null)
          throw new ArgumentException(string.Format("Cannot build uri when url segment parameter '{0}' value is null.", 
              (object) parameter.Name), nameof (request));

        if (!string.IsNullOrEmpty(uriString))
          uriString = uriString.Replace("{" + parameter.Name + "}", parameter.Value.ToString().UrlEncode());
        uriBuilder.Path = uriBuilder.Path.UrlDecode().Replace("{" + parameter.Name + "}", 
            parameter.Value.ToString().UrlEncode());
      }
      this.BaseUrl = new Uri(uriBuilder.ToString());
      if (!string.IsNullOrEmpty(uriString) && uriString.StartsWith("/"))
        uriString = uriString.Substring(1);

      if (this.BaseUrl != (Uri) null && !string.IsNullOrEmpty(this.BaseUrl.AbsoluteUri))
      {
        if (!this.BaseUrl.AbsoluteUri.EndsWith("/") && !string.IsNullOrEmpty(uriString))
          uriString = "/" + uriString;
        uriString = string.IsNullOrEmpty(uriString) 
                    ? this.BaseUrl.AbsoluteUri 
                    : string.Format("{0}{1}",
            (object) this.BaseUrl, (object) uriString);
      }

      IEnumerable<Parameter> parameters2 = request.Method == Method.POST 
                || request.Method == Method.PUT || request.Method == Method.PATCH 
                ? (IEnumerable<Parameter>) request.Parameters.Where<Parameter>((Func<Parameter, bool>)
                (p => p.Type == ParameterType.QueryString)).ToList<Parameter>() 
                : (IEnumerable<Parameter>) request.Parameters.Where<Parameter>((Func<Parameter, bool>)
                (p => p.Type == ParameterType.GetOrPost || p.Type == ParameterType.QueryString)).ToList<Parameter>();

      if (!parameters2.Any<Parameter>())
        return new Uri(uriString);
      string str1 = RestClient.EncodeParameters(parameters2);
      string str2 = uriString.Contains("?") ? "&" : "?";
      return new Uri(uriString + str2 + str1);
    }

    private static string EncodeParameters(IEnumerable<Parameter> parameters)
    {
      return string.Join("&", parameters.Select<Parameter, string>(
          new Func<Parameter, string>(RestClient.EncodeParameter)).ToArray<string>());
    }

    private static string EncodeParameter(Parameter parameter)
    {
      return parameter.Value != null ? parameter.Name.UrlEncode() 
                + "=" + parameter.Value.ToString().UrlEncode() : parameter.Name.UrlEncode() + "=";
    }

    private void ConfigureHttp(IRestRequest request, IHttp http)
    {
      http.Encoding = this.Encoding;
      http.AlwaysMultipartFormData = request.AlwaysMultipartFormData;
      http.UseDefaultCredentials = request.UseDefaultCredentials;
      http.ResponseWriter = request.ResponseWriter;
      http.CookieContainer = this.CookieContainer;
      foreach (Parameter defaultParameter in (IEnumerable<Parameter>) this.DefaultParameters)
      {
        Parameter p = defaultParameter;
        if (!request.Parameters.Any<Parameter>((Func<Parameter, bool>) (p2 => p2.Name == p.Name && p2.Type == p.Type)))
          request.AddParameter(p);
      }
      if (request.Parameters.All<Parameter>((Func<Parameter, bool>) (p2 => p2.Name.ToLowerInvariant() != "accept")))
      {
        string str = string.Join(", ", this.AcceptTypes.ToArray<string>());
        request.AddParameter("Accept", (object) str, ParameterType.HttpHeader);
      }
      http.Url = this.BuildUri(request);
      http.PreAuthenticate = this.PreAuthenticate;
      string input = this.UserAgent ?? http.UserAgent;
            http.UserAgent = input.HasValue() ? input : "RestSharp/105";// + (object) RestClient.version;
      int num1 = request.Timeout > 0 ? request.Timeout : this.Timeout;
      if (num1 > 0)
        http.Timeout = num1;
      int num2 = request.ReadWriteTimeout > 0 ? request.ReadWriteTimeout : this.ReadWriteTimeout;
      if (num2 > 0)
        http.ReadWriteTimeout = num2;
      http.FollowRedirects = this.FollowRedirects;
      if (request.Credentials != null)
        http.Credentials = request.Credentials;
      foreach (HttpHeader httpHeader in request.Parameters.Where<Parameter>(
          (Func<Parameter, bool>) (p => p.Type == ParameterType.HttpHeader)).Select<Parameter, HttpHeader>(
          (Func<Parameter, HttpHeader>) (p => new HttpHeader()
      {
        Name = p.Name,
        Value = Convert.ToString(p.Value)
      })))
        http.Headers.Add(httpHeader);
      foreach (HttpCookie httpCookie in request.Parameters.Where<Parameter>(
          (Func<Parameter, bool>) (p => p.Type == ParameterType.Cookie)).Select<Parameter, HttpCookie>(
          (Func<Parameter, HttpCookie>) (p => new HttpCookie()
      {
        Name = p.Name,
        Value = Convert.ToString(p.Value)
      })))
        http.Cookies.Add(httpCookie);
      foreach (HttpParameter httpParameter in request.Parameters.Where<Parameter>((Func<Parameter, bool>)
          (p => p.Type == ParameterType.GetOrPost && p.Value != null)).Select<Parameter, HttpParameter>(
          (Func<Parameter, HttpParameter>) (p => new HttpParameter()
      {
        Name = p.Name,
        Value = Convert.ToString(p.Value)
      })))
        http.Parameters.Add(httpParameter);
      foreach (FileParameter file in request.Files)
        http.Files.Add(new HttpFile()
        {
          Name = file.Name,
          ContentType = file.ContentType,
          Writer = file.Writer,
          FileName = file.FileName,
          ContentLength = file.ContentLength
        });
      Parameter parameter = request.Parameters.Where<Parameter>((Func<Parameter, bool>)
          (p => p.Type == ParameterType.RequestBody)).FirstOrDefault<Parameter>();
      if (parameter == null)
        return;
      http.RequestContentType = parameter.Name;
      if (!http.Files.Any<HttpFile>())
      {
        object obj = parameter.Value;
        if (obj is byte[])
          http.RequestBodyBytes = (byte[]) obj;
        else
          http.RequestBody = Convert.ToString(parameter.Value);
      }
      else
        http.Parameters.Add(new HttpParameter()
        {
          Name = parameter.Name,
          Value = Convert.ToString(parameter.Value)
        });
    }

    private static RestResponse ConvertToRestResponse(
      IRestRequest request,
      HttpResponse httpResponse)
    {
      RestResponse restResponse1 = new RestResponse();
      restResponse1.Content = httpResponse.Content;
      restResponse1.ContentEncoding = httpResponse.ContentEncoding;
      restResponse1.ContentLength = httpResponse.ContentLength;
      restResponse1.ContentType = httpResponse.ContentType;
      restResponse1.ErrorException = httpResponse.ErrorException;
      restResponse1.ErrorMessage = httpResponse.ErrorMessage;
      restResponse1.RawBytes = httpResponse.RawBytes;
      restResponse1.ResponseStatus = httpResponse.ResponseStatus;
      restResponse1.ResponseUri = httpResponse.ResponseUri;
      restResponse1.Server = httpResponse.Server;
      restResponse1.StatusCode = httpResponse.StatusCode;
      restResponse1.StatusDescription = httpResponse.StatusDescription;
      restResponse1.Request = request;
      RestResponse restResponse2 = restResponse1;
      foreach (HttpHeader header in (IEnumerable<HttpHeader>) httpResponse.Headers)
        restResponse2.Headers.Add(new Parameter()
        {
          Name = header.Name,
          Value = (object) header.Value,
          Type = ParameterType.HttpHeader
        });
      foreach (HttpCookie cookie in (IEnumerable<HttpCookie>) httpResponse.Cookies)
        restResponse2.Cookies.Add(new RestResponseCookie()
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
      return restResponse2;
    }

    private IRestResponse<T> Deserialize<T>(IRestRequest request, IRestResponse raw)
    {
      request.OnBeforeDeserialization(raw);
      IRestResponse<T> restResponse = (IRestResponse<T>) new RestResponse<T>();
      try
      {
        restResponse = raw.ToAsyncResponse<T>();
        restResponse.Request = request;
        if (restResponse.ErrorException == null)
        {
          IDeserializer handler = this.GetHandler(raw.ContentType);
          if (handler != null)
          {
            handler.RootElement = request.RootElement;
            handler.DateFormat = request.DateFormat;
            handler.Namespace = request.XmlNamespace;
            restResponse.Data = handler.Deserialize<T>(raw);
          }
        }
      }
      catch (Exception ex)
      {
        restResponse.ResponseStatus = ResponseStatus.Error;
        restResponse.ErrorMessage = ex.Message;
        restResponse.ErrorException = ex;
      }
      return restResponse;
    }

        public class c__DisplayClass3
        {
            public IRestRequest request;
            public Action<IRestResponse, RestRequestAsyncHandle> callback;
            public RestRequestAsyncHandle asyncHandle;
            public Action<HttpResponse> AHttpResponce;

            public c__DisplayClass3()
            {
            }
        }
    }
}
