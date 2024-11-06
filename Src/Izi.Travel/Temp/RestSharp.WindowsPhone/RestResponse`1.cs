// Decompiled with JetBrains decompiler
// Type: RestSharp.RestResponse`1
// Assembly: RestSharp.WindowsPhone, Version=105.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EBEB4E14-2430-4192-BEF2-49D8C49DBCCE
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.xml

#nullable disable
namespace RestSharp
{
  /// <summary>
  /// Container for data sent back from API including deserialized data
  /// </summary>
  /// <typeparam name="T">Type of data to deserialize to</typeparam>
  public class RestResponse<T> : RestResponseBase, IRestResponse<T>, IRestResponse
  {
    /// <summary>Deserialized entity data</summary>
    public T Data { get; set; }

    public static explicit operator RestResponse<T>(RestResponse response)
    {
      RestResponse<T> restResponse = new RestResponse<T>();
      restResponse.ContentEncoding = response.ContentEncoding;
      restResponse.ContentLength = response.ContentLength;
      restResponse.ContentType = response.ContentType;
      restResponse.Cookies = response.Cookies;
      restResponse.ErrorMessage = response.ErrorMessage;
      restResponse.ErrorException = response.ErrorException;
      restResponse.Headers = response.Headers;
      restResponse.RawBytes = response.RawBytes;
      restResponse.ResponseStatus = response.ResponseStatus;
      restResponse.ResponseUri = response.ResponseUri;
      restResponse.Server = response.Server;
      restResponse.StatusCode = response.StatusCode;
      restResponse.StatusDescription = response.StatusDescription;
      restResponse.Request = response.Request;
      return restResponse;
    }
  }
}
