// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Client.Rest.RestRequest
// Assembly: Izi.Travel.Client.WindowsPhone, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: C73077B8-157D-49B0-834E-CAFCC993728A
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Client.WindowsPhone.dll

using System.Collections.Generic;

#nullable disable
namespace Izi.Travel.Client.Rest
{
  public class RestRequest : IRestRequest
  {
    public List<Parameter> Parameters { get; private set; }

    public string Resource { get; set; }

    public Method Method { get; set; }

    public object Content { get; set; }

    public RestRequest()
      : this((string) null)
    {
    }

    public RestRequest(string resource)
      : this(resource, Method.Get)
    {
    }

    public RestRequest(string resource, Method method)
    {
      this.Resource = resource;
      this.Method = method;
      this.Parameters = new List<Parameter>();
    }

    public void AddParameter(string name, object value, ParameterType type = ParameterType.QueryString)
    {
      this.Parameters.Add(new Parameter(name, value, type));
    }
  }
}
