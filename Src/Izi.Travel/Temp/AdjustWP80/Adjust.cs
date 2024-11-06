// Decompiled with JetBrains decompiler
// Type: AdjustSdk.Adjust
// Assembly: AdjustWP80, Version=3.5.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 184DAB71-F439-4FAD-8AD3-F5ADA24036D2
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\AdjustWP80.dll

using AdjustSdk.Pcl;
using System;
using System.Collections.Generic;

#nullable disable
namespace AdjustSdk
{
  public class Adjust
  {
    private static DeviceUtil Util = (DeviceUtil) new UtilWP80();

    public static void AppDidLaunch(string appToken)
    {
      AdjustApi.AppDidLaunch(appToken, Adjust.Util);
    }

    public static void AppDidActivate() => AdjustApi.AppDidActivate();

    public static void AppDidDeactivate() => AdjustApi.AppDidDeactivate();

    public static void TrackEvent(string eventToken, Dictionary<string, string> callbackParameters = null)
    {
      AdjustApi.TrackEvent(eventToken, callbackParameters);
    }

    public static void TrackRevenue(
      double amountInCents,
      string eventToken = null,
      Dictionary<string, string> callbackParameters = null)
    {
      AdjustApi.TrackRevenue(amountInCents, eventToken, callbackParameters);
    }

    public static void SetLogLevel(LogLevel logLevel) => AdjustApi.SetLogLevel(logLevel);

    public static void SetEnvironment(AdjustEnvironment environment)
    {
      AdjustApi.SetEnvironment(environment);
    }

    public static void SetEventBufferingEnabled(bool enabledEventBuffering)
    {
      AdjustApi.SetEventBufferingEnabled(enabledEventBuffering);
    }

    public static void SetResponseDelegate(Action<ResponseData> responseDelegate)
    {
      AdjustApi.SetResponseDelegate(responseDelegate);
    }

    public static void SetEnabled(bool enabled) => AdjustApi.SetEnabled(enabled);

    public static bool IsEnabled() => AdjustApi.IsEnabled();

    public static void AppWillOpenUrl(Uri url) => AdjustApi.AppWillOpenUrl(url);

    public static void SetSdkPrefix(string sdkPrefix) => AdjustApi.SetSdkPrefix(sdkPrefix);

    public static void SetLogDelegate(Action<string> logDelegate)
    {
      AdjustApi.SetLogDelegate(logDelegate);
    }
  }
}
