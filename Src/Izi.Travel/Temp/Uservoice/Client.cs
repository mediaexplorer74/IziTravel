// Decompiled with JetBrains decompiler
// Type: UserVoice.Client
// Assembly: Uservoice, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: 038B5345-2117-47AA-93A0-4A054BBF5C1C
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Uservoice.dll

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

#nullable disable
namespace UserVoice
{
  public class Client
  {
    public const string ClientVersion = "0.0.4";
    public string Token;
    public string Secret;
    private RestClient _consumer;
    private RestClient _accessToken;
    private Client _requestToken;
    private string _apiKey;
    private string _apiSecret;
    private string _uservoiceDomain;
    private string _protocol;
    private string _subdomainName;
    private string _callback;

    public Client(
      string subdomainName,
      string apiKey,
      string apiSecret = null,
      string callback = null,
      string token = null,
      string secret = null,
      string uservoiceDomain = null,
      string protocol = null)
    {
      this._protocol = protocol ?? "https";
      this._uservoiceDomain = uservoiceDomain ?? "uservoice.com";
      this._apiKey = apiKey;
      this._apiSecret = apiSecret;
      this._subdomainName = subdomainName;
      this._callback = callback;
      this.Token = token;
      this.Secret = secret;
      this._consumer = new RestClient(this._protocol + "://" + this._subdomainName + "." + this._uservoiceDomain);
      if (apiSecret != null)
        this._consumer.Authenticator = (IAuthenticator) OAuth1Authenticator.ForRequestToken(apiKey, apiSecret, callback);
      if (token == null || secret == null)
        return;
      this._accessToken = new RestClient(this._protocol + "://" + this._subdomainName + "." + this._uservoiceDomain);
      this._accessToken.Authenticator = (IAuthenticator) OAuth1Authenticator.ForAccessToken(apiKey, apiSecret, token, secret);
    }

    private RestClient GetToken() => this._accessToken != null ? this._accessToken : this._consumer;

    public async Task<JToken> Request(Method method, string path, object body = null)
    {
      RestRequest request = new RestRequest(((IEnumerable<string>) path.Split('?')).First<string>(), method);
      request.AddHeader("Accept", "application/json");
      request.AddHeader("API-Client", string.Format("uservoice-csharp-{0}", (object) "0.0.4"));
      if (body != null)
        request.AddParameter("application/json", (object) JsonConvert.SerializeObject(body), ParameterType.RequestBody);
      if (body == null)
      {
        NameValueCollection queryString = HttpUtilityHelper.ParseQueryString(string.Join(string.Empty, ((IEnumerable<string>) path.Split('?')).Skip<string>(1)));
        if (queryString != null)
        {
          foreach (string allKey in queryString.AllKeys)
            request.AddParameter(allKey, (object) queryString[allKey], ParameterType.GetOrPost);
        }
      }
      if (this._apiSecret == null)
        request.AddParameter("client", (object) this._apiKey, ParameterType.GetOrPost);
      IRestResponse restResponse = await this.GetToken().Execute((IRestRequest) request);
      JToken jtoken;
      try
      {
        if (restResponse.ContentType.StartsWith("application/json"))
        {
          jtoken = (JToken) JObject.Parse(restResponse.Content);
        }
        else
        {
          jtoken = (JToken) new JObject();
          NameValueCollection queryString = HttpUtilityHelper.ParseQueryString(restResponse.Content);
          if (queryString.AllKeys != null)
          {
            foreach (string allKey in queryString.AllKeys)
            {
              if (allKey != null)
                jtoken[(object) allKey] = (JToken) queryString[allKey];
            }
          }
        }
      }
      catch (JsonReaderException ex)
      {
        throw new ApplicationError("Invalid JSON received: " + restResponse.Content);
      }
      if (HttpStatusCode.OK.Equals((object) restResponse.StatusCode))
        return jtoken;
      string msg = restResponse.Content;
      if (jtoken != null && jtoken[(object) "errors"] != null && jtoken[(object) "errors"][(object) "message"] != null)
        msg = jtoken[(object) "errors"].Value<string>((object) "message");
      switch (restResponse.StatusCode)
      {
        case HttpStatusCode.Unauthorized:
          throw new UnauthorizedError(msg);
        case HttpStatusCode.NotFound:
          throw new NotFoundError(msg);
        case HttpStatusCode.InternalServerError:
          throw new ApplicationError(msg);
        default:
          throw new ApiError(msg);
      }
    }

    public Client LoginWithAccessToken(string token, string secret)
    {
      return new Client(this._subdomainName, this._apiKey, this._apiSecret, this._callback, token, secret, this._uservoiceDomain, this._protocol);
    }

    public async Task<Client> RequestToken(string callback = null)
    {
      NameValueCollection queryString = HttpUtilityHelper.ParseQueryString((await this._consumer.Execute((IRestRequest) new RestRequest("/oauth/request_token", Method.POST))).Content);
      return queryString != null && queryString["oauth_token"] != null && queryString["oauth_token_secret"] != null ? this.LoginWithAccessToken(queryString["oauth_token"], queryString["oauth_token_secret"]) : throw new UnauthorizedError("Failed to get request token");
    }

    public async Task<Client> LoginAs(string email)
    {
      JObject parameters = new JObject();
      parameters["user"] = (JToken) new JObject();
      parameters["user"][(object) nameof (email)] = (JToken) email;
      JObject jobject = parameters;
      Client client = await this.RequestToken();
      jobject["request_token"] = (JToken) client.Token;
      jobject = (JObject) null;
      JToken jtoken = await this.Request(Method.POST, "/api/v1/users/login_as", (object) parameters);
      return this.LoginWithAccessToken((string) jtoken[(object) "token"][(object) "oauth_token"], (string) jtoken[(object) "token"][(object) "oauth_token_secret"]);
    }

    public async Task<Client> LoginAsOwner()
    {
      JObject parameters = new JObject();
      JObject jobject = parameters;
      Client client = await this.RequestToken();
      jobject["request_token"] = (JToken) client.Token;
      jobject = (JObject) null;
      JToken jtoken = await this.Request(Method.POST, "/api/v1/users/login_as_owner", (object) parameters);
      return this.LoginWithAccessToken((string) jtoken[(object) "token"][(object) "oauth_token"], (string) jtoken[(object) "token"][(object) "oauth_token_secret"]);
    }

    public async Task<string> AuthorizeUrl()
    {
      Client client = this;
      Client requestToken = client._requestToken;
      Client client1 = await this.RequestToken();
      client._requestToken = client1;
      client = (Client) null;
      return this._requestToken.GetToken().BaseUrl.ToString() + "/oauth/authorize?oauth_token=" + this._requestToken.Token;
    }

    public async Task<Client> LoginWithVerifier(string verifier)
    {
      JToken jtoken = await this._requestToken.Post("/oauth/access_token", (object) new
      {
        oauth_verifier = verifier
      });
      return jtoken != null && jtoken[(object) "oauth_token"] != null && jtoken[(object) "oauth_token_secret"] != null ? this.LoginWithAccessToken((string) jtoken[(object) "oauth_token"], (string) jtoken[(object) "oauth_token_secret"]) : throw new UnauthorizedError("Failed to get request token");
    }

    public Task<JToken> Get(string path) => this.Request(Method.GET, path);

    public Task<JToken> Delete(string path) => this.Request(Method.DELETE, path);

    public Task<JToken> Post(string path, object parameters)
    {
      return this.Request(Method.POST, path, parameters);
    }

    public Task<JToken> Put(string path, object parameters)
    {
      return this.Request(Method.PUT, path, parameters);
    }
  }
}
