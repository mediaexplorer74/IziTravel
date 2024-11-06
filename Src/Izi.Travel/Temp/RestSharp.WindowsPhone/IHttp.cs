// Decompiled with JetBrains decompiler
// Type: RestSharp.IHttp
// Assembly: RestSharp.WindowsPhone, Version=105.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EBEB4E14-2430-4192-BEF2-49D8C49DBCCE
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.xml

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

#nullable disable
namespace RestSharp
{
  public interface IHttp
  {
    Action<Stream> ResponseWriter { get; set; }

    CookieContainer CookieContainer { get; set; }

    ICredentials Credentials { get; set; }

    /// <summary>
    /// Always send a multipart/form-data request - even when no Files are present.
    /// </summary>
    bool AlwaysMultipartFormData { get; set; }

    string UserAgent { get; set; }

    int Timeout { get; set; }

    int ReadWriteTimeout { get; set; }

    bool FollowRedirects { get; set; }

    bool UseDefaultCredentials { get; set; }

    Encoding Encoding { get; set; }

    IList<HttpHeader> Headers { get; }

    IList<HttpParameter> Parameters { get; }

    IList<HttpFile> Files { get; }

    IList<HttpCookie> Cookies { get; }

    string RequestBody { get; set; }

    string RequestContentType { get; set; }

    bool PreAuthenticate { get; set; }

    /// <summary>
    /// An alternative to RequestBody, for when the caller already has the byte array.
    /// </summary>
    byte[] RequestBodyBytes { get; set; }

    Uri Url { get; set; }

    HttpWebRequest DeleteAsync(Action<HttpResponse> action);

    HttpWebRequest GetAsync(Action<HttpResponse> action);

    HttpWebRequest HeadAsync(Action<HttpResponse> action);

    HttpWebRequest OptionsAsync(Action<HttpResponse> action);

    HttpWebRequest PostAsync(Action<HttpResponse> action);

    HttpWebRequest PutAsync(Action<HttpResponse> action);

    HttpWebRequest PatchAsync(Action<HttpResponse> action);

    HttpWebRequest MergeAsync(Action<HttpResponse> action);

    HttpWebRequest AsPostAsync(Action<HttpResponse> action, string httpMethod);

    HttpWebRequest AsGetAsync(Action<HttpResponse> action, string httpMethod);
  }
}
