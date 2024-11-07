// Decompiled with JetBrains decompiler
// Type: RestSharp.OAuth2AuthorizationRequestHeaderAuthenticator
// Assembly: RestSharp.WindowsPhone, Version=105.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EBEB4E14-2430-4192-BEF2-49D8C49DBCCE
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.xml

using System;
using System.Linq;

#nullable disable
namespace RestSharp
{
  /// <summary>
  /// The OAuth 2 authenticator using the authorization request header field.
  /// </summary>
  /// <remarks>
  /// Based on http://tools.ietf.org/html/draft-ietf-oauth-v2-10#section-5.1.1
  /// </remarks>
  public class OAuth2AuthorizationRequestHeaderAuthenticator : OAuth2Authenticator
  {
    /// <summary>
    /// Stores the Authorization header value as "[tokenType] accessToken". used for performance.
    /// </summary>
    private readonly string _authorizationValue;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:RestSharp.OAuth2AuthorizationRequestHeaderAuthenticator" /> class.
    /// </summary>
    /// <param name="accessToken">The access token.</param>
    public OAuth2AuthorizationRequestHeaderAuthenticator(string accessToken)
      : this(accessToken, "OAuth")
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:RestSharp.OAuth2AuthorizationRequestHeaderAuthenticator" /> class.
    /// </summary>
    /// <param name="accessToken">The access token.</param>
    /// <param name="tokenType">The token type.</param>
    public OAuth2AuthorizationRequestHeaderAuthenticator(string accessToken, string tokenType)
      : base(accessToken)
    {
      this._authorizationValue = tokenType + " " + accessToken;
    }

    public override void Authenticate(IRestClient client, IRestRequest request)
    {
      if (request.Parameters.Any<Parameter>((Func<Parameter, bool>) (p => p.Name.Equals("Authorization", StringComparison.OrdinalIgnoreCase))))
        return;
      request.AddParameter("Authorization", (object) this._authorizationValue, ParameterType.HttpHeader);
    }
  }
}
