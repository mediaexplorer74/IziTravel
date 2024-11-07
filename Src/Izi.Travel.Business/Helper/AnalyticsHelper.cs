// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Helper.AnalyticsHelper
// Assembly: Izi.Travel.Business, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: ABF4D74A-55A9-49E1-BE11-CC83659F98DD
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.dll

using Caliburn.Micro;
using Izi.Travel.Business.Entities.Analytics;
using Izi.Travel.Business.Entities.Analytics.Events.Application;
using Izi.Travel.Business.Entities.Analytics.Events.Content;
using Izi.Travel.Business.Entities.Analytics.Parameters;
using Izi.Travel.Business.Entities.Analytics.Transaction;
using Izi.Travel.Business.Entities.Analytics.View;
using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Business.Entities.Media;
using Izi.Travel.Business.Entities.Purchase;
using Izi.Travel.Business.Managers;
using Izi.Travel.Business.Services;
using Izi.Travel.Utility;
using System;

#nullable disable
namespace Izi.Travel.Business.Helper
{
  public class AnalyticsHelper
  {
    private static readonly ILog Logger = LogManager.GetLog(typeof (AnalyticsHelper));

    public static void SendOpen(MtgObject mtgObject)
    {
      if (mtgObject == null)
        return;
      ServiceFacade.AnalyticsService.SendContentEvent((BaseContentEvent) AnalyticsHelper.CreateEventFromMtgObject<OpenEvent>(mtgObject));
      if (mtgObject.Type != MtgObjectType.Museum && mtgObject.Type != MtgObjectType.Tour)
        return;
      ServiceFacade.AnalyticsService.AdjustSendEvent("adzwjn");
    }

    public static void SetStartTour(MtgObject mtgObject)
    {
      if (mtgObject == null)
        return;
      ServiceFacade.AnalyticsService.AdjustSendEvent("84aelg");
    }

    public static void SendShare(MtgObject mtgObject)
    {
      if (mtgObject == null)
        return;
      ServiceFacade.AnalyticsService.SendContentEvent((BaseContentEvent) AnalyticsHelper.CreateEventFromMtgObject<ShareEvent>(mtgObject));
      ServiceFacade.AnalyticsService.AdjustSendEvent("dm79o");
    }

    public static void SendPlay() => ServiceFacade.AnalyticsService.AdjustSendEvent("4yrtsn");

    public static void SendPlayEnd(
      AudioTrackInfo trackInfo,
      CompletionReasonParameter mediaCompletionReason)
    {
      if (trackInfo == null)
        return;
      ActivationTypeParameter manual;
      if (!ActivationTypeParameter.TryParse(trackInfo.ActivationType, out manual))
        manual = ActivationTypeParameter.Manual;
      PlayEvent eventInfo = new PlayEvent(AnalyticsHelper.GetEventContentType(trackInfo.MtgObjectType));
      eventInfo.Uid = new UidParameter(trackInfo.MtgObjectUid);
      eventInfo.Title = new TitleParameter(trackInfo.Title);
      eventInfo.Language = new LanguageParameter(trackInfo.Language);
      eventInfo.AccessType = AnalyticsHelper.GetAccessType(trackInfo.MtgObjectAccessType);
      eventInfo.MediaActivationType = manual;
      eventInfo.MediaContentType = ContentTypeParameter.Audio;
      eventInfo.MediaCompletionReason = mediaCompletionReason;
      eventInfo.Rental = PurchaseManager.Instance.Contains(trackInfo.MtgParentUid) ? RentalParameter.Rental : RentalParameter.NonRental;
      ServiceFacade.AnalyticsService.SendContentEvent((BaseContentEvent) eventInfo);
      ServiceFacade.AnalyticsService.AdjustSendEvent("rzrb1m");
    }

    public static void SendReview(MtgObject mtgObject, string review, long rating)
    {
      ReviewEvent eventFromMtgObject = AnalyticsHelper.CreateEventFromMtgObject<ReviewEvent>(mtgObject);
      eventFromMtgObject.Label = review;
      eventFromMtgObject.Value = rating;
      ServiceFacade.AnalyticsService.SendContentEvent((BaseContentEvent) eventFromMtgObject);
      ServiceFacade.AnalyticsService.AdjustSendEvent("qbhh6a");
    }

