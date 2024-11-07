// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Client.Rest.RestResponse`1
// Assembly: Izi.Travel.Client.WindowsPhone, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: C73077B8-157D-49B0-834E-CAFCC993728A
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Client.WindowsPhone.dll

using System;
using System.Net;

#nullable disable
namespace Izi.Travel.Client.Rest
{
  public class RestResponse<T> : IRestResponse<T>
  {
    public HttpStatusCode StatusCode { get; set; }

    public string ErrorMessage { get; set; }

    public Exception ErrorException { get; set; }

    public T Data { get; set; }
  }
}
