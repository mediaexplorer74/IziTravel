// Decompiled with JetBrains decompiler
// Type: GoogleAnalytics.Core.Tracker
// Assembly: GoogleAnalytics.Core, Version=1.2.11.25892, Culture=neutral, PublicKeyToken=null
// MVID: DA6701CD-FFEA-4833-995F-5D20607A09B2
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\GoogleAnalytics.Core.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace GoogleAnalytics.Core
{
  public sealed class Tracker
  {
    private readonly PayloadFactory engine;
    private readonly IPlatformInfoProvider platformInfoProvider;
    private readonly TokenBucket hitTokenBucket;
    private readonly IServiceManager serviceManager;
    private bool startSession;
    private bool endSession;

    public Tracker(
      string propertyId,
      IPlatformInfoProvider platformInfoProvider,
      IServiceManager serviceManager)
    {
      this.serviceManager = serviceManager;
      if (string.IsNullOrEmpty(serviceManager.UserAgent))
        serviceManager.UserAgent = platformInfoProvider.GetUserAgent();
      this.platformInfoProvider = platformInfoProvider;
      this.engine = new PayloadFactory()
      {
        PropertyId = propertyId,
        AnonymousClientId = platformInfoProvider.AnonymousClientId,
        ScreenColorDepthBits = platformInfoProvider.ScreenColorDepthBits,
        ScreenResolution = platformInfoProvider.ScreenResolution,
        UserLanguage = platformInfoProvider.UserLanguage,
        ViewportSize = platformInfoProvider.ViewPortResolution
      };
      platformInfoProvider.ViewPortResolutionChanged += new EventHandler(this.platformTrackingInfo_ViewPortResolutionChanged);
      platformInfoProvider.ScreenResolutionChanged += new EventHandler(this.platformTrackingInfo_ScreenResolutionChanged);
      this.SampleRate = 100f;
      this.hitTokenBucket = new TokenBucket(60.0, 0.5);
    }

    public void SetCustomDimension(int index, string value)
    {
      this.engine.CustomDimensions[index] = value;
    }

    public void SetCustomMetric(int index, long value) => this.engine.CustomMetrics[index] = value;

    private void platformTrackingInfo_ViewPortResolutionChanged(object sender, EventArgs args)
    {
      this.engine.ViewportSize = this.platformInfoProvider.ViewPortResolution;
    }

    private void platformTrackingInfo_ScreenResolutionChanged(object sender, EventArgs args)
    {
      this.engine.ScreenResolution = this.platformInfoProvider.ScreenResolution;
    }

    public string TrackingId => this.engine.PropertyId;

    public bool IsAnonymizeIpEnabled
    {
      get => this.engine.AnonymizeIP;
      set => this.engine.AnonymizeIP = value;
    }

    public string AppName
    {
      get => this.engine.AppName;
      set => this.engine.AppName = value;
    }

    public string AppVersion
    {
      get => this.engine.AppVersion;
      set => this.engine.AppVersion = value;
    }

    public string AppId
    {
      get => this.engine.AppId;
      set => this.engine.AppId = value;
    }

    public string AppInstallerId
    {
      get => this.engine.AppInstallerId;
      set => this.engine.AppInstallerId = value;
    }

    public string UserId
    {
      get => this.engine.UserId;
      set => this.engine.UserId = value;
    }

    public Dimensions AppScreen
    {
      get => this.engine.ViewportSize;
      set => this.engine.ViewportSize = value;
    }

    public string CampaignName
    {
      get => this.engine.CampaignName;
      set => this.engine.CampaignName = value;
    }

    public string CampaignSource
    {
      get => this.engine.CampaignSource;
      set => this.engine.CampaignSource = value;
    }

    public string CampaignMedium
    {
      get => this.engine.CampaignMedium;
      set => this.engine.CampaignMedium = value;
    }

    public string CampaignKeyword
    {
      get => this.engine.CampaignKeyword;
      set => this.engine.CampaignKeyword = value;
    }

    public string CampaignContent
    {
      get => this.engine.CampaignContent;
      set => this.engine.CampaignContent = value;
    }

    public string CampaignId
    {
      get => this.engine.CampaignId;
      set => this.engine.CampaignId = value;
    }

    public string Referrer
    {
      get => this.engine.Referrer;
      set => this.engine.Referrer = value;
    }

    public string DocumentEncoding
    {
      get => this.engine.DocumentEncoding;
      set => this.engine.DocumentEncoding = value;
    }

    public string GoogleAdWordsId
    {
      get => this.engine.GoogleAdWordsId;
      set => this.engine.GoogleAdWordsId = value;
    }

    public string GoogleDisplayAdsId
    {
      get => this.engine.GoogleDisplayAdsId;
      set => this.engine.GoogleDisplayAdsId = value;
    }

    public string IpOverride
    {
      get => this.engine.IpOverride;
      set => this.engine.IpOverride = value;
    }

    public string UserAgentOverride
    {
      get => this.engine.UserAgentOverride;
      set => this.engine.UserAgentOverride = value;
    }

    public string DocumentLocationUrl
    {
      get => this.engine.DocumentLocationUrl;
      set => this.engine.DocumentLocationUrl = value;
    }

    public string DocumentHostName
    {
      get => this.engine.DocumentHostName;
      set => this.engine.DocumentHostName = value;
    }

    public string DocumentPath
    {
      get => this.engine.DocumentPath;
      set => this.engine.DocumentPath = value;
    }

    public string DocumentTitle
    {
      get => this.engine.DocumentTitle;
      set => this.engine.DocumentTitle = value;
    }

    public string LinkId
    {
      get => this.engine.LinkId;
      set => this.engine.LinkId = value;
    }

    public string ExperimentId
    {
      get => this.engine.ExperimentId;
      set => this.engine.ExperimentId = value;
    }

    public string ExperimentVariant
    {
      get => this.engine.ExperimentVariant;
      set => this.engine.ExperimentVariant = value;
    }

    public float SampleRate { get; set; }

    public bool IsUseSecure { get; set; }

    public bool ThrottlingEnabled { get; set; }

    public void SendView(string screenName)
    {
      this.platformInfoProvider.OnTracking();
      this.SendPayload(this.engine.TrackView(screenName, this.SessionControl));
    }

    public void SendException(string description, bool isFatal)
    {
      this.platformInfoProvider.OnTracking();
      this.SendPayload(this.engine.TrackException(description, isFatal, this.SessionControl));
    }

    public void SendSocial(string network, string action, string target)
    {
      this.platformInfoProvider.OnTracking();
      this.SendPayload(this.engine.TrackSocialInteraction(network, action, target, this.SessionControl));
    }

    public void SendTiming(TimeSpan time, string category, string variable, string label)
    {
      this.platformInfoProvider.OnTracking();
      this.SendPayload(this.engine.TrackUserTiming(category, variable, new TimeSpan?(time), label, new TimeSpan?(), new TimeSpan?(), new TimeSpan?(), new TimeSpan?(), new TimeSpan?(), new TimeSpan?(), this.SessionControl));
    }

    public void SendEvent(string category, string action, string label, long value)
    {
      this.platformInfoProvider.OnTracking();
      this.SendPayload(this.engine.TrackEvent(category, action, label, value, this.SessionControl));
    }

    public void SendTransaction(Transaction transaction)
    {
      this.platformInfoProvider.OnTracking();
      foreach (Payload payload in this.TrackTransaction(transaction, this.SessionControl))
        this.SendPayload(payload);
    }

    public void SendTransactionItem(TransactionItem transactionItem)
    {
      this.platformInfoProvider.OnTracking();
      this.SendPayload(this.engine.TrackTransactionItem(transactionItem.TransactionId, transactionItem.Name, (double) transactionItem.PriceInMicros / 1000000.0, transactionItem.Quantity, transactionItem.SKU, transactionItem.Category, transactionItem.CurrencyCode, this.SessionControl));
    }

    private IEnumerable<Payload> TrackTransaction(
      Transaction transaction,
      SessionControl sessionControl = SessionControl.None,
      bool isNonInteractive = false)
    {
      yield return this.engine.TrackTransaction(transaction.TransactionId, transaction.Affiliation, (double) transaction.TotalCostInMicros / 1000000.0, (double) transaction.ShippingCostInMicros / 1000000.0, (double) transaction.TotalTaxInMicros / 1000000.0, transaction.CurrencyCode, sessionControl, isNonInteractive);
      foreach (TransactionItem transactionItem in (IEnumerable<TransactionItem>) transaction.Items)
        yield return this.engine.TrackTransactionItem(transaction.TransactionId, transactionItem.Name, (double) transactionItem.PriceInMicros / 1000000.0, transactionItem.Quantity, transactionItem.SKU, transactionItem.Category, transaction.CurrencyCode, sessionControl, isNonInteractive);
    }

    private SessionControl SessionControl
    {
      get
      {
        if (this.endSession)
        {
          this.endSession = false;
          return SessionControl.End;
        }
        if (!this.startSession)
          return SessionControl.None;
        this.startSession = false;
        return SessionControl.Start;
      }
    }

    public void SetStartSession(bool value) => this.startSession = value;

    public void SetEndSession(bool value) => this.endSession = value;

    private void SendPayload(Payload payload)
    {
      if (string.IsNullOrEmpty(this.TrackingId) || this.IsSampledOut() || this.ThrottlingEnabled && !this.hitTokenBucket.Consume())
        return;
      payload.IsUseSecure = this.IsUseSecure;
      this.serviceManager.SendPayload(payload);
    }

    private bool IsSampledOut()
    {
      if ((double) this.SampleRate <= 0.0)
        return true;
      if ((double) this.SampleRate >= 100.0)
        return false;
      string anonymousClientId = this.platformInfoProvider.AnonymousClientId;
      return anonymousClientId != null && (double) (Math.Abs(anonymousClientId.GetHashCode()) % 10000) >= (double) this.SampleRate * 100.0;
    }
  }
}