    public static void SendSearch(string search)
    {
      ServiceFacade.AnalyticsService.SendApplicationEvent((BaseApplicationEvent) SearchEvent.Create(search));
    }

    public static void SendShareAndFollowUs(ShareAndFollowUsParameter parameter)
    {
      if (parameter == null || parameter == ShareAndFollowUsParameter.Empty)
        return;
      ServiceFacade.AnalyticsService.SendApplicationEvent((BaseApplicationEvent) ShareAndFollowUsEvent.Create(parameter));
    }

    public static void SendView(string viewName)
    {
      if (string.IsNullOrWhiteSpace(viewName))
        return;
      ServiceFacade.AnalyticsService.SendView(new ViewInfo(viewName));
    }

    public static void SendTransaction(MtgObject mtgObject, string receipt)
    {
      if (mtgObject == null || mtgObject.MainContent == null || mtgObject.Purchase == null || string.IsNullOrWhiteSpace(receipt))
        return;
      StoreReceipt storeReceipt = (StoreReceipt) null;
      try
      {
        storeReceipt = XmlSerializerHelper.Deserialize<StoreReceipt>(receipt);
      }
      catch (Exception ex)
      {
        AnalyticsHelper.Logger.Error(ex);
      }
      if (storeReceipt == null || storeReceipt.ProductReceipt == null)
        return;
      double price = (double) mtgObject.Purchase.Price;
      TransactionInfo transactionInfo = new TransactionInfo()
      {
        Id = storeReceipt.ProductReceipt.Id,
        TotalCost = price,
        TotalTax = 0.0,
        CurrencyCode = mtgObject.Purchase.Currency
      };
      transactionInfo.Items.Add(new TransactionItemInfo()
      {
        Sku = storeReceipt.ProductReceipt.ProductId,
        Name = mtgObject.MainContent.Title,
        Price = price,
        Quantity = 1L,
        CurrencyCode = mtgObject.Purchase.Currency
      });
      ServiceFacade.AnalyticsService.SendTransaction(transactionInfo);
      ServiceFacade.AnalyticsService.AdjustSendRevenue("201i85", price);
    }

    private static TEvent CreateEventFromMtgObject<TEvent>(MtgObject mtgObject) where TEvent : BaseContentEvent
    {
      if (mtgObject == null || mtgObject.Type == MtgObjectType.Unknown || mtgObject.MainContent == null)
        return default (TEvent);
      EventContentType eventContentType = AnalyticsHelper.GetEventContentType(mtgObject.Type);
      if (eventContentType == EventContentType.Empty)
        return default (TEvent);
      TEvent instance = (TEvent) Activator.CreateInstance(typeof (TEvent), (object) eventContentType);
      instance.Label = eventContentType.Value;
      instance.Uid = new UidParameter(mtgObject.Uid);
      instance.Title = new TitleParameter(mtgObject.MainContent.Title);
      instance.Language = new LanguageParameter(mtgObject.MainContent.Language);
      instance.AccessType = AnalyticsHelper.GetAccessType(mtgObject.AccessType);
      instance.Rental = PurchaseManager.Instance.Contains(mtgObject.Uid) ? RentalParameter.Rental : RentalParameter.NonRental;
      return instance;
    }

    private static EventContentType GetEventContentType(MtgObjectType mtgObjectType)
    {
      switch (mtgObjectType)
      {
        case MtgObjectType.Museum:
          return EventContentType.Museum;
        case MtgObjectType.Exhibit:
          return EventContentType.Exhibit;
        case MtgObjectType.StoryNavigation:
          return EventContentType.NavigationStory;
        case MtgObjectType.Tour:
          return EventContentType.Tour;
        case MtgObjectType.TouristAttraction:
          return EventContentType.TouristAttraction;
        case MtgObjectType.Collection:
          return EventContentType.Collection;
        default:
          return EventContentType.Empty;
      }
    }

    private static AccessTypeParameter GetAccessType(MtgObjectAccessType mtgObjectAccessType)
    {
      return mtgObjectAccessType == MtgObjectAccessType.Online || mtgObjectAccessType != MtgObjectAccessType.Offline ? AccessTypeParameter.Online : AccessTypeParameter.Offline;
    }
  }
}
