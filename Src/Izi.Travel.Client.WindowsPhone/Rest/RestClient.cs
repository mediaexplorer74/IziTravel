// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Client.Rest.RestClient
// Assembly: Izi.Travel.Client.WindowsPhone, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: C73077B8-157D-49B0-834E-CAFCC993728A
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Client.WindowsPhone.dll

using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

#nullable disable
namespace Izi.Travel.Client.Rest
{
  public class RestClient : IRestClient
  {
    private static readonly Regex RegexParameter = new Regex("(?<=\\{)[^}]*(?=\\})", RegexOptions.IgnoreCase);

    public Uri BaseUri { get; set; }

    public List<Parameter> DefaultParameters { get; private set; }

    public RestClient()
      : this((Uri) null)
    {
    }

    public RestClient(Uri baseUri)
    {
      this.BaseUri = baseUri;
      this.DefaultParameters = new List<Parameter>();
    }

    public async Task<IRestResponse<T>> ExecuteTaskAsync<T>(IRestRequest request)
    {
      HttpClientHandler httpClientHandler = new HttpClientHandler();
      if (httpClientHandler.SupportsAutomaticDecompression)
        httpClientHandler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
      using (HttpClient httpClient = new HttpClient())
      {
        foreach (Parameter parameter in this.GetParameters(request, ParameterType.HttpHeader))
          httpClient.DefaultRequestHeaders.Add(parameter.Name, parameter.Value.ToString());
        Parameter[] array = this.GetParameters(request, ParameterType.UrlSegment).ToArray<Parameter>();
        foreach (Match match in RestClient.RegexParameter.Matches(request.Resource))
        {
          Match segmentMatch = match;
          Parameter parameter = ((IEnumerable<Parameter>) array).FirstOrDefault<Parameter>((Func<Parameter, bool>) (x => x.Name.Equals(segmentMatch.Value, StringComparison.CurrentCultureIgnoreCase)));
          if (parameter != null && parameter.Value != null)
            request.Resource = request.Resource.Replace(string.Format("{{{0}}}", (object) segmentMatch.Value), Uri.EscapeDataString(parameter.Value.ToString()));
        }
        UriBuilder uriBuilder = new UriBuilder(new Uri(this.BaseUri, request.Resource))
        {
          Query = RestClient.GetQueryString(this.GetParameters(request, ParameterType.QueryString))
        };
        JsonSerializer serializer = new JsonSerializer();
        RestResponse<T> result = new RestResponse<T>();
        HttpResponseMessage httpResponseMessage = (HttpResponseMessage) null;
        switch (request.Method)
        {
          case Method.Get:
            httpResponseMessage = await httpClient.GetAsync(uriBuilder.Uri);
            break;
          case Method.Post:
            StringBuilder sb = new StringBuilder();
            using (StringWriter stringWriter = new StringWriter(sb))
              serializer.Serialize((TextWriter) stringWriter, request.Content);
            httpResponseMessage = await httpClient.PostAsync(uriBuilder.Uri, (HttpContent) new StringContent(sb.ToString()));
            break;
        }
        if (httpResponseMessage == null)
          return (IRestResponse<T>) null;
        result.StatusCode = httpResponseMessage.StatusCode;
        if (httpResponseMessage.IsSuccessStatusCode)
        {
          using (StreamReader reader1 = new StreamReader(await httpResponseMessage.Content.ReadAsStreamAsync()))
          {
            using (JsonReader reader2 = (JsonReader) new JsonTextReader((TextReader) reader1))
              result.Data = serializer.Deserialize<T>(reader2);
          }
        }
        return (IRestResponse<T>) result;
      }
    }

    private IEnumerable<Parameter> GetParameters(IRestRequest request, ParameterType type)
    {
      Dictionary<string, Parameter> parameters = new Dictionary<string, Parameter>();
      if (request != null)
      {
        IEnumerable<Parameter> source = request.Parameters.Where<Parameter>((Func<Parameter, bool>) (x => x.Type == type && !string.IsNullOrWhiteSpace(x.Name) && x.Value != null));
        foreach (Parameter parameter in source.Where<Parameter>((Func<Parameter, bool>) (requestParameter => !parameters.ContainsKey(requestParameter.Name))))
          parameters.Add(parameter.Name, parameter);
      }
      IEnumerable<Parameter> source1 = this.DefaultParameters.Where<Parameter>((Func<Parameter, bool>) (x => x.Type == type && !string.IsNullOrWhiteSpace(x.Name) && x.Value != null));
      foreach (Parameter parameter in source1.Where<Parameter>((Func<Parameter, bool>) (defaultParameter => !parameters.ContainsKey(defaultParameter.Name))))
        parameters.Add(parameter.Name, parameter);
      return (IEnumerable<Parameter>) parameters.Values;
    }

    private static string GetQueryString(IEnumerable<Parameter> parameters)
    {
      return parameters == null ? (string) null : string.Join("&", parameters.Where<Parameter>((Func<Parameter, bool>) (x => x.Value != null)).Select<Parameter, string>((Func<Parameter, string>) (parameter =>
      {
        string str = parameter.Value is IList source2 ? string.Join(",", source2.Cast<object>().Where<object>((Func<object, bool>) (x => x != null)).Select<object, string>((Func<object, string>) (x => Uri.EscapeDataString(x.ToString().ToLower())))) : Uri.EscapeDataString(parameter.Value.ToString().ToLower());
        return parameter.Name + "=" + str;
      })));
    }
  }
}
