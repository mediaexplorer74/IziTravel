// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Services.Implementation.AnalyticsService
// Assembly: Izi.Travel.Business, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: ABF4D74A-55A9-49E1-BE11-CC83659F98DD
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.dll

using AdjustSdk;
using Caliburn.Micro;
using GoogleAnalytics;
using GoogleAnalytics.Core;
using Izi.Travel.Business.Entities.Analytics;
using Izi.Travel.Business.Entities.Analytics.Events.Application;
using Izi.Travel.Business.Entities.Analytics.Events.Content;
using Izi.Travel.Business.Entities.Analytics.Parameters;
using Izi.Travel.Business.Entities.Analytics.Transaction;
using Izi.Travel.Business.Entities.Analytics.View;
using Izi.Travel.Business.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#nullable disable
namespace Izi.Travel.Business.Services.Implementation
{
  internal sealed class AnalyticsService : IAnalyticsService
  {
    private static readonly ILog Logger = LogManager.GetLog(typeof (AnalyticsService));
    private readonly ISettingsService _settingsService;
    private const string TrackingIdDeveloper = "UA-49540358-1";
    private const string TrackingIdBeta = "UA-38462614-1";
    private const string TrackingIdProduction = "UA-38461714-1";
    private const string AdjustApplicationToken = "mtagefwfqmwl";
    private const string AppName = "Izi.travel";

    public AnalyticsService(ISettingsService settingsService)
    {
      this._settingsService = settingsService;
    }

    public void GoogleAnalyticsLaunchApplication()
    {
      EasyTrackerConfig easyTrackerConfig = new EasyTrackerConfig()
      {
        AppName = "Izi.travel",
        AppVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString(),
        UserId = this._settingsService.GetAppSettings().UserUid
      };
      easyTrackerConfig.TrackingId = "UA-38461714-1";
      EasyTracker.Current.Config = easyTrackerConfig;
    }

    public void SendContentEvent(BaseContentEvent eventInfo)
    {
      if (eventInfo == null || eventInfo.ContentType == null)
        return;
      if (eventInfo.ContentType == EventContentType.Empty)
        return;
      try
      {
        AnalyticsService.SendContentEventInternal(EventCategory.Directory, eventInfo);
        AnalyticsService.SendContentEventInternal(EventCategory.Cms, eventInfo);
      }
      catch (Exception ex)
      {
        AnalyticsService.Logger.Error(ex);
      }
    }

    public void SendApplicationEvent(BaseApplicationEvent eventInfo)
    {
      if (eventInfo == null || string.IsNullOrWhiteSpace(eventInfo.Label))
        return;
      EasyTracker.GetTracker().SendEvent(EventCategory.Application.Value, eventInfo.Action.Value, eventInfo.Label, 0L);
    }

    public void SendView(ViewInfo viewInfo)
    {
      if (viewInfo == null || string.IsNullOrWhiteSpace(viewInfo.Name))
        return;
      EasyTracker.GetTracker().SendView(viewInfo.Name);
    }

    public void SendTransaction(TransactionInfo transactionInfo)
    {
      if (transactionInfo == null || transactionInfo.Items.Count == 0 || string.IsNullOrWhiteSpace(transactionInfo.Id))
        return;
      GoogleAnalytics.Core.Transaction transaction = new GoogleAnalytics.Core.Transaction()
      {
        TransactionId = transactionInfo.Id,
        Affiliation = "Windows Phone Store",
        TotalCostInMicros = (long) (transactionInfo.TotalCost * 1000000.0),
        TotalTaxInMicros = (long) (transactionInfo.TotalTax * 1000000.0),
        ShippingCostInMicros = 0,
        CurrencyCode = transactionInfo.CurrencyCode
      };
      foreach (TransactionItemInfo transactionItemInfo in (IEnumerable<TransactionItemInfo>) transactionInfo.Items)
        transaction.Items.Add(new TransactionItem()
        {
          TransactionId = transactionInfo.Id,
          Category = "Content",
          SKU = transactionItemInfo.Sku,
          Name = transactionItemInfo.Name,
          Quantity = transactionItemInfo.Quantity,
          PriceInMicros = (long) (transactionItemInfo.Price * 1000000.0),
          CurrencyCode = transactionItemInfo.CurrencyCode
        });
      EasyTracker.GetTracker().SendTransaction(transaction);
    }

    public void AdjustLaunchApplication()
    {
      try
      {
        Adjust.AppDidLaunch("mtagefwfqmwl");
        Adjust.SetLogLevel(LogLevel.Info);
        Adjust.SetEnvironment(AdjustEnvironment.Production);
        Adjust.SetLogDelegate((Action<string>) (x => AnalyticsService.Logger.Info("{0}", (object) x)));
      }
      catch (Exception ex)
      {
        AnalyticsService.Logger.Error(ex);
      }
    }

    public void AdjustSendEvent(string token)
    {
      if (string.IsNullOrWhiteSpace(token))
        return;
      try
      {
        Adjust.TrackEvent(token);
      }
      catch (Exception ex)
      {
        AnalyticsService.Logger.Error(ex);
      }
    }

    public void AdjustSendRevenue(string token, double amount)
    {
      if (string.IsNullOrWhiteSpace(token))
        return;
      try
      {
        Adjust.TrackRevenue(amount * 100.0, token);
      }
      catch (Exception ex)
      {
        AnalyticsService.Logger.Error(ex);
      }
    }

    private static void SendContentEventInternal(EventCategory category, BaseContentEvent eventInfo)
    {
      if (category == null || eventInfo == null)
        return;
      Tracker tracker = EasyTracker.GetTracker();
      string label;
      if (category == EventCategory.Directory)
      {
        label = eventInfo.Label;
        IEnumerable<BaseParameter> parameters = eventInfo.GetParameters();
        if (parameters != null)
        {
          foreach (BaseParameter baseParameter in (IEnumerable<BaseParameter>) parameters.Where<BaseParameter>((Func<BaseParameter, bool>) (x => x != null)).OrderBy<BaseParameter, int>((Func<BaseParameter, int>) (x => x.Index)))
            tracker.SetCustomDimension(baseParameter.Index, baseParameter.Value);
        }
      }
      else
      {
        string customLabel = eventInfo.GetCustomLabel();
        if (string.IsNullOrWhiteSpace(customLabel))
          return;
        label = customLabel;
      }
      tracker.SendEvent(category.Value, eventInfo.Action.Value, label, eventInfo.Value);
    }
  }
}
