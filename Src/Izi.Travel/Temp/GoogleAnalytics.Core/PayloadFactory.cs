// Decompiled with JetBrains decompiler
// Type: GoogleAnalytics.Core.PayloadFactory
// Assembly: GoogleAnalytics.Core, Version=1.2.11.25892, Culture=neutral, PublicKeyToken=null
// MVID: DA6701CD-FFEA-4833-995F-5D20607A09B2
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\GoogleAnalytics.Core.dll

using System;
using System.Collections.Generic;
using System.Globalization;

#nullable disable
namespace GoogleAnalytics.Core
{
  internal sealed class PayloadFactory
  {
    private const string HitType_Pageview = "screenview";
    private const string HitType_Event = "event";
    private const string HitType_Exception = "exception";
    private const string HitType_SocialNetworkInteraction = "social";
    private const string HitType_UserTiming = "timing";
    private const string HitType_Transaction = "transaction";
    private const string HitType_TransactionItem = "item";

    public string PropertyId { get; set; }

    public string AppName { get; set; }

    public string AppVersion { get; set; }

    public string AppId { get; set; }

    public string AppInstallerId { get; set; }

    public string UserId { get; set; }

    public bool AnonymizeIP { get; set; }

    public IDictionary<int, string> CustomDimensions { get; set; }

    public IDictionary<int, long> CustomMetrics { get; set; }

    public Dimensions ViewportSize { get; set; }

    public string ScreenName { get; set; }

    public string AnonymousClientId { get; set; }

    public Dimensions ScreenResolution { get; set; }

    public string UserLanguage { get; set; }

    public int? ScreenColorDepthBits { get; set; }

    public string CampaignName { get; set; }

    public string CampaignSource { get; set; }

    public string CampaignMedium { get; set; }

    public string CampaignKeyword { get; set; }

    public string CampaignContent { get; set; }

    public string CampaignId { get; set; }

    public string Referrer { get; set; }

    public string DocumentEncoding { get; set; }

    public string GoogleAdWordsId { get; set; }

    public string GoogleDisplayAdsId { get; set; }

    public string IpOverride { get; set; }

    public string UserAgentOverride { get; set; }

    public string DocumentLocationUrl { get; set; }

    public string DocumentHostName { get; set; }

    public string DocumentPath { get; set; }

    public string DocumentTitle { get; set; }

    public string LinkId { get; set; }

    public string ExperimentId { get; set; }

    public string ExperimentVariant { get; set; }

    public PayloadFactory()
    {
      this.CustomDimensions = (IDictionary<int, string>) new Dictionary<int, string>();
      this.CustomMetrics = (IDictionary<int, long>) new Dictionary<int, long>();
    }

    public Payload TrackView(
      string screenName,
      SessionControl sessionControl = SessionControl.None,
      bool isNonInteractive = false)
    {
      this.ScreenName = screenName;
      return this.PostData("screenview", (IDictionary<string, string>) null, isNonInteractive, sessionControl);
    }

    public Payload TrackEvent(
      string category,
      string action,
      string label,
      long value,
      SessionControl sessionControl = SessionControl.None,
      bool isNonInteractive = false)
    {
      Dictionary<string, string> additionalData = new Dictionary<string, string>();
      additionalData.Add("ec", category);
      additionalData.Add("ea", action);
      if (label != null)
        additionalData.Add("el", label);
      if (value != 0L)
        additionalData.Add("ev", value.ToString((IFormatProvider) CultureInfo.InvariantCulture));
      return this.PostData("event", (IDictionary<string, string>) additionalData, isNonInteractive, sessionControl);
    }

    public Payload TrackException(
      string description,
      bool isFatal,
      SessionControl sessionControl = SessionControl.None,
      bool isNonInteractive = false)
    {
      Dictionary<string, string> additionalData = new Dictionary<string, string>();
      if (description != null)
        additionalData.Add("exd", description);
      if (!isFatal)
        additionalData.Add("exf", "0");
      return this.PostData("exception", (IDictionary<string, string>) additionalData, isNonInteractive, sessionControl);
    }

    public Payload TrackSocialInteraction(
      string network,
      string action,
      string target,
      SessionControl sessionControl = SessionControl.None,
      bool isNonInteractive = false)
    {
      return this.PostData("social", (IDictionary<string, string>) new Dictionary<string, string>()
      {
        {
          "sn",
          network
        },
        {
          "sa",
          action
        },
        {
          "st",
          target
        }
      }, isNonInteractive, sessionControl);
    }

