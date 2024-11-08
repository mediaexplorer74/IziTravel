// Decompiled with JetBrains decompiler
// Type: RestSharp.Extensions.ResponseExtensions
// Assembly: RestSharp.WindowsPhone, Version=105.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EBEB4E14-2430-4192-BEF2-49D8C49DBCCE
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.xml

#nullable disable
namespace RestSharp.Extensions
{
  public static class ResponseExtensions
  {
    public static IRestResponse<T> ToAsyncResponse<T>(this IRestResponse response)
    {
      RestResponse<T> asyncResponse = new RestResponse<T>();
      asyncResponse.ContentEncoding = response.ContentEncoding;
      asyncResponse.ContentLength = response.ContentLength;
      asyncResponse.ContentType = response.ContentType;
      asyncResponse.Cookies = response.Cookies;
      asyncResponse.ErrorException = response.ErrorException;
      asyncResponse.ErrorMessage = response.ErrorMessage;
      asyncResponse.Headers = response.Headers;
      asyncResponse.RawBytes = response.RawBytes;
      asyncResponse.ResponseStatus = response.ResponseStatus;
      asyncResponse.ResponseUri = response.ResponseUri;
      asyncResponse.Server = response.Server;
      asyncResponse.StatusCode = response.StatusCode;
      asyncResponse.StatusDescription = response.StatusDescription;
      return (IRestResponse<T>) asyncResponse;
    }
  }
}
