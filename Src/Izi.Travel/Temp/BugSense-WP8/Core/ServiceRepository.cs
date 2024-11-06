// Decompiled with JetBrains decompiler
// Type: BugSense.Core.ServiceRepository
// Assembly: BugSense-WP8, Version=3.6.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 8A6D84D1-19B1-4B1A-84A1-50F2A1BD7889
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\BugSense-WP8.dll

using BugSense.Core.Helpers;
using BugSense.Core.Interfaces;
using BugSense.Core.Model;
using Splunk.Mi.Utilities;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

#nullable disable
namespace BugSense.Core
{
  internal class ServiceRepository : IBugSenseServiceClient
  {
    public bool IsError { get; set; }

    public async Task<BugSenseResponseResult> ExecuteBugSenseRequestAsync(
      string url,
      string requestData,
      bool isError,
      string contentType)
    {
      this.IsError = isError;
      BugSenseResponseResult senseResponseResult = new BugSenseResponseResult();
      senseResponseResult.RequestType = isError ? BugSenseRequestType.Error : BugSenseRequestType.Event;
      senseResponseResult.HandledWhileDebugging = BugSenseProperties.HandleWhileDebugging;
      BugSenseResponseResult result = senseResponseResult;
      if (BugSenseProperties.HandleWhileDebugging)
      {
        if (NetworkInterface.GetIsNetworkAvailable())
        {
          string responseString = (string) null;
          try
          {
            url = string.IsNullOrWhiteSpace(BugSenseProperties.DebugTestUrl) ? url : BugSenseProperties.DebugTestUrl;
            using (HttpClientHandler handler = new HttpClientHandler())
            {
              HttpClient httpClient = new HttpClient((HttpMessageHandler) handler);
              StringContent dataStringContent = new StringContent(requestData);
              dataStringContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);
              HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url)
              {
                Content = (HttpContent) dataStringContent
              };
              request.Content.Headers.Add("X-BugSense-Api-Key", BugSenseProperties.APIKey);
              httpClient.DefaultRequestHeaders.Add("user-agent", BugSenseProperties.UserAgent);
              HttpResponseMessage response = await httpClient.SendAsync(request).ConfigureAwait(false);
              responseString = await response.Content.ReadAsStringAsync();
              if (response.StatusCode == HttpStatusCode.OK && response.IsSuccessStatusCode)
              {
                result.ResultState = BugSenseResultState.OK;
                if (isError)
                {
                  BugSenseErrorResponse errorResponse = await this.SaveErrorResponseId(result, responseString, requestData);
                  if (errorResponse != null)
                  {
                    if (!string.IsNullOrWhiteSpace(errorResponse.Error))
                    {
                      result.ResultState = BugSenseResultState.Error;
                      result.Description = errorResponse.Error;
                    }
                  }
                }
              }
              else
              {
                result.ResultState = BugSenseResultState.Error;
                result.Description = response.ReasonPhrase;
              }
            }
          }
          catch (Exception ex)
          {
            ConsoleManager.LogToConsole(string.Format("Network request failed: {0}", (object) ex));
            result.ResultState = BugSenseResultState.Error;
            result.ExceptionError = ex;
          }
          finally
          {
            result.ServerResponse = responseString;
          }
        }
        else
        {
          result.ResultState = BugSenseResultState.Error;
          result.Description = "No Internet Connection Available";
        }
      }
      else
      {
        result.ResultState = BugSenseResultState.OK;
        result.SetHandleWhileDebuggingExceptionError();
      }
      return result;
    }

    private async Task<BugSenseErrorResponse> SaveErrorResponseId(
      BugSenseResponseResult responseResult,
      string response,
      string content)
    {
      BugSenseErrorResponse errorResponse = (BugSenseErrorResponse) null;
      if (this.IsError)
      {
        try
        {
          errorResponse = response.DeserializeJson<BugSenseErrorResponse>();
          if (errorResponse != null)
          {
            if (errorResponse.Data != null)
            {
              if (!string.IsNullOrWhiteSpace(content))
              {
                string filePath = BugSenseProperties.GeneralFolderName + "\\" + BugSenseProperties.CrashOnLastRunFileName;
                FileRepository fileRepository = new FileRepository();
                string contents = await fileRepository.ReadAsync(filePath);
                CrashOnLastRun crashOnLastRun = !string.IsNullOrWhiteSpace(contents) ? contents.DeserializeJson<CrashOnLastRun>() : new CrashOnLastRun();
                crashOnLastRun.ErrorID = errorResponse.Data.Eid;
                ++crashOnLastRun.TotalCrashes;
                BugSenseLogResult bugSenseLogResult = await fileRepository.SaveAsync(filePath, crashOnLastRun.SerializeToJson<CrashOnLastRun>());
                responseResult.TickerText = errorResponse.Data.TickerText;
                responseResult.ContentText = errorResponse.Data.ContentText;
                responseResult.ContentTitle = errorResponse.Data.ContentTitle;
                responseResult.ErrorId = errorResponse.Data.Eid;
                responseResult.Url = errorResponse.Data.Url;
              }
            }
          }
        }
        catch (Exception ex)
        {
          ConsoleManager.LogToConsole(string.Format("Response deserialization failed: {0}", (object) ex));
        }
      }
      return errorResponse;
    }
  }
}
