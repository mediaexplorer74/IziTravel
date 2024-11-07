// Decompiled with JetBrains decompiler
// Type: RestSharp.OAuth2Authenticator
// Assembly: RestSharp.WindowsPhone, Version=105.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EBEB4E14-2430-4192-BEF2-49D8C49DBCCE
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.xml

#nullable disable
namespace RestSharp
{
  /// <summary>Base class for OAuth 2 Authenticators.</summary>
  /// <remarks>
  /// Since there are many ways to authenticate in OAuth2,
  /// this is used as a base class to differentiate between
  /// other authenticators.
  /// 
  /// Any other OAuth2 authenticators must derive from this
  /// abstract class.
  /// </remarks>
  public abstract class OAuth2Authenticator : IAuthenticator
  {
    /// <summary>Access token to be used when authenticating.</summary>
    private readonly string _accessToken;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:RestSharp.OAuth2Authenticator" /> class.
    /// </summary>
    /// <param name="accessToken">The access token.</param>
    protected OAuth2Authenticator(string accessToken) => this._accessToken = accessToken;

    /// <summary>Gets the access token.</summary>
    public string AccessToken => this._accessToken;

    public abstract void Authenticate(IRestClient client, IRestRequest request);
  }
}
