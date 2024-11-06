// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Module
// Assembly: Izi.Travel.Business, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: ABF4D74A-55A9-49E1-BE11-CC83659F98DD
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.dll

using Caliburn.Micro;
using Izi.Travel.Business.Mapping.Entity;
using Izi.Travel.Business.Mapping.Enum;
using Izi.Travel.Business.Services.Contract;
using Izi.Travel.Business.Services.Implementation;
using Izi.Travel.Client;
using Izi.Travel.Data.Services.Contract;
using Izi.Travel.Data.Services.Implementation;

#nullable disable
namespace Izi.Travel.Business
{
  public static class Module
  {
    public static void ConfigureContainer(SimpleContainer container)
    {
      container.RegisterSingleton(typeof (ILocalDataService), (string) null, typeof (LocalDataService));
      container.RegisterSingleton(typeof (IDownloadDataService), (string) null, typeof (DownloadDataService));
      IziTravelClient implementation = new IziTravelClient("6d91c2ff-3993-4161-b72d-a42862c729d2")
      {
        ApiVersion = "1.2.5",
        Environment = IziTravelEnvironment.Production
      };
      container.RegisterInstance(typeof (IziTravelClient), (string) null, (object) implementation);
      container.Singleton<ContentSectionMapper>();
      container.Singleton<MediaTypeMapper>();
      container.Singleton<MtgObjectCategoryMapper>();
      container.Singleton<MtgObjectStatusMapper>();
      container.Singleton<MtgObjectTypeMapper>();
      container.Singleton<PlaybackTypeMapper>();
      container.Singleton<TriggerZoneTypeMapper>();
      container.Singleton<ImageExtensionMapper>();
      container.Singleton<ImageFormatMapper>();
      container.Singleton<ContentProviderMapper>();
      container.Singleton<LocationMapper>();
      container.Singleton<MapMapper>();
      container.Singleton<MediaMapper>();
      container.Singleton<MtgChildrenListResultCompactMapper>();
      container.Singleton<MtgChildrenListResultFullMapper>();
      container.Singleton<MtgChildrenListResultMetadataMapper>();
      container.Singleton<MtgObjectBookmarkMapper>();
      container.Singleton<MtgObjectCityCompactMapper>();
      container.Singleton<MtgObjectCompactMapper>();
      container.Singleton<MtgObjectContactsMapper>();
      container.Singleton<MtgObjectContentMapper>();
      container.Singleton<MtgObjectCountryCompactMapper>();
      container.Singleton<MtgObjectFeaturedContentMapper>();
      container.Singleton<MtgObjectFullMapper>();
      container.Singleton<MtgObjectHistoryMapper>();
      container.Singleton<MtgObjectPublisherCompactMapper>();
      container.Singleton<MtgObjectPublisherFullMapper>();
      container.Singleton<MtgObjectPurchaseMapper>();
      container.Singleton<PlaybackMapper>();
      container.Singleton<PublisherContactsMapper>();
      container.Singleton<PublisherContentMapper>();
      container.Singleton<PurchaseMapper>();
      container.Singleton<QuizAnswerMapper>();
      container.Singleton<QuizMapper>();
      container.Singleton<RatingMapper>();
      container.Singleton<ReviewMapper>();
      container.Singleton<ScheduleMapper>();
      container.Singleton<SponsorMapper>();
      container.Singleton<TriggerZoneMapper>();
      container.Singleton<QuizDataMapper>();
      container.Singleton<QuizDataStatisticsMapper>();
      container.RegisterSingleton(typeof (IAudioService), (string) null, typeof (AudioService));
      container.RegisterSingleton(typeof (ISettingsService), (string) null, typeof (SettingsService));
      container.RegisterSingleton(typeof (ICultureService), (string) null, typeof (CultureService));
      container.RegisterSingleton(typeof (IAnalyticsService), (string) null, typeof (AnalyticsService));
      container.RegisterSingleton(typeof (IMtgObjectService), (string) null, typeof (MtgObjectService));
      container.RegisterSingleton(typeof (IMtgObjectRegionService), (string) null, typeof (MtgObjectRegionService));
      container.RegisterSingleton(typeof (IMtgObjectDownloadService), (string) null, typeof (MtgObjectDownloadService));
      container.RegisterSingleton(typeof (IMediaService), (string) null, typeof (MediaService));
      container.RegisterSingleton(typeof (IQuizService), (string) null, typeof (QuizService));
    }

    public static void ConfigureLocalDatabase()
    {
      IoC.Get<ILocalDataService>().CreateOrUpdateDataBase();
    }
  }
}
