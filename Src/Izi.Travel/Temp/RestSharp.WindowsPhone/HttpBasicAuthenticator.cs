// Decompiled with JetBrains decompiler
// Type: RestSharp.HttpBasicAuthenticator
// Assembly: RestSharp.WindowsPhone, Version=105.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EBEB4E14-2430-4192-BEF2-49D8C49DBCCE
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.xml

using System;
using System.Linq;
using System.Text;

#nullable disable
namespace RestSharp
{
  public class HttpBasicAuthenticator : IAuthenticator
  {
    private readonly string _authHeader;

    public HttpBasicAuthenticator(string username, string password)
    {
      this._authHeader = string.Format("Basic {0}", (object) Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format("{0}:{1}", (object) username, (object) password))));
    }

    public void Authenticate(IRestClient client, IRestRequest request)
    {
      if (request.Parameters.Any<Parameter>((Func<Parameter, bool>) (p => p.Name.Equals("Authorization", StringComparison.OrdinalIgnoreCase))))
        return;
      request.AddParameter("Authorization", (object) this._authHeader, ParameterType.HttpHeader);
    }
  }
}
