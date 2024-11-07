// Decompiled with JetBrains decompiler
// Type: RestSharp.OAuth2UriQueryParameterAuthenticator
// Assembly: RestSharp.WindowsPhone, Version=105.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EBEB4E14-2430-4192-BEF2-49D8C49DBCCE
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.xml

#nullable disable
namespace RestSharp
{
  /// <summary>The OAuth 2 authenticator using URI query parameter.</summary>
  /// <remarks>
  /// Based on http://tools.ietf.org/html/draft-ietf-oauth-v2-10#section-5.1.2
  /// </remarks>
  public class OAuth2UriQueryParameterAuthenticator : OAuth2Authenticator
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:RestSharp.OAuth2UriQueryParameterAuthenticator" /> class.
    /// </summary>
    /// <param name="accessToken">The access token.</param>
    public OAuth2UriQueryParameterAuthenticator(string accessToken)
      : base(accessToken)
    {
    }

    public override void Authenticate(IRestClient client, IRestRequest request)
    {
      request.AddParameter("oauth_token", (object) this.AccessToken, ParameterType.GetOrPost);
    }
  }
}