    public Payload TrackUserTiming(
      string category,
      string variable,
      TimeSpan? time,
      string label,
      TimeSpan? loadTime,
      TimeSpan? dnsTime,
      TimeSpan? downloadTime,
      TimeSpan? redirectResponseTime,
      TimeSpan? tcpConnectTime,
      TimeSpan? serverResponseTime,
      SessionControl sessionControl = SessionControl.None,
      bool isNonInteractive = false)
    {
      Dictionary<string, string> additionalData = new Dictionary<string, string>();
      if (category != null)
        additionalData.Add("utc", category);
      if (variable != null)
        additionalData.Add("utv", variable);
      double num;
      if (time.HasValue)
      {
        Dictionary<string, string> dictionary = additionalData;
        num = Math.Round(time.Value.TotalMilliseconds);
        string str = num.ToString((IFormatProvider) CultureInfo.InvariantCulture);
        dictionary.Add("utt", str);
      }
      if (label != null)
        additionalData.Add("utl", label);
      if (loadTime.HasValue)
      {
        Dictionary<string, string> dictionary = additionalData;
        num = Math.Round(loadTime.Value.TotalMilliseconds);
        string str = num.ToString((IFormatProvider) CultureInfo.InvariantCulture);
        dictionary.Add("ptl", str);
      }
      if (dnsTime.HasValue)
      {
        Dictionary<string, string> dictionary = additionalData;
        num = Math.Round(dnsTime.Value.TotalMilliseconds);
        string str = num.ToString((IFormatProvider) CultureInfo.InvariantCulture);
        dictionary.Add("dns", str);
      }
      if (downloadTime.HasValue)
      {
        Dictionary<string, string> dictionary = additionalData;
        num = Math.Round(downloadTime.Value.TotalMilliseconds);
        string str = num.ToString((IFormatProvider) CultureInfo.InvariantCulture);
        dictionary.Add("pdt", str);
      }
      if (redirectResponseTime.HasValue)
      {
        Dictionary<string, string> dictionary = additionalData;
        num = Math.Round(redirectResponseTime.Value.TotalMilliseconds);
        string str = num.ToString((IFormatProvider) CultureInfo.InvariantCulture);
        dictionary.Add("rrt", str);
      }
      if (tcpConnectTime.HasValue)
      {
        Dictionary<string, string> dictionary = additionalData;
        num = Math.Round(tcpConnectTime.Value.TotalMilliseconds);
        string str = num.ToString((IFormatProvider) CultureInfo.InvariantCulture);
        dictionary.Add("tcp", str);
      }
      if (serverResponseTime.HasValue)
      {
        Dictionary<string, string> dictionary = additionalData;
        num = Math.Round(serverResponseTime.Value.TotalMilliseconds);
        string str = num.ToString((IFormatProvider) CultureInfo.InvariantCulture);
        dictionary.Add("srt", str);
      }
      return this.PostData("timing", (IDictionary<string, string>) additionalData, isNonInteractive, sessionControl);
    }

    public Payload TrackTransaction(
      string id,
      string affiliation,
      double revenue,
      double shipping,
      double tax,
      string currencyCode,
      SessionControl sessionControl = SessionControl.None,
      bool isNonInteractive = false)
    {
      Dictionary<string, string> additionalData = new Dictionary<string, string>();
      additionalData.Add("ti", id);
      if (affiliation != null)
        additionalData.Add("ta", affiliation);
      if (revenue != 0.0)
        additionalData.Add("tr", revenue.ToString((IFormatProvider) CultureInfo.InvariantCulture));
      if (shipping != 0.0)
        additionalData.Add("ts", shipping.ToString((IFormatProvider) CultureInfo.InvariantCulture));
      if (tax != 0.0)
        additionalData.Add("tt", tax.ToString((IFormatProvider) CultureInfo.InvariantCulture));
      if (currencyCode != null)
        additionalData.Add("cu", currencyCode);
      return this.PostData("transaction", (IDictionary<string, string>) additionalData, isNonInteractive, sessionControl);
    }

    public Payload TrackTransactionItem(
      string transactionId,
      string name,
      double price,
      long quantity,
      string code,
      string category,
      string currencyCode,
      SessionControl sessionControl = SessionControl.None,
      bool isNonInteractive = false)
    {
      Dictionary<string, string> additionalData = new Dictionary<string, string>();
      additionalData.Add("ti", transactionId);
      if (name != null)
        additionalData.Add("in", name);
      if (price != 0.0)
        additionalData.Add("ip", price.ToString((IFormatProvider) CultureInfo.InvariantCulture));
      if (quantity != 0L)
        additionalData.Add("iq", quantity.ToString((IFormatProvider) CultureInfo.InvariantCulture));
      if (code != null)
        additionalData.Add("ic", code);
      if (category != null)
        additionalData.Add("iv", category);
      if (currencyCode != null)
        additionalData.Add("cu", currencyCode);
      return this.PostData("item", (IDictionary<string, string>) additionalData, isNonInteractive, sessionControl);
    }

    private Payload PostData(
      string hitType,
      IDictionary<string, string> additionalData,
      bool isNonInteractive,
      SessionControl sessionControl)
    {
      IDictionary<string, string> requiredPayloadData = this.GetRequiredPayloadData(hitType, isNonInteractive, sessionControl);
      if (additionalData != null)
      {
        foreach (KeyValuePair<string, string> keyValuePair in (IEnumerable<KeyValuePair<string, string>>) additionalData)
          requiredPayloadData.Add(keyValuePair);
      }
      return new Payload(requiredPayloadData);
    }

