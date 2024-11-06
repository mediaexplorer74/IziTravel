// Decompiled with JetBrains decompiler
// Type: RestSharp.SimpleAuthenticator
// Assembly: RestSharp.WindowsPhone, Version=105.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EBEB4E14-2430-4192-BEF2-49D8C49DBCCE
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.xml

#nullable disable
namespace RestSharp
{
  public class SimpleAuthenticator : IAuthenticator
  {
    private readonly string _usernameKey;
    private readonly string _username;
    private readonly string _passwordKey;
    private readonly string _password;

    public SimpleAuthenticator(
      string usernameKey,
      string username,
      string passwordKey,
      string password)
    {
      this._usernameKey = usernameKey;
      this._username = username;
      this._passwordKey = passwordKey;
      this._password = password;
    }

    public void Authenticate(IRestClient client, IRestRequest request)
    {
      request.AddParameter(this._usernameKey, (object) this._username);
      request.AddParameter(this._passwordKey, (object) this._password);
    }
  }
}
