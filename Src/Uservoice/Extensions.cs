// Decompiled with JetBrains decompiler
// Type: UserVoice.Extensions
// Assembly: Uservoice, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: 038B5345-2117-47AA-93A0-4A054BBF5C1C
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Uservoice.dll

using RestSharp;
using System;
using System.Threading.Tasks;

#nullable disable
namespace UserVoice
{
  internal static class Extensions
  {
    public static Task<IRestResponse> Execute(this RestClient client, IRestRequest request)
    {
      TaskCompletionSource<IRestResponse> tcs = new TaskCompletionSource<IRestResponse>();
      client.ExecuteAsync(request, (Action<IRestResponse, RestRequestAsyncHandle>) ((x, y) => tcs.SetResult(x)));
      return tcs.Task;
    }
  }
}