    private IDictionary<string, string> GetRequiredPayloadData(
      string hitType,
      bool isNonInteractive,
      SessionControl sessionControl)
    {
      Dictionary<string, string> requiredPayloadData = new Dictionary<string, string>();
      requiredPayloadData.Add("v", "1");
      requiredPayloadData.Add("tid", this.PropertyId);
      requiredPayloadData.Add("cid", this.AnonymousClientId);
      requiredPayloadData.Add("an", this.AppName);
      requiredPayloadData.Add("av", this.AppVersion);
      requiredPayloadData.Add("t", hitType);
      if (this.AppId != null)
        requiredPayloadData.Add("aid", this.AppId);
      if (this.AppInstallerId != null)
        requiredPayloadData.Add("aiid", this.AppInstallerId);
      if (this.UserId != null)
        requiredPayloadData.Add("uid", this.UserId);
      if (this.ScreenName != null)
        requiredPayloadData.Add("cd", this.ScreenName);
      if (isNonInteractive)
        requiredPayloadData.Add("ni", "1");
      if (this.AnonymizeIP)
        requiredPayloadData.Add("aip", "1");
      if (sessionControl != SessionControl.None)
        requiredPayloadData.Add("sc", sessionControl == SessionControl.Start ? "start" : "end");
      if (this.ScreenResolution != null)
        requiredPayloadData.Add("sr", string.Format("{0}x{1}", (object) this.ScreenResolution.Width, (object) this.ScreenResolution.Height));
      if (this.ViewportSize != null)
        requiredPayloadData.Add("vp", string.Format("{0}x{1}", (object) this.ViewportSize.Width, (object) this.ViewportSize.Height));
      if (this.UserLanguage != null)
        requiredPayloadData.Add("ul", this.UserLanguage);
      if (this.ScreenColorDepthBits.HasValue)
        requiredPayloadData.Add("sd", string.Format("{0}-bits", (object) this.ScreenColorDepthBits.Value));
      if (this.CampaignName != null)
        requiredPayloadData.Add("cn", this.CampaignName);
      if (this.CampaignSource != null)
        requiredPayloadData.Add("cs", this.CampaignSource);
      if (this.CampaignMedium != null)
        requiredPayloadData.Add("cm", this.CampaignMedium);
      if (this.CampaignKeyword != null)
        requiredPayloadData.Add("ck", this.CampaignKeyword);
      if (this.CampaignContent != null)
        requiredPayloadData.Add("cc", this.CampaignContent);
      if (this.CampaignId != null)
        requiredPayloadData.Add("ci", this.CampaignId);
      if (this.Referrer != null)
        requiredPayloadData.Add("dr", this.Referrer);
      if (this.DocumentEncoding != null)
        requiredPayloadData.Add("de", this.DocumentEncoding);
      if (this.GoogleAdWordsId != null)
        requiredPayloadData.Add("gclid", this.GoogleAdWordsId);
      if (this.GoogleDisplayAdsId != null)
        requiredPayloadData.Add("dclid", this.GoogleDisplayAdsId);
      if (this.IpOverride != null)
        requiredPayloadData.Add("uip", this.IpOverride);
      if (this.UserAgentOverride != null)
        requiredPayloadData.Add("ua", this.UserAgentOverride);
      if (this.DocumentLocationUrl != null)
        requiredPayloadData.Add("dl", this.DocumentLocationUrl);
      if (this.DocumentHostName != null)
        requiredPayloadData.Add("dh", this.DocumentHostName);
      if (this.DocumentPath != null)
        requiredPayloadData.Add("dp", this.DocumentPath);
      if (this.DocumentTitle != null)
        requiredPayloadData.Add("dt", this.DocumentTitle);
      if (this.LinkId != null)
        requiredPayloadData.Add("linkid", this.LinkId);
      if (this.ExperimentId != null)
        requiredPayloadData.Add("xid", this.ExperimentId);
      if (this.ExperimentVariant != null)
        requiredPayloadData.Add("xvar", this.ExperimentVariant);
      foreach (KeyValuePair<int, string> customDimension in (IEnumerable<KeyValuePair<int, string>>) this.CustomDimensions)
        requiredPayloadData.Add(string.Format("cd{0}", (object) customDimension.Key), customDimension.Value);
      foreach (KeyValuePair<int, long> customMetric in (IEnumerable<KeyValuePair<int, long>>) this.CustomMetrics)
        requiredPayloadData.Add(string.Format("cm{0}", (object) customMetric.Key), customMetric.Value.ToString((IFormatProvider) CultureInfo.InvariantCulture));
      this.CustomDimensions.Clear();
      this.CustomMetrics.Clear();
      return (IDictionary<string, string>) requiredPayloadData;
    }
  }
}
