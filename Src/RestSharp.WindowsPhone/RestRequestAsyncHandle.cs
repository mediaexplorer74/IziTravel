// Decompiled with JetBrains decompiler
// Type: RestSharp.RestRequestAsyncHandle
// Assembly: RestSharp.WindowsPhone, Version=105.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EBEB4E14-2430-4192-BEF2-49D8C49DBCCE
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.xml

using System.Net;

#nullable disable
namespace RestSharp
{
  public class RestRequestAsyncHandle
  {
    public HttpWebRequest WebRequest;

    public RestRequestAsyncHandle()
    {
    }

    public RestRequestAsyncHandle(HttpWebRequest webRequest) => this.WebRequest = webRequest;

    public void Abort()
    {
      if (this.WebRequest == null)
        return;
      this.WebRequest.Abort();
    }
  }
}
