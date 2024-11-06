// Decompiled with JetBrains decompiler
// Type: GoogleAnalytics.Core.GAServiceManager
// Assembly: GoogleAnalytics.Core, Version=1.2.11.25892, Culture=neutral, PublicKeyToken=null
// MVID: DA6701CD-FFEA-4833-995F-5D20607A09B2
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\GoogleAnalytics.Core.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

#nullable disable
namespace GoogleAnalytics.Core
{
  public sealed class GAServiceManager : IServiceManager
  {
    private static Random random;
    private static GAServiceManager current;
    private static readonly Uri endPointUnsecure = new Uri("http://www.google-analytics.com/collect");
    private static readonly Uri endPointSecure = new Uri("https://ssl.google-analytics.com/collect");
    private readonly Queue<Payload> payloads;
    private readonly IList<Task> dispatchingTasks;
    private Timer timer;
    private TimeSpan dispatchPeriod;
    private bool isConnected = true;

    private GAServiceManager()
    {
      this.dispatchingTasks = (IList<Task>) new List<Task>();
      this.payloads = new Queue<Payload>();
      this.DispatchPeriod = TimeSpan.Zero;
    }

    public bool BustCache { get; set; }

    public void Clear()
    {
      lock (this.payloads)
        this.payloads.Clear();
    }

    private async void timer_Tick(object sender) => await this.Dispatch();

    public static GAServiceManager Current
    {
      get
      {
        if (GAServiceManager.current == null)
          GAServiceManager.current = new GAServiceManager();
        return GAServiceManager.current;
      }
    }

    public TimeSpan DispatchPeriod
    {
      get => this.dispatchPeriod;
      set
      {
        if (!(this.dispatchPeriod != value))
          return;
        this.dispatchPeriod = value;
        if (this.timer != null)
        {
          this.timer.Dispose();
          this.timer = (Timer) null;
        }
        if (!(this.dispatchPeriod > TimeSpan.Zero))
          return;
        this.timer = new Timer(new TimerCallback(this.timer_Tick), (object) null, this.DispatchPeriod, this.DispatchPeriod);
      }
    }

    public bool IsConnected
    {
      get => this.isConnected;
      set
      {
        if (this.isConnected == value)
          return;
        this.isConnected = value;
        if (!this.isConnected || !(this.DispatchPeriod >= TimeSpan.Zero))
          return;
        this.Dispatch();
      }
    }

    async void IServiceManager.SendPayload(Payload payload)
    {
      if (this.DispatchPeriod == TimeSpan.Zero && this.IsConnected)
      {
        await this.RunDispatchingTask(this.DispatchImmediatePayload(payload));
      }
      else
      {
        lock (this.payloads)
          this.payloads.Enqueue(payload);
      }
    }

    public async Task Dispatch()
    {
      if (!this.isConnected)
        return;
      Task task = (Task) null;
      lock (this.dispatchingTasks)
      {
        if (this.dispatchingTasks.Any<Task>())
          task = Task.WhenAll((IEnumerable<Task>) this.dispatchingTasks);
      }
      if (task != null)
        await task;
      if (!this.isConnected)
        return;
      IList<Payload> source = (IList<Payload>) new List<Payload>();
      lock (this.payloads)
      {
        while (this.payloads.Count > 0)
          source.Add(this.payloads.Dequeue());
      }
      if (!source.Any<Payload>())
        return;
      await this.RunDispatchingTask(this.DispatchQueuedPayloads((IEnumerable<Payload>) source));
    }

    private async Task RunDispatchingTask(Task newDispatchingTask)
    {
      lock (this.dispatchingTasks)
        this.dispatchingTasks.Add(newDispatchingTask);
      try
      {
        await newDispatchingTask;
      }
      finally
      {
        lock (this.dispatchingTasks)
          this.dispatchingTasks.Remove(newDispatchingTask);
      }
    }

    private async Task DispatchQueuedPayloads(IEnumerable<Payload> payloads)
    {
      using (HttpClient httpClient = this.GetHttpClient())
      {
        DateTimeOffset now = DateTimeOffset.UtcNow;
        foreach (Payload payload in payloads)
        {
          if (this.isConnected)
          {
            Dictionary<string, string> dictionary = payload.Data.ToDictionary<KeyValuePair<string, string>, string, string>((Func<KeyValuePair<string, string>, string>) (kvp => kvp.Key), (Func<KeyValuePair<string, string>, string>) (kvp => kvp.Value));
            dictionary.Add("qt", ((long) now.Subtract(payload.TimeStamp).TotalMilliseconds).ToString());
            await this.DispatchPayloadData(payload, httpClient, dictionary);
          }
          else
          {
            lock (payloads)
              this.payloads.Enqueue(payload);
          }
        }
        now = new DateTimeOffset();
      }
    }

    private async Task DispatchImmediatePayload(Payload payload)
    {
      using (HttpClient httpClient = this.GetHttpClient())
        await this.DispatchPayloadData(payload, httpClient, payload.Data.ToDictionary<KeyValuePair<string, string>, string, string>((Func<KeyValuePair<string, string>, string>) (kvp => kvp.Key), (Func<KeyValuePair<string, string>, string>) (kvp => kvp.Value)));
    }

    private async Task DispatchPayloadData(
      Payload payload,
      HttpClient httpClient,
      Dictionary<string, string> payloadData)
    {
      if (this.BustCache)
        payloadData.Add("z", GAServiceManager.GetCacheBuster());
      Uri requestUri = payload.IsUseSecure ? GAServiceManager.endPointSecure : GAServiceManager.endPointUnsecure;
      using (ByteArrayContent content = GAServiceManager.GetEncodedContent((IEnumerable<KeyValuePair<string, string>>) payloadData))
      {
        try
        {
          HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(requestUri, (HttpContent) content);
        }
        catch
        {
          this.OnPayloadFailed(payload);
        }
      }
    }

    private void OnPayloadFailed(Payload payload)
    {
    }

    private HttpClient GetHttpClient()
    {
      HttpClient httpClient = new HttpClient();
      if (!string.IsNullOrEmpty(this.UserAgent))
        httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(this.UserAgent);
      return httpClient;
    }

    public string UserAgent { get; set; }

    private static string GetCacheBuster()
    {
      if (GAServiceManager.random == null)
        GAServiceManager.random = new Random();
      return GAServiceManager.random.Next().ToString();
    }

    private static ByteArrayContent GetEncodedContent(
      IEnumerable<KeyValuePair<string, string>> nameValueCollection)
    {
      return (ByteArrayContent) new StringContent(GAServiceManager.GetUrlEncodedString(nameValueCollection));
    }

    private static string GetUrlEncodedString(
      IEnumerable<KeyValuePair<string, string>> nameValueCollection)
    {
      StringBuilder stringBuilder = new StringBuilder();
      bool flag = true;
      foreach (KeyValuePair<string, string> nameValue in nameValueCollection)
      {
        string stringToEscape = nameValue.Value;
        if (stringToEscape != null)
        {
          if (flag)
            flag = false;
          else
            stringBuilder.Append("&");
          stringBuilder.Append(nameValue.Key);
          stringBuilder.Append("=");
          stringBuilder.Append(Uri.EscapeDataString(stringToEscape));
        }
      }
      return stringBuilder.ToString();
    }
  }
}
