// Decompiled with JetBrains decompiler
// Type: RestSharp.IRestClient
// Assembly: RestSharp.WindowsPhone, Version=105.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EBEB4E14-2430-4192-BEF2-49D8C49DBCCE
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.xml

using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

#nullable disable
namespace RestSharp
{
  public interface IRestClient
  {
    CookieContainer CookieContainer { get; set; }

    int? MaxRedirects { get; set; }

    string UserAgent { get; set; }

    int Timeout { get; set; }

    int ReadWriteTimeout { get; set; }

    bool UseSynchronizationContext { get; set; }

    IAuthenticator Authenticator { get; set; }

    Uri BaseUrl { get; set; }

    Encoding Encoding { get; set; }

    bool PreAuthenticate { get; set; }

    IList<Parameter> DefaultParameters { get; }

    RestRequestAsyncHandle ExecuteAsync(
      IRestRequest request,
      Action<IRestResponse, RestRequestAsyncHandle> callback);

    RestRequestAsyncHandle ExecuteAsync<T>(
      IRestRequest request,
      Action<IRestResponse<T>, RestRequestAsyncHandle> callback);

    bool FollowRedirects { get; set; }

    Uri BuildUri(IRestRequest request);

    /// <summary>
    /// Executes a GET-style request and callback asynchronously, authenticating if needed
    /// </summary>
    /// <param name="request">Request to be executed</param>
    /// <param name="callback">Callback function to be executed upon completion providing access to the async handle.</param>
    /// <param name="httpMethod">The HTTP method to execute</param>
    RestRequestAsyncHandle ExecuteAsyncGet(
      IRestRequest request,
      Action<IRestResponse, RestRequestAsyncHandle> callback,
      string httpMethod);

    /// <summary>
    /// Executes a POST-style request and callback asynchronously, authenticating if needed
    /// </summary>
    /// <param name="request">Request to be executed</param>
    /// <param name="callback">Callback function to be executed upon completion providing access to the async handle.</param>
    /// <param name="httpMethod">The HTTP method to execute</param>
    RestRequestAsyncHandle ExecuteAsyncPost(
      IRestRequest request,
      Action<IRestResponse, RestRequestAsyncHandle> callback,
      string httpMethod);

    /// <summary>
    /// Executes a GET-style request and callback asynchronously, authenticating if needed
    /// </summary>
    /// <typeparam name="T">Target deserialization type</typeparam>
    /// <param name="request">Request to be executed</param>
    /// <param name="callback">Callback function to be executed upon completion</param>
    /// <param name="httpMethod">The HTTP method to execute</param>
    RestRequestAsyncHandle ExecuteAsyncGet<T>(
      IRestRequest request,
      Action<IRestResponse<T>, RestRequestAsyncHandle> callback,
      string httpMethod);

    /// <summary>
    /// Executes a GET-style request and callback asynchronously, authenticating if needed
    /// </summary>
    /// <typeparam name="T">Target deserialization type</typeparam>
    /// <param name="request">Request to be executed</param>
    /// <param name="callback">Callback function to be executed upon completion</param>
    /// <param name="httpMethod">The HTTP method to execute</param>
    RestRequestAsyncHandle ExecuteAsyncPost<T>(
      IRestRequest request,
      Action<IRestResponse<T>, RestRequestAsyncHandle> callback,
      string httpMethod);

    void AddHandler(string contentType, IDeserializer deserializer);

    void RemoveHandler(string contentType);

    void ClearHandlers();

    /// <summary>
    /// Executes the request and callback asynchronously, authenticating if needed
    /// </summary>
    /// <typeparam name="T">Target deserialization type</typeparam>
    /// <param name="request">Request to be executed</param>
    /// <param name="token">The cancellation token</param>
    Task<IRestResponse<T>> ExecuteTaskAsync<T>(IRestRequest request, CancellationToken token);

    /// <summary>
    /// Executes the request asynchronously, authenticating if needed
    /// </summary>
    /// <typeparam name="T">Target deserialization type</typeparam>
    /// <param name="request">Request to be executed</param>
    Task<IRestResponse<T>> ExecuteTaskAsync<T>(IRestRequest request);

    /// <summary>
    /// Executes a GET-style request asynchronously, authenticating if needed
    /// </summary>
    /// <typeparam name="T">Target deserialization type</typeparam>
    /// <param name="request">Request to be executed</param>
    Task<IRestResponse<T>> ExecuteGetTaskAsync<T>(IRestRequest request);

    /// <summary>
    /// Executes a GET-style request asynchronously, authenticating if needed
    /// </summary>
    /// <typeparam name="T">Target deserialization type</typeparam>
    /// <param name="request">Request to be executed</param>
    /// <param name="token">The cancellation token</param>
    Task<IRestResponse<T>> ExecuteGetTaskAsync<T>(IRestRequest request, CancellationToken token);

    /// <summary>
    /// Executes a POST-style request asynchronously, authenticating if needed
    /// </summary>
    /// <typeparam name="T">Target deserialization type</typeparam>
    /// <param name="request">Request to be executed</param>
    Task<IRestResponse<T>> ExecutePostTaskAsync<T>(IRestRequest request);

    /// <summary>
    /// Executes a POST-style request asynchronously, authenticating if needed
    /// </summary>
    /// <typeparam name="T">Target deserialization type</typeparam>
    /// <param name="request">Request to be executed</param>
    /// <param name="token">The cancellation token</param>
    Task<IRestResponse<T>> ExecutePostTaskAsync<T>(IRestRequest request, CancellationToken token);

    /// <summary>
    /// Executes the request and callback asynchronously, authenticating if needed
    /// </summary>
    /// <param name="request">Request to be executed</param>
    /// <param name="token">The cancellation token</param>
    Task<IRestResponse> ExecuteTaskAsync(IRestRequest request, CancellationToken token);

    /// <summary>
    /// Executes the request asynchronously, authenticating if needed
    /// </summary>
    /// <param name="request">Request to be executed</param>
    Task<IRestResponse> ExecuteTaskAsync(IRestRequest request);

    /// <summary>
    /// Executes a GET-style asynchronously, authenticating if needed
    /// </summary>
    /// <param name="request">Request to be executed</param>
    Task<IRestResponse> ExecuteGetTaskAsync(IRestRequest request);

    /// <summary>
    /// Executes a GET-style asynchronously, authenticating if needed
    /// </summary>
    /// <param name="request">Request to be executed</param>
    /// <param name="token">The cancellation token</param>
    Task<IRestResponse> ExecuteGetTaskAsync(IRestRequest request, CancellationToken token);

    /// <summary>
    /// Executes a POST-style asynchronously, authenticating if needed
    /// </summary>
    /// <param name="request">Request to be executed</param>
    Task<IRestResponse> ExecutePostTaskAsync(IRestRequest request);

    /// <summary>
    /// Executes a POST-style asynchronously, authenticating if needed
    /// </summary>
    /// <param name="request">Request to be executed</param>
    /// <param name="token">The cancellation token</param>
    Task<IRestResponse> ExecutePostTaskAsync(IRestRequest request, CancellationToken token);
  }
}
